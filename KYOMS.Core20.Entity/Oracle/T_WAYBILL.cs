using System;

namespace KYOMS.Core20.Entity.Oracle
{
    /// <summary>
    /// 运单主表
    /// </summary>
    [Serializable]
    public partial class T_WAYBILL
    {
        public T_WAYBILL()
        { }
        #region Model
        private decimal _id;
        private string _bill_code;
        private decimal? _fee_weight;
        private decimal? _piece_number;
        private string _destination;
        private string _accept_man_address;
        private string _class;
        private decimal? _topayment;
        private decimal? _goods_payment;
        private string _payment_type;
        private DateTime? _send_date;
        private string _take_piece_employee;
        private DateTime? _register_date;
        private string _register_man;
        private string _register_site;
        private string _send_site;
        private string _dispatch_site;
        private string _send_finance_center;
        private string _dispatch_finance_center;
        private decimal? _freight;
        private string _send_man;
        private string _send_man_phone;
        private string _send_man_address;
        private string _accept_man_phone;
        private decimal? _insurance;
        private string _remark;
        private string _goods_type;
        private string _accept_man;
        private decimal? _bl_return_bill;
        private string _goods_name;
        private string _pack_type;
        private string _servicmode;
        private string _dispatch_mode;
        private string _bill_code_sub;
        private decimal? _cube;
        private decimal? _volume_weight;
        private decimal? _settlement_weight;
        private string _source_type;
        private decimal? _need_upload_bos = 0M;
        private DateTime? _last_upload_bos_time;
        private string _create_by;
        private DateTime? _create_time;
        private string _update_by;
        private DateTime? _update_time;
        private decimal? _push_bos_status = 0M;
        private decimal? _push_bos_status_ver = 0M;
        private DateTime? _last_push_bos_time;

        private string _send_man_mobile;
        private string _sender_province;
        private string _sender_city;
        private string _sender_county;
        private string _sender_companyname;
        private string _sender_postcode;
        private string _accept_man_mobile;
        private string _accept_province;
        private string _accept_city;
        private string _accept_county;
        private string _accept_companyname;
        private string _accept_postcode;
        private decimal? _total_price = 0M;
        private decimal? _weight_rate = 0M;
        private decimal? _volume_rate = 0M;
        private decimal? _least_expenses = 0M;
        private string _outsys_paytype;
        private string _transport_type;
        private decimal _supportvalue;

