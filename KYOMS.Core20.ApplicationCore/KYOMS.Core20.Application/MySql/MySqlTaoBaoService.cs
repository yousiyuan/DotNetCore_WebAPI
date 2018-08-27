using KYOMS.Core20.Application.Interface;
using KYOMS.Core20.Common.Extend;
using KYOMS.Core20.Common.Log4NetCore;
using KYOMS.Core20.Common.LogCommon;
using KYOMS.Core20.DE.Model;
using KYOMS.Core20.Entity.MySqlDB;
using KYOMS.Core20.Respository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KYOMS.Core20.Application
{
    public class MySqlTaoBaoService : T_MySql_OrderRepository<MySqlTaoBaoService>, IMySqlTaoBaoService
    {

        public async Task<bool> AddOrder(TmsOrderModel taobaoOrderModel, string logisticsInterface)
        {
            var entity = CreateMap(taobaoOrderModel, logisticsInterface);
            if (entity == null) return false;

            entity.MSG_TYPE = "JSON";
            entity.IS_SYNC_SUCCESS = 0;
            entity.CREATE_TIME = DateTime.Now;
            //利用反射获取类的属性和值，并添加到Hashtable中//foreach (var item in entity.GetType().GetProperties()) { ht.Add(item.Name, item.GetValue(entity)); }
            var pdics = entity.GetType().GetProperties().ToDictionary(item => item.Name, item => item.GetValue(entity));
            try
            {
                return await Insert(entity);
            }
            catch (Exception e)
            {
                var message = new StringBuilder();
                message.Append("\r\n新增订单写入时数据库发生错误:\r\n" + "外部订单号：" + entity.OUTSYS_ORDER_NO + "\r\n外部运单号：" + entity.OUTSYS_BILL_CODE);
                message.Append("\r\n订单内容：<MySqlOrderData>" + JsonConvert.SerializeObject(entity) + "</MySqlOrderData>");
                message.Append("\r\n" + e.Message + e.StackTrace);
                message.ToString().WriteToLog(LogerType.Error);
                return false;
            }
        }

        public async Task<bool> InsertList(IList<T_MySql_Order> t_MySql_Orders)
        {
            return await Insert2(t_MySql_Orders);
        }
        /// <summary>
        /// 映射数据
        /// </summary>
        /// <param name="taobaoOrderModel"></param>
        /// <param name="logisticsInterface"></param>
        /// <returns></returns>
        private T_MySql_Order CreateMap(TmsOrderModel taobaoOrderModel, string logisticsInterface)
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
                    CREATE_BY = "TAOBAO_ONLINE",
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
                message.Append("TAOBAO订单映射过程发生错误：" + logisticsInterface);
                message.Append(e.Message + e.StackTrace);
                message.ToString().WriteToLog(LogerType.Error);
                return null;
            }
        }


        /// <summary>
        /// 添加订单
        /// </summary>
        /// <param name="taobaoOrderModel"></param>
        /// <param name="logisticsInterface"></param>
        /// <returns></returns>
        public async Task<bool> AddTaoBao(TaobaoOrderModel taobaoOrderModel)
        {
            var mySqlOrder = this.CreateMap(taobaoOrderModel);
            if (mySqlOrder == null) return false;
            var pdics = mySqlOrder.GetType().GetProperties().ToDictionary(item => item.Name, item => item.GetValue(mySqlOrder));
            var ret =await Insert(mySqlOrder);
            return ret;
        }

        /// <summary>
        /// 映射数据
        /// </summary>
        /// <param name="taobaoOrderModel"></param>
        /// <param name="logisticsInterface"></param>
        /// <returns></returns>
        private T_MySql_Order CreateMap(TaobaoOrderModel taobaoOrderModel)
        {
            try
            {
                var msgContent = taobaoOrderModel.ToJson();
                var mySqlOrder = new T_MySql_Order();
                mySqlOrder.OUTSYS_BILL_CODE = string.IsNullOrEmpty(taobaoOrderModel.mailNo) ? " " : taobaoOrderModel.mailNo;
                mySqlOrder.OUTSYS_ORDER_NO = string.IsNullOrEmpty(taobaoOrderModel.txLogisticID) ? " " : taobaoOrderModel.txLogisticID;
                mySqlOrder.ORDER_SOURCE = "TAOBAO";
                mySqlOrder.MSG_TYPE = "JSON";
                //mySqlOrder.CREATE_BY = "";
                mySqlOrder.MSG_CONTENT = string.IsNullOrEmpty(msgContent) ? "" : msgContent;
                mySqlOrder.IS_SYNC_SUCCESS = 0;
                mySqlOrder.CREATE_TIME = DateTime.Now;

                mySqlOrder.CREATE_BY = mySqlOrder.ORDER_SOURCE;
                mySqlOrder.C1 = "";
                mySqlOrder.C2 = "";
                mySqlOrder.C3 = "";
                mySqlOrder.REMARK = "";
                return mySqlOrder;
            }
            catch (Exception e)
            {
                var message = new StringBuilder();
                message.Append("TAOBAO订单映射过程发生错误：" + taobaoOrderModel.ToJson());
                message.Append(e.Message + e.StackTrace);
                message.ToString().WriteToLog(LogerType.Error);
                return null;
            }
        }
    }
}