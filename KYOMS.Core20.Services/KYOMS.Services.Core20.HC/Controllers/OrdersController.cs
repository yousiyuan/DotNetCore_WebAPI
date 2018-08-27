using KYOMS.Core20.Application;
using KYOMS.Core20.Application.Interface;
using KYOMS.Core20.Application.MySql;
using KYOMS.Core20.Common.Base;
using KYOMS.Core20.Common.Extend;
using KYOMS.Core20.Common.Log4NetCore;
using KYOMS.Core20.Common.LogCommon;
using KYOMS.Core20.DE.Model;
using KYOMS.Services.Core20.HC.Common;
using KYOMS.Services.Core20.HC.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Internal.System.Collections.Sequences;
using System;
using System.Threading.Tasks;

namespace KYOMS.Services.Core20.HC.Controllers
{
    [Route("api/[controller]")]
    public class OrdersController : BaseController
    {
        IHC_MySql_OrderService t_MySql_OrderService;
        IHC_ORDERService t_ORDERService;
        public OrdersController(IHC_MySql_OrderService _t_MySql_OrderService, IHC_ORDERService _t_ORDERService)
        {
            t_MySql_OrderService = _t_MySql_OrderService;
            t_ORDERService = _t_ORDERService;
        }
        /// <summary>
        /// 添加慧聪订单
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost("AddOrder")]
        public async Task<string> AddOrder(RequestModel requestModel)
        {
            string strRequestJsons = requestModel.ToJson();
            ("原始请求内容：" + strRequestJsons).WriteToLog();
            //LogInfo($"原始请求报文:{strRequestJsons} 日期：{DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            if (string.IsNullOrWhiteSpace(requestModel.@params)
                 || string.IsNullOrWhiteSpace(requestModel.digest)
                 || string.IsNullOrWhiteSpace(requestModel.timestamp))
            {
                return ResponseHandle(null, false, 3001, "参数错误", " 参数为空");
            }
            //验证时间戳
            try
            {
                if (PublicMethods.IsTimeOuts(requestModel.timestamp))
                {
                    return ResponseHandle(null, false, 2002, "超时", " 时间戳验证超时");
                }
            }
            catch (Exception)
            {
                return ResponseHandle(null, false, 2002, "超时", " 时间戳验证超时");
            }
            try
            {//验证签名
                if (!PublicMethods.CheckSign(requestModel))
                {
                    // return ResponseHandle(null, false, 2002, "身份验证错误", " 身份验证错误");
                }
            }
            catch (Exception ex)
            {
                //return ResponseHandle(null, false, 2002, "身份验证错误", " 身份验证错误");
            }
            //序列化报文
            AddOrder_HC model;
            try
            {
                model = requestModel.@params.ToObjectIgnoreNull<AddOrder_HC>();

            }
            catch (Exception)
            {
                return ResponseHandle(null, false, 3002, "请求报文转换失败", " 请求报文转换失败");
            }
            // Log(null, $"数据校验与处理用时：{sw.ElapsedMilliseconds}毫秒");
            // var result = HC_Order.AddHcOrder(model, requestModel.logistics_interface);

