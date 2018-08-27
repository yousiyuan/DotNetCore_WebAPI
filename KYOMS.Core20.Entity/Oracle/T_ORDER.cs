using System;
namespace KYOMS.Core20.Entity
{
    /// <summary>
    /// T_ORDER:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class T_ORDER
    {
        public T_ORDER()
        { }
        #region Model
        private decimal _id;
        private string _order_no;
        private string _order_source_type;
        private decimal _order_status;
        private string _order_source;
        private decimal? _assignsite_type;
        private string _outsys_code;
        private string _outsys_uid;
        private string _customer_code;
        private string _outsys_ordercode;
        private string _biz_type;
        private string _pickup_sitecode;
        private string _sendto_sitecode;
        private string _outsys_membertype;
        private string _sender_companyname;
        private string _sender_name;
        private string _sender_postcode;
        private string _sender_mobile;
        private string _sender_phone;
        private string _sender_province;
        private string _sender_city;
        private string _sender_county;
        private string _sender_address;
        private string _receiver_companyname;
        private string _receiver_name;
        private string _receiver_postcode;
        private string _receiver_mobile;
        private string _receiver_phone;
        private string _receiver_province;
        private string _receiver_city;
        private string _receiver_county;
        private string _receiver_address;
        private string _cargo_name;
        private decimal? _total_number;
        private decimal? _total_weight;
        private decimal? _total_volume;
        private decimal? _total_price;
        private decimal? _transport_price;
        private decimal? _weight_rate;
        private decimal? _volume_rate;
        private decimal? _least_expenses;
        private string _outsys_paytype;
        private string _material_cost;
        private decimal? _material_costprice;
        private string _transport_type;
        private string _visit_receive;
        private decimal? _visit_receiveprice;
        private string _delivery_type;
        private decimal? _delivery_price;
        private decimal? _is_insured_price;
        private decimal? _insurance_value;
        private decimal? _insurance_price;
        private decimal? _cod_type;
        private string _remark;
        private string _remark2;
        private decimal? _cod_price;
        private string _promotion;
        private string _backsignbill;
        private decimal? _backsignbill_price;
        private string _package_service;
        private decimal? _package_serviceprice;
        private string _wait_notifysend;
        private decimal? _wait_notifysendprice;
        private string _sms_notify;
        private decimal? _sms_notifyprice;
        private string _fuel_surcharge;
        private decimal? _fuel_surcharge_price;
        private decimal? _other_price;
        private decimal? _cod_value;
        private decimal? _delivery_notice;
        private string _reserve_pickup_begintime;
        private string _reserve_pickup_endtime;
        private DateTime? _outsys_order_createdate;

        private string _assigned_site_code;
        private string _assigned_driver_code;
        private DateTime? _assign_site_time;
        private DateTime? _assign_driver_time;
        private string _pickup_fail_reason;
        private DateTime? _pickup_time;
        private string _order_cancel_by;
        private DateTime? _order_cancel_time;
        private DateTime? _recieve_order_time;
        private string _order_cancel_remark;
        private string _operator_ip;
        private string _create_by;
        private DateTime? _create_time;
        private string _update_by;
        private DateTime? _update_time;
        private string _pick_up_operator;
        private string _wlb_order_biz_type;
        private string _wlb_service_flag;
        private string _wlb_sender_customerid;
        private string _wlb_receiver_customerid;
        private string _outsys_tradeno;
        private string _ext_fields;
        private string _send_starttime;
        private string _send_endtime;
        private string _send_schedule_desc;
        private decimal? _wlb_cod_total_service_fee;
        private decimal? _wlb_cod_buy_service_fee;
        private decimal? _wlb_total_fee;
        private decimal? _wlb_goods_value;
        private decimal? _need_upload_status_bos = 0M;
        private decimal? _need_update_record_bos = 0M;
        private decimal? _need_add_record_bos = 0M;
        private decimal? _wlb_codsplitfee;
        private decimal? _push_outsys_status = 0M;
        private DateTime? _last_push_outsys_time;
        private decimal? _push_outsys_status_ver = 0M;
        private decimal? _push_bos_status_ver = 0M;
        private DateTime? _last_push_bos_time;
        private decimal? _push_outsys_fail_num = 0M;
        private string _outsys_customer_code;
        private string _pickup_site;
        private string _sendto_site;
        private decimal _need_create_bill;
        private string _bill_no;

        private string _sender_private_mobile;
        private string _receiver_private_mobile;
        private DateTime? _reserve_send_starttime;
        private DateTime? _reserve_send_endtime;
        private string _product_type;
        private decimal? _is_private;
        private decimal? _hide_type;
        private decimal? _need_return_ebill_template;
        private string _sender_town_name;
        private string _receiver_town_name;
        private decimal _is_locked;
        private string _locked_site_name;
        private string _locked_by;
        private DateTime? _locked_time;
        private string _unlocked_site_name;
        private string _unlocked_by;
        private DateTime? _unlocked_time;
        private decimal? _need_push_wbms = 0M;



        /// <summary>
        /// 
        /// </summary>
        public decimal ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string ORDER_NO
        {
            set { _order_no = value; }
            get { return _order_no; }
        }
        /// <summary>
        /// 1 手动录入 2 :接口 3: 批量导入
        /// </summary>
        public string ORDER_SOURCE_TYPE
        {
            set { _order_source_type = value; }
            get { return _order_source_type; }
        }
        /// <summary>
        /// 订单状态: 10 待受理 20  已分派 30 已撤销 40 已接单(既指派司机) 50揽件成功 60 揽件失败 70已签收 80 已回单
        /// </summary>
        public decimal ORDER_STATUS
        {
            set { _order_status = value; }
            get { return _order_status; }
        }
        /// <summary>
        /// 订单来源 : ALI,慧聪(具体待配置)
        /// </summary>
        public string ORDER_SOURCE
        {
            set { _order_source = value; }
            get { return _order_source; }
        }
        /// <summary>
        /// 分派网点方式: -1:未知 0: 自动匹配  1:人工分派
        /// </summary>
        public decimal? ASSIGNSITE_TYPE
        {
            set { _assignsite_type = value; }
            get { return _assignsite_type; }
        }
        /// <summary>
        /// 外部系统编号(基础数据在系统常量表)
        /// </summary>
        public string OUTSYS_CODE
        {
            set { _outsys_code = value; }
            get { return _outsys_code; }
        }
        /// <summary>
        /// 第三方对接系统用户ID
        /// </summary>
        public string OUTSYS_UID
        {
            set { _outsys_uid = value; }
            get { return _outsys_uid; }
        }
        /// <summary>
        /// 客户编码(预留 crm对接)
        /// </summary>
        public string CUSTOMER_CODE
        {
            set { _customer_code = value; }
            get { return _customer_code; }
        }
        /// <summary>
        /// 外部订单编号[预留]
        /// </summary>
        public string OUTSYS_ORDERCODE
        {
            set { _outsys_ordercode = value; }
            get { return _outsys_ordercode; }
        }
        /// <summary>
        /// 业务类型[预留]:  示例:MRO用于MRO业务 POW:用于实力商家业务 空:普通业务
        /// </summary>
        public string BIZ_TYPE
        {
            set { _biz_type = value; }
            get { return _biz_type; }
        }
        /// <summary>
        /// 取件营业网点
        /// </summary>
        public string PICKUP_SITECODE
        {
            set { _pickup_sitecode = value; }
            get { return _pickup_sitecode; }
        }
        /// <summary>
        /// 收货营业网点编号
        /// </summary>
        public string SENDTO_SITECODE
        {
            set { _sendto_sitecode = value; }
            get { return _sendto_sitecode; }
        }
        /// <summary>
        /// 外部会员类型[预留]: 示例:会员类型 CXT: 诚信通PT：普通会员MRO: MRO会员POW:POW会员[新增实力商家会员]
        /// </summary>
        public string OUTSYS_MEMBERTYPE
        {
            set { _outsys_membertype = value; }
            get { return _outsys_membertype; }
        }
        /// <summary>
        /// 发货人公司名称
        /// </summary>
        public string SENDER_COMPANYNAME
        {
            set { _sender_companyname = value; }
            get { return _sender_companyname; }
        }
        /// <summary>
        /// 发货人姓名
        /// </summary>
        public string SENDER_NAME
        {
            set { _sender_name = value; }
            get { return _sender_name; }
        }
        /// <summary>
        /// 发货人邮编
        /// </summary>
        public string SENDER_POSTCODE
        {
            set { _sender_postcode = value; }
            get { return _sender_postcode; }
        }
        /// <summary>
        /// 发货人手机号码
        /// </summary>
        public string SENDER_MOBILE
        {
            set { _sender_mobile = value; }
            get { return _sender_mobile; }
        }
        /// <summary>
        /// 发货人固定电话
        /// </summary>
        public string SENDER_PHONE
        {
            set { _sender_phone = value; }
            get { return _sender_phone; }
        }
        /// <summary>
        /// 发货人省
        /// </summary>
        public string SENDER_PROVINCE
        {
            set { _sender_province = value; }
            get { return _sender_province; }
        }
        /// <summary>
        /// 发货人市
        /// </summary>
        public string SENDER_CITY
        {
            set { _sender_city = value; }
            get { return _sender_city; }
        }
        /// <summary>
        /// 发货人区
        /// </summary>
        public string SENDER_COUNTY
        {
            set { _sender_county = value; }
            get { return _sender_county; }
        }
        /// <summary>
        /// 发货人详细地址
        /// </summary>
        public string SENDER_ADDRESS
        {
            set { _sender_address = value; }
            get { return _sender_address; }
        }
        /// <summary>
        /// 收货人公司名称
        /// </summary>
        public string RECEIVER_COMPANYNAME
        {
            set { _receiver_companyname = value; }
            get { return _receiver_companyname; }
        }
        /// <summary>
        /// 收货人姓名
        /// </summary>
        public string RECEIVER_NAME
        {
            set { _receiver_name = value; }
            get { return _receiver_name; }
        }
        /// <summary>
        /// 收货人邮编
        /// </summary>
        public string RECEIVER_POSTCODE
        {
            set { _receiver_postcode = value; }
            get { return _receiver_postcode; }
        }
        /// <summary>
        /// 收货人移动电话
        /// </summary>
        public string RECEIVER_MOBILE
        {
            set { _receiver_mobile = value; }
            get { return _receiver_mobile; }
        }
        /// <summary>
        /// 收货人固定电话
        /// </summary>
        public string RECEIVER_PHONE
        {
            set { _receiver_phone = value; }
            get { return _receiver_phone; }
        }
        /// <summary>
        /// 收货人省
        /// </summary>
        public string RECEIVER_PROVINCE
        {
            set { _receiver_province = value; }
            get { return _receiver_province; }
        }
        /// <summary>
        /// 收货人市
        /// </summary>
        public string RECEIVER_CITY
        {
            set { _receiver_city = value; }
            get { return _receiver_city; }
        }
        /// <summary>
        /// 收货人区
        /// </summary>
        public string RECEIVER_COUNTY
        {
            set { _receiver_county = value; }
            get { return _receiver_county; }
        }
        /// <summary>
        /// 收货人详细地址
        /// </summary>
        public string RECEIVER_ADDRESS
        {
            set { _receiver_address = value; }
            get { return _receiver_address; }
        }
        /// <summary>
        /// 货物名称
        /// </summary>
        public string CARGO_NAME
        {
            set { _cargo_name = value; }
            get { return _cargo_name; }
        }
        /// <summary>
        /// 总件数
        /// </summary>
        public decimal? TOTAL_NUMBER
        {
            set { _total_number = value; }
            get { return _total_number; }
        }
        /// <summary>
        /// 总重量(千克)
        /// </summary>
        public decimal? TOTAL_WEIGHT
        {
            set { _total_weight = value; }
            get { return _total_weight; }
        }
        /// <summary>
        /// 总体积(立方米)
        /// </summary>
        public decimal? TOTAL_VOLUME
        {
            set { _total_volume = value; }
            get { return _total_volume; }
        }
        /// <summary>
        /// 总价格
        /// </summary>
        public decimal? TOTAL_PRICE
        {
            set { _total_price = value; }
            get { return _total_price; }
        }
        /// <summary>
        /// 运输费用
        /// </summary>
        public decimal? TRANSPORT_PRICE
        {
            set { _transport_price = value; }
            get { return _transport_price; }
        }
        /// <summary>
        /// 重量费率（重量单价）
        /// </summary>
        public decimal? WEIGHT_RATE
        {
            set { _weight_rate = value; }
            get { return _weight_rate; }
        }
        /// <summary>
        /// 体积费率（体积单价）
        /// </summary>
        public decimal? VOLUME_RATE
        {
            set { _volume_rate = value; }
            get { return _volume_rate; }
        }
        /// <summary>
        /// 最低一票
        /// </summary>
        public decimal? LEAST_EXPENSES
        {
            set { _least_expenses = value; }
            get { return _least_expenses; }
        }
        /// <summary>
        /// 支付方式:值域如下：0:发货人付款（现付）1:收货人付款（到付）2:混合支付3:线上支付4:按月支付5:菜鸟代扣
        /// </summary>
        public string OUTSYS_PAYTYPE
        {
            set { _outsys_paytype = value; }
            get { return _outsys_paytype; }
        }
        /// <summary>
        /// 工本服务
        /// </summary>
        public string MATERIAL_COST
        {
            set { _material_cost = value; }
            get { return _material_cost; }
        }
        /// <summary>
        /// 工本费
        /// </summary>
        public decimal? MATERIAL_COSTPRICE
        {
            set { _material_costprice = value; }
            get { return _material_costprice; }
        }
        /// <summary>
        /// 运输方式 : 1 快递 2 快运 3 物流 
        /// </summary>
        public string TRANSPORT_TYPE
        {
            set { _transport_type = value; }
            get { return _transport_type; }
        }
        /// <summary>
        /// 上门接货，值域如下：Y:需要上门接货 N:客户自送
        /// </summary>
        public string VISIT_RECEIVE
        {
            set { _visit_receive = value; }
            get { return _visit_receive; }
        }
        /// <summary>
        /// 上门接货费用
        /// </summary>
        public decimal? VISIT_RECEIVEPRICE
        {
            set { _visit_receiveprice = value; }
            get { return _visit_receiveprice; }
        }
        /// <summary>
        /// 送货方式 :ALI 0:自提1:送货（不含上楼）2:机场自提3:送货上楼
        /// </summary>
        public string DELIVERY_TYPE
        {
            set { _delivery_type = value; }
            get { return _delivery_type; }
        }
        /// <summary>
        /// 送货费用
        /// </summary>
        public decimal? DELIVERY_PRICE
        {
            set { _delivery_price = value; }
            get { return _delivery_price; }
        }
        /// <summary>
        /// 是否保价
        /// </summary>
        public decimal? IS_INSURED_PRICE
        {
            set { _is_insured_price = value; }
            get { return _is_insured_price; }
        }
        /// <summary>
        /// 保价金额
        /// </summary>
        public decimal? INSURANCE_VALUE
        {
            set { _insurance_value = value; }
            get { return _insurance_value; }
        }
        /// <summary>
        /// 保价费
        /// </summary>
        public decimal? INSURANCE_PRICE
        {
            set { _insurance_price = value; }
            get { return _insurance_price; }
        }
        /// <summary>
        /// 代收货款类型，值域如下：3：三日退1：即日退
        /// </summary>
        public decimal? COD_TYPE
        {
            set { _cod_type = value; }
            get { return _cod_type; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string REMARK
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 备注2
        /// </summary>
        public string REMARK2
        {
            set { _remark2 = value; }
            get { return _remark2; }
        }
        /// <summary>
        /// 代收货款费
        /// </summary>
        public decimal? COD_PRICE
        {
            set { _cod_price = value; }
            get { return _cod_price; }
        }
        /// <summary>
        /// 促销活动说明
        /// </summary>
        public string PROMOTION
        {
            set { _promotion = value; }
            get { return _promotion; }
        }
        /// <summary>
        /// 确认返单,是否需要签收回单 
        //0:无需返单
        //1:客户签收单原件返回
        //2:客户签收单传真返回
        //3:运单签收联原件返回
        //4:运单签收联传真返回
        //5:传真回单
        /// </summary>
        public string BACKSIGNBILL
        {
            set { _backsignbill = value; }
            get { return _backsignbill; }
        }
        /// <summary>
        /// 签收回单费用
        /// </summary>
        public decimal? BACKSIGNBILL_PRICE
        {
            set { _backsignbill_price = value; }
            get { return _backsignbill_price; }
        }
        /// <summary>
        /// 包装 纸 纤 木箱 木架托膜托木[预留]
        /// </summary>
        public string PACKAGE_SERVICE
        {
            set { _package_service = value; }
            get { return _package_service; }
        }
        /// <summary>
        /// 包装服务费
        /// </summary>
        public decimal? PACKAGE_SERVICEPRICE
        {
            set { _package_serviceprice = value; }
            get { return _package_serviceprice; }
        }
        /// <summary>
        /// 等通知发货 Y：等通知发货 N：不需要等通知发货
        /// </summary>
        public string WAIT_NOTIFYSEND
        {
            set { _wait_notifysend = value; }
            get { return _wait_notifysend; }
        }
        /// <summary>
        /// 等通知发货费
        /// </summary>
        public decimal? WAIT_NOTIFYSENDPRICE
        {
            set { _wait_notifysendprice = value; }
            get { return _wait_notifysendprice; }
        }
        /// <summary>
        /// 短信通知  Y：需要 N: 不需要
        /// </summary>
        public string SMS_NOTIFY
        {
            set { _sms_notify = value; }
            get { return _sms_notify; }
        }
        /// <summary>
        /// 短信通知费用
        /// </summary>
        public decimal? SMS_NOTIFYPRICE
        {
            set { _sms_notifyprice = value; }
            get { return _sms_notifyprice; }
        }
        /// <summary>
        /// 燃油附加 Y：需要　N: 不需要
        /// </summary>
        public string FUEL_SURCHARGE
        {
            set { _fuel_surcharge = value; }
            get { return _fuel_surcharge; }
        }
        /// <summary>
        /// 燃油附加费
        /// </summary>
        public decimal? FUEL_SURCHARGE_PRICE
        {
            set { _fuel_surcharge_price = value; }
            get { return _fuel_surcharge_price; }
        }
        /// <summary>
        /// 其他费用
        /// </summary>
        public decimal? OTHER_PRICE
        {
            set { _other_price = value; }
            get { return _other_price; }
        }
        /// <summary>
        /// 货到付款费用
        /// </summary>
        public decimal? COD_VALUE
        {
            set { _cod_value = value; }
            get { return _cod_value; }
        }
        /// <summary>
        /// 派送指示（0-工作日内送货,1-节假日可送货)
        /// </summary>
        public decimal? DELIVERY_NOTICE
        {
            set { _delivery_notice = value; }
            get { return _delivery_notice; }
        }
        /// <summary>
        /// 预约开始时间
        /// </summary>
        public string RESERVE_PICKUP_BEGINTIME
        {
            set { _reserve_pickup_begintime = value; }
            get { return _reserve_pickup_begintime; }
        }
        /// <summary>
        /// 预约结束时间
        /// </summary>
        public string RESERVE_PICKUP_ENDTIME
        {
            set { _reserve_pickup_endtime = value; }
            get { return _reserve_pickup_endtime; }
        }
        /// <summary>
        /// 外部订单创建时间
        /// </summary>
        public DateTime? OUTSYS_ORDER_CREATEDATE
        {
            set { _outsys_order_createdate = value; }
            get { return _outsys_order_createdate; }
        }

        /// <summary>
        /// 被分派网点编号
        /// </summary>
        public string ASSIGNED_SITE_CODE
        {
            set { _assigned_site_code = value; }
            get { return _assigned_site_code; }
        }
        /// <summary>
        /// 被指派司机(揽件人)
        /// </summary>
        public string ASSIGNED_DRIVER_CODE
        {
            set { _assigned_driver_code = value; }
            get { return _assigned_driver_code; }
        }
        /// <summary>
        /// 分派网点时间
        /// </summary>
        public DateTime? ASSIGN_SITE_TIME
        {
            set { _assign_site_time = value; }
            get { return _assign_site_time; }
        }
        /// <summary>
        /// 指派司机时间
        /// </summary>
        public DateTime? ASSIGN_DRIVER_TIME
        {
            set { _assign_driver_time = value; }
            get { return _assign_driver_time; }
        }
        /// <summary>
        /// 揽件失败原因
        /// </summary>
        public string PICKUP_FAIL_REASON
        {
            set { _pickup_fail_reason = value; }
            get { return _pickup_fail_reason; }
        }
        /// <summary>
        /// 揽件时间
        /// </summary>
        public DateTime? PICKUP_TIME
        {
            set { _pickup_time = value; }
            get { return _pickup_time; }
        }
        /// <summary>
        /// 订单撤销人
        /// </summary>
        public string ORDER_CANCEL_BY
        {
            set { _order_cancel_by = value; }
            get { return _order_cancel_by; }
        }
        /// <summary>
        /// 订单撤销时间
        /// </summary>
        public DateTime? ORDER_CANCEL_TIME
        {
            set { _order_cancel_time = value; }
            get { return _order_cancel_time; }
        }
        /// <summary>
        /// 接单时间
        /// </summary>
        public DateTime? RECIEVE_ORDER_TIME
        {
            set { _recieve_order_time = value; }
            get { return _recieve_order_time; }
        }
        /// <summary>
        /// 撤消备注
        /// </summary>
        public string ORDER_CANCEL_REMARK
        {
            set { _order_cancel_remark = value; }
            get { return _order_cancel_remark; }
        }
        /// <summary>
        /// 操作人IP
        /// </summary>
        public string OPERATOR_IP
        {
            set { _operator_ip = value; }
            get { return _operator_ip; }
        }
        /// <summary>
        /// 创建人工号
        /// </summary>
        public string CREATE_BY
        {
            set { _create_by = value; }
            get { return _create_by; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CREATE_TIME
        {
            set { _create_time = value; }
            get { return _create_time; }
        }
        /// <summary>
        /// 更新人工号
        /// </summary>
        public string UPDATE_BY
        {
            set { _update_by = value; }
            get { return _update_by; }
        }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UPDATE_TIME
        {
            set { _update_time = value; }
            get { return _update_time; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PICK_UP_OPERATOR
        {
            set { _pick_up_operator = value; }
            get { return _pick_up_operator; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WLB_ORDER_BIZ_TYPE
        {
            set { _wlb_order_biz_type = value; }
            get { return _wlb_order_biz_type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WLB_SERVICE_FLAG
        {
            set { _wlb_service_flag = value; }
            get { return _wlb_service_flag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WLB_SENDER_CUSTOMERID
        {
            set { _wlb_sender_customerid = value; }
            get { return _wlb_sender_customerid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WLB_RECEIVER_CUSTOMERID
        {
            set { _wlb_receiver_customerid = value; }
            get { return _wlb_receiver_customerid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OUTSYS_TRADENO
        {
            set { _outsys_tradeno = value; }
            get { return _outsys_tradeno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string EXT_FIELDS
        {
            set { _ext_fields = value; }
            get { return _ext_fields; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SEND_STARTTIME
        {
            set { _send_starttime = value; }
            get { return _send_starttime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SEND_ENDTIME
        {
            set { _send_endtime = value; }
            get { return _send_endtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SEND_SCHEDULE_DESC
        {
            set { _send_schedule_desc = value; }
            get { return _send_schedule_desc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? WLB_COD_TOTAL_SERVICE_FEE
        {
            set { _wlb_cod_total_service_fee = value; }
            get { return _wlb_cod_total_service_fee; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? WLB_COD_BUY_SERVICE_FEE
        {
            set { _wlb_cod_buy_service_fee = value; }
            get { return _wlb_cod_buy_service_fee; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? WLB_TOTAL_FEE
        {
            set { _wlb_total_fee = value; }
            get { return _wlb_total_fee; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? WLB_GOODS_VALUE
        {
            set { _wlb_goods_value = value; }
            get { return _wlb_goods_value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? NEED_UPLOAD_STATUS_BOS
        {
            set { _need_upload_status_bos = value; }
            get { return _need_upload_status_bos; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? NEED_UPDATE_RECORD_BOS
        {
            set { _need_update_record_bos = value; }
            get { return _need_update_record_bos; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? NEED_ADD_RECORD_BOS
        {
            set { _need_add_record_bos = value; }
            get { return _need_add_record_bos; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? WLB_CODSPLITFEE
        {
            set { _wlb_codsplitfee = value; }
            get { return _wlb_codsplitfee; }
        }
        /// <summary>
        /// 0: 不需要推送 1 需要推送 2 推送失败
        /// </summary>
        public decimal? PUSH_OUTSYS_STATUS
        {
            set { _push_outsys_status = value; }
            get { return _push_outsys_status; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? LAST_PUSH_OUTSYS_TIME
        {
            set { _last_push_outsys_time = value; }
            get { return _last_push_outsys_time; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? PUSH_OUTSYS_STATUS_VER
        {
            set { _push_outsys_status_ver = value; }
            get { return _push_outsys_status_ver; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? PUSH_BOS_STATUS_VER
        {
            set { _push_bos_status_ver = value; }
            get { return _push_bos_status_ver; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? LAST_PUSH_BOS_TIME
        {
            set { _last_push_bos_time = value; }
            get { return _last_push_bos_time; }
        }

        /// <summary>
        /// 推送失败次数
        /// </summary>
        public decimal? PUSH_OUTSYS_FAIL_NUM
        {
            set { _push_outsys_fail_num = value; }
            get { return _push_outsys_fail_num; }
        }

        /// <summary>
        /// 外部客户编码，最终客户的编码
        /// </summary>
        public string OUTSYS_CUSTOMER_CODE
        {
            set { _outsys_customer_code = value; }
            get { return _outsys_customer_code; }
        }

        /// <summary>
        /// 取件网点编号，存编号，对应的code存中文
        /// </summary>
        public string PICKUP_SITE
        {
            set { _pickup_site = value; }
            get { return _pickup_site; }
        }

        /// <summary>
        /// 寄件网点编号，存编号，对应的code存中文
        /// </summary>
        public string SENDTO_SITE
        {
            set { _sendto_site = value; }
            get { return _sendto_site; }
        }

        /// <summary>
        /// 是否需要调用BOS接口，创建运单
        /// </summary>
        public decimal NEED_CREATE_BILL
        {
            set { _need_create_bill = value; }
            get { return _need_create_bill; }
        }

        /// <summary>
        /// 运单号(自联物流用)
        /// </summary>
        public string BILL_NO
        {
            set { _bill_no = value; }
            get { return _bill_no; }
        }

        //以下字段为快递鸟项目添加

        /// <summary>
        /// 发货人手机安全号
        /// </summary>
        public string SENDER_PRIVATE_MOBILE
        {
            set { _sender_private_mobile = value; }
            get { return _sender_private_mobile; }
        }
        /// <summary>
        /// 收货人手机安全号
        /// </summary>
        public string RECEIVER_PRIVATE_MOBILE
        {
            set { _receiver_private_mobile = value; }
            get { return _receiver_private_mobile; }
        }
        /// <summary>
        /// 预约物流公司上门取货时间段（开始时间）
        /// </summary>
        public DateTime? RESERVE_SEND_STARTTIME
        {
            set { _reserve_send_starttime = value; }
            get { return _reserve_send_starttime; }
        }
        /// <summary>
        /// 预约物流公司上门取货时间段（结束时间）
        /// </summary>
        public DateTime? RESERVE_SEND_ENDTIME
        {
            set { _reserve_send_endtime = value; }
            get { return _reserve_send_endtime; }
        }
        /// <summary>
        /// 产品类型（次日达等(快递单、物流单、航空单)
        /// </summary>
        public string PRODUCT_TYPE
        {
            set { _product_type = value; }
            get { return _product_type; }
        }
        /// <summary>
        /// 是否隐私快递（0.是；1.否）
        /// </summary>
        public decimal? IS_PRIVATE
        {
            set { _is_private = value; }
            get { return _is_private; }
        }
        /// <summary>
        /// 安全号生成规则(1，隐藏收件人信息，2.隐身发件人信息，3.同时隐藏收件人，发件人信息)
        /// </summary>
        public decimal? HIDE_TYPE
        {
            set { _hide_type = value; }
            get { return _hide_type; }
        }
        /// <summary>
        /// 返回电子面单模板：0-不需要；1-需要
        /// </summary>
        public decimal? NEED_RETURN_EBILL_TEMPLATE
        {
            set { _need_return_ebill_template = value; }
            get { return _need_return_ebill_template; }
        }
        /// <summary>
        /// 发货人街道/镇
        /// </summary>
        public string SENDER_TOWN_NAME
        {
            set { _sender_town_name = value; }
            get { return _sender_town_name; }
        }
        /// <summary>
        /// 收货人街道/镇
        /// </summary>
        public string RECEIVER_TOWN_NAME
        {
            set { _receiver_town_name = value; }
            get { return _receiver_town_name; }
        }

        //快递鸟项目字段添加结束




        //运单开单锁定订单
        /// <summary>
        /// 是否锁定(0:不锁定；1：锁定）
        /// </summary>
        public decimal IS_LOCKED
        {
            set { _is_locked = value; }
            get { return _is_locked; }
        }
        /// <summary>
        /// 锁定操作网点名称
        /// </summary>
        public string LOCKED_SITE_NAME
        {
            set { _locked_site_name = value; }
            get { return _locked_site_name; }
        }
        /// <summary>
        /// 锁定操作人
        /// </summary>
        public string LOCKED_BY
        {
            set { _locked_by = value; }
            get { return _locked_by; }
        }
        /// <summary>
        /// 锁定时间
        /// </summary>
        public DateTime? LOCKED_TIME
        {
            set { _locked_time = value; }
            get { return _locked_time; }
        }
        /// <summary>
        /// 解锁操作网点名称
        /// </summary>
        public string UNLOCKED_SITE_NAME
        {
            set { _unlocked_site_name = value; }
            get { return _unlocked_site_name; }
        }
        /// <summary>
        /// 解锁操作人
        /// </summary>
        public string UNLOCKED_BY
        {
            set { _unlocked_by = value; }
            get { return _unlocked_by; }
        }
        /// <summary>
        /// 解锁时间
        /// </summary>
        public DateTime? UNLOCKED_TIME
        {
            set { _unlocked_time = value; }
            get { return _unlocked_time; }
        }

        public decimal? NEED_PUSH_WBMS
        {
            get { return _need_push_wbms; }
            set { _need_push_wbms = value; }
        }

        #endregion Model


    }
}

