using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Network.Entity
{
    /// <summary>
    /// 发送数据实体类
    /// </summary>
    public class SendInfo
    {
        /// <summary>
        /// 数据类型—必填项
        /// </summary>
        public DataType DataType { get; set; }
        /// <summary>
        /// 待发送数据—必填项(消息/文件路径)
        /// </summary>
        public string Data { get; set; }
        /// <summary>
        /// 新文件名—选填项(不填写发送新文件名则默认发送文件当前名)
        /// </summary>
        public string NewFileName { get; set; }
        /// <summary>
        /// 附参—选填项(文件传输附带消息，不宜过大)
        /// </summary>
        public string Parameter { get; set; }
        /// <summary>
        /// 目标机器
        /// </summary>
        public IPorPort AimIpPort { get; set; }
    }
}
