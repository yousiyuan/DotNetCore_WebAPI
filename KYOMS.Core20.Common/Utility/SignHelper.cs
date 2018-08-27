using System;
using System.Security.Cryptography;
using System.Text;

namespace KYOMS.Core20.Common.Utility
{
    public class SignHelper
    {
        /// <summary>
        /// 签名检查 类型:MD5摘要
        /// </summary>
        /// <param name="content">报文内容</param>
        /// <param name="dataDigest">请求签名</param>
        /// <param name="secretKey"></param>
        /// <returns></returns>
        public static bool CheckDataDigest(string content, string dataDigest, string secretKey)
        {
            var dataDigestContent = CreateDataDigest(content, secretKey);
            return dataDigest == dataDigestContent;
        }

        /// <summary>
        /// 根据报文内容创建签名
        /// </summary>
        /// <param name="content"></param>
        /// <param name="secretKey"></param>
        /// <returns></returns>
        public static string CreateDataDigest(string content,string secretKey)
        {
            byte[] binaryData = Encoding.UTF8.GetBytes(content + secretKey);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(binaryData);
            var dataDigest = Convert.ToBase64String(output);

            return dataDigest;
        }
    }
}
