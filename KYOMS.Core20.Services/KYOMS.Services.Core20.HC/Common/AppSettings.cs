using KYOMS.Core20.Common.Config;
using KYOMS.Core20.Common.Log4NetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KYOMS.Services.Core20.HC.Common
{
    public class AppSettings
    {
        /// <summary>
        /// log4net.config文件路径
        /// </summary>
        public static string Log4NetConfig => ConfigHelper.GetWebConfigString("Log4NetConfig");
        /// <summary>
        /// 服务监听端口
        /// </summary>
        public static string ServicePort => ConfigHelper.GetWebConfigString("ServicePort");
        /// <summary>
        /// 日志文件命名(等效)
        /// </summary>
        public string LogFileName()
        {
            return $"log//Hc{IpAddressHandle.GetLocalIp()}_{ServicePort}_{DateTime.Now:yyyyMMdd}";
        }
        private static AppSettings _instance;
        public static AppSettings Instance => _instance ?? (_instance = new AppSettings());
    }
}
