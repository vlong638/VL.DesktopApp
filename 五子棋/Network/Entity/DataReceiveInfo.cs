using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Network.Entity
{
    /// <summary>
    /// 数据通讯实体类
    /// </summary>
    public class DataReceiveInfo
    {
        /// <summary>
        /// 数据通讯IP端口
        /// </summary>
        public IPorPort iPorPort { get; set; }
        /// <summary>
        /// 报头信息列表
        /// </summary>
        public List<TokenData> TokenData { get; set; }
    }
}
