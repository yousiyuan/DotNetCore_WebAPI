using System;
using System.Collections.Generic;
using System.Text;

namespace KYOMS.Core20.Common.Log4NetCore
{
    public static class LoggerExt
    {
        /// <summary>
        /// 将字符串写入到日志文件中，并指定日志级别
        /// </summary>
        /// <param name="logText"></param>
        /// <param name="logerType">log级别，默认为：Info</param>
        public static void WriteToLog(this string logText, LogerType logerType = LogerType.Info)
        {
            LoggerHandle loggerHandle = new LoggerHandle(null, $"log//{AppDomain.CurrentDomain.GetData("ProjectName")}_{IpAddressHandle.GetLocalIp()}_{AppDomain.CurrentDomain.GetData("Port")}_{DateTime.Now:yyyyMMdd}");
            loggerHandle.Set(logText, logerType);
        }
    }
}
