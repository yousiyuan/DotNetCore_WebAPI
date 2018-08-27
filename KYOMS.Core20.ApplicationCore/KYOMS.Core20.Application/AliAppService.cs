using KYOMS.Core20.Application.Interface;
using KYOMS.Core20.Common.Log4NetCore;
using KYOMS.Core20.Entity;
using KYOMS.Core20.Entity.MySqlDB;
using KYOMS.Core20.Entity.Oracle;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KYOMS.Core20.Application
{
    public class AliAppService : AppService
    {
        public AliAppService(string dbConfig, DbConfigType dbType)
            : base(dbConfig, dbType)
        {
            //Logger = new LoggerHandle(null, logfilename);
        }
        public AliAppService(string mySqlConfig = null, string oracleConfig = null, string sqlServerConfig = null)
            : base(mySqlConfig, oracleConfig, sqlServerConfig)
        {
            //Logger = new LoggerHandle(null, logfilename);
        }
        public override void Dispose()
        {
            //Logger = null;
            GC.SuppressFinalize(this);

            base.Dispose();
        }

        /// <summary>
        /// 写入到数据库Order-插入前检查是否重复，重复数据不插入，按天分表，表名动态传入
        /// </summary>
        /// <param name="mySqlOrder"></param>
        /// <returns></returns>
        public async Task<bool> Add(T_MySql_Order mySqlOrder)
        {
            try
            {
                var parameters = CreateMysqlOrderParams(mySqlOrder);
                var eftRows = await MysqlScope.Repository.InsertAsync<T_MySql_Order>("T_MySql_Order.Insert", parameters);
                return eftRows > 0;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                var message = new StringBuilder();
                message.Append("新增订单写入时数据库发生错误:\n" + "订单号：" + mySqlOrder.OUTSYS_BILL_CODE + "运单号：" + mySqlOrder.OUTSYS_ORDER_NO);
                message.Append(msg);
                msg = message.ToString();

                msg += string.Format("\n JSON:\n<MySqlOrderData>{0}</MySqlOrderData>\n", mySqlOrder.MSG_CONTENT);
                msg.WriteToLog(LogerType.Error);

                return false;
            }
        }

        /// <summary>
        /// 根据单号查询订单
        /// </summary>
        public Task<T_ORDER> QueryById(string logisticId)
        {
            return OracleScope.Repository.QuerySingleAsync<T_ORDER>("T_ORDER.FindByOrderNo",
                new {ORDER_NO = logisticId, OUTSYS_CODE = logisticId});
        }

        public async Task<string> QuerySiteName(string siteCode)
        {
            var result = await OracleScope.Repository.QueryScalarAsync<T_SITE_INFO>("T_SITE_INFO.GetName",
                new {SITE_CODE = siteCode});
            return result == null ? "" : result.ToString();
        }

        public Task<int> QueryCount(string billCode)
        {
            return OracleScope.Repository.CountAsync<T_WAYBILL>("T_WAYBILL.Count", new {BILL_CODE = billCode});
        }

        /// <summary>
        /// 更新订单
        /// </summary>
        public Task<int> UpdateOrder(T_ORDER order)
        {
            return OracleScope.Repository.UpdateAsync<T_ORDER>("T_ORDER.Update", order);
        }

        public Task<int> UpdateOrderByOutCode(T_ORDER order)
        {
            return OracleScope.Repository.UpdateAsync<T_ORDER>("T_ORDER.UpdateORDER_STATUSByOutCode",
                new
                {
                    order.ORDER_STATUS,
                    order.ORDER_CANCEL_TIME,
                    order.ORDER_CANCEL_REMARK,
                    order.ORDER_CANCEL_BY,
                    order.NEED_ADD_RECORD_BOS,
                    order.OUTSYS_CODE
                });
        }

        public Task<int> UpdateOrder(T_ORDER_WAYBILL_MAP order)
        {
            return OracleScope.Repository.UpdateAsync<T_ORDER_WAYBILL_MAP>("T_ORDER_WAYBILL_MAP.Update", order);
        }

        public async Task<string> QueryOrderNo(string mailno)
        {
            var result = await OracleScope.Repository.QueryScalarAsync<T_ORDER_WAYBILL_MAP>("T_ORDER_WAYBILL_MAP.GetOMSCode",
                new { Value = mailno });
            return result == null ? "" : result.ToString();
        }

        public List<T_ORDER_DELIVERY_TRACK> QueryOrderTrackList(string orderno)
        {
            return OracleScope.Repository.FindList<T_ORDER_DELIVERY_TRACK>("T_ORDER_DELIVERY_TRACK.FindByOrderNo",
                new {Value = orderno});
        }

        public Task<int> UpdateSite(T_ORDER order)
        {
            return OracleScope.Repository.UpdateAsync<T_ORDER>("T_ORDER.UpdateSite", order);
        }

        public Task<int> Add(T_NAVIGATE_DISTANCE distance)
        {
            return OracleScope.Repository.InsertAsync<T_NAVIGATE_DISTANCE>("T_NAVIGATE_DISTANCE.Insert", distance);
        }

        public Task<T_ORDER> QueryByOutSysNo(string outCode, string outId)
        {
            return OracleScope.Repository.QuerySingleAsync<T_ORDER>("T_ORDER.FindByOutSysNo",
                new {OUTSYS_CODE = outCode, OUTSYS_UID = outId});
        }

        public Task<T_WAYBILL> QueryWayBillByOrderNo(string orderno)
        {
            return OracleScope.Repository.QuerySingleAsync<T_WAYBILL>("T_WAYBILL.GetByOrderNo",
                new {ORDER_NO = orderno});
        }
    }
}
