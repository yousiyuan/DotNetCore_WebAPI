using System;
namespace KYOMS.Core20.Entity.Oracle
{
	/// <summary>
	/// 订单运单关系表
	/// </summary>
	[Serializable]
	public partial class T_ORDER_WAYBILL_MAP
	{
		public T_ORDER_WAYBILL_MAP()
		{}
		#region Model
		private decimal _map_id;
		private string _order_no;
		private string _mail_no;
		private string _remark;
		private string _create_by;
		private DateTime? _create_time;
		private string _update_by;
		private DateTime? _update_time;
		/// <summary>
		/// 
		/// </summary>a
		public decimal MAP_ID
		{
			set{ _map_id=value;}
			get{return _map_id;}
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
		/// 运单号
		/// </summary>
		public string MAIL_NO
		{
			set{ _mail_no=value;}
			get{return _mail_no;}
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
		public DateTime? CREATE_TIME
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
		public DateTime? UPDATE_TIME
		{
			set{ _update_time=value;}
			get{return _update_time;}
		}
		#endregion Model

	}
}

