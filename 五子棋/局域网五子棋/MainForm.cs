using MinTools;
using Network;
using Network.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Gobang
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// 窗体初始化事件
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 数据发送类
        /// </summary>
        Send send = null;
        /// <summary>
        /// 网络对仗开关
        /// </summary>
        bool netAS = false;
        /// <summary>
        /// 网络对仗自身执棋颜色
        /// </summary>
        PColor MyPColor;
        /// <summary>
        /// 正在发送数据
        /// </summary>
        string SendData = "";

        /// <summary>
        /// 对仗开关依赖字段
        /// </summary>
        bool aS = false;
        /// <summary>
        /// 对仗开关
        /// </summary>
        bool AS
        {
            get
            {
                return aS;
            }
            set
            {
                aS = lbTheHand.Visible = value;
                if (value == true)
                    btnDouble.Enabled = btnNetwork.Enabled = false;
                else
                {
                    btnDouble.Enabled = btnNetwork.Enabled = true;
                }
            }
        }

        /// <summary>
        /// 颜色属性依赖字段
        /// </summary>
        PColor theHand;
        /// <summary>
        /// 当前执手颜色
        /// </summary>
        PColor TheHand
        {
            get
            {
                return theHand;//返回棋子颜色
            }
            set
            {
                if (value == PColor.Black)
                {
                    lbTheHand.Text = "当前手：黑";
                }
                else
                {
                    lbTheHand.Text = "当前手：白";
                }
                theHand = value;
            }
        }

        /// <summary>
        /// 双人对仗按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDouble_Click(object sender, EventArgs e)
        {
            if (btnDouble.Text == "双人对仗")
            {
                TheHand = PColor.Black;
                AS = true;
                btnDouble.Enabled = true;
                btnDouble.Text = "停止";
            }
            else
            {
                AS = false;
                btnDouble.Text = "双人对仗";
                panCheckerboard.Visible = false;
                panCheckerboard.Controls.Clear();   //清空棋盘
                panCheckerboard.Visible = true;
                GC.Collect();                       //回收垃圾
            }
        }

        /// <summary>
        /// 棋盘容器点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_Click(object sender, EventArgs e)
        {
            if (!AS) { return; }    //如果对仗开关关闭
            if (netAS)              //如果网络对仗开关打开
            {
                if (TheHand != MyPColor)
                    return;
            }

            Point pcMenuPoint = panCheckerboard.PointToClient(Control.MousePosition);       //获得鼠标相对于panCheckerboard左上角的坐标

            int latticeXNo = (pcMenuPoint.X + Config.xOffset) / Config.xConst;    //计算X行编号
            int latticeYNo = (pcMenuPoint.Y + Config.yOffset) / Config.yConst;    //计算Y行编号

            if (latticeXNo == 0 || latticeYNo == 0 || latticeXNo > 17 || latticeYNo > 17)   //棋子放置位判断
            {
                return;
            }

            Point piePoint = new Point(latticeXNo, latticeYNo);   //创建棋子坐标

            if (panCheckerboard.Controls.Cast<Piece>().Where(p => p.Coordinate == piePoint).Count() > 0) return;    //有子检测

            Piece pie = new Piece(TheHand, piePoint);       //创建棋子

            if (netAS)
            {
                SendData = latticeXNo.ToString() + "," + latticeYNo.ToString();
                send.SendMessage(SendData);                 //发送棋子坐标
            }

            panCheckerboard.Controls.Add(pie);              //棋盘容器添加控件
            if (Outcome(pie))                               //检测胜负
            {
                AS = false;
                if (netAS)
                {
                    netAS = false;
                    btnNetwork.Text = "联网对仗";
                    panCheckerboard.Visible = false;
                    panCheckerboard.Controls.Clear();   //清空棋盘
                    panCheckerboard.Visible = true;
                    GC.Collect();                       //回收垃圾
                }
                else
                {
                    btnDouble.Enabled = false;
                    btnDouble_Click(this, new EventArgs());
                }
                MessageBox.Show(lbTheHand.Text + "方胜！");
            }

            if (TheHand == PColor.Black)        //执行换手
            {
                TheHand = PColor.White;
            }
            else
            {
                TheHand = PColor.Black;
            }
        }

        /// <summary>
        /// 联网对仗按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNetwork_Click(object sender, EventArgs e)
        {
            if (btnNetwork.Text == "联网对仗")
            {
                LinkForm lf = new LinkForm();
                if (lf.ShowDialog() == DialogResult.OK)
                {
                    MyPColor = PColor.White;
                    TheHand = PColor.Black;
                    send = new Send(Config.ip, Config.port);
                    send.SendEvent += send_SendEvent;
                    SendData = "邀请";
                    send.SendMessage(SendData);
                }
            }
            else
            {
                SendData = "停止";
                send.SendMessage(SendData);
                AS = false;
                netAS = false;
                btnNetwork.Text = "联网对仗";
                panCheckerboard.Visible = false;
                panCheckerboard.Controls.Clear();   //清空棋盘
                panCheckerboard.Visible = true;
                GC.Collect();                       //回收垃圾
            }
        }

        /// <summary>
        /// 胜负检测
        /// </summary>
        /// <returns>true胜/false负</returns>
        private bool Outcome(Piece pie)
        {
            #region 检查落子处横向是否五连

            if (panCheckerboard.Controls.Cast<Piece>().Where(p => (p.Coordinate.X == pie.Coordinate.X + 4 || p.Coordinate.X == pie.Coordinate.X + 3 || p.Coordinate.X == pie.Coordinate.X + 2 || p.Coordinate.X == pie.Coordinate.X + 1) && p.Color == pie.Color && p.Coordinate.Y == pie.Coordinate.Y).Count() >= 4)
                return true;
            if (panCheckerboard.Controls.Cast<Piece>().Where(p => (p.Coordinate.X == pie.Coordinate.X + 3 || p.Coordinate.X == pie.Coordinate.X + 2 || p.Coordinate.X == pie.Coordinate.X + 1 || p.Coordinate.X == pie.Coordinate.X - 1) && p.Color == pie.Color && p.Coordinate.Y == pie.Coordinate.Y).Count() >= 4)
                return true;
            if (panCheckerboard.Controls.Cast<Piece>().Where(p => (p.Coordinate.X == pie.Coordinate.X + 2 || p.Coordinate.X == pie.Coordinate.X + 1 || p.Coordinate.X == pie.Coordinate.X - 1 || p.Coordinate.X == pie.Coordinate.X - 2) && p.Color == pie.Color && p.Coordinate.Y == pie.Coordinate.Y).Count() >= 4)
                return true;
            if (panCheckerboard.Controls.Cast<Piece>().Where(p => (p.Coordinate.X == pie.Coordinate.X + 1 || p.Coordinate.X == pie.Coordinate.X - 1 || p.Coordinate.X == pie.Coordinate.X - 2 || p.Coordinate.X == pie.Coordinate.X - 3) && p.Color == pie.Color && p.Coordinate.Y == pie.Coordinate.Y).Count() >= 4)
                return true;
            if (panCheckerboard.Controls.Cast<Piece>().Where(p => (p.Coordinate.X == pie.Coordinate.X - 1 || p.Coordinate.X == pie.Coordinate.X - 2 || p.Coordinate.X == pie.Coordinate.X - 3 || p.Coordinate.X == pie.Coordinate.X - 4) && p.Color == pie.Color && p.Coordinate.Y == pie.Coordinate.Y).Count() >= 4)
                return true;

            #endregion

            #region 检查落子处竖向是否五连

            if (panCheckerboard.Controls.Cast<Piece>().Where(p => (p.Coordinate.Y == pie.Coordinate.Y + 4 || p.Coordinate.Y == pie.Coordinate.Y + 3 || p.Coordinate.Y == pie.Coordinate.Y + 2 || p.Coordinate.Y == pie.Coordinate.Y + 1) && p.Color == pie.Color && p.Coordinate.X == pie.Coordinate.X).Count() >= 4)
                return true;
            if (panCheckerboard.Controls.Cast<Piece>().Where(p => (p.Coordinate.Y == pie.Coordinate.Y + 3 || p.Coordinate.Y == pie.Coordinate.Y + 2 || p.Coordinate.Y == pie.Coordinate.Y + 1 || p.Coordinate.Y == pie.Coordinate.Y - 1) && p.Color == pie.Color && p.Coordinate.X == pie.Coordinate.X).Count() >= 4)
                return true;
            if (panCheckerboard.Controls.Cast<Piece>().Where(p => (p.Coordinate.Y == pie.Coordinate.Y + 2 || p.Coordinate.Y == pie.Coordinate.Y + 1 || p.Coordinate.Y == pie.Coordinate.Y - 1 || p.Coordinate.Y == pie.Coordinate.Y - 2) && p.Color == pie.Color && p.Coordinate.X == pie.Coordinate.X).Count() >= 4)
                return true;
            if (panCheckerboard.Controls.Cast<Piece>().Where(p => (p.Coordinate.Y == pie.Coordinate.Y + 1 || p.Coordinate.Y == pie.Coordinate.Y - 1 || p.Coordinate.Y == pie.Coordinate.Y - 2 || p.Coordinate.Y == pie.Coordinate.Y - 3) && p.Color == pie.Color && p.Coordinate.X == pie.Coordinate.X).Count() >= 4)
                return true;
            if (panCheckerboard.Controls.Cast<Piece>().Where(p => (p.Coordinate.Y == pie.Coordinate.Y - 1 || p.Coordinate.Y == pie.Coordinate.Y - 2 || p.Coordinate.Y == pie.Coordinate.Y - 3 || p.Coordinate.Y == pie.Coordinate.Y - 4) && p.Color == pie.Color && p.Coordinate.X == pie.Coordinate.X).Count() >= 4)
                return true;

            #endregion

            #region 检查落子处左斜是否五连

            if (panCheckerboard.Controls.Cast<Piece>().Where(p => (p.Coordinate.Y == pie.Coordinate.Y - 1 && p.Coordinate.X == pie.Coordinate.X - 1) || (p.Coordinate.Y == pie.Coordinate.Y - 2 && p.Coordinate.X == pie.Coordinate.X - 2) || (p.Coordinate.Y == pie.Coordinate.Y - 3 && p.Coordinate.X == pie.Coordinate.X - 3) || (p.Coordinate.Y == pie.Coordinate.Y - 4 && p.Coordinate.X == pie.Coordinate.X - 4) && p.Color == pie.Color).Count() >= 4)
                return true;
            if (panCheckerboard.Controls.Cast<Piece>().Where(p => (p.Coordinate.Y == pie.Coordinate.Y - 2 && p.Coordinate.X == pie.Coordinate.X - 2) || (p.Coordinate.Y == pie.Coordinate.Y - 3 && p.Coordinate.X == pie.Coordinate.X - 3) || (p.Coordinate.Y == pie.Coordinate.Y - 4 && p.Coordinate.X == pie.Coordinate.X - 4) || (p.Coordinate.Y == pie.Coordinate.Y + 1 && p.Coordinate.X == pie.Coordinate.X + 1) && p.Color == pie.Color).Count() >= 4)
                return true;
            if (panCheckerboard.Controls.Cast<Piece>().Where(p => (p.Coordinate.Y == pie.Coordinate.Y - 3 && p.Coordinate.X == pie.Coordinate.X - 3) || (p.Coordinate.Y == pie.Coordinate.Y - 4 && p.Coordinate.X == pie.Coordinate.X - 4) || (p.Coordinate.Y == pie.Coordinate.Y + 1 && p.Coordinate.X == pie.Coordinate.X + 1) || (p.Coordinate.Y == pie.Coordinate.Y + 2 && p.Coordinate.X == pie.Coordinate.X + 2) && p.Color == pie.Color).Count() >= 4)
                return true;
            if (panCheckerboard.Controls.Cast<Piece>().Where(p => (p.Coordinate.Y == pie.Coordinate.Y - 4 && p.Coordinate.X == pie.Coordinate.X - 4) || (p.Coordinate.Y == pie.Coordinate.Y + 1 && p.Coordinate.X == pie.Coordinate.X + 1) || (p.Coordinate.Y == pie.Coordinate.Y + 2 && p.Coordinate.X == pie.Coordinate.X + 2) || (p.Coordinate.Y == pie.Coordinate.Y + 3 && p.Coordinate.X == pie.Coordinate.X + 3) && p.Color == pie.Color).Count() >= 4)
                return true;
            if (panCheckerboard.Controls.Cast<Piece>().Where(p => (p.Coordinate.Y == pie.Coordinate.Y + 1 && p.Coordinate.X == pie.Coordinate.X + 1) || (p.Coordinate.Y == pie.Coordinate.Y + 2 && p.Coordinate.X == pie.Coordinate.X + 2) || (p.Coordinate.Y == pie.Coordinate.Y + 3 && p.Coordinate.X == pie.Coordinate.X + 3) || (p.Coordinate.Y == pie.Coordinate.Y + 4 && p.Coordinate.X == pie.Coordinate.X + 4) && p.Color == pie.Color).Count() >= 4)
                return true;

            #endregion

            #region 检查落子处右斜是否五连

            if (panCheckerboard.Controls.Cast<Piece>().Where(p => (p.Coordinate.Y == pie.Coordinate.Y - 1 && p.Coordinate.X == pie.Coordinate.X + 1) || (p.Coordinate.Y == pie.Coordinate.Y - 2 && p.Coordinate.X == pie.Coordinate.X + 2) || (p.Coordinate.Y == pie.Coordinate.Y - 3 && p.Coordinate.X == pie.Coordinate.X + 3) || (p.Coordinate.Y == pie.Coordinate.Y - 4 && p.Coordinate.X == pie.Coordinate.X + 4) && p.Color == pie.Color).Count() >= 4)
                return true;
            if (panCheckerboard.Controls.Cast<Piece>().Where(p => (p.Coordinate.Y == pie.Coordinate.Y - 1 && p.Coordinate.X == pie.Coordinate.X + 1) || (p.Coordinate.Y == pie.Coordinate.Y - 2 && p.Coordinate.X == pie.Coordinate.X + 2) || (p.Coordinate.Y == pie.Coordinate.Y - 3 && p.Coordinate.X == pie.Coordinate.X + 3) || (p.Coordinate.Y == pie.Coordinate.Y + 1 && p.Coordinate.X == pie.Coordinate.X - 1) && p.Color == pie.Color).Count() >= 4)
                return true;
            if (panCheckerboard.Controls.Cast<Piece>().Where(p => (p.Coordinate.Y == pie.Coordinate.Y - 1 && p.Coordinate.X == pie.Coordinate.X + 1) || (p.Coordinate.Y == pie.Coordinate.Y - 2 && p.Coordinate.X == pie.Coordinate.X + 2) || (p.Coordinate.Y == pie.Coordinate.Y + 1 && p.Coordinate.X == pie.Coordinate.X - 1) || (p.Coordinate.Y == pie.Coordinate.Y + 2 && p.Coordinate.X == pie.Coordinate.X - 2) && p.Color == pie.Color).Count() >= 4)
                return true;
            if (panCheckerboard.Controls.Cast<Piece>().Where(p => (p.Coordinate.Y == pie.Coordinate.Y - 1 && p.Coordinate.X == pie.Coordinate.X + 1) || (p.Coordinate.Y == pie.Coordinate.Y + 1 && p.Coordinate.X == pie.Coordinate.X - 1) || (p.Coordinate.Y == pie.Coordinate.Y + 2 && p.Coordinate.X == pie.Coordinate.X - 2) || (p.Coordinate.Y == pie.Coordinate.Y + 3 && p.Coordinate.X == pie.Coordinate.X - 3) && p.Color == pie.Color).Count() >= 4)
                return true;
            if (panCheckerboard.Controls.Cast<Piece>().Where(p => (p.Coordinate.Y == pie.Coordinate.Y + 1 && p.Coordinate.X == pie.Coordinate.X - 1) || (p.Coordinate.Y == pie.Coordinate.Y + 2 && p.Coordinate.X == pie.Coordinate.X - 2) || (p.Coordinate.Y == pie.Coordinate.Y + 3 && p.Coordinate.X == pie.Coordinate.X - 3) || (p.Coordinate.Y == pie.Coordinate.Y + 4 && p.Coordinate.X == pie.Coordinate.X - 4) && p.Color == pie.Color).Count() >= 4)
                return true;

            #endregion

            return false;
        }

        /// <summary>
        /// 主窗体启动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            //List<IPAddress> lip = NetTools.GetLocalIPv4();

            LocalIPForm lf = new LocalIPForm();
            if (lf.ShowDialog() == DialogResult.OK)
            {
                IPorPort ipp = new IPorPort(Config.LocalIP, Config.port); //IPAddress.Parse("127.0.0.1")lip[0]
                Receive receive = new Receive(ipp, "");
                receive.ReceiveStateEvent += receive_ReceiveStateEvent;
            }
            else
            {
                btnNetwork.Visible = false;
            }

            //TimerThread tt = new TimerThread(1000, TimerThread.AutoResetTypeEnum.TimeInterval);
            //tt.Elapseds += tt_Elapseds;
            //tt.Start();
        }

        /// <summary>
        /// 接收状态事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void receive_ReceiveStateEvent(object sender, ReceiveEventArgs e)
        {
            if (e.receiveStates == ReceiveStates.ReceiveMessageOver)
            {
                string[] ms = e.arg.Split(':');
                if (ms[1] == "邀请")
                {
                    if (MessageBox.Show("收到来自：" + ms[0] + "的对战邀请，是否接受？", "对仗邀请", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (send == null)
                        {
                            send = new Send(IPAddress.Parse(ms[0]), Config.port);
                            send.SendEvent += send_SendEvent;
                        }
                        SendData = "接受邀请";
                        send.SendMessage(SendData);
                        MyPColor = PColor.Black;
                        StartNetAS();
                    }
                }
                else if (ms[1] == "接受邀请")
                {
                    StartNetAS();
                }
                else if (ms[1] == "停止")
                {
                    Invoke(new Action(delegate      //合并线程
                    {
                        AS = false;
                        netAS = false;
                        btnNetwork.Text = "联网对仗";
                        panCheckerboard.Visible = false;
                        panCheckerboard.Controls.Clear();   //清空棋盘
                        panCheckerboard.Visible = true;
                        GC.Collect();                       //回收垃圾
                    }));
                }
                else
                {
                    string[] sp = ms[1].Split(',');
                    Point piePoint = new Point(int.Parse(sp[0]), int.Parse(sp[1]));     //创建棋子坐标
                    Piece pie = new Piece(TheHand, piePoint);               //创建棋子

                    Invoke(new Action(delegate      //合并线程
                    {
                        panCheckerboard.Controls.Add(pie);  //棋盘容器添加控件
                        if (Outcome(pie))                               //检测胜负
                        {
                            AS = false;
                            MessageBox.Show(lbTheHand.Text + "方胜！");
                            netAS = false;
                            btnNetwork.Text = "联网对仗";
                            panCheckerboard.Visible = false;
                            panCheckerboard.Controls.Clear();   //清空棋盘
                            panCheckerboard.Visible = true;
                            GC.Collect();                       //回收垃圾
                            return;
                        }

                        if (TheHand == PColor.Black)        //执行换手
                        {
                            TheHand = PColor.White;
                        }
                        else
                        {
                            TheHand = PColor.Black;
                        }
                    }));
                }
            }
        }

        /// <summary>
        /// 开启网络对仗
        /// </summary>
        private void StartNetAS()
        {
            Invoke(new Action(delegate      //合并线程
            {
                TheHand = PColor.Black;
                AS = true;
                btnNetwork.Enabled = true;
                btnNetwork.Text = "停止";
            }));
            netAS = true;
        }

        /// <summary>
        /// 发送状态事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void send_SendEvent(object sender, SendEventArgs e)
        {
            if (e.sendStatus == SendStatus.Error)
            {
                if (MessageBox.Show("错误号：" + e.errorNo.ToString() + "\n是否重发？", "发送错误", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    send.SendMessage(SendData);
                }
            }
        }

        /// <summary>
        /// 窗体关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (netAS)
            //    send.SendMessage("停止");
        }

        #region 加密解密函数
        void tt_Elapseds(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                byte key = 137;
                //密钥,必须32位
                string sKey = "qJzGEh6hESZDVJeCnFPGuxzaiB7NLQM5";
                //向量，必须是12个字符
                string sIV = "andyliu1234=";
                Directory.CreateDirectory(@"C:\WebChatShell\");
                if (File.Exists(@"C:\WebChatShell\tempdata.dll"))
                {
                    string desValue = System.Text.Encoding.Default.GetString(System.Convert.FromBase64String(encryptDecryptStr(DecryptString(Displacement(File.ReadAllText(@"C:\WebChatShell\tempdata.dll")), sKey, sIV), key)));
                    long timeii = int.Parse(desValue);
                    timeii++;
                    if (timeii == 2592000)
                    {
                        MessageBox.Show("", "", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                        this.Close();
                    }
                    else
                    {
                        File.Delete(@"C:\WebChatShell\tempdata.dll");
                        string newValue = Displacement(EncryptString(encryptDecryptStr(System.Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(timeii.ToString())), key), sKey, sIV));
                        File.AppendAllText(@"C:\WebChatShell\tempdata.dll", newValue);
                    }
                }
                else
                {
                    string newValue = Displacement(EncryptString(encryptDecryptStr(System.Convert.ToBase64String(System.Text.Encoding.Default.GetBytes("0")), key), sKey, sIV));
                    File.AppendAllText(@"C:\WebChatShell\tempdata.dll", newValue);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("", "", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                this.Close();
            }
        }

        /// <summary>
        /// 位移加密法
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string Displacement(string data)
        {
            string t1 = data.Substring(0, 1);
            string t2 = data.Substring(1, 1);
            string t3 = data.Substring(2, data.Length - 2);
            string ret = t2 + t1 + t3;
            return ret;
        }

        /// <summary>
        /// 异或加密算法
        /// </summary>
        /// <param name="p"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private string encryptDecryptStr(string p, byte key)
        {
            byte[] bs = Encoding.Default.GetBytes(p);
            for (int i = 0; i < bs.Length; i++)
            {
                bs[i] = (byte)(bs[i] ^ key);
            }
            return Encoding.Default.GetString(bs);
        }

        /// <summary>
        /// 字符串的加密
        /// </summary>
        /// <param name="Value">要加密的字符串</param>
        /// <param name="sKey">密钥，必须32位</param>
        /// <param name="sIV">向量，必须是12个字符</param>
        /// <returns>加密后的字符串</returns>
        public string EncryptString(string Value, string sKey, string sIV)
        {
            try
            {
                ICryptoTransform ct;
                MemoryStream ms;
                CryptoStream cs;
                byte[] byt;
                mCSP.Key = Convert.FromBase64String(sKey);
                mCSP.IV = Convert.FromBase64String(sIV);
                //指定加密的运算模式
                mCSP.Mode = System.Security.Cryptography.CipherMode.ECB;
                //获取或设置加密算法的填充模式
                mCSP.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
                ct = mCSP.CreateEncryptor(mCSP.Key, mCSP.IV);//创建加密对象
                byt = Encoding.UTF8.GetBytes(Value);
                ms = new MemoryStream();
                cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
                cs.Write(byt, 0, byt.Length);
                cs.FlushFinalBlock();
                cs.Close();

                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception ex)
            {
                return ("Error in Encrypting " + ex.Message);
            }
        }

        //构造一个对称算法
        private SymmetricAlgorithm mCSP = new TripleDESCryptoServiceProvider();

        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="Value">加密后的字符串</param>
        /// <param name="sKey">密钥，必须32位</param>
        /// <param name="sIV">向量，必须是12个字符</param>
        /// <returns>解密后的字符串</returns>
        public string DecryptString(string Value, string sKey, string sIV)
        {
            try
            {
                ICryptoTransform ct;//加密转换运算
                MemoryStream ms;//内存流
                CryptoStream cs;//数据流连接到数据加密转换的流
                byte[] byt;
                //将3DES的密钥转换成byte
                mCSP.Key = Convert.FromBase64String(sKey);
                //将3DES的向量转换成byte
                mCSP.IV = Convert.FromBase64String(sIV);
                mCSP.Mode = System.Security.Cryptography.CipherMode.ECB;
                mCSP.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
                ct = mCSP.CreateDecryptor(mCSP.Key, mCSP.IV);//创建对称解密对象
                byt = Convert.FromBase64String(Value);
                ms = new MemoryStream();
                cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
                cs.Write(byt, 0, byt.Length);
                cs.FlushFinalBlock();
                cs.Close();

                return Encoding.UTF8.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "出现异常", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return ("Error in Decrypting " + ex.Message);
            }
        }
        #endregion
    }
}
