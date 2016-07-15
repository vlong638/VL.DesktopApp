using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using MinTools;
using System.Net;
using Network.Entity;

namespace Network
{
    /// <summary>
    /// 同步网络传输接收服务
    /// </summary>
    public class Receive
    {
        /// <summary>
        /// 接收状态事件
        /// </summary>
        public event EventHandler<ReceiveEventArgs> ReceiveStateEvent;
        /// <summary>
        /// TCP侦听连接
        /// </summary>
        TcpListener tcpListener;
        /// <summary>
        /// TCP连接
        /// </summary>
        TcpClient tcpClient;
        /// <summary>
        /// 监听线程
        /// </summary>
        TimerThread threadMonitor;
        /// <summary>
        /// 监听地址端口
        /// </summary>
        IPorPort iPorPort;

        /// <summary>
        /// 初始化网络传输接收服务
        /// </summary>
        /// <param name="iPorPort">监听对象IP—端口</param>
        /// <param name="fileSavePath">文件储存路径-此值为null则默认存储至程序当前运行目录下</param>
        public Receive(IPorPort iPorPort, string fileSavePath)
        {
            if (fileSavePath != null)
            {
                Config.FileSavePath = fileSavePath;
            }
            this.iPorPort = iPorPort;

            threadMonitor = new TimerThread();
            threadMonitor.Elapseds += threadMonitor_Elapseds;
            threadMonitor.Start();
        }

        /// <summary>
        /// 线程监听事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void threadMonitor_Elapseds(object sender, System.Timers.ElapsedEventArgs e)
        {
            tcpClient = new TcpClient();
            tcpListener = new TcpListener(iPorPort.IP, iPorPort.Port);                                //创建消息接收监听
            tcpListener.Start();                                                                      //开始侦听传入的连接请求
            byte[] buffer = new byte[Config.BufferSize];                                              //声明数据缓存并定义大小
            NetworkStream networkStream = null;                                                       //创建网络访问基础数据流
            if (ReceiveStateEvent != null)
                ReceiveStateEvent(this, new ReceiveEventArgs(ReceiveStates.MainListenerOver, ""));      //触发接收状态事件——开启主监听

            while (true)        //启动监听
            {
                try
                {
                    tcpClient = tcpListener.AcceptTcpClient();              //等待客户端连接
                }
                catch (Exception ex)
                {
                    if (ReceiveStateEvent != null)
                        ReceiveStateEvent(this, new ReceiveEventArgs(ErrorNo.ErrorListener, ex.Message));           //触发接收状态事件——监听开启错误 
                    tcpClient = null;
                }
                if (tcpClient != null)
                {
                    networkStream = tcpClient.GetStream();                  //接收网络数据流
                }
                else
                {
                    return;
                }

                byte[] dataFlow = new byte[networkStream.Read(buffer, 0, buffer.Length)];   //创建有效数据流
                Array.Copy(buffer, dataFlow, dataFlow.Length);                              //填充有效数据流

                int headerLength = GetDataLength(dataFlow);                                 //获得报头长度
                string header = Encoding.Unicode.GetString(dataFlow);                       //获得报头

                if (headerLength == (header.Length * 2) - Config.DataLength)                //校验报头长度
                {
                    List<TokenData> listTokenData = GetListTokenData(header);               //获得报头数据命令
                    int transmissionPort = NetTools.GetRandomAvailablePort(6000, 65535);    //获得数据传输新端口
                    DataReceiveInfo dataReceiveInfo = new DataReceiveInfo();                //初始化数据接收结构体
                    dataReceiveInfo.TokenData = listTokenData;                              //构建报头信息列表
                    dataReceiveInfo.iPorPort = new IPorPort(iPorPort.IP, transmissionPort);
                    if (ReceiveStateEvent != null)
                        ReceiveStateEvent(this, new ReceiveEventArgs(ReceiveStates.ReceiveHeaderOver, transmissionPort.ToString()));
                    byte[] dataPort = Encoding.Unicode.GetBytes(transmissionPort.ToString());       //byte化数据传输新端口
                    networkStream.Write(dataPort, 0, dataPort.Length);                              //写入流
                    ReceiveThread transmissionTimer = new ReceiveThread(dataReceiveInfo);           //声明数据接收线程
                    transmissionTimer.Elapseds += transmissionTimer_Elapseds;
                    transmissionTimer.Start();
                }
            }
        }

