using MinTools;
using Network.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Network
{
    /// <summary>
    /// 数据接收线程
    /// </summary>
    public class ReceiveThread : TimerThread
    {
        /// <summary>
        /// 接收数据实体类
        /// </summary>
        public DataReceiveInfo dataReceiveInfo;
        /// <summary>
        /// 初始化数据接收线程
        /// </summary>
        public ReceiveThread(DataReceiveInfo dataReceiveInfo)
        {
            this.dataReceiveInfo = dataReceiveInfo;
        }

    }
}
