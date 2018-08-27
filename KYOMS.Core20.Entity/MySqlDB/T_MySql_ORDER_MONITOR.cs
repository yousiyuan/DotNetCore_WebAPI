using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KYOMS.Core20.Entity.MySqlDB
{
    /// <summary>
    /// 订单监控实体类
    /// </summary>
    public class T_MySql_ORDER_MONITOR : BaseEntity
    {
        /// <summary>
        /// 编号，自动增长
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string ORDER_NO { get; set; }
        /// <summary>
        /// 外部订单号
        /// </summary>
        public string OUTSYS_ORDERCODE { get; set; }
        /// <summary>
        /// 运单号
        /// </summary>
        public string MAILNO { get; set; }
        /// <summary>
        /// 订单来源
        /// </summary>
        public string ORDER_SOURCE { get; set; }
        /// <summary>
        /// 动作时间
        /// </summary>
        public DateTime ACTION_TIME { get; set; }
        /// <summary>
        /// 耗时
        /// </summary>
        public double ELAPSED { get; set; }
        /// <summary>
        /// 揽件
        /// </summary>
        public int GOT { get; set; }
        /// <summary>
        /// 揽件时间
        /// </summary>
        public DateTime GOT_TIME { get; set; }
        /// <summary>
        /// 发件
        /// </summary>
        public int DEPARTURE { get; set; }
        /// <summary>
        /// 发件时间
        /// </summary>
        public DateTime DEPARTURE_TIME { get; set; }
        /// <summary>
        /// 到件
        /// </summary>
        public int ARRIVAL { get; set; }
        /// <summary>
        /// 到件时间
        /// </summary>
        public DateTime ARRIVAL_TIME { get; set; }
        /// <summary>
        /// 派件
        /// </summary>
        public int SENT_SCAN { get; set; }
        /// <summary>
        /// 派件时间
        /// </summary>
        public DateTime SENT_SCAN_TIME { get; set; }
        /// <summary>
        /// 签收
        /// </summary>
        public int SIGNED { get; set; }
        /// <summary>
        /// 签收时间
        /// </summary>
        public DateTime SIGNED_TIME { get; set; }
        /// <summary>
        /// 问题件 -留仓，自提
        /// </summary>
        public int OTHER { get; set; }
        /// <summary>
        /// 问题件 -留仓，自提 时间
        /// </summary>
        public DateTime OTHER_TIME { get; set; }
        /// <summary>
        /// 异常签收
        /// </summary>
        public int FAILED { get; set; }
        /// <summary>
        /// 异常签收时间
        /// </summary>
        public DateTime FAILED_TIME { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public int ORDER_STATUS { get; set; }
        /// <summary>
        /// 订单创建时间
        /// </summary>
        public DateTime? ORDER_TIME { get; set; }
        /// <summary>
        /// 记录创建时间
        /// </summary>
        public DateTime? CREATE_TIME { get; set; }
        /// <summary>
        /// 揽件耗时 揽件时间减订单创建时间
        /// </summary>
        public double GOT_ELAPSED { get; set; }
    }
}
