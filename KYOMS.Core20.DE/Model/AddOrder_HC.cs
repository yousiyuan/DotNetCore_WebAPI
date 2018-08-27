
namespace KYOMS.Core20.DE.Model
{

    public class AddOrder_HC
    {

        public string logisticCode { get; set; }//HC201411260626265030  ,<!-慧聪物流编号--> 
        public string logisticCompanyID { get; set; }//DBWULIU , <!-物流公司编号--> 
        public string businessNetworkNo { get; set; }// ,<!-发货营业网点编号--> 
        public string toNetworkNo { get; set; }//  ,<!-收货营业网点编号--> 
        public Sender sender { get; set; }//发送人信息
        public receiver receiver { get; set; }//接收人信息
        public string gmtCommit { get; set; }//:1416997587000, <!-下单时间--> 
        public string scheduleTime { get; set; }// :1416997587000,<!-预约时间--> 
        public string cargoName { get; set; }//衣服  ,<!-货物名称--> 
        public string totalNumber { get; set; }// :10, <!-总数量--> 
        public string totalVolume { get; set; }// :3, <!-总体积--> 
        public string totalWeight { get; set; }// :20, <!-总重量--> 
        public string weightRate { get; set; }// :1.5,<!-重货单价--> 
        public string volumeRate { get; set; }// :300, <!-轻货单价--> 
        public string leastExpenses { get; set; }//:110, <!-最低一票--> 
        public string transportType { get; set; }//QC_JZKH , <!-运输方式类型--> 
        public string transportName { get; set; }//汽车-精准卡航  , <!-运输方式名称--> 
        public string vistReceive { get; set; }//N  ,<!-上门接货类型--> 
        public string vistReceivePrice { get; set; }// :0.0, <!-上门接货费用  --> 
        public string deliveryType { get; set; }//网点自提  ,<!-送货方式  --> 
        public string deliveryPrice { get; set; }// :0.0, <!-送货方式费用  --> 
        public string insuranceType { get; set; }//BX  , <!-保价类型：保险 BX 或 BJ --> 
        public string insuranceValue { get; set; }// :0.0, <!-保价金额  --> 
        public string insurancePrice { get; set; }// :0.0, <!-保价费用--> 
        public string waitNotifySend { get; set; }//N g , <!-等通知发货--> 
        public string waitNotifySendPrice { get; set; }// :0.0, <!-等通知发货费用--> 
        public string waitNotifyDelivery { get; set; }//N  , <!-等通知送货--> 
        public string waitNotifyDeliveryPrice { get; set; }// :0.0, <!-等通知送货费用--> 
        public string codType { get; set; }//0  , <!-代收货款类型  --> 
        public string codValue { get; set; }// :0.0, <!-代收货款金额  --> 
        public string codPrice { get; set; }// :0.0, <!-代收货款费用--> 
        public string fuelSurcharge { get; set; }//N , <!-是否燃油附加费--> 
        public string fuelSurchargePrice { get; set; }// :0.0, <!-燃油附加费--> 
        public string packageService { get; set; }//N  ,<!-是否包装--> 
        public string packageServicePrice { get; set; }// :0.0,<!-包装费用--> 
        public string smsNotify { get; set; }//N ,<!-是否短信通知--> 
        public string smsNotifyPrice { get; set; }// :0.0, <!-短信通知费用--> 
        public string backSignBill { get; set; }//N , <!-是否签收回单--> 
        public string backSignBillPrice { get; set; }// :0.0, <!-签收回单费用--> 
        public string inspection { get; set; }//N , <!-是否开箱验货--> 
        public string inspectionPrice { get; set; }// :0.0, <!-开箱验货费用--> 
        public string havePayPrice { get; set; }// :0, <!-已付款金额--> 
        public string freightRateDiscount { get; set; }// :0, <!-运价折扣--> 
        public string totalprice { get; set; }// :1000, <!-订单总价--> 
        public string noSalePrice { get; set; }// :900, <!-优惠前总费用--> 
        public string orderSubtract { get; set; }// :0, <!-全单直减费用--> 
        public string payType { get; set; }//0 ,<!-支付方式--> 
        public string payTypePrice { get; set; }// :0.0,<!-支付方式,只有到付时才会有费用--> 
        public string promotionRule { get; set; }// ,<!-促销规则  --> 
        public string remark { get; set; }//轻拿轻放 ,<!-注意事项--> 
    }

    public class receiver
    {
        public string companyName { get; set; }//  , 
        public string name { get; set; }//  , 
        public string mobile { get; set; }//  , 
        public string phone { get; set; }//  , 
        public string address { get; set; }//  , 
        public string county { get; set; }//  , 
        public string province { get; set; }//  , 
        public string city { get; set; }//  , 
        public string area { get; set; }//   
        public string postCode { get; set; }//海淀区 
    }
}
