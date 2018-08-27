using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KYOMS.Core20.DE.AliModel
{
    public class Aorder
    {
        public Aorder() { }
        //提交订单必填参数
        private string _subOrderID, _deliveryType, _payType = "0", _transportType, _cargoName, _Sname, _Smobile = "0", _Sprovince = "", _Scity = "",
            _Saddress = "", _Rname, _Rmobile = "0", _Rphone, _Rprovince = "", _Rcity = "", _Raddress = "", _logisticID, _mailNo, _smsNotify, _fuelSurcharge
           , _logisticCompanyID, _serviceType, _attention;
        private DateTime _gmtCommit = DateTime.Now, _gmtCancel = DateTime.Now, _gmtUpdated = DateTime.Now;
        //可选参数
        private string _businessNetworkNo = "", _toNetworkNo = "", _materialCost = "", _vistReceive = "", _waitNotifySend = "", _packageService = "", _remark = "", _ScompanyName = "", _SpostCode = "", _Sphone = "", _Scounty = ""
            , _RcompanyName = "", _RpostCode = "", _Rcounty = "", _comments = "", _promotion = "", _aliUID = "", _memberType = "", _bizType = "", _codType = "", _backSignBill = "";
        private decimal _transportPrice, _materialCostPrice, _vistReceivePrice, _deliveryPrice, _insuranceValue, _insurancePrice, _backSignBillPrice, _waitNotifySendPrice, _packageServicePrice, _totalPrice, _codPrice
            , _otherPrice, _Weight, _Volume, _Price, _weightRate, _volumeRate, _smsNotifyPrice, _fuelSurchargePrice, _totalVolume, _totalWeight, _codValue, _leastExpenses;
        private int _Number, _totalNumber;
        /// <summary>
        /// 
        /// </summary>
        public string subOrderID
        {
            set { _subOrderID = value; }
            get { return _subOrderID; }
        }
        /// <summary>
        /// 送货方式
        /// </summary>
        public string deliveryType
        {
            set { _deliveryType = value; }
            get { return _deliveryType; }
        }
        /// <summary>
        /// 付款方式
        /// </summary>
        public string payType
        {
            set { _payType = value; }
            get { return _payType; }
        }

        /// <summary> 
        /// 运输方式
        /// </summary> 
        public string transportType
        {
            set { _transportType = value; }
            get { return _transportType; }
        }
        /// <summary> 
        /// 货物名称
        /// </summary> 
        public string cargoName
        {
            set { _cargoName = value; }
            get { return _cargoName; }
        }
        /// <summary> 
        /// 发件人
        /// </summary> 
        public string Sname
        {
            set { _Sname = value; }
            get { return _Sname; }
        }
        /// <summary> 
        /// 发件人手机
        /// </summary> 
        public string Smobile
        {
            set { _Smobile = value; }
            get { return _Smobile; }
        }
        /// <summary> 
        /// 发件人省
        /// </summary> 
        public string Sprovince
        {
            set { _Sprovince = value; }
            get { return _Sprovince; }
        }
        /// <summary> 
        /// 发件人市
        /// </summary> 
        public string Scity
        {
            set { _Scity = value; }
            get { return _Scity; }
        }
        /// <summary> 
        /// 发件人地址
        /// </summary> 
        public string Saddress
        {
            set { _Saddress = value; }
            get { return _Saddress; }
        }
        /// <summary> 
        /// 收件人
        /// </summary> 
        public string Rname
        {
            set { _Rname = value; }
            get { return _Rname; }
        }
        /// <summary> 
        /// 收件人手机
        /// </summary> 
        public string Rmobile
        {
            set { _Rmobile = value; }
            get { return _Rmobile; }
        }
        /// <summary> 
        /// 收件人座机
        /// </summary> 
        public string Rphone
        {
            set { _Rphone = value; }
            get { return _Rphone; }
        }
        /// <summary> 
        /// 收件人省
        /// </summary> 
        public string Rprovince
        {
            set { _Rprovince = value; }
            get { return _Rprovince; }
        }
        /// <summary> 
        /// 收件人市
        /// </summary> 
        public string Rcity
        {
            set { _Rcity = value; }
            get { return _Rcity; }
        }
        /// <summary> 
        /// 收件人地址
        /// </summary> 
        public string Raddress
        {
            set { _Raddress = value; }
            get { return _Raddress; }
        }
        /// <summary> 
        /// AL单号
        /// </summary> 
        public string logisticID
        {
            set { _logisticID = value; }
            get { return _logisticID; }
        }
        /// <summary> 
        /// 运单号
        /// </summary> 
        public string mailNo
        {
            set { _mailNo = value; }
            get { return _mailNo; }
        }
        /// <summary> 
        /// 短信通知
        /// </summary> 
        public string smsNotify
        {
            set { _smsNotify = value; }
            get { return _smsNotify; }
        }
        /// <summary> 
        /// 燃油附加
        /// </summary> 
        public string fuelSurcharge
        {
            set { _fuelSurcharge = value; }
            get { return _fuelSurcharge; }
        }

        /// <summary> 
        /// 客户编号
        /// </summary> 
        public string logisticCompanyID
        {
            set { _logisticCompanyID = value; }
            get { return _logisticCompanyID; }
        }
        /// <summary> 
        /// 服务类型
        /// </summary> 
        public string serviceType
        {
            set { _serviceType = value; }
            get { return _serviceType; }
        }
        /// <summary>
        /// 订单新建时间
        /// </summary>
        public DateTime gmtCommit
        {
            set { _gmtCommit = value; }
            get { return _gmtCommit; }
        }
        /// <summary>
        /// 订单取消时间
        /// </summary>
        public DateTime gmtCancel
        {
            set { _gmtCancel = value; }
            get { return _gmtCancel; }
        }
        /// <summary>
        /// 订单修改时间
        /// </summary>
        public DateTime gmtUpdated
        {
            set { _gmtUpdated = value; }
            get { return _gmtUpdated; }
        }

        /// <summary> 
        /// 发件网点编号
        /// </summary> 
        public string businessNetworkNo
        {
            set { _businessNetworkNo = value; }
            get { return _businessNetworkNo; }
        }
        /// <summary> 
        /// 收件网点编号
        /// </summary> 
        public string toNetworkNo
        {
            set { _toNetworkNo = value; }
            get { return _toNetworkNo; }
        }
        /// <summary> 
        /// 工本服务
        /// </summary> 
        public string materialCost
        {
            set { _materialCost = value; }
            get { return _materialCost; }
        }
        /// <summary> 
        /// 上门接货
        /// </summary> 
        public string vistReceive
        {
            set { _vistReceive = value; }
            get { return _vistReceive; }
        }
        /// <summary> 
        /// 等通知发货
        /// </summary> 
        public string waitNotifySend
        {
            set { _waitNotifySend = value; }
            get { return _waitNotifySend; }
        }
        /// <summary> 
        /// 包装费
        /// </summary> 
        public string packageService
        {
            set { _packageService = value; }
            get { return _packageService; }
        }
        /// <summary> 
        /// 注意事项
        /// </summary> 
        public string remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary> 
        /// 发件人公司
        /// </summary> 
        public string ScompanyName
        {
            set { _ScompanyName = value; }
            get { return _ScompanyName; }
        }
        /// <summary> 
        /// 发件人邮编
        /// </summary> 
        public string SpostCode
        {
            set { _SpostCode = value; }
            get { return _SpostCode; }
        }
        /// <summary> 
        /// 发件人座机
        /// </summary> 
        public string Sphone
        {
            set { _Sphone = value; }
            get { return _Sphone; }
        }
        /// <summary> 
        /// 发件人区县
        /// </summary> 
        public string Scounty
        {
            set { _Scounty = value; }
            get { return _Scounty; }
        }
        /// <summary>
        /// 收件公司
        /// </summary>
        public string RcompanyName
        {
            set { _RcompanyName = value; }
            get { return _RcompanyName; }
        }
        /// <summary> 
        /// 收件邮编
        /// </summary> 
        public string RpostCode
        {
            set { _RpostCode = value; }
            get { return _RpostCode; }
        }
        /// <summary> 
        /// 收件县区
        /// </summary> 
        public string Rcounty
        {
            set { _Rcounty = value; }
            get { return _Rcounty; }
        }
        /// <summary> 
        /// 
        /// </summary> 
        public string comments
        {
            set { _comments = value; }
            get { return _comments; }
        }
        /// <summary> 
        /// 优惠信息
        /// </summary> 
        public string promotion
        {
            set { _promotion = value; }
            get { return _promotion; }
        }
        /// <summary> 
        /// 阿里巴巴用户唯一ID号
        /// </summary> 
        public string aliUID
        {
            set { _aliUID = value; }
            get { return _aliUID; }
        }
        /// <summary> 
        /// 会员类型：诚信通，普通，MRO会员
        /// </summary> 
        public string memberType
        {
            set { _memberType = value; }
            get { return _memberType; }
        }
        /// <summary> 
        /// 业务类型:MRO用于MRO业务 POW:用于实力商家业务 空:普通业务
        /// </summary> 
        public string bizType
        {
            set { _bizType = value; }
            get { return _bizType; }
        }
        /// <summary> 
        /// 代收货款类型（三日还，即日还等）
        /// </summary> 
        public string codType
        {
            set { _codType = value; }
            get { return _codType; }
        }
        /// <summary> 
        /// 签收回单
        /// </summary> 
        public string backSignBill
        {
            set { _backSignBill = value; }
            get { return _backSignBill; }
        }

        /// <summary> 
        /// 运输费用
        /// </summary> 
        public decimal transportPrice
        {
            set { _transportPrice = value; }
            get { return _transportPrice; }
        }
        /// <summary> 
        /// 工本费
        /// </summary> 
        public decimal materialCostPrice
        {
            set { _materialCostPrice = value; }
            get { return _materialCostPrice; }
        }
        /// <summary> 
        /// 上门接货费
        /// </summary> 
        public decimal vistReceivePrice
        {
            set { _vistReceivePrice = value; }
            get { return _vistReceivePrice; }
        }
        /// <summary> 
        /// 送货费用
        /// </summary> 
        public decimal deliveryPrice
        {
            set { _deliveryPrice = value; }
            get { return _deliveryPrice; }
        }
        /// <summary> 
        /// 保价
        /// </summary> 
        public decimal insuranceValue
        {
            set { _insuranceValue = value; }
            get { return _insuranceValue; }
        }
        /// <summary> 
        /// 保价费
        /// </summary> 
        public decimal insurancePrice
        {
            set { _insurancePrice = value; }
            get { return _insurancePrice; }
        }
        /// <summary> 
        /// 签收回单费
        /// </summary> 
        public decimal backSignBillPrice
        {
            set { _backSignBillPrice = value; }
            get { return _backSignBillPrice; }
        }
        /// <summary> 
        /// 等通知发货费
        /// </summary> 
        public decimal waitNotifySendPrice
        {
            set { _waitNotifySendPrice = value; }
            get { return _waitNotifySendPrice; }
        }
        /// <summary> 
        /// 包装费
        /// </summary> 
        public decimal packageServicePrice
        {
            set { _packageServicePrice = value; }
            get { return _packageServicePrice; }
        }
        /// <summary> 
        /// 总价格
        /// </summary> 
        public decimal totalPrice
        {
            set { _totalPrice = value; }
            get { return _totalPrice; }
        }
        /// <summary> 
        /// 代收货款费
        /// </summary> 
        public decimal codPrice
        {
            set { _codPrice = value; }
            get { return _codPrice; }
        }
        /// <summary> 
        /// 其他费用
        /// </summary> 
        public decimal otherPrice
        {
            set { _otherPrice = value; }
            get { return _otherPrice; }
        }
        /// <summary>
        /// 注意事项
        /// </summary>
        public string attention
        {
            set { _attention = value; }
            get { return _attention; }
        }
        /// <summary> 
        /// 
        /// </summary> 
        //public decimal Weight
        //{
        //    set { _Weight = value; }
        //    get { return _Weight; }
        //}
        ///// <summary> 
        ///// 
        ///// </summary> 
        //public decimal Volume
        //{
        //    set { _Volume = value; }
        //    get { return _Volume; }
        //}
        ///// <summary> 
        ///// 
        ///// </summary> 
        //public decimal Price
        //{
        //    set { _Price = value; }
        //    get { return _Price; }
        //}
        /// <summary> 
        /// 重量费率（重量单价）
        /// </summary> 
        public decimal weightRate
        {
            set { _weightRate = value; }
            get { return _weightRate; }
        }
        /// <summary> 
        /// 体积费率（体积单价）
        /// </summary> 
        public decimal volumeRate
        {
            set { _volumeRate = value; }
            get { return _volumeRate; }
        }
        /// <summary> 
        /// 短信通知费
        /// </summary> 
        public decimal smsNotifyPrice
        {
            set { _smsNotifyPrice = value; }
            get { return _smsNotifyPrice; }
        }
        /// <summary> 
        /// 燃油附加费
        /// </summary> 
        public decimal fuelSurchargePrice
        {
            set { _fuelSurchargePrice = value; }
            get { return _fuelSurchargePrice; }
        }
        /// <summary> 
        /// 总体积，单位m3
        /// </summary> 
        public decimal totalVolume
        {
            set { _totalVolume = value; }
            get { return _totalVolume; }
        }
        /// <summary> 
        /// 总重量，单位：kg
        /// </summary> 
        public decimal totalWeight
        {
            set { _totalWeight = value; }
            get { return _totalWeight; }
        }
        /// <summary> 
        /// 代收货款
        /// </summary> 
        public decimal codValue
        {
            set { _codValue = value; }
            get { return _codValue; }
        }
        /// <summary> 
        /// 最低一票
        /// </summary> 
        public decimal leastExpenses
        {
            set { _leastExpenses = value; }
            get { return _leastExpenses; }
        }

        /// <summary> 
        /// 
        /// </summary> 
        //public int Number
        //{
        //    set { _Number = value; }
        //    get { return _Number; }
        //}
        /// <summary> 
        /// 总件数
        /// </summary> 
        public int totalNumber
        {
            set { _totalNumber = value; }
            get { return _totalNumber; }
        }
    }

    /// <summary>
    /// 地址匹配接口返回信息
    /// </summary>
    public class AddrResponse
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool success { get; set; }
        /// <summary>
        /// 错误代码
        /// </summary>
        public string errorCode { get; set; }
        /// <summary>
        /// 错误消息
        /// </summary>
        public string errorMsg { get; set; }
        /// <summary>
        /// 站点名称
        /// </summary>
        public string siteName { get; set; }
    }
}
