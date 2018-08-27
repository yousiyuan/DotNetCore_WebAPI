using KYOMS.Core20.Application.Interface;
using KYOMS.Core20.Common.Extend;
using KYOMS.Core20.Common.LogCommon;
using KYOMS.Core20.DE.Model;
using KYOMS.Core20.Entity;
using KYOMS.Core20.Respository;
using System;
using System.Threading.Tasks;

namespace KYOMS.Core20.Application
{

    public class T_OrderService_HC : T_ORDERRepository<T_OrderService_HC>, IHC_ORDERService
    {
        //IHC_MySql_OrderService t_MySql_OrderService;
        CancelOrderHC cancelOrderHC = new CancelOrderHC();
        public async Task<string> CanlOrder(CancelOrderHC cancelOrder, string OrderNo)
        {
            T_ORDER order = new T_ORDER();
            var result = new HCResult();
            order = await GetOrderByOrderNo(cancelOrder.logisticCode);
            if (order == null)
            {
                return ResponseHandle(cancelOrder.logisticCode, false, 1001, "订单不存在", " 订单不存在");
            }
            if (order.ORDER_STATUS >= 30)
            {
                return ResponseHandle(cancelOrder.logisticCode, false, 1003, "该订单不允许撤销", " 订单已经接单或已经撤销");
            }
            var ret = CancelOrder(order).Result;
            if (ret > 0)
            {
                return ResponseHandle(cancelOrder.logisticCode, true, 1000, "撤销成功", " 撤销成功");
            }
            else
            {
                return ResponseHandle(cancelOrder.logisticCode, false, 9000, "系统数据库错误", " 系统数据库处理数据出错");
            }
        }

