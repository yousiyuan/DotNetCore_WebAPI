using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KYOMS.Services.Core20.OrderMonitor.Entity
{
    public class SearchEntity : BaseSearchEntity
    {
        public string ORDER_NO { get; set; }
        public string ORDER_SOURCE { get; set; }
        public string START_TIME { get; set; }
        public string END_TIME { get; set; }
        public string GOT { get; set; }
        public string DEPARTURE { get; set; }
        public string ARRIVAL { get; set; }
        public string SENT_SCAN { get; set; }
        public string SIGNED { get; set; }
        public string OTHER { get; set; }
        public string FAILED { get; set; }
        public string ORDER_STATUS { get; set; }
        public string OrderBy { get; set; }
    }
}
