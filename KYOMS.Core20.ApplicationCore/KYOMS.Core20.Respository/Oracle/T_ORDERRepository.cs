using KYOMS.Core20.Common.Database;
using KYOMS.Core20.Common.Extend;
using KYOMS.Core20.Common.Log4NetCore;
using KYOMS.Core20.Common.LogCommon;
using KYOMS.Core20.Entity;
using KYOMS.Core20.Respository.Core;
using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace KYOMS.Core20.Respository
{
    public class T_ORDERRepository<T> : BaseRepository<T>
    {
        private string _scope = "T_ORDER";

        public T_ORDERRepository() : base(typeof(OracleMapper))
        {

        }
        public async Task<T_ORDER> GetOrderByOutSysNo(string OutSysNo)
        {
            try
            {
                return await QueryFirstAsync<T_ORDER>(_scope, new { OUTSYS_CODE = OutSysNo }, "GetOrderByOutSysNo");
            }
            catch (Exception e)
            {
                var message = new StringBuilder();
                message.Append("查询订单时，数据库发生错误:\n" + "订单号：" + OutSysNo);
                message.Append("订单表内容：" + OutSysNo);
                message.Append(e.Message + e.StackTrace);
                message.ToString().WriteToLog(LogerType.Error);
                return null;
            }
        }
        public async Task<int> CancelOrderByOrderNo(T_ORDER order)
        {
            try
            {
                var count = await GetWayBillCount(order.BILL_NO);
                var ret = 0;
                if (count > 0)
                {
                    ret = -2;
                }
                else
                {
                    ret = await base.UpdateAsync(_scope, new { ORDER_CANCEL_REMARK = order.ORDER_CANCEL_REMARK, OUTSYS_CODE = order.OUTSYS_CODE }, "Cancel");
                }
                return ret;
            }
            catch (Exception e)
            {
                var message = new StringBuilder();
                message.Append("取消订单时，数据库发生错误:\n" + "订单号：" + order.ORDER_NO);
                message.Append("订单表内容：" + order.ToJson());
                message.Append(e.Message + e.StackTrace);
                message.ToString().WriteToLog(LogerType.Error);
                return 0;
            }
        }
        public async Task<int> UpdateMailNo(T_ORDER order, string mailNo)
        {
            try
            {
                sqlMapper.BeginTransaction();
                order.BILL_NO = mailNo;
                int rows = await base.UpdateAsync("T_ORDER_WAYBILL_MAP", new { MAIL_NO = mailNo, ORDER_NO = order.ORDER_NO });
                int ret = await base.UpdateAsync(_scope, new { BILL_NO = order.BILL_NO, REMARK2 = order.REMARK2, OUTSYS_CODE = order.OUTSYS_CODE }, "UpdateMailNo");
                sqlMapper.CommitTransaction();
                return ret;
            }
            catch (Exception e)
            {
                sqlMapper.RollbackTransaction();
                var message = new StringBuilder();
                message.Append("更新订单的运单号时，数据库发生错误:\n" + "订单号：" + order.ORDER_NO);
                message.Append(e.Message + e.StackTrace);
                message.ToString().WriteToLog(LogerType.Error);
                return 0;
            }
        }
        public async Task<int> GetWayBillCount(string billNo)
        {
            return await QueryFirstAsync<int>("T_WAYBILL", new { BILL_CODE = billNo }, "Count");
        }
        /// <summary>
        /// 查询订单详情
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public async Task<T_ORDER> GetOrderByOrderNo(string OrderNo)
        {
            try
            {
                return await QueryFirstAsync<T_ORDER>(_scope, new { ORDER_NO = OrderNo }, "GetOrderByOrderNo");
            }
            catch (Exception e)
            {
                var message = new StringBuilder();
                message.Append("查询订单时，数据库发生错误:\n" + "订单号：" + OrderNo);
                message.Append("订单表内容：" + OrderNo);
                message.Append(e.Message + e.StackTrace);
                message.ToString().WriteToLog(LogerType.Error);
                return null;
            }
        }
        /// <summary>
        /// 撤销订单
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public async Task<int> CancelOrder(T_ORDER order)
        {
            try
            {
                var ret = 0;
                ret = await base.UpdateAsync(_scope, new { ORDER_CANCEL_REMARK = order.ORDER_CANCEL_REMARK, OUTSYS_CODE = order.OUTSYS_CODE }, "Cancel");
                return ret;
            }
            catch (Exception e)
            {
                var message = new StringBuilder();
                message.Append("取消订单时，数据库发生错误:\n" + "订单号：" + order.ORDER_NO);
                message.Append("订单表内容：" + order.ToJson());
                message.Append(e.Message + e.StackTrace);
                message.ToString().WriteToLog(LogerType.Error);
                return 0;
            }

        }
        /// <summary>
        /// 根据外部订单号查询订单信息【慧聪】
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public async Task<T_ORDER> GetOrderByOutSysCode(string OrderNo)
        {
            try
            {
                return await QueryFirstAsync<T_ORDER>(_scope, new { OUTSYS_CODE = OrderNo }, "GetOrderByOutSysCode");
            }
            catch (Exception e)
            {
                var message = new StringBuilder();
                message.Append("查询订单时，数据库发生错误:\n" + "订单号：" + OrderNo);
                message.Append("订单表内容：" + OrderNo);
                message.Append(e.Message + e.StackTrace);
                message.ToString().WriteToLog(LogerType.Error);
                return null;
            }
        }
        /// <summary>
        /// 根据订单号查询订单信息【慧聪】
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public async Task<T_ORDER> GetOrder(string OrderNo)
        {
            try
            {
                return await QueryFirstAsync<T_ORDER>(_scope, new { ORDER_NO = OrderNo }, "GetOrderByOrderNo");
            }
            catch (Exception e)
            {
                var message = new StringBuilder();
                message.Append("查询订单时，数据库发生错误:\n" + "订单号：" + OrderNo);
                message.Append("订单表内容：" + OrderNo);
                message.Append(e.Message + e.StackTrace);
                message.ToString().WriteToLog(LogerType.Error);
                return null;
            }
        }
        /// <summary>
        /// 根据订单号查询慧聪扩展表信息【慧聪】
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public async Task<T_ORDER_EXT_HC> T_ORDER_EXT_HCFindByOrderNo(string OrderNo)
        {
            try
            {
                return await QueryFirstAsync<T_ORDER_EXT_HC>(_scope, new { ORDER_NO = OrderNo }, "T_ORDER_EXT_HCFindByOrderNo");

            }
            catch (Exception e)
            {
                var message = new StringBuilder();
                message.Append("查询订单时,数据库发生错误:\n" + "订单号:" + OrderNo);
                message.Append("订单号:" + OrderNo);
                message.Append(e.Message + e.StackTrace);
                message.ToString().WriteToLog(LogerType.Error);
                return null;
            }
        }
    }
}
