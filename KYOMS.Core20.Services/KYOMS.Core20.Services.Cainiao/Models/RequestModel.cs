using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace KYOMS.Core20.Services.Cainiao.Models
{
    public class RequestModel
    {
        public string logistics_interface { get; set; }
        public string data_digest { get; set; }
        public string msg_type { get; set; }
        public string msg_id { get; set; }
        public string ecCompanyId { get; set; }
        public static RequestModel ModelDecode(RequestModel requestModel)
        {
            requestModel.logistics_interface = HttpUtility.UrlDecode(requestModel.logistics_interface, Encoding.UTF8);//请求报文
            requestModel.data_digest = HttpUtility.UrlDecode(requestModel.data_digest, Encoding.UTF8);//签名
            requestModel.msg_type = HttpUtility.UrlDecode(requestModel.msg_type, Encoding.UTF8);//消息类型
            requestModel.ecCompanyId = HttpUtility.UrlDecode(requestModel.ecCompanyId, Encoding.UTF8);//目的方编码【可选】
            return requestModel;
        }
    }
}