        private decimal? _deliveryprice;
        private decimal? _backsignbillprice;
        private decimal? _codprice;
        /// <summary>
        /// 
        /// </summary>
        public decimal ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BILL_CODE
        {
            set { _bill_code = value; }
            get { return _bill_code; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? FEE_WEIGHT
        {
            set { _fee_weight = value; }
            get { return _fee_weight; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? PIECE_NUMBER
        {
            set { _piece_number = value; }
            get { return _piece_number; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DESTINATION
        {
            set { _destination = value; }
            get { return _destination; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ACCEPT_MAN_ADDRESS
        {
            set { _accept_man_address = value; }
            get { return _accept_man_address; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CLASS
        {
            set { _class = value; }
            get { return _class; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? TOPAYMENT
        {
            set { _topayment = value; }
            get { return _topayment; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? GOODS_PAYMENT
        {
            set { _goods_payment = value; }
            get { return _goods_payment; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PAYMENT_TYPE
        {
            set { _payment_type = value; }
            get { return _payment_type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? SEND_DATE
        {
            set { _send_date = value; }
            get { return _send_date; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TAKE_PIECE_EMPLOYEE
        {
            set { _take_piece_employee = value; }
            get { return _take_piece_employee; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? REGISTER_DATE
        {
            set { _register_date = value; }
            get { return _register_date; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string REGISTER_MAN
        {
            set { _register_man = value; }
            get { return _register_man; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string REGISTER_SITE
        {
            set { _register_site = value; }
            get { return _register_site; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SEND_SITE
        {
            set { _send_site = value; }
            get { return _send_site; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DISPATCH_SITE
        {
            set { _dispatch_site = value; }
            get { return _dispatch_site; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SEND_FINANCE_CENTER
        {
            set { _send_finance_center = value; }
            get { return _send_finance_center; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DISPATCH_FINANCE_CENTER
        {
            set { _dispatch_finance_center = value; }
            get { return _dispatch_finance_center; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? FREIGHT
        {
            set { _freight = value; }
            get { return _freight; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SEND_MAN
        {
            set { _send_man = value; }
            get { return _send_man; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SEND_MAN_PHONE
        {
            set { _send_man_phone = value; }
            get { return _send_man_phone; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SEND_MAN_ADDRESS
        {
            set { _send_man_address = value; }
            get { return _send_man_address; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ACCEPT_MAN_PHONE
        {
            set { _accept_man_phone = value; }
            get { return _accept_man_phone; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? INSURANCE
        {
            set { _insurance = value; }
            get { return _insurance; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string REMARK
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GOODS_TYPE
        {
            set { _goods_type = value; }
            get { return _goods_type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ACCEPT_MAN
        {
            set { _accept_man = value; }
            get { return _accept_man; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? BL_RETURN_BILL
        {
            set { _bl_return_bill = value; }
            get { return _bl_return_bill; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GOODS_NAME
        {
            set { _goods_name = value; }
            get { return _goods_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PACK_TYPE
        {
            set { _pack_type = value; }
            get { return _pack_type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SERVICMODE
        {
            set { _servicmode = value; }
            get { return _servicmode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DISPATCH_MODE
        {
            set { _dispatch_mode = value; }
            get { return _dispatch_mode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BILL_CODE_SUB
        {
            set { _bill_code_sub = value; }
            get { return _bill_code_sub; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? CUBE
        {
            set { _cube = value; }
            get { return _cube; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? VOLUME_WEIGHT
        {
            set { _volume_weight = value; }
            get { return _volume_weight; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? SETTLEMENT_WEIGHT
        {
            set { _settlement_weight = value; }
            get { return _settlement_weight; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SOURCE_TYPE
        {
            set { _source_type = value; }
            get { return _source_type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? NEED_UPLOAD_BOS
        {
            set { _need_upload_bos = value; }
            get { return _need_upload_bos; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? LAST_UPLOAD_BOS_TIME
        {
            set { _last_upload_bos_time = value; }
            get { return _last_upload_bos_time; }
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
        public DateTime? CREATE_TIME
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
        public DateTime? UPDATE_TIME
        {
            set { _update_time = value; }
            get { return _update_time; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? PUSH_BOS_STATUS
        {
            set { _push_bos_status = value; }
            get { return _push_bos_status; }
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
        /// 
        /// </summary>
        public string SEND_MAN_MOBILE
        {
            set { _send_man_mobile = value; }
            get { return _send_man_mobile; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SENDER_PROVINCE
        {
            set { _sender_province = value; }
            get { return _sender_province; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SENDER_CITY
        {
            set { _sender_city = value; }
            get { return _sender_city; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SENDER_COUNTY
        {
            set { _sender_county = value; }
            get { return _sender_county; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SENDER_COMPANYNAME
        {
            set { _sender_companyname = value; }
            get { return _sender_companyname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SENDER_POSTCODE
        {
            set { _sender_postcode = value; }
            get { return _sender_postcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ACCEPT_MAN_MOBILE
        {
            set { _accept_man_mobile = value; }
            get { return _accept_man_mobile; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ACCEPT_PROVINCE
        {
            set { _accept_province = value; }
            get { return _accept_province; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ACCEPT_CITY
        {
            set { _accept_city = value; }
            get { return _accept_city; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ACCEPT_COUNTY
        {
            set { _accept_county = value; }
            get { return _accept_county; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ACCEPT_COMPANYNAME
        {
            set { _accept_companyname = value; }
            get { return _accept_companyname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ACCEPT_POSTCODE
        {
            set { _accept_postcode = value; }
            get { return _accept_postcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? TOTAL_PRICE
        {
            set { _total_price = value; }
            get { return _total_price; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? WEIGHT_RATE
        {
            set { _weight_rate = value; }
            get { return _weight_rate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? VOLUME_RATE
        {
            set { _volume_rate = value; }
            get { return _volume_rate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? LEAST_EXPENSES
        {
            set { _least_expenses = value; }
            get { return _least_expenses; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OUTSYS_PAYTYPE
        {
            set { _outsys_paytype = value; }
            get { return _outsys_paytype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TRANSPORT_TYPE
        {
            set { _transport_type = value; }
            get { return _transport_type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal SUPPORTVALUE
        {
            set { _supportvalue = value; }
            get { return _supportvalue; }
        }

        /// <summary>
        /// 送货费
        /// </summary>
        public decimal? DELIVERYPRICE
        {
            set { _deliveryprice = value; }
            get { return _deliveryprice; }
        }
        /// <summary>
        /// 回单费
        /// </summary>
        public decimal? BACKSIGNBILLPRICE
        {
            set { _backsignbillprice = value; }
            get { return _backsignbillprice; }
        }
        /// <summary>
        /// 代收货款费
        /// </summary>
        public decimal? CODPRICE
        {
            set { _codprice = value; }
            get { return _codprice; }
        }
        #endregion Model

    }
}

