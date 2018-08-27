using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using KYOMS.Core20.Common.LogCommon;

namespace KYOMS.Core20.Common
{
    public class HttpClientHandle : IDisposable
    {
        #region 变量定义

        private readonly LogHandle _logger = new LogHandle(typeof(HttpClientHandle));
        bool _disposed;
        HttpClient _httpClient;
        #endregion

        /// <summary>
        /// 构造函数初始化HttpClient对象
        /// </summary>
        public HttpClientHandle(string url)
        {
            //HttpClient会遵循DNS TTL（生存期）值，默认为1小时,这个时间过长，需要修改为1-5分钟
            var sp = ServicePointManager.FindServicePoint(new Uri(url));
            sp.ConnectionLeaseTimeout = 60 * 1000 * 5; // 5分钟
            InitHttpClient();
        }

        /// <summary>
        /// 初始化HttpClient
        /// </summary>
        private void InitHttpClient()
        {
            //设置HttpClientHandler的AutomaticDecompression
            var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip };

            //创建HttpClient（注意传入HttpClientHandler）
            _httpClient = new HttpClient(handler);
            _httpClient.DefaultRequestHeaders.Add("KeepAlive", "true"); // HTTP KeepAlive设为true，连接保持
            _httpClient.DefaultRequestHeaders.Add("UserAgent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.11 (KHTML, like Gecko) chrome/23.0.1271.95 Safari/537.11");
            _httpClient.DefaultRequestHeaders.Add("ContentType", "application/x-www-form-urlencoded;charset=UTF-8");
            _httpClient.Timeout = new TimeSpan(0, 0, 10); //超时时间设置为10秒
        }

        /// <summary>
        /// 异步推送数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public async Task<string> PostAsync(string url,Dictionary<string,string> dictionary)
        {
            try
            {
                var content = new FormUrlEncodedContent(dictionary);
                var response = _httpClient.PostAsync(url, content);
                var responseStr = await response.Result.Content.ReadAsStringAsync();
                return responseStr;
            }
            catch (HttpRequestException e)
            {
                var message = new StringBuilder();
                message.Append("异步推送数据时,出现HttpRequestException异常，发生错误:\r\n");
                message.Append("推送地址是：" + url + "\r\n");
                message.Append("推送内容是：" + JsonConvert.SerializeObject(dictionary) + "\r\n");
                message.Append("错误是：" + JsonConvert.SerializeObject(dictionary) + "\r\n");

                message.Append(e.Message + e.StackTrace + e.Data);
                _logger.Set(message.ToString(), LogHandle.LogerType.Fatal);
                return null;
            }
        }

        /// <summary>
        /// 必须，以备程序员忘记了显式调用Dispose方法
        /// </summary>
        ~HttpClientHandle()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (_disposed) return;
            _httpClient.Dispose();
            _disposed = true;
        }
    }
}
