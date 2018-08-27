
using System;
namespace KYOMS.Core20.Entity
{
	/// <summary>
	/// T_SITE_INFO:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class T_SITE_INFO
	{
		public T_SITE_INFO()
		{}
		#region Model
		private string _site_code;
		private string _site_name;
		private string _site_shortname;
		private string _superior_site;
		private string _superior_code;
		private string _site_type;
		private string _area_name;
		private string _range_name;
		private string _province;
		private string _city;
		private string _county_name;
		private string _type_kind;
		private string _manager_phone;
		private decimal _enabled;
		private string _detail_address;
		private string _remark;
		private string _create_by;
		private DateTime _create_time;
		private string _update_by;
		private DateTime _update_time;
		/// <summary>
		/// 网点编号
		/// </summary>
		public string SITE_CODE
		{
			set{ _site_code=value;}
			get{return _site_code;}
		}
		/// <summary>
		/// 网点名称
		/// </summary>
		public string SITE_NAME
		{
			set{ _site_name=value;}
			get{return _site_name;}
		}
		/// <summary>
		/// 网点简称
		/// </summary>
		public string SITE_SHORTNAME
		{
			set{ _site_shortname=value;}
			get{return _site_shortname;}
		}
		/// <summary>
		/// 上级网点名称
		/// </summary>
		public string SUPERIOR_SITE
		{
			set{ _superior_site=value;}
			get{return _superior_site;}
		}
		/// <summary>
		/// 上级网点CODE
		/// </summary>
		public string SUPERIOR_CODE
		{
			set{ _superior_code=value;}
			get{return _superior_code;}
		}
		/// <summary>
		/// 网点属性
		/// </summary>
		public string SITE_TYPE
		{
			set{ _site_type=value;}
			get{return _site_type;}
		}
		/// <summary>
		/// 片区
		/// </summary>
		public string AREA_NAME
		{
			set{ _area_name=value;}
			get{return _area_name;}
		}
		/// <summary>
		/// 地区
		/// </summary>
		public string RANGE_NAME
		{
			set{ _range_name=value;}
			get{return _range_name;}
		}
		/// <summary>
		/// 省
		/// </summary>
		public string PROVINCE
		{
			set{ _province=value;}
			get{return _province;}
		}
		/// <summary>
		/// 市
		/// </summary>
		public string CITY
		{
			set{ _city=value;}
			get{return _city;}
		}
		/// <summary>
		/// 区
		/// </summary>
		public string COUNTY_NAME
		{
			set{ _county_name=value;}
			get{return _county_name;}
		}
		/// <summary>
		/// 网点类型
		/// </summary>
		public string TYPE_KIND
		{
			set{ _type_kind=value;}
			get{return _type_kind;}
		}
        /// <summary>
        /// 网点负责人姓名
        /// </summary>
        public string MANAGER_NAME { get; set; }

		/// <summary>
		/// 负责人电话
		/// </summary>
		public string MANAGER_PHONE
		{
			set{ _manager_phone=value;}
			get{return _manager_phone;}
		}
		/// <summary>
		/// 是否启用
		/// </summary>
		public decimal ENABLED
		{
			set{ _enabled=value;}
			get{return _enabled;}
		}
		/// <summary>
		/// 详细地址
		/// </summary>
		public string DETAIL_ADDRESS
		{
			set{ _detail_address=value;}
			get{return _detail_address;}
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

