using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using KYOMS.Core20.Common.Extend;
using KYOMS.Core20.Services.Ali.Common;
using KYOMS.Core20.Services.Ali.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KYOMS.Core20.Services.Ali.Controllers
{
    [Route("api/[controller]")]
    public class TestOrdersController : ApiController
    {
        #region 捕获原始的Request.Body并从原始缓冲区中读取参数
        // TODO：POST api/testorders/addorder
        [HttpPost("AddOrder")]
        public async Task<JsonResult> AddOrder()
        {
            var requestContent = await HttpContext.Request.GetRawBodyStringAsync();
            requestContent = HttpUtility.UrlDecode(requestContent);

            //Response.WriteAsync()
            return Json(new { RequestContent = requestContent });
        }


        // TODO：POST api/testorders/addorder2
        [HttpPost("AddOrder2")]
        public async Task<JsonResult> AddOrder2()
        {
            var requestContent = await HttpContext.Request.GetRawBodyBytesAsync();
            var result = Encoding.UTF8.GetString(requestContent);
            result = HttpUtility.UrlDecode(result);

            //Response.WriteAsync()
            return Json(new { RequestContent = result });
        }
        #endregion

        #region 自定义InputFormatter测试
        // TODO：POST api/testorders/addorder4
        [HttpPost("addorder4")]
        public async Task AddOrder4([FromBody] string rawString)
        {
            await Task.Delay(1);
            var result = HttpUtility.UrlDecode(rawString);

            await WriteAsync(result);
        }
        // TODO：POST api/testorders/rawstring
        [HttpPost("rawstring")]
        public async Task<JsonResult> RawRequestBodyFormatter([FromBody] string rawString)
        {
            await Task.Delay(1);

            var result = HttpUtility.UrlDecode(rawString);

            return Json(new { result = result });
        }
        // TODO：POST api/testorders/rawbytes
        [HttpPost("rawbytes")]
        public async Task<JsonResult> RawBytesFormatter([FromBody] byte[] rawData)
        {
            await Task.Delay(1);

            var result = Encoding.UTF8.GetString(rawData);
            result = HttpUtility.UrlDecode(result);
            
            return Json(new { result = Encoding.UTF8.GetString(rawData) });
        }
        #endregion

        // TODO：POST api/testorders/addorder3
        // contentType：application/x-www-form-urlencoded
        [HttpPost("AddOrder3")]
        public async Task<JsonResult> AddOrder3(RequestModel model)
        {
            await Task.Delay(1);

            return Json(new { RequestContent = model });
        }
    }
}
