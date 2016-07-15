using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

namespace MinTools
{
    /// <summary>
    /// 网络工具类
    /// </summary>
    public class NetTools
    {
        /// <summary>
        /// 查看已占用端口方式
        /// </summary>
        public enum QueryPortIsUsedWay
        {
            /// <summary>
            /// 查看TCP端口
            /// </summary>
            TCP = 0,
            /// <summary>
            /// 查看UDP端口
            /// </summary>
            UDP = 1,
            /// <summary>
            /// 命令
            /// </summary>
            All = 2,
        }

        /// <summary>
        /// 获得本机IPv4地址列表
        /// </summary>
        public static List<IPAddress> GetLocalIPv4()
        {
            IPAddress[] iPAddresss = Dns.GetHostAddresses(Dns.GetHostName());
            List<IPAddress> IpCollection = new List<IPAddress>();
            foreach (IPAddress ip in iPAddresss)
            {
                //根据AddressFamily判断是否为ipv4,如果是InterNetWork则为ipv6
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    IpCollection.Add(ip);
            }
            return IpCollection;
        }

        /// <summary>
        /// 获取操作系统已用的端口号
        /// </summary>
        /// <param name="queryPortIsUsedWay">端口查看方式</param>
        /// <returns></returns>
        public static IList PortIsUsed(QueryPortIsUsedWay queryPortIsUsedWay)
        {
            IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();//获取本地计算机的网络连接和通信统计数据的信息

            IPEndPoint[] ipsTCP = ipGlobalProperties.GetActiveTcpListeners();//返回本地计算机上的所有Tcp监听程序

            IPEndPoint[] ipsUDP = ipGlobalProperties.GetActiveUdpListeners();//返回本地计算机上的所有UDP监听程序

            TcpConnectionInformation[] tcpConnInfoArray = ipGlobalProperties.GetActiveTcpConnections();//返回本地计算机上的Internet协议版本4(IPV4 传输控制协议(TCP)连接的信息。

            IList allPorts = new ArrayList();
            switch (queryPortIsUsedWay)
            {
                case QueryPortIsUsedWay.All:
                    foreach (IPEndPoint ep in ipsTCP) if (!allPorts.Contains(ep.Port)) allPorts.Add(ep.Port);
                    foreach (IPEndPoint ep in ipsUDP) if (!allPorts.Contains(ep.Port)) allPorts.Add(ep.Port);
                    foreach (TcpConnectionInformation conn in tcpConnInfoArray) if (!allPorts.Contains(conn.LocalEndPoint.Port)) allPorts.Add(conn.LocalEndPoint.Port);
                    break;
                case QueryPortIsUsedWay.TCP:
                    foreach (IPEndPoint ep in ipsTCP) if (!allPorts.Contains(ep.Port)) allPorts.Add(ep.Port);
                    foreach (TcpConnectionInformation conn in tcpConnInfoArray) if (!allPorts.Contains(conn.LocalEndPoint.Port)) allPorts.Add(conn.LocalEndPoint.Port);
                    break;
                case QueryPortIsUsedWay.UDP:
                    foreach (IPEndPoint ep in ipsUDP) allPorts.Add(ep.Port);
                    break;
            }
            return allPorts;
        }

        /// <summary>
        /// 检查指定端口是否已用
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public static bool PortIsAvailable(int port)
        {
            bool isAvailable = true;

            IList portUsed = PortIsUsed(QueryPortIsUsedWay.All);

            foreach (int p in portUsed)
            {
                if (p == port)
                {
                    isAvailable = false; break;
                }
            }
            return isAvailable;
        }

        /// <summary>
        /// 获取一个可用的端口号
        /// <returns></returns>
        /// </summary>
        /// <param name="endPort">结束检测端口——系统tcp/udp端口数最大是65535</param>
        /// <param name="beginPort">开始检测端口</param>
        /// <returns></returns>
        public static int GetFirstAvailablePort(int beginPort, int endPort)
        {
            for (int i = beginPort; i < endPort; i++)
            {
                if (PortIsAvailable(i)) return i;
            }
            return -1;
        }

        /// <summary>
        /// 随机获取一个可用的端口号
        /// <returns></returns>
        /// </summary>
        /// <param name="minPost">最小端口号</param>
        /// <param name="maxPost">最大端口号</param>
        /// <returns></returns>
        public static int GetRandomAvailablePort(int minPost, int maxPost)
        {
            Random ra = new Random();
            while (true)
            {
                int port = ra.Next(minPost, maxPost);
                if (PortIsAvailable(port)) return port;
            }
        }


        /// <summary>
        /// ping方法
        /// </summary>
        /// <param name="aimIP">目标IP</param>
        /// <param name="delay">迟缓界限</param>
        /// <returns>0/正常，1/迟缓，2/无连接</returns>
        public static int Ping(IPAddress aimIP, int delay)
        {
            PingReply reply;//服务器回包
            Ping pingSender = new Ping();
            PingOptions options = new PingOptions();
            options.DontFragment = true;
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 120;
            try    //如果能ping通
            {
                reply = pingSender.Send(aimIP, timeout, buffer, options);
                if (reply.Address.ToString() == "")    //如果服务器无返回值
                {
                    return 2;
                }
                else    //如果服务器有返回值
                {
                    if (reply.RoundtripTime >= delay)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            catch    //如果本地网络网络状态错误
            {
                return 2;
            }
        }
    }
}
