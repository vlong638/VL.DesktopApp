using MinTools;
using Network.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Network
{
    /// <summary>
    /// 数据发送线程
    /// </summary>
    public class SendThread : TimerThread
    {
        /// <summary>
        /// 发送数据实体类
        /// </summary>
        public SendInfo sendInfo;
        /// <summary>
        /// 初始化数据发送线程
        /// </summary>
        public SendThread(SendInfo sendInfo)
        {
            this.sendInfo = sendInfo;
        }

    }
}
