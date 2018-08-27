using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KYOMS.Core20.DE.Model
{
    /// <summary>
    /// 更新淘宝订单模型
    /// </summary>
    public class UpdateTaobaoOrderModel
    {
        /// <summary>
        /// 物流公司编号
        /// </summary>
        public string logisticProviderID { get; set; }
        /// <summary>
        /// 电商标识；淘宝发送的消息，此字段固定为“TAOBAO”；
        /// </summary>
        public string ecCompanyId { get; set; }
        /// <summary>
        /// 更新内容
        /// </summary>
        public List<field> fieldList { get; set; }
    }

    /// <summary>
    /// 更新字段内容
    /// </summary>
    public class field
    {
        /// <summary>
        /// 订单号--物流平台的物流号（不能为空）
        /// </summary>
        public string txLogisticID { get; set; }
        /// <summary>
        /// 字段名称-可更新字段：1、mailNo;2、weight；3、status；4、exception；5、suspect
        /// </summary>
        public string fieldName { get; set; }
        /// <summary>
        /// 字段内容
        /// </summary>
        public string fieldValue { get; set; }
        /// <summary>
        /// 取消订单、不接单、不揽收时，此字段用于填写原因;
        /// </summary>
        public string remark { get; set; }
    }
}