            #region 5、写入Mysql数据库
            var ret = await t_MySql_OrderService.AddHC(model, requestModel.@params);
            return !ret ?
                ResponseHandle("", false, 3002, "", "") :
                 ResponseHandle("", true, 3002, "添加成功", "添加成功");
            #endregion
        }

        /// <summary>
        /// 撤销慧聪订单
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost("CancelOrder")]
        public async Task<string> CancelOrder(RequestModel requestModel, string OrderNo)
        {
            string strjson = requestModel.ToJson();
            ("原始报文:" + requestModel).WriteToLog();
            if (string.IsNullOrWhiteSpace(requestModel.@params)
                || string.IsNullOrWhiteSpace(requestModel.digest)
                || string.IsNullOrWhiteSpace(requestModel.timestamp))
            {
                return ResponseHandle(null, false, 3001, "参数错误", " 参数为空");
            }
            try
            {
                if (!PublicMethods.IsTimeOut(requestModel.timestamp))
                {
                    // return ResponseHandle(null, false, 2002, "时间戳验证超时", "时间戳验验证超时");
                }
            }
            catch (Exception)
            {

                //return ResponseHandle(null, false, 2002, "超时", " 时间戳验证超时");
            }

            try
            {
                if (!PublicMethods.CheckSign(requestModel))
                {
                    // return ResponseHandle(null, false, 2002, "安全验证失败", "安全验证失败");
                }

            }
            catch (Exception)
            {

                // return ResponseHandle(null, false, 2002, "安全验证失败", "安全验证失败");
            }
            CancelOrderHC model;
            try
            {
                var ss = new ArrayList<string>();
                model = requestModel.@params.ToObjectIgnoreNull<CancelOrderHC>();
            }
            catch (Exception)
            {

                return ResponseHandle(null, false, 3002, "请求报文转换失败", " 请求报文转换失败");
            }
            var ret = t_ORDERService.CanlOrder(model, model.logisticCode);


            var jsonRet = ret.Result;
            var cc = jsonRet.ToJson();
            ("返回结果" + cc).WriteToLog();
            // LogInfo($"返回结果:{cc} 日期：{DateTime.Now:yyyy-MM-dd HH:mm:ss}");


            return cc;
        }

        [HttpPost("QueryOrder")]
        public async Task<string> QueryOrder(RequestModel requestModel, string OrderNo)
        {
            string strRequestJsons = requestModel.ToJson();
            ("原始请求内容：" + strRequestJsons).WriteToLog();

            if (string.IsNullOrWhiteSpace(requestModel.@params)
                 || string.IsNullOrWhiteSpace(requestModel.digest)
                 || string.IsNullOrWhiteSpace(requestModel.timestamp))
            {
                return ResponseHandle(null, false, 3001, "参数错误", " 参数为空");
            }
            //验证时间戳
            try
            {
                if (PublicMethods.IsTimeOuts(requestModel.timestamp))
                {
                    return ResponseHandle(null, false, 2002, "超时", " 时间戳验证超时");
                }
            }
            catch (Exception)
            {
                return ResponseHandle(null, false, 2002, "超时", " 时间戳验证超时");
            }
            try
            {//验证签名
                if (!PublicMethods.CheckSign(requestModel))
                {
                    // return ResponseHandle(null, false, 2002, "身份验证错误", " 身份验证错误");
                }
            }
            catch (Exception ex)
            {
                //return ResponseHandle(null, false, 2002, "身份验证错误", " 身份验证错误");
            }

            QueryOrder_HC model;
            try
            {
                model = requestModel.@params.ToObjectIgnoreNull<QueryOrder_HC>();
            }
            catch (Exception ex)
            {

                return ResponseHandle(null, false, 3002, "请求报文转换失败", " 请求报文转换失败");
            }
            QueryOrderInfoResult queryOrderInfo = new QueryOrderInfoResult();
            var ret = await t_ORDERService.QueryOrderInfon(model);
            if (ret == null)
            {
                queryOrderInfo.responseParam = null;
                queryOrderInfo.result = false;
                queryOrderInfo.resultCode = 3001;
                queryOrderInfo.resultInfo = "未查询到数据";
                queryOrderInfo.reason = "未查询到数据";
            }
            else
            {
                queryOrderInfo.responseParam = ret;
                queryOrderInfo.result = true;
                queryOrderInfo.resultCode = 3001;
                queryOrderInfo.resultInfo = "";
                queryOrderInfo.reason = "";
            }
            var jsonRet = queryOrderInfo.ToJson();
            ("返回结果" + jsonRet).WriteToLog();
            // LogInfo($"返回结果:{jsonRet} 日期：{DateTime.Now:yyyy-MM-dd HH:mm:ss}");

            return jsonRet;
        }

        [HttpGet("test")]
        public ActionResult test()
        {
            return Json(new { msg = "11111" });
        }
        private string ResponseHandle(string logisticCode, bool result, int resultCode, string resultInfo = "", string reason = "", LogerType logType = LogerType.Error)
        {
            try
            {
                var results = new HCResult
                {
                    logisticCode = logisticCode,
                    result = result,
                    resultCode = resultCode,
                    resultInfo = resultInfo,
                    reason = reason
                };
                var jsonRet = results.ToJson();
                ("返回结果：" + result + jsonRet).WriteToLog();
                return jsonRet;
            }
            catch (Exception ex)
            {

                throw;
            }


        }

    }
}
