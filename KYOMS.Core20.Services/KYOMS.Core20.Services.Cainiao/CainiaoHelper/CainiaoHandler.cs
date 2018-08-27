using KYOMS.Core20.Common.Log4NetCore;
using KYOMS.Core20.DE.CainiaoModel;
using KYOMS.Core20.Entity.MySqlDB;
using KYOMS.Core20.Services.Cainiao.Common;
using System;
using System.Text;

namespace KYOMS.Core20.Services.Cainiao.CainiaoHelper
{
    public static class CainiaoHandler
    {
        //private static readonly LoggerHandle CainiaoLog;

        static CainiaoHandler()
        {
            //if (CainiaoLog == null)
            //    CainiaoLog = new LoggerHandle(null, AppSettings.Instance.LogFileName());
        }

        /// <summary>
        /// 映射数据
        /// </summary>
        /// <param name="taobaoOrderModel"></param>
        /// <param name="logisticsInterface"></param>
        /// <returns></returns>
        public static T_MySql_Order CreateMap(TmsOrderModel taobaoOrderModel, string logisticsInterface)
        {
            try
            {
                var mySqlOrder = new T_MySql_Order
                {
                    OUTSYS_BILL_CODE = string.IsNullOrEmpty(taobaoOrderModel.mailNo) ? " " : taobaoOrderModel.mailNo,
                    OUTSYS_ORDER_NO = string.IsNullOrEmpty(taobaoOrderModel.logisticsId)
                        ? " "
                        : taobaoOrderModel.logisticsId,
                    ORDER_SOURCE = "TAOBAO_ONLINE",
                    MSG_TYPE = "JSON",
                    //mySqlOrder.CREATE_BY = "",
                    MSG_CONTENT = string.IsNullOrEmpty(logisticsInterface) ? "" : logisticsInterface,
                    IS_SYNC_SUCCESS = 0,
                    CREATE_TIME = DateTime.Now,
                    CREATE_BY = AppSettings.CreateBy,
                    C1 = "",
                    C2 = "",
                    C3 = "",
                    REMARK = ""
                };
                return mySqlOrder;
            }
            catch (Exception e)
            {
                var message = new StringBuilder();
                message.Append("TAOBAO_ONLINE订单映射过程发生错误：" + logisticsInterface);
                message.Append(e.Message + e.StackTrace);
                //message.SetLog(message.ToString(), LogerType.Fatal);
                message.ToString().WriteToLog(LogerType.Fatal);
                return null;
            }
        }
    }
}
