using MinTools;
using Network.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Network
{
    /// <summary>
    /// 数据发送类
    /// </summary>
    public class Send
    {
        /// <summary>
        /// 监听地址端口
        /// </summary>
        IPorPort iPorPort = null;

        /// <summary>
        /// 接收状态事件
        /// </summary>
        public event EventHandler<SendEventArgs> SendEvent;

        /// <summary>
        /// 初始化发送类
        /// </summary>
        /// <param name="receiveIP">接收端IP</param>
        /// <param name="receivePort">接收端端口</param>
        public Send(IPAddress receiveIP, int receivePort)
        {
            iPorPort = new IPorPort(receiveIP, receivePort);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns></returns>
        public void SendMessage(string message)
        {
            SendInfo sendInfo = new SendInfo()      //创建发送数据对象
            {
                Data = message,
                DataType = DataType.MessageData,
                AimIpPort = iPorPort
            };

            if (NetTools.Ping(iPorPort.IP, 10000) != 2)     //如能否ping通对方机器，开始异步发送消息
            {
                SendThread sendThread = new SendThread(sendInfo);
                sendThread.Elapseds += sendThread_Elapseds;
                sendThread.Start();
            }
            else
            {
                if (SendEvent != null)
                    SendEvent(this, new SendEventArgs(ErrorNo.ErrorLink, ""));  //触发发送结果事件——连接失败
            }
        }

        /// <summary>
        /// 发送线程触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void sendThread_Elapseds(object sender, System.Timers.ElapsedEventArgs e)
        {
            SendInfo sendInfo = ((SendThread)sender).sendInfo;                  //获得发送数据实体对象
            List<byte[]> ltBuffer = new List<byte[]>();                         //消息分包列表
            string subCount = "";                                               //分包数
            if (sendInfo.DataType == DataType.MessageData)                      //如果发送数据位消息类型
            {
                byte[] messageByte = Encoding.Unicode.GetBytes(sendInfo.Data);  //获得消息数据流
                ltBuffer = Truncation(messageByte);                             //获得数据分包列表
                subCount = ltBuffer.Count.ToString();                           //获得数据分包数
                if (SendEvent != null)
                    SendEvent(this, new SendEventArgs(SendStatus.SubOver, subCount));  //触发发送事件——数据处理完毕，回传分包数
            }

            #region 构建总报头 结构说明(*号为选填)：<报头长度><数据类型><数据分包数><*文件名*><*文件流长度*><*文件发送附参*>

            StringBuilder sbHeader = new StringBuilder(); //声明报头字符串

            sbHeader.Append(Config.TypeOrder + ((int)sendInfo.DataType).ToString() + Config.StrOrderInterval);  //构建数据类型
            sbHeader.Append(Config.CountOrder + subCount + Config.StrOrderInterval);                            //构建数据分包数
            sbHeader.Insert(0, (sbHeader.Length * 2).ToString("D8"));                     //构建报头长度
            string header = sbHeader.ToString();                                                                //获得报头
            #endregion

            #region 发送报头
            TcpClient tcpClient = new TcpClient();          //创建Tcp连接
            try
            {
                tcpClient.Connect(iPorPort.IP, iPorPort.Port);  //连接接收端
            }
            catch (Exception ex)            //建立连接失败
            {
                if (SendEvent != null)
                    SendEvent(this, new SendEventArgs(ErrorNo.ErrorLink, ""));  //触发发送事件——连接失败
                return;
            }
            NetworkStream networkStream = tcpClient.GetStream();                             //获得网络访问的基础数据流
            networkStream.Write(Encoding.Unicode.GetBytes(header), 0, header.Length * 2);    //发送报头

            if (tcpClient.Client.LocalEndPoint.ToString().Split(':')[0] == null)
            {
                if (SendEvent != null)
                    SendEvent(this, new SendEventArgs(ErrorNo.ErrorSendHeader, ""));  //触发发送事件——报头发送失败
                return;
            }

            byte[] buffer = new byte[Config.BufferSize];                                //声明数据缓存并定义大小
            byte[] dataFlow = new byte[networkStream.Read(buffer, 0, buffer.Length)];   //创建有效数据流
            Array.Copy(buffer, dataFlow, dataFlow.Length);                              //填充有效数据流
            int datePort = int.Parse(Encoding.Unicode.GetString(dataFlow));             //获得数据传输端口

            if (SendEvent != null)
                SendEvent(this, new SendEventArgs(SendStatus.HeaderOver, datePort.ToString()));                //回传数据传输端口
            networkStream.Close();
            tcpClient.Close();
            tcpClient = new TcpClient();
            #endregion

            #region 建立数据传输通道
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    tcpClient.Connect(iPorPort.IP, datePort);
                }
                catch (Exception ex)
                {
                    tcpClient = null;
                }
                if (tcpClient != null)
                {
                    break;
                }
                else
                {
                    if (i >= 9)
                    {
                        if (SendEvent != null)
                            SendEvent(this, new SendEventArgs(ErrorNo.ErrorLinkDate, ""));  //触发发送事件——连接数据传输端口失败
                        return;
                    }
                }
            }

            #endregion

            #region 发送数据
            networkStream = tcpClient.GetStream();          //获得网络访问的基础数据流

            for (int i = 0; i < ltBuffer.Count; i++)
            {
                if (sendInfo.DataType == DataType.MessageData)
                {
                    try
                    {
                        #region 构建分包头 <数据长度><分包编号><MD5>
                        string subHeader = ltBuffer[i].Length.ToString("D8");           //添加数据长度
                        subHeader += i.ToString("D5");                                  //添加分包编号
                        subHeader += MD5Tools.GetMd5Hash(ltBuffer[i]);                  //添加MD5
                        byte[] byteSubHeader = Encoding.Unicode.GetBytes(subHeader);    //获得分包头Byte数组
                        #endregion

                        byte[] subBuffer = new byte[byteSubHeader.Length + ltBuffer[i].Length];                 //创建数据分包
                        Array.Copy(byteSubHeader, subBuffer, byteSubHeader.Length);                             //填充分包头byte
                        Array.Copy(ltBuffer[i], 0, subBuffer, byteSubHeader.Length, ltBuffer[i].Length);        //填充分包数据
                        networkStream.Write(subBuffer, 0, subBuffer.Length);                                    //发送分包

                        if (SendEvent != null)
                            SendEvent(this, new SendEventArgs(SendStatus.SendSub, (i + 1).ToString()));         //触发发送事件——发送分包
                    }
                    catch (Exception ex)
                    {
                        if (SendEvent != null)
                            SendEvent(this, new SendEventArgs(ErrorNo.ErrorLink, ex.Message));                  //触发发送事件——远程连接丢失
                        return;
                    }
                }
            }

            for (int i = 0; i < Config.TimeOut; i++)//等待数据发送完毕指令
            {
                try
                {
                    Thread.Sleep(1000);
                    dataFlow = new byte[networkStream.Read(buffer, 0, buffer.Length)];       //创建有效数据流
                    Array.Copy(buffer, dataFlow, dataFlow.Length);                           //填充有效数据流
                    string order = Encoding.Unicode.GetString(dataFlow);                     //获得命令
                    if (order == Config.DataOverOrder)
                    {
                        networkStream.Close();
                        tcpClient.Close();
                        if (SendEvent != null)
                            SendEvent(this, new SendEventArgs(SendStatus.SendOver, ""));//触发发送事件——数据接收完毕
                        return;
                    }
                }
                catch (Exception ex)
                {
                    if (SendEvent != null)
                        SendEvent(this, new SendEventArgs(ErrorNo.ErrorSendOver, ""));                  //触发发送事件——获取数据发送完毕指令失败
                    return;
                }
            }

            networkStream.Close();
            tcpClient.Close();

            if (SendEvent != null)
                SendEvent(this, new SendEventArgs(ErrorNo.ErrorSendOverTimerOut, ""));                  //触发发送事件——数据接收确认超时


            #endregion

        }



        /// <summary>
        /// 构建数据分割列表
        /// </summary>
        /// <param name="dataByte">待分包数据</param>
        /// <returns>数据列表</returns>
        private static List<byte[]> Truncation(byte[] dataByte)
        {
            List<byte[]> ltbuffer = new List<byte[]>();

            for (int i = 0; i < dataByte.Length; i += Config.Payload)
            {
                if (dataByte.Length - i >= Config.Payload)//如果未发数据长度大于等于有效载荷则构建完全包
                {
                    byte[] dataTemp = new byte[Config.Payload];
                    Array.Copy(dataByte, i, dataTemp, 0, dataTemp.Length);  //添加有效数据
                    ltbuffer.Add(dataTemp);
                }
                else                               //如果未发数据长度小于有效载荷则构建精简包
                {
                    byte[] dataTemp = new byte[dataByte.Length - i];
                    Array.Copy(dataByte, i, dataTemp, 0, dataTemp.Length);  //添加有效数据
                    ltbuffer.Add(dataTemp);
                }
            }
            return ltbuffer;
        }

        /// <summary>
        /// 获得命令数据
        /// </summary>
        /// <param name="strHeader">报头字符串</param>
        /// <returns></returns>
        private static TokenData GetTokenData(string strHeader)
        {
            string[] headers = strHeader.Split(Config.CharOrderInterval); //还原命令
            TokenData tokenData = new TokenData();
            tokenData.PatternName = headers[0].Substring(0, headers[0].IndexOf(Config.CharBeWithoutEqual) + 1);
            tokenData.PatternData = headers[0].Substring(headers[0].IndexOf(Config.CharBeWithoutEqual) + 1);
            return tokenData;
        }
    }
}
