using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Gobang
{
    /// <summary>
    /// 配置类
    /// </summary>
    public class Config
    {
        /// <summary>
        /// 格子X量
        /// </summary>
        public const int xConst = 34;
        /// <summary>
        /// 格子Y量
        /// </summary>
        public const int yConst = 34;
        /// <summary>
        /// X偏移量
        /// </summary>
        public const int xOffset = 17;
        /// <summary>
        /// Y偏移量
        /// </summary>
        public const int yOffset = 17;
        /// <summary>
        /// 主机IP
        /// </summary>
        public static IPAddress ip;
        /// <summary>
        /// 本机IP
        /// </summary>
        public static IPAddress LocalIP;

        /// <summary>
        /// 主机端口
        /// </summary>
        public static int port = 61549;
    }
}
