
using System;
namespace KYOMS.Core20.Entity
{
    /// <summary>
    /// 订单修改记录;
    //部分同步bos
    /// </summary>
    [Serializable]
    public partial class T_ORDER_STATUS_TRACE
    {
        public T_ORDER_STATUS_TRACE()
        { }
        #region Model
        private decimal _id;
        private string _order_no;
        private decimal _changed_status;
        private decimal _total_price;
        private string _remark;
        private decimal _op_type;
        private string _op_site_code;
        private string _create_by;
        private DateTime _create_time;
        private string _update_by;
        private DateTime _update_time;
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
        /// 变更后状态
        /// </summary>
        public decimal CHANGED_STATUS
        {
            set { _changed_status = value; }
            get { return _changed_status; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal TOTAL_PRICE
        {
            set { _total_price = value; }
            get { return _total_price; }
        }
        /// <summary>
        /// 描述
        /// </summary>
        public string REMARK
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 1: 状态变更 2: 订单内容修改记录
        /// </summary>
        public decimal OP_TYPE
        {
            set { _op_type = value; }
            get { return _op_type; }
        }
        /// <summary>
        /// 更新网点编码
        /// </summary>
        public string OP_SITE_CODE
        {
            set { _op_site_code = value; }
            get { return _op_site_code; }
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
        #endregion Model
        public DateTime OP_TIME { get; set; }
    }
}

