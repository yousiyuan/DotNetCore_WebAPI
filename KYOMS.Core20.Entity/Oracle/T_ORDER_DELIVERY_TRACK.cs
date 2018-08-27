using System;
namespace KYOMS.Core20.Entity
{
    /// <summary>
    /// 运单跟踪信息(bos同步)
    /// </summary>
    [Serializable]
    public partial class T_ORDER_DELIVERY_TRACK
    {
        public T_ORDER_DELIVERY_TRACK()
        { }
        #region Model
        private decimal _id;
        private string _order_id;
        private string _mailno;
        private string _op_site;
        private string _operation_description;
        private DateTime _op_time;
        private string _operatorinfo;
        private string _operatormobile;
        private string _action;
        private string _city;
        private string _facilityname;
        private string _facilitytype;
        private string _facilityno;
        private string _nextcity;
        private string _nextnodecode;
        private string _nextnodetype;
        private DateTime _dispatch_time;
        private string _supplier_code;
        private string _supplier_name;
        private string _vehicle_id;
        private string _notes;
        private decimal _delete_flag = 0M;
        private decimal _lock_flag = 0M;
        private DateTime _created_time = DateTime.Now;
        private string _created_by;
        private string _created_by_id;
        private decimal _edi_receive_status;
        private DateTime _edi_receive_time;
        private decimal _statepushsign;
        private string _create_by;
        private DateTime _create_time;
        private string _update_by;
        private DateTime _update_time;
        private decimal _push_outsys_status=0M;
        private decimal _push_outsys_status_ver=0M;
        private DateTime _last_push_outsys_time;
        private string _order_source;
        private decimal _push_outsys_fail_num=0M;
        private string _outsys_ordercode;
        private string _contact_code;
        private string _contact_name;
        private string _contact_phone;

        public string REMARK { get; set; }

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
        public string ORDER_ID
        {
            set { _order_id = value; }
            get { return _order_id; }
        }
        /// <summary>
        /// 运单编号
        /// </summary>
        public string MAILNO
        {
            set { _mailno = value; }
            get { return _mailno; }
        }
        /// <summary>
        /// 操作网点
        /// </summary>
        public string OP_SITE
        {
            set { _op_site = value; }
            get { return _op_site; }
        }
        /// <summary>
        /// 轨迹类型
        /// </summary>
        public string OPERATION_DESCRIPTION
        {
            set { _operation_description = value; }
            get { return _operation_description; }
        }
        /// <summary>
        ///操作时间
        /// </summary>
        public DateTime OP_TIME
        {
            set { _op_time = value; }
            get { return _op_time; }
        }
        /// <summary>
        /// 操作人员
        /// </summary>
        public string OPERATORINFO
        {
            set { _operatorinfo = value; }
            get { return _operatorinfo; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OPERATORMOBILE
        {
            set { _operatormobile = value; }
            get { return _operatormobile; }
        }
        /// <summary>
        /// 轨迹类型
        /// </summary>
        public string ACTION
        {
            set { _action = value; }
            get { return _action; }
        }
        /// <summary>
        /// 所属城市
        /// </summary>
        public string CITY
        {
            set { _city = value; }
            get { return _city; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FACILITYNAME
        {
            set { _facilityname = value; }
            get { return _facilityname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FACILITYTYPE
        {
            set { _facilitytype = value; }
            get { return _facilitytype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FACILITYNO
        {
            set { _facilityno = value; }
            get { return _facilityno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string NEXTCITY
        {
            set { _nextcity = value; }
            get { return _nextcity; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string NEXTNODECODE
        {
            set { _nextnodecode = value; }
            get { return _nextnodecode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string NEXTNODETYPE
        {
            set { _nextnodetype = value; }
            get { return _nextnodetype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime DISPATCH_TIME
        {
            set { _dispatch_time = value; }
            get { return _dispatch_time; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SUPPLIER_CODE
        {
            set { _supplier_code = value; }
            get { return _supplier_code; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SUPPLIER_NAME
        {
            set { _supplier_name = value; }
            get { return _supplier_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string VEHICLE_ID
        {
            set { _vehicle_id = value; }
            get { return _vehicle_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string NOTES
        {
            set { _notes = value; }
            get { return _notes; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal DELETE_FLAG
        {
            set { _delete_flag = value; }
            get { return _delete_flag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal LOCK_FLAG
        {
            set { _lock_flag = value; }
            get { return _lock_flag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CREATED_TIME
        {
            set { _created_time = value; }
            get { return _created_time; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CREATED_BY
        {
            set { _created_by = value; }
            get { return _created_by; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CREATED_BY_ID
        {
            set { _created_by_id = value; }
            get { return _created_by_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal EDI_RECEIVE_STATUS
        {
            set { _edi_receive_status = value; }
            get { return _edi_receive_status; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime EDI_RECEIVE_TIME
        {
            set { _edi_receive_time = value; }
            get { return _edi_receive_time; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal STATEPUSHSIGN
        {
            set { _statepushsign = value; }
            get { return _statepushsign; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CREATE_BY
        {
            set { _create_by = value; }
            get { return _create_by; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CREATE_TIME
        {
            set { _create_time = value; }
            get { return _create_time; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UPDATE_BY
        {
            set { _update_by = value; }
            get { return _update_by; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime UPDATE_TIME
        {
            set { _update_time = value; }
            get { return _update_time; }
        }

        /// <summary>
        /// 推送状态 0: 不需要推送 1 需要推送 2 推送失败
        /// </summary>
        public decimal PUSH_OUTSYS_STATUS
        {
            set { _push_outsys_status = value; }
            get { return _push_outsys_status; }
        }
        /// <summary>
        /// 推送版本号
        /// </summary>
        public decimal PUSH_OUTSYS_STATUS_VER
        {
            set { _push_outsys_status_ver = value; }
            get { return _push_outsys_status_ver; }
        }
        /// <summary>
        /// 上次推送时间
        /// </summary>
        public DateTime LAST_PUSH_OUTSYS_TIME
        {
            set { _last_push_outsys_time = value; }
            get { return _last_push_outsys_time; }
        }
        /// <summary>
        /// 订单来源
        /// </summary>
        public string ORDER_SOURCE
        {
            set { _order_source = value; }
            get { return _order_source; }
        }
        /// <summary>
        /// 推送失败的次数
        /// </summary>
        public decimal PUSH_OUTSYS_FAIL_NUM
        {
            set { _push_outsys_fail_num = value; }
            get { return _push_outsys_fail_num; }
        }

        /// <summary>
        /// 外部订单号
        /// </summary>
        public string OUTSYS_ORDERCODE
        {
            set { _outsys_ordercode = value; }
            get { return _outsys_ordercode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CONTACT_CODE
        {
            set { _contact_code = value; }
            get { return _contact_code; }
        }
        /// <summary>
        /// 派件员名称
        /// </summary>
        public string CONTACT_NAME
        {
            set { _contact_name = value; }
            get { return _contact_name; }
        }
        /// <summary>
        /// 派件员联系方式
        /// </summary>
        public string CONTACT_PHONE
        {
            set { _contact_phone = value; }
            get { return _contact_phone; }
        }
        #endregion Model

    }
}

