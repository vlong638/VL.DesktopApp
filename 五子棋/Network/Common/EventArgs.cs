using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Network
{
    /// <summary>
    /// 接收状态事件参数类
    /// </summary>
    public class ReceiveEventArgs : EventArgs
    {
        /// <summary>
        /// 接收状态
        /// </summary>
        public ReceiveStates receiveStates;
        /// <summary>
        /// 错误编号
        /// </summary>
        public ErrorNo errorNo;
        /// <summary>
        /// 附参
        /// </summary>
        public string arg;

        /// <summary>
        /// 初始化一般接收状态事件参数类
        /// </summary>
        /// <param name="receiveStates">接收状态</param>
        /// <param name="arg">附参</param>
        public ReceiveEventArgs(ReceiveStates receiveStates, string arg)
        {
            this.receiveStates = receiveStates;
            this.arg = arg;
        }
        /// <summary>
        /// 初始化发送事件(错误)
        /// </summary>
        /// <param name="errorNo">错误编号</param>
        /// <param name="arg">附参</param>
        public ReceiveEventArgs(ErrorNo errorNo, string arg)
        {
            this.receiveStates = ReceiveStates.Error;
            this.errorNo = errorNo;
            this.arg = arg;
        }
    }

    /// <summary>
    /// 发送事件参数类
    /// </summary>
    public class SendEventArgs : EventArgs
    {
        /// <summary>
        /// 发送状态
        /// </summary>
        public SendStatus sendStatus;
        /// <summary>
        /// 错误编号
        /// </summary>
        public ErrorNo errorNo;
        /// <summary>
        /// 附参
        /// </summary>
        public string arg;

        /// <summary>
        /// 初始化发送事件(错误)
        /// </summary>
        /// <param name="errorNo">错误编号</param>
        /// <param name="arg">附参</param>
        public SendEventArgs(ErrorNo errorNo, string arg)
        {
            this.sendStatus = SendStatus.Error;
            this.errorNo = errorNo;
            this.arg = arg;
        }

        /// <summary>
        /// 初始化发送事件(阶段成功，回传附参)
        /// </summary>
        /// <param name="sendStatus">发送执行状态</param>
        /// <param name="arg">附参</param>
        public SendEventArgs(SendStatus sendStatus, string arg)
        {
            this.sendStatus = sendStatus;
            this.arg = arg;
        }

        public abstract class test
        {

        }
    }
}