        public async Task<QueryOrderInfoToResponse> QueryOrderInfon(QueryOrder_HC queryOrder_HC)
        {
            var order = await GetOrderByOutSysCode(queryOrder_HC.logisticCode);


            if (order == null)
            {
                return null;
            }
            var dbOrder = await GetOrder(order.ORDER_NO);
            var dbOrderExt = await T_ORDER_EXT_HCFindByOrderNo(order.ORDER_NO);
            if (order == null)
            {
                return null;
            }
            QueryOrderInfoToResponse qoitr = new QueryOrderInfoToResponse();
            qoitr.sender = new Sender_HC();
            qoitr.receiver = new Receiver_HC();
            qoitr.logisticCode = dbOrder.OUTSYS_CODE;//	
            qoitr.logisticCompanyID = dbOrder.OUTSYS_UID;//
            qoitr.businessNetworkNo = dbOrder.PICKUP_SITECODE;//	dbOrder. = hcOrder.,<!-发货营业网点编号--> 
            qoitr.toNetworkNo = dbOrder.SENDTO_SITECODE;//	dbOrder. = hcOrder.,<!-收货营业网点编号--> 
            qoitr.gmtCommit = dbOrder.OUTSYS_ORDER_CREATEDATE.ToString();//	:1416997587000, <!-下单时间--> 
            qoitr.scheduleTime = dbOrder.RESERVE_PICKUP_BEGINTIME;//	 :1416997587000,<!-预约时间--> 
            qoitr.cargoName = dbOrder.CARGO_NAME;//	衣服dbOrder. = hcOrder.,<!-货物名称--> 
            qoitr.totalNumber = dbOrder.TOTAL_NUMBER.ToString();//	 :10, <!-总数量--> 
            qoitr.totalVolume = dbOrder.TOTAL_VOLUME.ToString();//	 :3, <!-总体积--> 
            qoitr.totalWeight = dbOrder.TOTAL_WEIGHT.ToString();//	 :20, <!-总重量--> 
            qoitr.weightRate = dbOrder.WEIGHT_RATE.ToString();//	 :1.5,<!-重货单价--> 
            qoitr.volumeRate = dbOrder.VOLUME_RATE.ToString();//	 :300, <!-轻货单价--> 
            qoitr.leastExpenses = dbOrder.LEAST_EXPENSES.ToString();//	:110, <!-最低一票--> 
            qoitr.transportType = dbOrder.TRANSPORT_TYPE;//	QC_JZKHdbOrder. = hcOrder., <!-运输方式类型--> 
            qoitr.vistReceive = dbOrder.VISIT_RECEIVE;//	NdbOrder. = hcOrder.,<!-上门接货类型--> 
            qoitr.vistReceivePrice = dbOrder.VISIT_RECEIVEPRICE.ToString();//	 :0.0, <!-上门接货费用  --> 
            qoitr.deliveryType = dbOrder.DELIVERY_TYPE;//	网点自提dbOrder. = hcOrder.,<!-送货方式  --> 
            qoitr.deliveryPrice = dbOrder.VISIT_RECEIVEPRICE.ToString();//	 :0.0, <!-送货方式费用  --> 
            if (dbOrder.IS_INSURED_PRICE == 1)//	BXdbOrder. = hcOrder., <!-保价类型：保险 BX 或 BJ -->
            {
                qoitr.insuranceType = "BJ";
            }
            else
            {
                qoitr.insuranceType = "BX";
            }
            qoitr.insuranceValue = dbOrder.INSURANCE_VALUE.ToString();//	 :0.0, <!-保价金额  --> 
            qoitr.insurancePrice = dbOrder.INSURANCE_PRICE.ToString();//	 :0.0, <!-保价费用--> 
            qoitr.waitNotifySend = dbOrder.WAIT_NOTIFYSEND;//	NdbOrder. = hcOrder., <!-等通知发货--> 
            qoitr.waitNotifySendPrice = dbOrder.WAIT_NOTIFYSENDPRICE.ToString();//	 :0.0, <!-等通知发货费用--> 
            qoitr.waitNotifyDelivery = dbOrder.DELIVERY_NOTICE.ToString();//	NdbOrder. = hcOrder., <!-等通知送货--> 
            qoitr.codType = dbOrder.COD_TYPE.ToString();//	0dbOrder. = hcOrder., <!-代收货款类型  --> 
            qoitr.codValue = dbOrder.COD_VALUE.ToString();//	 :0.0, <!-代收货款金额  --> 
            qoitr.codPrice = dbOrder.COD_PRICE.ToString();//	 :0.0, <!-代收货款费用--> 
            qoitr.fuelSurcharge = dbOrder.FUEL_SURCHARGE;//	NdbOrder. = hcOrder., <!-是否燃油附加费--> 
            qoitr.fuelSurchargePrice = dbOrder.FUEL_SURCHARGE_PRICE.ToString();//	 :0.0, <!-燃油附加费--> 
            qoitr.packageService = dbOrder.PACKAGE_SERVICE;//	NdbOrder. = hcOrder.,<!-是否包装--> 
            qoitr.packageServicePrice = dbOrder.PACKAGE_SERVICEPRICE.ToString();//	 :0.0,<!-包装费用--> 
            qoitr.smsNotify = dbOrder.SMS_NOTIFY;//	NdbOrder. = hcOrder.,<!-是否短信通知--> 
            qoitr.smsNotifyPrice = dbOrder.SMS_NOTIFYPRICE.ToString();//	 :0.0, <!-短信通知费用--> 
            qoitr.backSignBill = dbOrder.BACKSIGNBILL;//	NdbOrder. = hcOrder., <!-是否签收回单--> 
            qoitr.backSignBillPrice = dbOrder.BACKSIGNBILL_PRICE.ToString();//	 :0.0, <!-签收回单费用-->             
            qoitr.totalprice = dbOrder.TOTAL_PRICE.ToString();//	 :1000, <!-订单总价--> 
            qoitr.payType = dbOrder.OUTSYS_PAYTYPE;//	0dbOrder. = hcOrder.,<!-支付方式--> 
            qoitr.promotionRule = dbOrder.PROMOTION;//	dbOrder. = hcOrder.,<!-促销规则  --> 
            qoitr.remark = dbOrder.REMARK;//	轻拿轻放dbOrder. = hcOrder.,<!-注意事项--> 
            qoitr.sender.companyName = dbOrder.SENDER_COMPANYNAME;//	
            qoitr.sender.name = dbOrder.SENDER_NAME;//	
            qoitr.sender.postCode = dbOrder.SENDER_POSTCODE;//	
            qoitr.sender.mobile = dbOrder.SENDER_MOBILE;//	
            qoitr.sender.phone = dbOrder.SENDER_PHONE;//	
            qoitr.sender.address = dbOrder.SENDER_ADDRESS;//	
            qoitr.sender.province = dbOrder.SENDER_PROVINCE;//	
            qoitr.sender.city = dbOrder.SENDER_CITY;//	
            qoitr.sender.county = dbOrder.SENDER_COUNTY;//	
            qoitr.receiver.companyName = dbOrder.RECEIVER_COMPANYNAME;//	
            qoitr.receiver.name = dbOrder.RECEIVER_NAME;//	
            qoitr.receiver.postCode = dbOrder.RECEIVER_POSTCODE;//	
            qoitr.receiver.mobile = dbOrder.RECEIVER_MOBILE;//	
            qoitr.receiver.phone = dbOrder.RECEIVER_PHONE;//	
            qoitr.receiver.address = dbOrder.RECEIVER_ADDRESS;//	
            qoitr.receiver.province = dbOrder.RECEIVER_PROVINCE;//	
            qoitr.receiver.city = dbOrder.RECEIVER_CITY;//	
            qoitr.receiver.county = dbOrder.RECEIVER_COUNTY;//	
            qoitr.logisticCompanyID = dbOrder.CREATE_BY;//

            //以下字段来自扩展表
            if (dbOrderExt != null)
            {
                qoitr.transportName = dbOrderExt.TRANSPORT_NAME;//	汽车-精准卡航dbOrder. = hcOrder., <!-运输方式名称--> 
                qoitr.inspection = dbOrderExt.INSPECTION;//	NdbOrder. = hcOrder., <!-是否开箱验货--> 
                qoitr.inspectionPrice = dbOrderExt.INSPECTION_PRICE.ToString();//	 :0.0, <!-开箱验货费用--> 
                qoitr.havePayPrice = dbOrderExt.HAVE_PAY_PRICE.ToString();//	 :0, <!-已付款金额--> 
                qoitr.freightRateDiscount = dbOrderExt.FREIGH_TRATE_DISCOUNT.ToString();//	 :0, <!-运价折扣--> 
                qoitr.noSalePrice = dbOrderExt.NO_SALE_PRICE.ToString();//	 :900, <!-优惠前总费用--> 
                qoitr.orderSubtract = dbOrderExt.ORDER_SUBTRACT.ToString();//	 :0, <!-全单直减费用--> 
                qoitr.payTypePrice = dbOrderExt.PAY_TYPE_PRICE.ToString();//	 :0.0,<!-支付方式,只有到付时才会有费用--> 
                qoitr.waitNotifyDeliveryPrice = dbOrderExt.DELIVERY_NOTICE_PRICE.ToString();//	 :0.0, <!-等通知送货费用--> 
                qoitr.receiver.area = dbOrderExt.RECEIVER_AREA;//	
                qoitr.sender.area = dbOrderExt.SENDER_AREA;//	
                qoitr.logisticCompanyID = dbOrderExt.CREATE_BY;//	
            }
            //查询订单信息
            qoitr.statusType = GetOrderStatusByCode(dbOrder.ORDER_STATUS);
            qoitr.mailNo = dbOrder.BILL_NO;
            qoitr.gmtUpdated = (dbOrder.UPDATE_TIME.ToString());
            return qoitr;
        }
        public static string GetOrderStatusByCode(decimal orderStatus)
        {
            var status = orderStatus;
            var statusType = "";
            switch (status)
            {
                case 10: statusType = "WAITACCEPT"; break;
                case 20: statusType = "ACCEPT"; break;
                case 30: statusType = "CANCELLED"; break;
                //慧聪不存在已接单的场景，与之接近的是已受理
                case 40: statusType = "ACCEPT"; break;
                case 45: statusType = "UNACCEPT"; break;
                case 50: statusType = "GOT"; break;
                case 60: statusType = "NOGET"; break;
                case 70: statusType = "SIGNSUCCESS"; break;
                //慧聪不存在已回单的情况，但回单一定是签收成功
                case 80: statusType = "SIGNSUCCESS"; break;
                case 90: statusType = "SIGNFAILED"; break;
                default: break;

            }
            return statusType;
        }
        private string ResponseHandle(string logisticCode, bool result, int resultCode, string resultInfo = "", string reason = "", LogHandle.LogerType logType = LogHandle.LogerType.Error)
        {
            try
            {
                var results = new HCResult();
                results.logisticCode = logisticCode;
                results.result = result;
                results.resultCode = resultCode;
                results.resultInfo = resultInfo;
                results.reason = reason;
                var jsonRet = results.ToJson();
                //t_MySql_OrderService.Logger.Set("返回内容：" + result + jsonRet, logType);
                return jsonRet;

            }
            catch (Exception ex)
            {

                throw;
            }


        }

        private string ResponseHandles(QueryOrderInfoToResponse ss, bool result, int resultCode, string resultInfo = "", string reason = "")
        {
            try
            {
                QueryOrderInfoResult queryOrderInfo = new QueryOrderInfoResult();
                queryOrderInfo.responseParam = ss;
                queryOrderInfo.result = result;
                queryOrderInfo.resultCode = resultCode;
                queryOrderInfo.resultInfo = resultInfo;
                queryOrderInfo.reason = reason;
                var jsonRet = queryOrderInfo.ToJson();
                return jsonRet;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
