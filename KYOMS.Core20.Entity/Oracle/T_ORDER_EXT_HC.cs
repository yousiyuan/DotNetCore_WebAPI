using System;
namespace KYOMS.Core20.Entity
{
	/// <summary>
	/// 订单扩展表
	/// </summary>
	[Serializable]
	public partial class T_ORDER_EXT_HC
	{
		public T_ORDER_EXT_HC()
		{}
		#region Model
		private decimal _id;
		private string _order_no;
		private string _outsys_code;
        private string _outsys_source;
		private string _transport_name;
		private string _inspection;
		private decimal _inspection_price;
		private decimal _have_pay_price;
		private decimal _no_sale_price;
		private decimal _order_subtract;
		private decimal _freigh_trate_discount;
		private decimal _pay_type_price;
        private decimal _delivery_notice_price;
        private string _receiver_area;
        private string _sender_area;
		private string _remark;
		private string _create_by;
		private DateTime _create_time;
		private string _update_by;
		private DateTime _update_time;
		/// <summary>
		/// 
		/// </summary>
		public decimal ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 订单编号
		/// </summary>
		public string ORDER_NO
		{
			set{ _order_no=value;}
			get{return _order_no;}
		}
        /// <summary>
        /// 订单来源
        /// </summary>
        public string ORDER_SOURCE
        {
            set { _outsys_source = value; }
            get { return _outsys_source; }
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
		/// 运输方式名称
		/// </summary>
		public string TRANSPORT_NAME
		{
			set{ _transport_name=value;}
			get{return _transport_name;}
		}
		/// <summary>
		/// 是否开箱验货
		/// </summary>
		public string INSPECTION
		{
			set{ _inspection=value;}
			get{return _inspection;}
		}
		/// <summary>
		/// 开箱验货价格
		/// </summary>
		public decimal INSPECTION_PRICE
		{
			set{ _inspection_price=value;}
			get{return _inspection_price;}
		}
		/// <summary>
		/// 已付款金额
		/// </summary>
		public decimal HAVE_PAY_PRICE
		{
			set{ _have_pay_price=value;}
			get{return _have_pay_price;}
		}
		/// <summary>
		/// 优惠前总价
		/// </summary>
		public decimal NO_SALE_PRICE
		{
			set{ _no_sale_price=value;}
			get{return _no_sale_price;}
		}
		/// <summary>
		/// 全单直减费用
		/// </summary>
		public decimal ORDER_SUBTRACT
		{
			set{ _order_subtract=value;}
			get{return _order_subtract;}
		}
		/// <summary>
		/// 运价折扣
		/// </summary>
		public decimal FREIGH_TRATE_DISCOUNT
		{
			set{ _freigh_trate_discount=value;}
			get{return _freigh_trate_discount;}
		}
        /// <summary>
        /// 付款方式费用
        /// </summary>
        public decimal PAY_TYPE_PRICE
        {
            set { _pay_type_price = value; }
            get { return _pay_type_price; }
        }
        /// <summary>
        /// 等通知派送费用
        /// </summary>
        public decimal DELIVERY_NOTICE_PRICE
        {
            set { _delivery_notice_price = value; }
            get { return _delivery_notice_price; }
        }
        /// <summary>
        /// 收货人区域
        /// </summary>
        public string RECEIVER_AREA
        {
            set { _receiver_area = value; }
            get { return _receiver_area; }
        }
        /// <summary>
        /// 发货人区域
        /// </summary>
        public string SENDER_AREA
        {
            set { _sender_area = value; }
            get { return _sender_area; }
        }
		/// <summary>
		/// 
		/// </summary>
		public string REMARK
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CREATE_BY
		{
			set{ _create_by=value;}
			get{return _create_by;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime CREATE_TIME
		{
			set{ _create_time=value;}
			get{return _create_time;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UPDATE_BY
		{
			set{ _update_by=value;}
			get{return _update_by;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime UPDATE_TIME
		{
			set{ _update_time=value;}
			get{return _update_time;}
		}
		#endregion Model

	}
}

