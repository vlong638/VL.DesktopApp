using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Network.Entity
{
    /// <summary>
    /// IP地址端口实体类
    /// </summary>
    public class IPorPort
    {
        /// <summary>
        /// IP
        /// </summary>
        public IPAddress IP { get; set; }
        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 初始化IP地址端口实体类
        /// </summary>
        /// <param name="IP"></param>
        /// <param name="Port"></param>
        public IPorPort(IPAddress IP, int Port)
        {
            this.IP = IP;
            this.Port = Port;
        }
    }
}
