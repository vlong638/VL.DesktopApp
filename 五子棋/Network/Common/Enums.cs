using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Network
{
    /// <summary>
    /// 接收执行状态
    /// </summary>
    public enum ReceiveStates
    {
        /// <summary>
        /// 错误
        /// </summary>
        Error,
        /// <summary>
        /// 主监听端口初始化完毕
        /// </summary>
        MainListenerOver,
        /// <summary>
        /// 子监听端口初始化完毕
        /// </summary>
        SubListenerOver,
        /// <summary>
        /// 报头接收完毕
        /// </summary>
        ReceiveHeaderOver,
        /// <summary>
        /// 收到数据包
        /// </summary>
        ReceiveData,
        /// <summary>
        /// 数据接收完毕，开始组装数据
        /// </summary>
        ReceiveDataOver,
        /// <summary>
        /// 消息组装完毕
        /// </summary>
        ReceiveMessageOver,
        /// <summary>
        /// 文件写入完毕
        /// </summary>
        ReceiveFileOver,
        /// <summary>
        /// 停止监听
        /// </summary>
        StopListener,
    }

    /// <summary>
    /// 发送执行状态
    /// </summary>
    public enum SendStatus
    {
        /// <summary>
        /// 错误
        /// </summary>
        Error = 0,
        /// <summary>
        /// 分包完毕
        /// </summary>
        SubOver = 1,
        /// <summary>
        /// 报头发送完毕
        /// </summary>
        HeaderOver = 2,
        /// <summary>
        /// 发送分包
        /// </summary>
        SendSub = 3,
        /// <summary>
        /// 数据发送完毕
        /// </summary>
        SendOver = 4,
    }

    /// <summary>
    /// 数据类型
    /// </summary>
    public enum DataType
    {
        /// <summary>
        /// 消息数据
        /// </summary>
        MessageData = 0,
        /// <summary>
        /// 文件数据
        /// </summary>
        FileData = 1,
    }

    /// <summary>
    /// 错误编号枚举
    /// </summary>
    public enum ErrorNo
    {
        #region 数据发送
        /// <summary>
        /// 报头发送失败
        /// </summary>
        ErrorSendHeader = 10,
        /// <summary>
        /// 远程连接错误
        /// </summary>
        ErrorLink = 11,
        /// <summary>
        /// 数据端口连接错误
        /// </summary>
        ErrorLinkDate = 12,
        /// <summary>
        /// 等待数据发送完毕错误
        /// </summary>
        ErrorSendOver = 13,
        /// <summary>
        /// 等待数据发送完毕超时
        /// </summary>
        ErrorSendOverTimerOut = 14,
        #endregion

        #region 数据接收
        /// <summary>
        /// 监听开启错误
        /// </summary>
        ErrorListener = 20,
        /// <summary>
        /// 数据接收端口开启错误
        /// </summary>
        ErrorDataListener = 21,
        /// <summary>
        /// MD5验证错误
        /// </summary>
        ErrorMD5 = 22,
        /// <summary>
        /// 数据接收失败
        /// </summary>
        ErrorReceiveData = 23
        #endregion
    }
}
