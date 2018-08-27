using KYOMS.Core20.DE.Model;
using KYOMS.Services.Core20.HC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KYOMS.Services.Core20.HC.Common
{
    public class PublicMethods : RequestModel
    {
        public static string CheckSecurity(string content, string timestamp)
        {
            //慧聪约定密钥
            string SecretKey = "4e2ac824a8ba473fb56691c73df302b4";
            byte[] binaryData = Encoding.UTF8.GetBytes(content + SecretKey + timestamp);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(binaryData);
            var dataDigest = Convert.ToBase64String(output);
            return dataDigest;
        }
        public static bool CheckSign(RequestModel requestModel)
        {
            return CheckDataDigest(requestModel.@params, NewMethod(requestModel), requestModel.timestamp);
        }

        private static string NewMethod(RequestModel requestModel)
        {
            return requestModel.digest.Replace(" ", "+");
        }

        public static bool CheckDataDigest(string requestModel, string dataDigest, string timestamp)
        {
            var dataDigestContent = CheckSecurity(requestModel, timestamp);
            return dataDigest == dataDigestContent;

        }
        /// <summary>
        ///时间戳
        /// </summary>
        /// <returns></returns>
        public static bool IsTimeOuts(string timestamp)
        {
            return IsTimeOut(timestamp);
        }

        public static bool IsTimeOut(string timeSpan)
        {
            return false;
        }


    }
}
