using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;

namespace KYOMS.Core20.Services.Cainiao.Common
{
    /// <inheritdoc />
    /// <summary>
    /// 自定义InputFormatter
    /// </summary>
    public class HandleRequestBodyFormatter : InputFormatter
    {
        //TODO  对于自定义InputFormatter需要满足以下两个条件：
        //  （1）必须使用[FromBody] 特性来触发它。
        //  （2）对于对应的请求内容需自定义对应处理。

        public HandleRequestBodyFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/plain"));
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/octet-stream"));
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/x-www-form-urlencoded"));
        }


        /// <inheritdoc />
        /// <summary>
        /// 允许 text/plain, application/octet-stream和没有Content-Type的参数类型解析到原始数据
        /// 自定义InputFormatter使用CanRead方法来检查要支持的请求内容类型
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override bool CanRead(InputFormatterContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            var contentType = context.HttpContext.Request.ContentType;
            if (string.IsNullOrEmpty(contentType)
                || contentType == "text/plain"
                || contentType == "text/json"
                || contentType == "application/x-www-form-urlencoded"
                || contentType == "application/json"
                || contentType == "application/octet-stream")
                return true;

            return false;
        }

        /// <inheritdoc />
        /// <summary>
        /// 处理text/plain或者没有Content-Type作为字符串结果
        /// 处理application/octet-stream类型作为byte[]数组结果
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
        {
            var request = context.HttpContext.Request;
            var contentType = context.HttpContext.Request.ContentType;


            if (string.IsNullOrEmpty(contentType)
                || contentType == "text/plain"
                || contentType == "text/json"
                || contentType == "application/json")
            {
                using (var reader = new StreamReader(request.Body))
                {
                    var content = await reader.ReadToEndAsync();
                    return await InputFormatterResult.SuccessAsync(content);
                }
            }
            if (contentType == "application/x-www-form-urlencoded")
            {
                var keyvaluepair = new List<string>();
                if (request.Method == HttpMethod.Get.ToString())
                {
                    foreach (var key in request.Query.Keys)
                    {
                        var value = request.Query[key];
                        keyvaluepair.Add($"{key}={HttpUtility.UrlEncode(value)}");
                    }
                }
                if (request.Method == HttpMethod.Post.ToString())
                {
                    foreach (var key in request.Form.Keys)
                    {
                        var value = request.Form[key];
                        keyvaluepair.Add($"{key}={HttpUtility.UrlEncode(value)}");
                    }
                }
                var content = string.Join('&', keyvaluepair);
                return await InputFormatterResult.SuccessAsync(content);
            }
            if (contentType == "application/octet-stream")
            {
                using (var ms = new MemoryStream(2048))
                {
                    await request.Body.CopyToAsync(ms);
                    var content = ms.ToArray();
                    return await InputFormatterResult.SuccessAsync(content);
                }
            }

            return await InputFormatterResult.FailureAsync();
        }
    }
}
