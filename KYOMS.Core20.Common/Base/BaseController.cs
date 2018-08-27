using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace KYOMS.Core20.Common.Base
{
    public class BaseController : Controller
    {
        private JsonSerializerSettings jsonSettings;
        public JsonSerializerSettings JsonSettings
        {
            get
            {
                if (jsonSettings == null)
                {
                    jsonSettings = new JsonSerializerSettings();
                }
                jsonSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                jsonSettings.ContractResolver = new DefaultContractResolver();
                jsonSettings.Formatting = Formatting.Indented;
                return jsonSettings;
            }
        }
        public BaseController()
        {

        }

    }
}
