using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace KYOMS.Core20.Test.ParallelDemo
{
    public class HttpRequestUtility
    {
        private readonly HttpRequestMessage _httpRequestMessage;

        public HttpRequestUtility(HttpMethod method, string httpPostUrl)
        {
            _httpRequestMessage = new HttpRequestMessage(method, httpPostUrl + "?t=" + DateTime.Now.Millisecond);
        }

        public Task<string> HttpClientSendAsync(object data, string contentType)
        {
            var httpClient = new HttpClient {Timeout = new TimeSpan(0, 0, 10)};
            if (data != null)
            {
                var byteArray = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(data));
                var memoryStream = new MemoryStream(byteArray);
                _httpRequestMessage.Content = new StringContent(new StreamReader(memoryStream).ReadToEnd(), Encoding.UTF8, contentType);
            }
            return httpClient.SendAsync(_httpRequestMessage).ContinueWith(task =>
            {
                var response = task.Result;
                return response.Content.ReadAsStringAsync().ContinueWith(stringTask => stringTask.Result);
            }).Result;
        }
    }
}
