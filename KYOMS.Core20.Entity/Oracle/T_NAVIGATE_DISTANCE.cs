using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KYOMS.Core20.Entity
{
    /// <summary>
    /// 订单导航距离记录
    /// </summary>
    [Serializable]
    public partial class T_NAVIGATE_DISTANCE
    {
        public T_NAVIGATE_DISTANCE()
        {
        }

        #region Model

        private decimal _id;
        private string _order_no;
        private string _mail_no;
        private string _pickup_distance;
        private string _send_distance;
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
        /// 运单号
        /// </summary>
        public string MAIL_NO
        {
            set { _mail_no = value; }
            get { return _mail_no; }
        }

        /// <summary>
        /// 揽件距离,单位km
        /// </summary>
        public string PICKUP_DISTANCE
        {
            set { _pickup_distance = value; }
            get { return _pickup_distance; }
        }

        /// <summary>
        /// 派件距离,单位km
        /// </summary>
        public string SEND_DISTANCE
        {
            set { _send_distance = value; }
            get { return _send_distance; }
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

    }
}
