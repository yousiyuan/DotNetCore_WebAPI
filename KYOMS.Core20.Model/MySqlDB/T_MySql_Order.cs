using System;

namespace KYOMS.Core20.Model.MySqlDB
{
    /// <summary>
    /// T_MySql_Order:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class T_MySql_Order : BaseEntity
    {
        public T_MySql_Order()
        { }
        #region Model
        private long _id;
        private string _outsys_order_no;
        private string _outsys_bill_code;
        private string _order_source;
        private string _msg_content;
        private string _msg_type;
        private string _remark;
        private int? _is_sync_success;
        private string _create_by;
        private DateTime? _create_time;
        private string _c1;
        private string _c2;
        private string _c3;
        /// <summary>
        /// 
        /// </summary>
        public long ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 外部订单号
        /// </summary>
        public string OUTSYS_ORDER_NO
        {
            set { _outsys_order_no = value; }
            get { return _outsys_order_no; }
        }
        /// <summary>
        /// 外部运单号
        /// </summary>
        public string OUTSYS_BILL_CODE
        {
            set { _outsys_bill_code = value; }
            get { return _outsys_bill_code; }
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
        /// 报文内容
        /// </summary>
        public string MSG_CONTENT
        {
            set { _msg_content = value; }
            get { return _msg_content; }
        }
        /// <summary>
        /// 消息类型：XML,JSON
        /// </summary>
        public string MSG_TYPE
        {
            set { _msg_type = value; }
            get { return _msg_type; }
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
        /// 是否推送成功 0:需要推送；1:推送成功；2:推送失败
        /// </summary>
        public int? IS_SYNC_SUCCESS
        {
            set { _is_sync_success = value; }
            get { return _is_sync_success; }
        }
        /// <summary>
        /// 创建人
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
        /// 备用字段1
        /// </summary>
        public string C1
        {
            set { _c1 = value; }
            get { return _c1; }
        }
        /// <summary>
        /// 备用字段2
        /// </summary>
        public string C2
        {
            set { _c2 = value; }
            get { return _c2; }
        }
        /// <summary>
        /// 备用字段3
        /// </summary>
        public string C3
        {
            set { _c3 = value; }
            get { return _c3; }
        }
        #endregion Model

    }
}