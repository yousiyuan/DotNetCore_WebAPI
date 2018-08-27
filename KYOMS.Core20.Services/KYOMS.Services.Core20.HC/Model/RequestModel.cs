using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace KYOMS.Services.Core20.HC.Model
{
    public class RequestModel
    {
        public string @params { get; set; }
        public string digest { get; set; }
        public string msg_type { get; set; }
        public string msg_id { get; set; }
        public string ecCompanyId { get; set; }
        public string timestamp { get; set; }
        public string companyCode { get; set; }

        public static RequestModel ModelDecode(RequestModel requestModel)
        {
            requestModel.@params = HttpUtility.UrlDecode(requestModel.@params, Encoding.UTF8);//请求报文
            requestModel.digest = HttpUtility.UrlDecode(requestModel.digest, Encoding.UTF8);//签名
            requestModel.msg_type = HttpUtility.UrlDecode(requestModel.msg_type, Encoding.UTF8);//消息类型
            requestModel.ecCompanyId = HttpUtility.UrlDecode(requestModel.ecCompanyId, Encoding.UTF8);//目的方编码【可选】
            return requestModel;
        }


    }
}
