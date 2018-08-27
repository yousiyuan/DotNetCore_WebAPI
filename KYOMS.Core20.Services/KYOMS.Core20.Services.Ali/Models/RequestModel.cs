using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KYOMS.Core20.Services.Ali.Models
{
    public class RequestModel
    {
        public string logistics_interface { get; set; }
        public string data_digest { get; set; }
        public string partner_code { get; set; }
        public string from_code { get; set; }
        public string msg_type { get; set; }
        public string msg_id { get; set; }
    }
}
