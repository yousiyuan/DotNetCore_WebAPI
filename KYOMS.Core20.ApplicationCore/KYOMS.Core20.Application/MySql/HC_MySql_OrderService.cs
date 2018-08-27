using KYOMS.Core20.Application.Interface;
using KYOMS.Core20.Common.Log4NetCore;
using KYOMS.Core20.DE.Model;
using KYOMS.Core20.Entity.MySqlDB;
using KYOMS.Core20.Respository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KYOMS.Core20.Application.MySql
{
    public class HC_MySql_OrderService : T_MySql_OrderRepository<HC_MySql_OrderService>, IHC_MySql_OrderService
    {
        public async Task<bool> AddHC(AddOrder_HC order_HC, string logisticsInterface)
        {
            var mysql_order = CreateMap(order_HC, logisticsInterface);
            if (mysql_order == null) return false;
            var pdics = mysql_order.GetType().GetProperties().ToDictionary(item => item.Name, item => item.GetValue(mysql_order));
            try
            {
                //调用service
                var ret = await Insert(mysql_order);
                return true;
            }
            catch (Exception e)
            {
                var message = new StringBuilder();
                message.Append("\r\n新增订单写入时数据库发生错误:\r\n" + "外部订单号：" + mysql_order.OUTSYS_ORDER_NO + "\r\n外部运单号：" + mysql_order.OUTSYS_BILL_CODE);
                message.Append("\r\n订单内容：<MySqlOrderData>" + JsonConvert.SerializeObject(mysql_order) + "</MySqlOrderData>");
                message.Append("\r\n" + e.Message + e.StackTrace);
                message.ToString().WriteToLog(LogerType.Error);
                return false;

            }

        }
        /// <summary>
        /// 映射关系
        /// </summary>
        /// <param name="taobaoOrderModel"></param>
        /// <param name="logisticsInterface"></param>
        /// <returns></returns>
        public T_MySql_Order CreateMap(AddOrder_HC addOrder_HC, string logisticsInterface)
        {
            try
            {
                var mysql_order = new T_MySql_Order
                {
                    OUTSYS_ORDER_NO = string.IsNullOrEmpty(addOrder_HC.logisticCode)
                            ? " "
                            : addOrder_HC.logisticCode,

                    OUTSYS_BILL_CODE = addOrder_HC.logisticCode,
                    ORDER_SOURCE = "HUICHONG",
                    MSG_TYPE = "JSON",
                    //mySqlOrder.CREATE_BY = "",
                    MSG_CONTENT = string.IsNullOrEmpty(logisticsInterface) ? "" : logisticsInterface,
                    IS_SYNC_SUCCESS = 0,
                    CREATE_TIME = DateTime.Now,
                    CREATE_BY = "HUICHONG",
                    C1 = "",
                    C2 = "",
                    C3 = "",
                    REMARK = ""
                };
                return mysql_order;
            }
            catch (Exception e)
            {
                var message = new StringBuilder();
                message.Append("TAOBAO订单映射过程发生错误：" + logisticsInterface);
                message.Append(e.Message + e.StackTrace);
                message.ToString().WriteToLog(LogerType.Error);
                return null;

            }
        }

        public Task<bool> InsertList(IList<T_MySql_Order> t_MySql_Orders)
        {
            throw new NotImplementedException();
        }
    }
}
