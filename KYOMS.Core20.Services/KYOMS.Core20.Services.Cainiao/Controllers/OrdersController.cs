using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using KYOMS.Core20.Common.Utility;
using KYOMS.Core20.Services.Cainiao.CainiaoHelper;
using KYOMS.Core20.Services.Cainiao.Common;
using KYOMS.Core20.DE.CainiaoModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KYOMS.Core20.Services.Cainiao.Controllers
{
    [Route("api/[controller]")]
    public class OrdersController : ApiController
    {
        //TODO: POST api/orders/addorder
        /// <summary>
        /// 菜鸟仓配在线下单服务
        /// </summary>
        /// <param name="model">请求报文</param>
        /// <returns>返回一个已完成的任务</returns>
        [HttpPost("AddOrder")]
        public async Task AddOrder([FromBody] object model)
        {
            //0、获取请求报文
            var requestContent = "";
            if (Request.ContentType == "application/octet-stream")
            {
                var bytePacket = (byte[]) model;
                requestContent = Encoding.UTF8.GetString(bytePacket);
            }
            else
            {
                if (model == null) model = "";
                requestContent = model.ToString();
            }

            if (string.IsNullOrWhiteSpace(requestContent))
            {
                var log = $"请求类型是{Request.ContentType}的请求要求报文不允许为空，添加订单操作终止";
                LogWarn($"{log} 日期：{DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                await WriteAsync(log);
                return;
            }
            await AddOrderHandle(requestContent, Request.ContentType);
        }

        async Task AddOrderHandle(string requestContent, string requestType)
        {
            var logtext = $"请求类型：{requestType}添加订单AddOrder原始请求报文：{requestContent}";
            LogInfo(logtext);

            var sw = new Stopwatch();
            sw.Start();

            //1、解析报文
            var dic = requestContent.UrlFormat();
            var logisticsInterface = dic.ContainsKey("logistics_interface") ? dic["logistics_interface"] : "";
            var dataDigest = dic.ContainsKey("data_digest") ? dic["data_digest"] : "";
            var msgType = dic.ContainsKey("msg_type") ? dic["msg_type"] : "";

            //2、检查参数,为空则返回
            if (string.IsNullOrWhiteSpace(logisticsInterface) || string.IsNullOrWhiteSpace(dataDigest))
            {
                LogError($"请求消息报文内容为空导致添加订单操作终止，{logtext}");
                await WriteAsync(new { errorCode = "S05", errorMsg = "消息内容为空", success = false });
                LogInfo($"本次AddOrder提交订单操作耗时：{sw.ElapsedMilliseconds}毫秒");
                sw.Stop();
                return;
            }

            //3、消息类型检查
            if (!string.Equals(msgType, AppSettings.MsgType))
            {
                LogError($"请求消息类型验证失败导致添加订单操作终止，{logtext}");
                await WriteAsync(new { errorCode = "S04", errorMsg = "消息类型msg_type错误", success = false });
                LogInfo($"本次AddOrder提交订单操作耗时：{sw.ElapsedMilliseconds}毫秒");
                sw.Stop();
                return;
            }

            //4、签名检查
            try
            {
                var dataDigestContent = HttpUtility.UrlEncode(logisticsInterface);
                if (!SignHelper.CheckDataDigest(dataDigestContent, dataDigest, AppSettings.SecretKey))
                {
                    LogError($"签名验证失败导致添加订单操作终止，{logtext}");
                    await WriteAsync(new { errorCode = "S02", errorMsg = "消息签名不符，请检查签名", success = false });
                    LogInfo($"本次AddOrder提交订单操作耗时：{sw.ElapsedMilliseconds}毫秒");
                    sw.Stop();
                    return;
                }
            }
            catch (Exception ex)
            {
                LogError($"签名验证过程中出现异常导致添加订单操作终止，{logtext}", ex.Message, ex.StackTrace);
                await WriteAsync(new { errorCode = "S02", errorMsg = "报文消息签名检查失败", success = false });
                LogInfo($"本次AddOrder提交订单操作耗时：{sw.ElapsedMilliseconds}毫秒");
                sw.Stop();
                return;
            }

            //5、报文转换为实体对象
            TmsOrderModel model;
            try
            {
                //URL解码
                model = JsonConvert.DeserializeObject<TmsOrderModel>(logisticsInterface);
            }
            catch (Exception ex)
            {
                var errTitle = "报文内容格式不符合规范,Json格式转换失败";
                LogError($"{errTitle}导致添加订单操作终止，{logtext}", ex.Message, ex.StackTrace);
                await WriteAsync(new { errorCode = "S01", errorMsg = errTitle, success = false });
                LogInfo($"本次AddOrder提交订单操作耗时：{sw.ElapsedMilliseconds}毫秒");
                sw.Stop();
                return;
            }

            //6、创建订单映射
            LogInfo($"运单号：{model.mailNo}外部订单号{model.logisticsId}提交订单原始数据：{JsonConvert.SerializeObject(model)}");
            var mySqlOrder = CainiaoHandler.CreateMap(model, logisticsInterface);
            if (mySqlOrder == null)
            {
                LogInfo($"本次AddOrder提交订单操作耗时：{sw.ElapsedMilliseconds}毫秒");
                sw.Stop();
                return;
            }

            LogInfo($"数据校验与处理用时：{sw.ElapsedMilliseconds}毫秒");

            var hasSuccess = await AppService.AddOrder(mySqlOrder);
            if (!hasSuccess)
            {
                await WriteAsync(new { errorCode = "S07", errorMsg = "订单写入数据库失败", success = false });
                LogInfo($"本次AddOrder提交订单操作耗时：{sw.ElapsedMilliseconds}毫秒");
                sw.Stop();
                return;
            }
            LogInfo($"运单号是{mySqlOrder.OUTSYS_BILL_CODE}外部订单号是{mySqlOrder.OUTSYS_ORDER_NO}的菜鸟仓配订单已成功落盘");
            await WriteAsync(new { errorCode = "", errorMsg = "", success = true });

            LogInfo($"总计用时：{sw.ElapsedMilliseconds}毫秒");
            sw.Stop();
        }
        
        //TODO: POST api/orders/addorder
        /// <summary>
        /// 用于测试
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetServiceInfo")]
        public async Task GetServiceInfo()
        {
            var result = HttpContext.Request.GetAbsoluteUri();
            await WriteAsync(result);
        }
    }
}
