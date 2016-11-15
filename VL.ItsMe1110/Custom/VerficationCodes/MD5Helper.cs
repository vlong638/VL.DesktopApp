using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace VL.ItsMe1110.Custom.VerficationCodes
{
    public class MD5Helper
    {
        //加密/解密钥匙 8个字符，64位    
        const string KEY_64 = "vlong638";
        const string IV_64 = "42507570";

        public static string ToMD5(string key)
        {
            if (key.Count()==8)
            {
                return key;
            }
            byte[] result = Encoding.Default.GetBytes(key);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            string KEY_64 = BitConverter.ToString(output).Replace("-", "").Substring(0, 8);
            return KEY_64;

        }
        public static string Encode(string data, string key_64 = KEY_64, string iv_64 = IV_64)
        {
            key_64 = ToMD5(key_64);
            iv_64 = ToMD5(iv_64);
            byte[] byKey = Encoding.ASCII.GetBytes(key_64);
            byte[] byIV = Encoding.ASCII.GetBytes(iv_64);
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            int i = cryptoProvider.KeySize;
            MemoryStream ms = new MemoryStream();
            CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateEncryptor(byKey, byIV), CryptoStreamMode.Write);
            StreamWriter sw = new StreamWriter(cst);
            sw.Write(data);
            sw.Flush();
            cst.FlushFinalBlock();
            sw.Flush();
            return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
        }
        public static string Decode(string data, string key_64 = KEY_64, string iv_64 = IV_64)
        {
            key_64 = ToMD5(key_64);
            iv_64 = ToMD5(iv_64);
            byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(key_64);
            byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV_64);
            byte[] byEnc;
            try
            {
                byEnc = Convert.FromBase64String(data);
            }
            catch
            {
                return null;
            }

            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream ms = new MemoryStream(byEnc);
            CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateDecryptor(byKey, byIV), CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cst);
            return sr.ReadToEnd();
        }
    }
}