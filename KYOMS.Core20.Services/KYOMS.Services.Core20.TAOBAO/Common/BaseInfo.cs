using KYOMS.Core20.Common.Config;
using KYOMS.Core20.Common.Log4NetCore;
using KYOMS.Core20.DE.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using System;
using System.Text;

namespace KYOMS.Services.Core20.TAOBAO.Common
{
    public class BaseInfo
    {
        #region 公用信息
        /// <summary>
        /// 定义一个默认日期的最小日期
        /// </summary>
        public static DateTime MinDate = DateTime.MinValue;

        /// <summary>
        /// 创建人
        /// </summary>
        public static string CreateBy = ENUM_ORDER_SOURCE.TAOBAO.ToString();

        /// <summary>
        /// SecretKey
        /// </summary>
        public static string SecretKey = ConfigHelper.GetWebConfigString("SecretKey");

        /// <summary>
        /// 客户编码：这里要填写阿里在我们系统客户表中的编号，D00000836  菜鸟供应链
        /// </summary>
        public static string CustomerCode = "D00000836";

        /// <summary>
        /// Content-Type
        /// </summary>
        public static string ContentType = "application/json";

        /// <summary>
        /// 编码格式
        /// </summary>
        public static Encoding UrlEncoder = Encoding.UTF8;

        /// <summary>
        /// 根据送货地址获取派件网点接口地质，使用时，直接传入编码后的地址
        /// </summary>
        public static string GetDispatchOrderToSiteUrl = ConfigHelper.GetWebConfigString("DispatchOrderToSiteUrl");

        #endregion

        #region MsgType

        /// <summary>
        /// ORDERCREATE MsgType
        /// </summary>
        public static string OrderCreateMsgType = "ORDERCREATE";

        /// <summary>
        /// UPDATE MsgType
        /// </summary>
        public static string OrderUpdateMsgType = "UPDATE";

        /// <summary>
        /// TRACEPUSH MsgType
        /// </summary>
        public static string OrderTracePushMsgType = "TRACEPUSH";


        /// <summary>
        /// trace_info_query MsgType
        /// </summary>
        public static string OrderTraceInfoQueryType = "trace_info_query";
        #endregion

        /// <summary>
        /// 物流公司编号
        /// </summary>
        public static readonly string CpCode = ConfigHelper.GetWebConfigString("CpCode");

        /// <summary>
        /// 合作伙伴编码-可选
        /// </summary>
        public static readonly string PartnerCode = "KDWKV20150210";

        public static IServiceProvider ServiceProvider;
        public static int Port
        {
            get {
                return Convert.ToInt32(AppDomain.CurrentDomain.GetData("Port")); ;
            }
        }
    }
}