using System.Text;
using System.Threading.Tasks;
using KYOMS.Core20.Application;
using KYOMS.Core20.Common.Log4NetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace KYOMS.Core20.Services.Ali.Common
{
    public class ApiController : Controller
    {
        protected AliAppService AppService;
        //protected LoggerHandle Logger;

        #region Json格式相关
        private JsonSerializerSettings _jsonSettings;

        /// <summary>
        /// JSON序列化格式设置
        /// </summary>
        protected JsonSerializerSettings JsonSettings
        {
            get
            {
                if (_jsonSettings == null)
                {
                    _jsonSettings = new JsonSerializerSettings();
                }
                _jsonSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                _jsonSettings.ContractResolver = new DefaultContractResolver();
                _jsonSettings.Formatting = Formatting.Indented;
                return _jsonSettings;
            }
        }

        public override JsonResult Json(object data)
        {
            return Json(data, JsonSettings);
        }
        #endregion

        #region 输出相关
        protected async Task WriteAsync(object obj)
        {
            Response.ContentType = "text/plain;charset=utf-8";
            var result = JsonConvert.SerializeObject(obj, JsonSettings);
            await Response.WriteAsync(result);
            //await Response.WriteAsync(obj.ToJson(), System.Text.Encoding.GetEncoding("GBK"));
        }

        protected async Task WriteAsync(string value)
        {
            Response.ContentType = "text/plain;charset=utf-8";
            await Response.WriteAsync(value);
            //await Response.WriteAsync(value, System.Text.Encoding.GetEncoding("GBK"));
        }
        #endregion

        #region 日志记录相关

        protected void WriteLog(string logStr, string errMsg, string stackTrace, LogerType logerType)
        {
            var msgBuilder = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(logStr))
            {
                msgBuilder.Append($"{logStr} \r\n");
            }
            if (!string.IsNullOrWhiteSpace(errMsg))
            {
                msgBuilder.Append($"异常信息：{errMsg} \r\n");
            }
            if (!string.IsNullOrWhiteSpace(stackTrace))
            {
                msgBuilder.Append($"堆栈信息：{stackTrace} \r\n");
            }
            msgBuilder.ToString().WriteToLog(logerType);
        }

        protected void LogDebug(string logStr)
        {
            WriteLog(logStr, null, null, LogerType.Debug);
        }

        protected void LogInfo(string logStr)
        {
            WriteLog(logStr, null, null, LogerType.Info);
        }

        protected void LogWarn(string logStr)
        {
            WriteLog(logStr, null, null, LogerType.Warn);
        }

        protected void LogError(string logStr, string errMsg = null, string stackTrace = null)
        {
            WriteLog(logStr, errMsg, stackTrace, LogerType.Error);
        }

        protected void LogFatal(string logStr, string errMsg = null, string stackTrace = null)
        {
            WriteLog(logStr, errMsg, stackTrace, LogerType.Fatal);
        }

        #endregion

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //TODO:
            //  在Action执行前被调用
            AppService =
                new AliAppService(AppSettings.MySqlDbConfig, AppSettings.OracleDbConfig);
            //Logger = new LoggerHandle(null, AppSettings.Instance.LogFileName());

            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            //TODO:
            //  在Action执行后被调用
            AppService.Dispose();
            //Logger = null;

            base.OnActionExecuted(context);
        }
    }
}
