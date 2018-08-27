using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KYOMS.Core20.DE.AliModel
{
    public class MQuerySite
    {
        /// <summary>
        /// orderid
        /// </summary>
        public string orderid { get; set; }
        /// <summary>
        /// address
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 来源OMS
        /// </summary>
        public string operSource { get; set; } 
    }

    public class SiteResultInfo
    {
        /// <summary>
        /// 明细
        /// </summary>
        public List<SiteDetail> RESULT { get; set; }
        /// <summary>
        /// 返回码
        /// </summary>
        public string RESULT_CODE { get; set; }
        /// <summary>
        /// 原因
        /// </summary> 
        public string RESULT_DESC { get; set; }
     
    }

    public class SiteDetail
    {
        public string address { get; set; }
        public string bizAreaValue { get; set; } 
        public string statusFlag { get; set; }
        public string orderid { get; set; } 
        public string distance { get; set; }
        public string operSource { get; set; } 
    }
}