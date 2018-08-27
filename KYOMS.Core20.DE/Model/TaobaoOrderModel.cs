using System;
using System.Collections.Generic;

namespace KYOMS.Core20.DE.Model
{
    /// <summary>
    /// TAOBAO订单基本信息--模型设计参考文档-【淘宝接口--物流平台-物流公司接口文档1.4.2】
    /// </summary>
    public class TaobaoOrderModelBase
    {
        /// <summary>
        /// 电商标识（TAOBAO）[必选]
        /// </summary>
        public string ecCompanyId { get; set; }

        /// <summary>
        /// 物流公司ID[必选]
        /// </summary>
        public string logisticProviderID { get; set; }

        /// <summary>
        /// 客户标识（如为仓配订单，该字段为仓编码）[可选]
        /// </summary>
        public string customerId { get; set; }

        /// <summary>
        /// 物流订单号[必选]
        /// </summary>
        public string txLogisticID { get; set; }

        /// <summary>
        /// 业务交易号[可选]
        /// </summary>
        public string tradeNo { get; set; }

        /// <summary>
        /// 物流运单号[可选]
        /// </summary>
        public string mailNo { get; set; }

        /// <summary>
        /// 订单类型(0-COD  1-普通订单 3 - 退货单)[必选]
        /// </summary>
        public int orderType { get; set; }

        /// <summary>
        /// 服务类型[必选](0-自己联系 1-在线下单（上门揽收）4-限时物流 8-快捷COD 10-我要寄快递 16-快递保障 26-预约配送5-仓配订单 12-预约寄件
        /// </summary>
        public int serviceType { get; set; }

        /// <summary>
        /// 物流公司上门取货时间段，[可选]通过“yyyy-MM-dd HH:mm:ss[注意,时间格式和以前的版本不一样]”格式化，本文中所有时间格式相同。
        /// </summary>
        public DateTime sendStartTime { get; set; }

        /// <summary>
        /// 物流公司上门取货时间段，[可选]通过“yyyy-MM-dd HH:mm:ss[注意,时间格式和以前的版本不一样]”格式化，本文中所有时间格式相同。
        /// </summary>
        public DateTime sendEndTime { get; set; }

        /// <summary>
        /// 商品金额，包括优惠和运费，但无服务费；(单位：分)	long[必选]
        /// </summary>
        public decimal goodsValue { get; set; }

        /// <summary>
        /// 支付宝金额/代收货款金额，商品金额+服务费；(单位：分)	long[必选]
        /// </summary>
        public decimal itemsValue { get; set; }

        /// <summary>
        /// 商品类型（保留字段，暂时不用）[可选]
        /// </summary>
        public int special { get; set; }

        /// <summary>
        /// 备注[可选]
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 总服务费[COD]；(单位：分)	long[可选]
        /// </summary>
        //public decimal totalServiceFee { get; set; }
        public string totalServiceFee { get; set; }

        /// <summary>
        /// 买家服务费[COD]；(单位：分)[现在不做COD][可选]
        /// </summary>
        //public decimal buyServiceFee { get; set; }
        public string buyServiceFee { get; set; }

        /// <summary>
        /// 物流公司分润[COD]；(单位：分)	long[可选]
        /// </summary>
        //public decimal codSplitFee { get; set; }
        public string codSplitFee { get; set; }
    }

    /// <summary>
    /// 落地的订单模型
    /// </summary>
    public class TaobaoOrderModel : TaobaoOrderModelBase
    {
        /// <summary>
        /// 发货联系人信息（sender）[必选]
        /// </summary>
        public ContactInfo sender { get; set; }

        /// <summary>
        /// 收货联系人信息（receiver）[必选]
        /// </summary>
        public ContactInfo receiver { get; set; }

        /// <summary>
        /// 商品信息[必选]
        /// </summary>
        public List<item> items { get; set; }
    }

    /// <summary>
    /// 存储到Mysql中订单模型，与落地的模型区别是，三个对象序列化为json格式
    /// </summary>
    public class TaobaoOrderMysql : TaobaoOrderModelBase
    {
        /// <summary>
        /// 发货联系人信息（sender）[必选]
        /// </summary>
        public string sender { get; set; }

        /// <summary>
        /// 收货联系人信息（receiver）[必选]
        /// </summary>
        public string receiver { get; set; }

        /// <summary>
        /// 商品信息[必选]
        /// </summary>
        public string items { get; set; }
    }

    /// <summary>
    /// 联系人信息（必选）
    /// </summary>
    public class ContactInfo
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>
        public string postCode { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string phone { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string mobile { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        public string prov { get; set; }
        /// <summary>
        /// 市-用户所在市县（区），市区中间用“,”分隔；注意有些市下面是没有区[必选]
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 区
        /// </summary>
        //public string county { get; set; }
        /// <summary>
        /// 用户详细地址
        /// </summary>
        public string address { get; set; }
        
        /// <summary>
        /// 街道
        /// </summary>
        //public string receiverTown { get; set; }

        /// <summary>
        /// 联系人编码，如为仓库，可填写仓库编码，如为普通用户，可填写用户id
        /// </summary>
        //public string customerId { get; set; }
    }

    /// <summary>
    /// 商品信息（items）（可选）
    /// </summary>
    public class item
    {
        /// <summary>
        /// 商品名称[必选]
        /// </summary>
        public string itemName { get; set; }

        /// <summary>
        /// 商品数量[必选]
        /// </summary>
        public int number { get; set; }

        /// <summary>
        /// 商品单价（单位：分）[必选]
        /// </summary>
        public decimal itemValue { get; set; }
    }

    #region 5.1.1订单创建结果返回和5.1.2 订单更新结果返回

    /// <summary>
    /// 返回结果
    /// </summary>
    public class ResponseResult
    {
        /// <summary>
        /// 物流公司编号
        /// </summary>
        public string logisticProviderID { get; set; }

        /// <summary>
        /// 错误代码
        /// </summary>
        public List<Response> responseItems { get; set; }
    }

    /// <summary>
    /// 返回结果详细信息（response）
    /// </summary>
    public class Response
    {
        /// <summary>
        /// 物流编号
        /// </summary>
        public string txLogisticID { get; set; }

        /// <summary>
        /// 字段名称
        /// </summary>
        public string fieldName { get; set; }

        /// <summary>
        /// 是否成功 
        /// </summary>
        public bool success { get; set; }

        /// <summary>
        /// 失败原因 
        /// </summary>
        public string reason { get; set; }
    }

    #endregion

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