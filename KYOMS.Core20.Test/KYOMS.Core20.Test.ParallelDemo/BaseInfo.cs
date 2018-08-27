using System;
using System.Security.Cryptography;
using System.Text;

namespace KYOMS.Core20.Test.ParallelDemo
{
    public class BaseInfo
    {

        /// <summary>
        /// 物流跟踪信息推送地址
        /// </summary>
        public static readonly string PostOrderUrl = "http://localhost:62844/api/orders/addorder";

        public static readonly string SecretKey = "eV111z83Y63r553B8gwF3c236e9a8881";

        #region 签名检查-MD5摘要
        /// <summary>
        /// 签名检查 类型:MD5摘要
        /// </summary>
        /// <param name="content">报文内容</param>
        /// <param name="dataDigest">请求签名</param>
        /// <returns></returns>
        public static bool CheckDataDigest(string content, string dataDigest)
        {
            var dataDigestContent = CreateDataDigest(content);
            return dataDigest == dataDigestContent;
        }

        /// <summary>
        /// 根据报文内容创建签名
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string CreateDataDigest(string content)
        {
            byte[] binaryData = Encoding.UTF8.GetBytes(content);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(binaryData);
            var dataDigest = Convert.ToBase64String(output);

            return dataDigest;
        }

        /// <summary>
        /// 根据报文内容创建签名
        /// </summary>
        /// <param name="content"></param>
        /// <param name="secretKey"></param>
        /// <returns></returns>
        public static string CreateDataDigest(string content, string secretKey)
        {
            byte[] binaryData = Encoding.UTF8.GetBytes(content + secretKey);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(binaryData);
            var dataDigest = Convert.ToBase64String(output);

            return dataDigest;
        }
        #endregion
    }
}
