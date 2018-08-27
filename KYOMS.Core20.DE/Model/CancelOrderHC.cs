using System;
using System.Collections.Generic;
using System.Text;

namespace KYOMS.Core20.DE.Model
{
    public class CancelOrderHC
    {
        public string logisticCode { get; set; }  //慧聪物流编号
        public string logisticCompanyID { get; set; } //物流公司编号
        public string gmtCancel { get; set; } //撤销时间
        public string remark { get; set; }  //撤销原因
    }
}