        /// <summary>
        /// 数据传输线程事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void transmissionTimer_Elapseds(object sender, System.Timers.ElapsedEventArgs e)
        {
            DataReceiveInfo dataReceiveInfo = ((ReceiveThread)sender).dataReceiveInfo;  //获得接收数据实体对象
            byte[] buffer = new byte[Config.BufferSize];                                //声明数据缓存并定义大小
            TcpListener tcpListener = null;

            try
            {
                tcpListener = new TcpListener(dataReceiveInfo.iPorPort.IP, dataReceiveInfo.iPorPort.Port);//创建消息接收监听
                tcpListener.Start();                                                                      //开始侦听传入的连接请求
            }
            catch (Exception ex)
            {
                if (ReceiveStateEvent != null)
                    ReceiveStateEvent(this, new ReceiveEventArgs(ErrorNo.ErrorListener, ex.Message));     //触发接收状态事件——数据接收链连接错误

                return;
            }
            TcpClient tcpClient;                                                                                //创建TCP连接
            NetworkStream networkStream;                                                                        //创建网络访问基础数据流
            if (ReceiveStateEvent != null)
                ReceiveStateEvent(this, new ReceiveEventArgs(ReceiveStates.SubListenerOver, dataReceiveInfo.iPorPort.Port.ToString()));       //触发接收状态事件——连接数据传输端口

            tcpClient = tcpListener.AcceptTcpClient();                      //等待客户端连接
            string subNO = dataReceiveInfo.TokenData[1].PatternData;        //获得总分包数
            byte[] dataFlow = new byte[0];                                  //创建剩余有效数据缓存
            List<byte[]> listSubpackageData = new List<byte[]>();           //创建数据包列表

            while (true)
            {
                try
                {
                    networkStream = tcpClient.GetStream();                                              //接收网络数据流
                    byte[] thisTimeBuffer = new byte[networkStream.Read(buffer, 0, buffer.Length)];     //创建本次有效数据缓存
                    Array.Copy(buffer, thisTimeBuffer, thisTimeBuffer.Length);                          //填充本次有效数据流
                    byte[] tempBuffer = dataFlow;                                                       //创建临时数据缓存
                    dataFlow = new byte[dataFlow.Length + thisTimeBuffer.Length];                       //扩充有效数据缓存
                    Array.Copy(tempBuffer, dataFlow, tempBuffer.Length);                                //填充上期剩余有效数据
                    Array.Copy(thisTimeBuffer, 0, dataFlow, tempBuffer.Length, thisTimeBuffer.Length);  //填充当期有效数据

                    int subpackageLength = GetDataLength(dataFlow);                                 //获得分包长度
                    int completesubpackageLength = subpackageLength + Config.SubpackageTopLength;   //获得完整包长

                    if (subpackageLength <= dataFlow.Length - Config.SubpackageTopLength)
                    {
                        string subpackage = Encoding.Unicode.GetString(dataFlow).Substring(Config.DataLength / 2, (dataFlow.Length - Config.DataLength) / 2);    //获得分包

                        string subpackageNO = subpackage.Substring(0, Config.SubpackageNOLength / 2);                           //获得分包编号
                        string subpackageMD5 = subpackage.Substring(Config.SubpackageNOLength / 2, Config.MD5Length / 2);       //获得MD5

                        byte[] subpackageData = new byte[subpackageLength];                                                     //声明实际数据缓存
                        Array.Copy(dataFlow, Config.SubpackageTopLength, subpackageData, 0, subpackageData.Length);             //获得实际数据


                        if (MD5Tools.VerifyMd5Hash(subpackageData, subpackageMD5))
                        {
                            byte[] cutBuffer = dataFlow;                                                                        //创建已处理数据临时缓存
                            dataFlow = new byte[dataFlow.Length - completesubpackageLength];                                    //缩减有效数据缓存
                            Array.Copy(cutBuffer, completesubpackageLength, dataFlow, 0, dataFlow.Length);                      //填充剩余有效数据
                            int percentage = (int)Math.Floor((double.Parse(subpackageNO) + 1.0) / double.Parse(subNO) * 100.00);//获得数据百分比
                            if (ReceiveStateEvent != null)
                                ReceiveStateEvent(this, new ReceiveEventArgs(ReceiveStates.ReceiveData, (int.Parse(subpackageNO) + 1).ToString()));     //触发接收状态事件——收到数据包

                            listSubpackageData.Add(subpackageData);                                         //添加分包数据
                        }
                        else
                        {
                            if (ReceiveStateEvent != null)
                                ReceiveStateEvent(this, new ReceiveEventArgs(ErrorNo.ErrorMD5, ""));     //触发接收状态事件——MD5验证错误
                            return;
                        }
                        if (int.Parse(subpackageNO) == int.Parse(dataReceiveInfo.TokenData[1].PatternData) - 1)
                        {
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (ReceiveStateEvent != null)
                        ReceiveStateEvent(this, new ReceiveEventArgs(ErrorNo.ErrorMD5, ""));        //触发接收状态事件——数据接收失败
                    return;
                }
            }
            if (ReceiveStateEvent != null)
                ReceiveStateEvent(this, new ReceiveEventArgs(ReceiveStates.ReceiveDataOver, ""));     //触发接收状态事件——数据接收完毕

            string message = "";
            for (int i = 0; i < listSubpackageData.Count; i++)
            {
                message += Encoding.Unicode.GetString(listSubpackageData[i]);
            }

            networkStream.Write(Config.dataOverOrderByte, 0, Config.dataOverOrderByte.Length);

            if (ReceiveStateEvent != null)
                ReceiveStateEvent(this, new ReceiveEventArgs(ReceiveStates.ReceiveMessageOver, tcpClient.Client.RemoteEndPoint.ToString().Split(':')[0] + ":" + message));     //触发接收状态事件——消息接收完毕

            networkStream.Close(10000);//延时10秒，防止过早退出导致发送端未能接收到数据接收完毕指令
            tcpListener.Stop();
            tcpClient.Close();
        }


        /// <summary>
        /// 数据长度获得
        /// </summary>
        /// <returns></returns>
        private static int GetDataLength(byte[] dataFlow)
        {
            byte[] byteHeaderLength = new byte[Config.DataLength];                  //声明长度byte[]
            Array.Copy(dataFlow, byteHeaderLength, byteHeaderLength.Length);        //获得长度byte[]
            string strHeaderLength = Encoding.Unicode.GetString(byteHeaderLength);  //获得长度string
            return int.Parse(strHeaderLength);                                      //返回长度int
        }

        /// <summary>
        /// 解析报头命令数据
        /// </summary>
        /// <param name="strHeader">报头字符串</param>
        /// <returns></returns>
        public static List<TokenData> GetListTokenData(string strHeader)
        {
            strHeader = strHeader.Substring(8);                             //去除报头长度
            string[] headers = strHeader.Split(Config.CharOrderInterval);   //还原命令
            List<TokenData> listTokenData = new List<TokenData>();          //建立报头命令数据列表
            for (int i = 0; i < headers.Length; i++)
            {
                TokenData tokenData = new TokenData();
                tokenData.PatternName = headers[i].Substring(0, headers[i].IndexOf(Config.CharBeWithoutEqual) + 1);
                tokenData.PatternData = headers[i].Substring(headers[i].IndexOf(Config.CharBeWithoutEqual) + 1);
                listTokenData.Add(tokenData);
            }
            return listTokenData;
        }
    }
}
