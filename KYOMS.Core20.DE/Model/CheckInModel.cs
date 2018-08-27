using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace KYOMS.Core20.DE.Model
{
    public class CheckInModel
    {
        public string logistics_interface { get; set; }
        public string data_digest { get; set; }
        public string msg_type { get; set; }
        public static CheckInModel ModelDecode(CheckInModel requestModel)
        {
            requestModel.logistics_interface = HttpUtility.UrlDecode(requestModel.logistics_interface, Encoding.UTF8);//请求报文
            requestModel.data_digest = HttpUtility.UrlDecode(requestModel.data_digest, Encoding.UTF8);//签名
            requestModel.msg_type = HttpUtility.UrlDecode(requestModel.msg_type, Encoding.UTF8);//消息类型
            return requestModel;
        }
    }
}
