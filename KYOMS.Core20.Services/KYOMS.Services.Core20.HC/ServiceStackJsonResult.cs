using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KYOMS.Services.Core20.HC.Controllers
{
    internal class ServiceStackJsonResult : JsonResult
    {
        public ServiceStackJsonResult(object value, JsonSerializerSettings serializerSettings) : base(value, serializerSettings)
        {
        }
    }
}