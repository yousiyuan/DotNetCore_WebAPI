using System;
using System.Collections.Generic;

namespace KYOMS.Core20.DE.Model
{
    /// <summary>
    /// 首重信息
    /// </summary>
    public class BasicWeight
    {
        /// <summary>
        /// 首重价格
        /// </summary>
        public string fee { get; set; }
        /// <summary>
        /// 首重单位重量
        /// </summary>
        public string weight { get; set; }
    }
    /// <summary>
    /// 续重信息
    /// </summary>
    public class StepWeight
    {
        /// <summary>
        /// 续重价格
        /// </summary>
        public string fee { get; set; }
        /// <summary>
        /// 续重单位重量
        /// </summary>
        public string weight { get; set; }
    }
    /// <summary>
    /// 结算信息
    /// </summary>
    public class ChargingInfo
    {
        /// <summary>
        /// 首重信息
        /// </summary>
        public BasicWeight basicWeight { get; set; }
        /// <summary>
        /// 续重信息
        /// </summary>
        public StepWeight stepWeight { get; set; }
        /// <summary>
        /// 配送价格（一口价）
        /// </summary>
        public string totalFee { get; set; }
    }
    /// <summary>
    /// 扩展模型
    /// </summary>
    public class ExtendField
    {
        /// <summary>
        /// 扩展的key
        /// </summary>
        public string key { get; set; }
        /// <summary>
        /// 扩展字段value
        /// </summary>
        public string value { get; set; }
    }
    /// <summary>
    /// 小件员信息
    /// </summary>
    public class CourierInfo
    {
        /// <summary>
        /// 取件员所属公司
        /// </summary>
        public string company { get; set; }
        /// <summary>
        /// 取件员编号
        /// </summary>
        public string courierNo { get; set; }
        /// <summary>
        /// 取件员手机
        /// </summary>
        public string courierPhone { get; set; }
        /// <summary>
        /// 扩展列表
        /// </summary>
        public List<ExtendField> extendFields { get; set; }
        /// <summary>
        /// 取件人姓名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 取件员所属网点
        /// </summary>
        public string station { get; set; }
        /// <summary>
        /// 网点电话
        /// </summary>
        public string stationPhone { get; set; }
    }
    /// <summary>
    /// 作业操作要求，主要表达对作业操作的要求，类似SOP
    /// </summary>
    public class OpRequire
    {
        /// <summary>
        /// 作业操作描述
        /// </summary>
        public string opDesc { get; set; }
        /// <summary>
        /// 作业操作类型（TMQQD表示天猫全渠道的作业类型）
        /// </summary>
        public string opType { get; set; }
    }
    /// <summary>
    /// 订单属性模型
    /// </summary>
    public class OrderExtendField
    {
        /// <summary>
        /// 订单扩展属性key,已有:lbxCode(仓订单编号),cpStationId(驿站标),zbStatus(众包标),storeDeliveryTime(包裹出库时间),storeCode(仓库资源编码),prevWarehouseOrderCode(原仓发货单号),identificationStatus(运输物鉴定状态，逆向上门取退时，是商家鉴定的货品状态标示，是给日日顺判断货物状态是否符合取件标准，以及是否要准备封箱工具去上门取件的) 1:已质检、已封箱;2:未质检、外包装完好;3:未质检、未封箱;4:已质检、未封箱,allocateOrderStrategy(分单策略)1:表示下发的网点、小件员等信息只是一个推荐;2:表示快递公司不能依据自己的分单系统进行分单，必须强制按照推荐的网点和小件员进行作业
        /// </summary>
        public string key { get; set; }
        /// <summary>
        /// 订单扩展属性值
        /// </summary>
        public string value { get; set; }
    }
    /// <summary>
    /// 订单包裹商品信息
    /// </summary>
    public class ItemInfo
    {
        /// <summary>
        /// 扩展信息:is_precious:是否贵品;itemId:货品id;serviceFlag:值为long类型字符串,左起第32位为送装一体,从0开始;
        /// </summary>
        public string extendFields { get; set; }
        /// <summary>
        /// 保价金额,此商品明细的总保价金额,单位分
        /// </summary>
        public string insuredAmount { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string itemName { get; set; }
        /// <summary>
        /// 商品数量
        /// </summary>
        public string itemNum { get; set; }
    }
    /// <summary>
    /// 包裹信息
    /// </summary>
    public class PackageInfo
    {
        /// <summary>
        /// 包裹商品信息
        /// </summary>
        public List<ItemInfo> itemList { get; set; }
        /// <summary>
        /// 包裹高，不保证是整数，单位：毫米
        /// </summary>
        public string packageHeight { get; set; }
        /// <summary>
        /// 包裹长，不保证是整数，单位：毫米
        /// </summary>
        public string packageLength { get; set; }
        /// <summary>
        /// 包裹体积，不保证是整数，单位：立方厘米
        /// </summary>
        public string packageVolume { get; set; }
        /// <summary>
        /// 包裹重量，不保证是整数，单位：克
        /// </summary>
        public string packageWeight { get; set; }
        /// <summary>
        /// 包裹宽，不保证是整数，单位：毫米
        /// </summary>
        public string packageWidth { get; set; }
    }
    /// <summary>
    /// 收件人信息
    /// </summary>
    public class Receiver
    {
        /// <summary>
        /// 详细地址
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 市
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 区
        /// </summary>
        public string county { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string mobile { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 电话(手机和电话必须填一个)
        /// </summary>
        public string phone { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        public string province { get; set; }
        /// <summary>
        /// 街道四级地址ID
        /// </summary>
        public string receiverDivisionId { get; set; }
        /// <summary>
        /// 街道
        /// </summary>
        public string receiverTown { get; set; }
        /// <summary>
        /// 收件人userId
        /// </summary>
        public string userId { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>
        public string zipCode { get; set; }
    }
    /// <summary>
    /// 后一承运cp信息用于同后一段交接
    /// </summary>
    public class After
    {
        /// <summary>
        /// 交接详细地址
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 交接城市
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 交接人联系电话
        /// </summary>
        public string contactNumber { get; set; }
        /// <summary>
        /// 交接人
        /// </summary>
        public string contacts { get; set; }
        /// <summary>
        /// 交接所在区域
        /// </summary>
        public string county { get; set; }
        /// <summary>
        /// 前一承运cp编码
        /// </summary>
        public string cpCode { get; set; }
        /// <summary>
        /// 交接省份
        /// </summary>
        public string prov { get; set; }
        /// <summary>
        /// 交接所在街道
        /// </summary>
        public string town { get; set; }
    }
    /// <summary>
    /// 前一承运cp信息用于同前一段进行交接
    /// </summary>
    public class Before
    {
        /// <summary>
        /// 交接详细地址
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 交接所在的城市
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 交接联系人电话
        /// </summary>
        public string contactNumber { get; set; }
        /// <summary>
        /// 交接联系人
        /// </summary>
        public string contacts { get; set; }
        /// <summary>
        /// 交接所在的区
        /// </summary>
        public string county { get; set; }
        /// <summary>
        /// cp代码
        /// </summary>
        public string cpCode { get; set; }
        /// <summary>
        /// 交接所在省份
        /// </summary>
        public string prov { get; set; }
        /// <summary>
        /// 交接所在的街
        /// </summary>
        public string town { get; set; }
    }
    /// <summary>
    /// 路由信息
    /// </summary>
    public class RouteInfo
    {
        /// <summary>
        /// 后一承运cp信息用于同后一段交接
        /// </summary>
        public After after { get; set; }
        /// <summary>
        /// 前一承运cp信息用于同前一段进行交接
        /// </summary>
        public Before before { get; set; }
        /// <summary>
        /// 承运人编码CP内部需要
        /// </summary>
        public string carrierCode { get; set; }
        /// <summary>
        /// 路由扩展信息列表
        /// </summary>
        public List<ExtendField> extendFields { get; set; }
        /// <summary>
        /// 大头笔信息
        /// </summary>
        public string shortAddress { get; set; }
        /// <summary>
        /// 站点编码
        /// </summary>
        public string siteCode { get; set; }
        /// <summary>
        /// 站点名称
        /// </summary>
        public string siteName { get; set; }
    }
    /// <summary>
    /// 发件人信息
    /// </summary>
    public class Sender
    {
        /// <summary>
        /// 街道(已废弃,用senderTown)
        /// </summary>
        public string Town { get; set; }
        /// <summary>
        /// 详细地址
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 市
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 区
        /// </summary>
        public string county { get; set; }
        /// <summary>
        /// 发件人的发货方唯一编码，如果为淘系订单就为卖家的userId
        /// </summary>
        public string customerId { get; set; }
        /// <summary>
        /// 非淘系统订单商家编码,主要用于向cp下发结算账号
        /// </summary>
        public string customerNo { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string mobile { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 电话(手机和电话必须填一个)
        /// </summary>
        public string phone { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        public string province { get; set; }
        /// <summary>
        /// 发件人详细地址id（自定义）
        /// </summary>
        public string senderAddressId { get; set; }
        /// <summary>
        /// 街道四级地址ID
        /// </summary>
        public string senderDivisionId { get; set; }
        /// <summary>
        /// 街道
        /// </summary>
        public string senderTown { get; set; }
        /// <summary>
        /// 发件人userId
        /// </summary>
        public string userId { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>
        public string zipCode { get; set; }
    }
    /// <summary>
    /// 服务详情（serviceFlag中服务所需的信息在服务详情中填写）
    /// </summary>
    public class ServiceDetail
    {
        /// <summary>
        /// Cod买家服务费（在serviceFlag中包含cod时有效）
        /// </summary>
        public string codBuyServiceFee { get; set; }
        /// <summary>
        /// Cod物流公司分润（在serviceFlag中包含cod时有效）
        /// </summary>
        public string codSplitFee { get; set; }
        /// <summary>
        /// Cod总服务费（在serviceFlag中包含cod时有效）
        /// </summary>
        public string codTotalServiceFee { get; set; }
        /// <summary>
        /// 客户在裹裹下单时间
        /// </summary>
        public DateTime customerOrderTime { get; set; }
        /// <summary>
        /// 最后妥投时间，必需送达的最后时间点
        /// </summary>
        public DateTime endSignTime { get; set; }
        /// <summary>
        /// 投递表达时延要求(投递时延要求：1-工作日，2-节假日，101-当日达，102-次晨达，103-次日达，104-预约达，109-菜鸟联盟送装一体),售卖商品时买卖家所看到的时效表达，实际履行时效受截单时间影响
        /// </summary>
        public string expressScheduleType { get; set; }
        /// <summary>
        /// 特定业务场景数据扩展
        /// </summary>
        public List<ExtendField> extendFields { get; set; }
        /// <summary>
        /// 订单商品金额（单位：分，在serviceFlag中包含cod时有效）
        /// </summary>
        public string goodsValue { get; set; }
        /// <summary>
        /// 预约上门揽收业务（如：上门取退）时有效，上门揽件结束时间，格式：yyyy-MM-dd hh:mm:ss（在serviceFlag中包含上门揽件时有效）
        /// </summary>
        public string gotEndTime { get; set; }
        /// <summary>
        /// 上门揽件时效要求
        /// </summary>
        public string gotInTime { get; set; }
        /// <summary>
        /// 预约上门揽收业务（如：上门取退）时有效，上门揽件开始时间，格式：yyyy-MM-dd hh:mm:ss（在serviceFlag中包含上门揽件时有效）
        /// </summary>
        public string gotStartTime { get; set; }
        /// <summary>
        /// 抢单时间
        /// </summary>
        public DateTime grabOrderTime { get; set; }
        /// <summary>
        /// 拦截、拒签(serviceFlag=101或102)退回单据会附带下发
        /// </summary>
        public string prevMailNo { get; set; }
        /// <summary>
        /// 承诺妥投时间，售卖商品时给卖家和买家承诺的送达时间
        /// </summary>
        public DateTime promiseSignTime { get; set; }
        /// <summary>
        /// 预约配送时间描述（在serviceFlag中包含预约配送时有效）
        /// </summary>
        public string scheduleDesc { get; set; }
        /// <summary>
        /// 投递时延要求：1-工作日，2-节假日，101-当日达，102-次晨达，103-次日达，104-预约达（在serviceFlag中包含预约配送时有效），10101-当日下午达，10102－当日夜间达，10301－次日上午达
        /// </summary>
        public string scheduleType { get; set; }
        /// <summary>
        /// 预约与送时有效，送达结束时间，格式：yyyy-MM-dd hh:mm:ss（在serviceFlag中包含预约配送时有效）
        /// </summary>
        public DateTime sendEndTime { get; set; }
        /// <summary>
        /// 预约配送时有效，送达开始时间，格式：yyyy-MM-dd hh:mm:ss（在serviceFlag中包含预约配送时有效）
        /// </summary>
        public DateTime sendStartTime { get; set; }
        /// <summary>
        /// 配送温度要求（在serviceFlag中包含环保配时有效）
        /// </summary>
        public string temperatureRequirement { get; set; }
        /// <summary>
        /// 订单总费用（支付宝支付金额，包括服务费，单位：分，在serviceFlag中包含Cod时有效）
        /// </summary>
        public string totalFee { get; set; }
        /// <summary>
        /// 保价总金额（在serviceFlag中包含保价时有效）
        /// </summary>
        public string totalInsuredAmount { get; set; }
    }
    /// <summary>
    /// TMS_CREATE_ORDER_ONLINE_NOTIFY 请求报文
    /// </summary>
    public class TmsOrderModel
    {
        /// <summary>
        /// 结算信息
        /// </summary>
        public ChargingInfo chargingInfo { get; set; }
        /// <summary>
        /// 小件员信息(目前仅小件员众包时使用)
        /// </summary>
        public CourierInfo courierInfo { get; set; }
        /// <summary>
        /// 物流公司编号(针对老的快递业务，新业务全部是资源code：快递是CP编码，其它是资源code)
        /// </summary>
        public string cpCode { get; set; }
        /// <summary>
        /// 菜鸟配送平台唯一包裹号
        /// </summary>
        public string dicCode { get; set; }
        /// <summary>
        /// 面单打印码
        /// </summary>
        public string expressPrintCode { get; set; }
        /// <summary>
        /// 菜鸟物流订单号
        /// </summary>
        public string logisticsId { get; set; }
        /// <summary>
        /// 运单号
        /// </summary>
        public string mailNo { get; set; }
        /// <summary>
        /// 作业操作要求，主要表达对作业操作的要求，类似SOP
        /// </summary>
        public OpRequire opRequire { get; set; }
        /// <summary>
        /// 订单业务类型（1-在线下单 2-cod 5-仓配订单 10-我要寄快递 36－o2o订单 41-钉钉企业快递）
        /// </summary>
        public string orderBizType { get; set; }
        /// <summary>
        /// 订单创建时间,格式见示例
        /// </summary>
        public DateTime orderCreateTime { get; set; }
        /// <summary>
        /// 订单扩展属性列表 
        /// </summary>
        public List<OrderExtendField> orderExtendFields { get; set; }
        /// <summary>
        /// 订单渠道:其他渠道-OTHERS 淘宝平台订单-TB 天猫平台订单-TM 京东-JD 一号店平台订单-YHD 当当网平台订单-DD 易贝-EBAY 亚马逊-AMAZON 等
        /// </summary>
        public string orderSource { get; set; }
        /// <summary>
        /// 包裹信息
        /// </summary>
        public PackageInfo packageInfo { get; set; }
        /// <summary>
        /// 下单方式（1表示用纸质面单下单，2表示用二维码面单下单）
        /// </summary>
        public string placeOrderType { get; set; }
        /// <summary>
        /// 收件人信息
        /// </summary>
        public Receiver receiver { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 路由信息
        /// </summary>
        public RouteInfo routeInfo { get; set; }
        /// <summary>
        /// 发件人信息
        /// </summary>
        public Sender sender { get; set; }
        /// <summary>
        /// 服务详情（serviceFlag中服务所需的信息在服务详情中填写）
        /// </summary>
        public ServiceDetail serviceDetail { get; set; }
        /// <summary>
        /// 订单所需服务，以英文逗号分隔，当前服务枚举值:（1-Cod 货到付款(包括货款和快递费) 2-预约配送 9-上门揽件 13-退货时带发票 34-常温 35-冷链 37-上门取退 38-保价 39-环保配 101-拒签退回 102-拦截退回 42-精准 50-到付(仅仅付快递费，不包括货款)）举例:精准预约配送2,42;精准预约对配送CP来说是个不同的履行服务，对精准预约配送的订单，配送有不同的操作流程，比如要按时间段精准送达，精准预约的订单优先给一些CP或网点
        /// </summary>
        public string serviceFlag { get; set; }
        /// <summary>
        /// 淘宝交易订单号
        /// </summary>
        public string tradeNo { get; set; }
    }
}