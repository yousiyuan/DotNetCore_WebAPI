using KYOMS.Core20.Application.Interface;
using KYOMS.Core20.Common.Log4NetCore;
using KYOMS.Core20.Common.LogCommon;
using KYOMS.Core20.DE.Model;
using KYOMS.Core20.Entity;
using KYOMS.Core20.Respository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace KYOMS.Core20.Application
{
    
    public class OracleTaoBaoService : T_ORDERRepository<OracleTaoBaoService>, IOracleTaoBaoService
    {
        static OrderStatusTraceRespository<OracleTaoBaoService> orderStatusTraceRespository = new OrderStatusTraceRespository<OracleTaoBaoService>();
        #region 淘宝改单或者取消订单
        /// <summary>
        /// 更新订单-包含取消订单和更新订单号码两个动作
        /// </summary>
        /// <param name="orderModel"></param>
        /// <returns></returns>
        public async Task<ResponseResult> EditOrder(UpdateTaobaoOrderModel orderModel, string CpCode)
        {
            ResponseResult responseResult = new ResponseResult();
            responseResult.logisticProviderID = CpCode;
            responseResult.responseItems = new List<Response>();

            var fieldList = orderModel.fieldList;
            foreach (var f in fieldList)
            {
                switch (f.fieldName)
                {
                    case "mailNo": //更新运单号
                        responseResult.responseItems.Add(await this.UpdateMailNo(f));
                        break;
                    case "weight":
                        break;
                    case "status": //取消订单
                        responseResult.responseItems.Add(await this.CancelOrder(f));
                        break;
                    case "exception":
                        break;
                    case "suspect":
                        break;
                }
            }
            return responseResult;
        }

        #region 取消一笔订单
        /// <summary>
        /// 取消一笔订单
        /// 现在的规则是：根据运单号，检查运单表，如果运单表有数据，则允许取消订单；否则允许取消订单
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        private async Task<Response> CancelOrder(field fields)
        {
            /* status（订单状态）字段可选值：
            名称	说明
            WITHDRAW	取消订单
            ACCEPT	接单成功
            UNACCEPT	接单失败
            NOT_SEND	揽收失败*/
            try
            {
                if (string.IsNullOrEmpty(fields.fieldValue)) return null;
                //只处理取消订单--其他的有字段值有 WITHDRAW
                if (fields.fieldValue != "WITHDRAW") return null;

                //获取订单号
                var txLogisticId = fields.txLogisticID;

                //获取备注
                string remark = fields.remark;

                var response = new Response();
                response.txLogisticID = txLogisticId;

                response =await HandleOrder(txLogisticId, response, remark);
                return response;
            }
            catch (Exception e)
            {
                var message = new StringBuilder();
                message.Append("取消一笔订单时发生错误,订单号：" + fields.txLogisticID);
                message.Append(e.Message + e.StackTrace);
                message.ToString().WriteToLog(LogerType.Error);
                return null;
            }
        }

        /// <summary>
        /// 获取订单状态，根据特定情况判断订单是否能取消
        /// </summary>
        /// <param name="txLogisticId"></param>
        /// <param name="response"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        private async Task<Response> HandleOrder(string txLogisticId, Response response, string remark)
        {
            response =await Cancel(txLogisticId, remark);
            return response;
        }

        /// <summary>
        /// 撤销订单
        /// </summary>
        /// <param name="txLogisticId"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        private async Task<Response> Cancel(string txLogisticId, string remark)
        {
            var response = new Response();
            var order = new T_ORDER();

            //查询订单
            order =await GetOrderByOutSysNo(txLogisticId);
            if (order == null)
            {
                response.success = false;
                response.reason = "订单不存在";
                return response;
            }

            //order.OUTSYS_CODE = txLogisticId;
            //order.ORDER_STATUS = 30;
            //order.ORDER_CANCEL_TIME = DateTime.Now;
            order.ORDER_CANCEL_REMARK = remark;
            //order.NEED_ADD_RECORD_BOS = 1;

            var ret =await CancelOrderByOrderNo(order);

            if (ret > 0)
            {
                response.success = true;
                response.reason = "订单撤销成功";
            }
            else
                switch (ret)
                {
                    case 0:
                        response.success = false;
                        response.reason = "订单撤销失败或此单不存在";
                        break;
                    case -2:
                        response.success = false;
                        response.reason = "此单号已接单,不允许取消操作";
                        break;
                }
            return response;
        }
        #endregion

        #region 更新一笔订单

        private async Task<Response> UpdateMailNo(field fields)
        {
            var response = new Response();
            response.fieldName = "mailNo";
            //response.fieldName = "status";

            //获取订单号
            var txLogisticId = fields.txLogisticID;
            //获取运单号
            var mailNo = fields.fieldValue;
            //获取备注
            var remark = fields.remark;

            response.txLogisticID = txLogisticId;

            if (string.IsNullOrEmpty(fields.fieldValue))
            {
                response.success = false;
                response.reason = "错误，要更新的运单号为空！";
                return response;
            }

            var order =await GetOrderByOutSysNo(txLogisticId);
            if (order == null)
            {
                response.success = false;
                response.reason = "错误，查询不到该订单！";
                return response;
            }
            var result =await CheckOrderStatusTrace(order.ORDER_STATUS, order.BILL_NO);
            long ret = 0;
            if (result.success)//如果可以更新，则执行更新操作
            {
             
                ret =await UpdateMailNo(order,mailNo);
              
            }
            if (ret > 0)
            {
                response.success = true;
                response.reason = "面单号更新成功！";
            }
            else
            {
                response.success = false;
                response.reason = result.reason;
            }
            return response;
        }

        /// <summary>
        /// 检查订单记录变更表
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        private async Task<Response> CheckOrderStatusTrace(string orderNo)
        {
            Response response = new Response();
            var result =await FindByOrderNo(orderNo);
            if (result == null)
            {
                response.success = true;
                response.reason = "此单号允许操作";
                return response;
            }
            if (result.CHANGED_STATUS == 70)
            {
                response.success = false;
                response.reason = "此单号已签收,不允许操作";
                return response;
            }

            response.success = true;
            response.reason = "此单号允许操作";
            return response;
        }
        /// <summary>
        /// 检查订单记录变更表
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        private async Task<Response> CheckOrderStatusTrace(decimal status, string billNo)
        {
            Response response = new Response();
            var count =await GetWayBillCount(billNo);
            if (status == 70 && count > 0)
            {
                response.success = false;
                response.reason = "此单号不允许操作";
                return response;
            }

            if (status != 70 && status >= 46)
            {
                response.success = false;
                response.reason = "此单号不允许操作";
                return response;
            }
            response.success = true;
            response.reason = "此单号允许操作";
            return response;
        }
        #endregion

        #endregion

        public async Task<T_ORDER_STATUS_TRACE> FindByOrderNo(string orderNo)
        {
            return await orderStatusTraceRespository.findByOrderNo(orderNo);
        }

    }
}
