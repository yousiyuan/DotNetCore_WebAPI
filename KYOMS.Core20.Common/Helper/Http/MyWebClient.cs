using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace KYOMS.Core20.Common
{
    public class MyWebClient : WebClient
    {
        protected override WebRequest GetWebRequest(Uri address)
        {
            HttpWebRequest request = (HttpWebRequest)base.GetWebRequest(address);
            request.Timeout = 1000 * 30;
            request.ReadWriteTimeout = 1000 * 30;
            return request;
        }
    }
}
