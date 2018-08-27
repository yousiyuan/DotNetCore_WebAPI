using KYOMS.Core20.Common.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KYOMS.Core20.Common.Log4NetCore;
using KYOMS.Core20.DE.Enum;

namespace KYOMS.Core20.Services.Ali.Common
{
    public class AppSettings
    {
        /// <summary>
        /// 创建人
        /// </summary>
        public static string CreateBy => ENUM_ORDER_SOURCE.ALI.ToString();
        /// <summary>
        /// 密钥
        /// </summary>
        public static string SecretKey => ConfigHelper.GetWebConfigString("SecretKey");
        /// <summary>
        /// Redis配置信息
        /// </summary>
        public static string Redis => ConfigHelper.GetWebConfigString("Redis");
        /// <summary>
        /// MySql库配置路径
        /// </summary>
        public static string MySqlDbConfig => ConfigHelper.GetWebConfigString("MySqlDbConfig");
        /// <summary>
        /// Oracle库配置路径
        /// </summary>
        public static string OracleDbConfig => ConfigHelper.GetWebConfigString("OracleDbConfig");
        /// <summary>
        /// log4net.config文件路径
        /// </summary>
        public static string Log4NetConfig => ConfigHelper.GetWebConfigString("Log4NetConfig");
        /// <summary>
        /// 服务监听端口
        /// </summary>
        public static string ServicePort => ConfigHelper.GetWebConfigString("ServicePort");
        /// <summary>
        /// 根据送货地址获取派件网点接口地质，使用时，直接传入编码后的地址
        /// </summary>
        public static string GetDispatchOrderToSiteUrl => ConfigHelper.GetWebConfigString("DispatchOrderToSiteUrl") + "&addr=";
        /// <summary>
        /// 获取派件网点接口地址//地盘系统地址
        /// </summary>
        public static string QuerySite => ConfigHelper.GetWebConfigString("QuerySite");
        /// <summary>
        /// ZooKeeper配置路径
        /// </summary>
        public static string Zsettings => ConfigHelper.GetWebConfigString("Zsettings");

        #region 常量配置
        public const string QueryQuote = "http://222.68.191.182:8091/QueryQuoteInterface/queryOrderTracking.do";
        public const string url = "http://pac.partner.taobao.com/gateway/pac_message_receiver.do";
        public const string appkey = "278617";
        public const string secretKey = "FpQ2x3C6849od2N91420407p70R7O706";
        public const string fromCode = "2460304407_385";
        public const string QuerySitekey = "Key123";
        #endregion

        public static string ProjectName => "Ali";
        ///// <summary>
        ///// 日志文件命名
        ///// </summary>
        //public string LogFileName()
        //{
        //    return $"log//Ali_{IpAddressHandle.GetLocalIp()}_{ServicePort}_{DateTime.Now:yyyyMMdd}";
        //}
        //private static AppSettings _instance;
        //public static AppSettings Instance => _instance ?? (_instance = new AppSettings());
    }
}
