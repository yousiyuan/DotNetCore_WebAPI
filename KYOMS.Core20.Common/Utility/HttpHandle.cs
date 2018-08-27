using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KYOMS.Core20.Common.Utility
{
    public class HttpHandle : IDisposable
    {
        #region 变量定义
        bool _disposed;
        HttpClient _httpClient;
        string _postUrl;
        #endregion

        /// <summary>
        /// 构造函数初始化HttpClient对象
        /// </summary>
        public HttpHandle(string url)
        {
            //HttpClient会遵循DNS TTL（生存期）值，默认为1小时,这个时间过长，需要修改为1-5分钟
            var sp = ServicePointManager.FindServicePoint(new Uri(url));
            sp.ConnectionLeaseTimeout = 60 * 1000 * 5; // 5分钟
            _postUrl = url;
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
        /// 生成需要推送的数据,包含报文和报头
        /// </summary>
        /// <param name="logisticsInterface"></param>
        /// <returns></returns>
        private Dictionary<string, string> InitData(string logisticsInterface,string secretKey)
        {
            try
            {
                var data = new Dictionary<string, string>();
                data.Add("data_digest", SignHelper.CreateDataDigest(logisticsInterface, secretKey)); //创建签名
                data.Add("logistics_interface", logisticsInterface); //请求报文内容
                return data;
            }
            catch (Exception e)
            {
                var message = new StringBuilder();
                message.Append("生成需要推送的数据时，发生错误:\r\n");
                message.Append("数据内容：" + logisticsInterface);
                message.Append(e.Message + e.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// 异步推送数据
        /// </summary>
        /// <param name="contentDictionary">数据报文键值对</param>
        /// <returns>返回请求报文</returns>
        public string PostAsync(Dictionary<string, string> contentDictionary)
        {
            try
            {
                var content = new FormUrlEncodedContent(contentDictionary);
                var response = _httpClient.PostAsync(_postUrl, content).Result;
                var responseStr = response.Content.ReadAsStringAsync().Result;
                return responseStr;
            }
            catch (HttpRequestException e)
            {
                var message = new StringBuilder();
                message.Append("异步推送数据时,出现HttpRequestException异常，发生错误:\r\n");
                message.Append("推送地址是：" + _postUrl + "\r\n");
                message.Append(e.Message + e.StackTrace + e.Data);
                return null;
            }
        }

        /// <summary>
        /// 必须，以备程序员忘记了显式调用Dispose方法
        /// </summary>
        ~HttpHandle()
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
