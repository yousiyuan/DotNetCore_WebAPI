using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace KYOMS.Core20.Services.Ali.Common
{
    public static class HttpRequestExtensions
    {
        /// <summary>
        /// 接受不带参数的POST或PUT数据,从Request.Body读取原始数据
        /// </summary>
        public static async Task<string> GetRawBodyStringAsync(this HttpRequest request, Encoding encoding = null)
        {
            if (encoding is null)
                encoding = Encoding.UTF8;

            using (var reader = new StreamReader(request.Body, encoding))
                return await reader.ReadToEndAsync();
        }
        /// <summary>
        /// 接受不带参数的POST或PUT数据,从Request.Body读取二进制数据
        /// </summary>
        public static async Task<byte[]> GetRawBodyBytesAsync(this HttpRequest request)
        {
            using (var ms = new MemoryStream(2048))
            {
                await request.Body.CopyToAsync(ms);
                return ms.ToArray();
            }
        }
    }
}
