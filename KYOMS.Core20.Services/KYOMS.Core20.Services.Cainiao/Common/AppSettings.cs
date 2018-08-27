using KYOMS.Core20.Common.Config;
using KYOMS.Core20.DE.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KYOMS.Core20.Common.Log4NetCore;

namespace KYOMS.Core20.Services.Cainiao.Common
{
    public class AppSettings
    {
        /// <summary>
        /// 创建人
        /// </summary>
        public static string CreateBy => ENUM_ORDER_SOURCE.TAOBAO_ONLINE.ToString();
        /// <summary>
        /// 请求消息类型：添加订单
        /// </summary>
        public static string MsgType => ConfigHelper.GetWebConfigString("MsgType");
        /// <summary>
        /// MySql库配置路径
        /// </summary>
        public static string MySqlDbConfig => ConfigHelper.GetWebConfigString("MySqlDbConfig");
        /// <summary>
        /// log4net.config文件路径
        /// </summary>
        public static string Log4NetConfig => ConfigHelper.GetWebConfigString("Log4NetConfig");
        /// <summary>
        /// 服务监听端口
        /// </summary>
        public static string ServicePort => ConfigHelper.GetWebConfigString("ServicePort");
        /// <summary>
        /// CpCode
        /// </summary>
        public static string CpCode => ConfigHelper.GetWebConfigString("CpCode");
        /// <summary>
        /// 报文加密token
        /// </summary>
        public static string SecretKey => ConfigHelper.GetWebConfigString("SecretKey");
        /// <summary>
        /// Redis配置
        /// </summary>
        public static string Redis => ConfigHelper.GetWebConfigString("Redis");
        /// <summary>
        /// ZooKeeper配置路径
        /// </summary>
        public static string Zsettings => ConfigHelper.GetWebConfigString("Zsettings");

        public static string ProjectName => "Cainiao";
        ///// <summary>
        ///// 日志文件命名(等效)
        ///// </summary>
        //public string LogFileName()
        //{
        //    return $"log//Cainiao_{IpAddressHandle.GetLocalIp()}_{ServicePort}_{DateTime.Now:yyyyMMdd}";
        //}
        //private static AppSettings _instance;
        //public static AppSettings Instance => _instance ?? (_instance = new AppSettings());
    }
}
