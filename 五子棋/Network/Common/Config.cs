using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Network
{
    /// <summary>
    /// 网络传输配置类
    /// </summary>
    public class Config
    {
        #region 命令/令牌

        /// <summary>
        /// 数据类型令牌
        /// </summary>
        public static string TypeOrder = "DT=";
        /// <summary>
        /// 数据分包数
        /// </summary>
        public static string CountOrder = "BC=";
        /// <summary>
        /// 数据发送完毕指令
        /// </summary>
        public static string DataOverOrder = "DO";

        #endregion


        /// <summary>
        /// 文件储存路径
        /// </summary>
        public static string FileSavePath = System.Environment.CurrentDirectory + @"\";
        /// <summary>
        /// 数据单元大小，决定传输速度单位为KB-发送端、接收端必须一致，默认为10MB
        /// </summary>
        public static int BufferSize = 10240 * 1024;
        /// <summary>
        /// 数据/头长度(位)
        /// </summary>
        public static int DataLength = 16;
        /// <summary>
        /// 数据最大有效载荷
        /// </summary>
        public static int Payload = BufferSize - DataLength - SubpackageNOLength - MD5Length;
        /// <summary>
        /// 分包编号字节长度
        /// </summary>
        public static int SubpackageNOLength = 10;
        /// <summary>
        /// 数据MD5长度
        /// </summary>
        public static int MD5Length = 64;
        /// <summary>
        /// 分包头长度
        /// </summary>
        public static int SubpackageTopLength = DataLength + SubpackageNOLength + MD5Length;
        /// <summary>
        /// 报头键值对分隔符string型
        /// </summary>
        public static string StrOrderInterval = ";";
        /// <summary>
        /// 报头键值对分隔符char型
        /// </summary>
        public static char CharOrderInterval = ';';
        /// <summary>
        /// 令牌属性/值分隔符char型
        /// </summary>
        public static char CharBeWithoutEqual = '=';
        /// <summary>
        /// byte数据发送完毕指令
        /// </summary>
        public static byte[] dataOverOrderByte = Encoding.Unicode.GetBytes(DataOverOrder);
        /// <summary>
        /// 等待数据传输完毕指令时长（秒）
        /// </summary>
        public static int TimeOut = 10;
    }
}
