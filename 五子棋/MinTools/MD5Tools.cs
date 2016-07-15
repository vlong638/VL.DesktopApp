using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace MinTools
{
    /// <summary>
    /// MD5类
    /// </summary>
    public class MD5Tools
    {
        /// <summary>
        /// 取得输入byte的MD5哈希值
        /// </summary>
        /// <param name="argInput">待计算哈希值字节数组</param>
        /// <returns>MD5哈希值</returns>
        public static string GetMd5Hash(byte[] argInput)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(argInput);
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        /// <summary>
        /// 验证MD5哈希值
        /// </summary>
        /// <param name="argInput">待验证哈希值字节数组</param>
        /// <param name="argHash">哈希值</param>
        /// <returns>相同返回true,不同返回false</returns>
        public static bool VerifyMd5Hash(byte[] argInput, string argHash)
        {
            string hashOfInput = GetMd5Hash(argInput);

            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, argHash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
