using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace KYOMS.Core20.Services.Cainiao.Common
{
    public static class StringExtensions
    {
        public static Dictionary<string, string> UrlFormat(this string inputContent)
        {
            try
            {
                var dict = new Dictionary<string, string>();
                var array = inputContent.Split('&');
                foreach (var s in array)
                {
                    var length = s.IndexOf("=", 0, StringComparison.Ordinal);
                    var value = s.Remove(0, length + 1).Trim();
                    dict.Add(s.Substring(0, length).Trim(), HttpUtility.UrlDecode(value));
                }
                return dict;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
        }
    }
}
