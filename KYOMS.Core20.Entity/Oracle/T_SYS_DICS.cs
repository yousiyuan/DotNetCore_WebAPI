using System;
using System.Collections.Generic;
using System.Text;

namespace KYOMS.Core20.Entity.Oracle
{
    public class T_SYS_DICS
    {
        public int ID { get; set; }
        public string TITLE { get; set; }
        public string CODE { get; set; }
        public decimal SORTNUM { get; set; }
        public decimal PARENTID { get; set; }
        public decimal CATEGORYID { get; set; }
        public decimal STATUS { get; set; }
        public decimal ISDEFAULT { get; set; }
        public string REMARK { get; set; }
        public string CREATE_BY { get; set; }
        public DateTime CREATE_TIME { get; set; }
        public string UPDATE_BY { get; set; }
        public DateTime UPDATE_TIME { get; set; }
        public string DETAILS { get; set; }
    }
}
