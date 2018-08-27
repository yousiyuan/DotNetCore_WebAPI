using KYOMS.Core20.Application.Interface;
using KYOMS.Core20.Common.Base;
using KYOMS.Core20.Common.Config;
using KYOMS.Core20.Common.Extend;
using KYOMS.Core20.Common.Log4NetCore;
using KYOMS.Core20.Common.LogCommon;
using KYOMS.Core20.Common.Utility;
using KYOMS.Core20.Common.ZooKeeper;
using KYOMS.Core20.DE.Model;
using KYOMS.Services.Core20.TAOBAO.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace KYOMS.Services.Core20.TAOBAO.Controllers
{

    [Route("api/[controller]")]
    public class OrderController : BaseController
    {
        IMySqlTaoBaoService t_MySql_OrderService;
        IOracleTaoBaoService t_ORDERService;
        public OrderController(IMySqlTaoBaoService _t_MySql_OrderService, IOracleTaoBaoService _t_ORDERService)
        {
            t_MySql_OrderService = _t_MySql_OrderService;
            t_ORDERService = _t_ORDERService;
        }
        [HttpPost("add")]
        public async Task<string> Add(CheckInModel checkInModel)
        {
            string strRequestJson = checkInModel.ToJson();
            ("原始请求内容：" + strRequestJson).WriteToLog();
            if (string.IsNullOrEmpty(strRequestJson))
            {
                return ResponseMessageHandle("", "S05", "消息内容为空");
            }
            //2、消息类型检查
            if (!string.Equals(checkInModel.msg_type, BaseInfo.OrderCreateMsgType)) return ResponseMessageHandle("", "S04", "消息类型msg_type错误");
            //3、签名检查
            try
            {
                if (!SignHelper.CheckDataDigest(checkInModel.logistics_interface, checkInModel.data_digest, BaseInfo.SecretKey))
                {
                    return ResponseMessageHandle("", "S02", "消息签名不符，请检查签名");
                }
            }
            catch (Exception ex)
            {
                var mess = new StringBuilder();
                mess.Append("报文消息签名检查失败:\n");
                mess.Append("\n报文内容:" + checkInModel.logistics_interface);
                mess.Append("\n错误信息:" + ex.Message + "\n" + ex.StackTrace);
                mess.ToString().WriteToLog(LogerType.Error);
                return ResponseMessageHandle("", "S02", "报文消息签名检查失败");
            }
            //4、报文转换为实体对象
            TaobaoOrderModel model;
            try
            {
                model = checkInModel.logistics_interface.ToObjectIgnoreNull<TaobaoOrderModel>();
            }
            catch (Exception ex)
            {
                var mess = new StringBuilder();
                mess.Append("报文内容格式不符合规范,Json格式转换失败:\n");
                mess.Append("\n报文内容:" + checkInModel.logistics_interface);
                mess.Append("\n错误信息:" + ex.Message + "\n" + ex.StackTrace);
                mess.ToString().WriteToLog(LogerType.Error);
                return ResponseMessageHandle("", "S01", "报文内容格式不符合规范,Json格式转换失败");
            }

            #region 5、写入Mysql数据库
            var ret = await t_MySql_OrderService.AddTaoBao(model);
            return !ret ?
                ResponseMessageHandle(model.txLogisticID, "S07", "订单写入数据库失败", LogerType.Fatal) :
                ResponseMessageHandle(model.txLogisticID, "", "", LogerType.Info, true);
            #endregion
        }

        [HttpPost("modify_cancel")]
        public async Task<string> modifyCancelAsync(CheckInModel checkInModel)
        {
            string strRequestJson = checkInModel.ToJson();
            ("原始请求内容：" + strRequestJson).WriteToLog();
            if (string.IsNullOrEmpty(strRequestJson))
            {
                return ResponseMessageHandle("", "S05", "消息内容为空");
            }
            //2、消息类型检查
            if (!string.Equals(checkInModel.msg_type, BaseInfo.OrderUpdateMsgType)) return ResponseMessageHandle("", "S04", "消息类型msg_type错误"); ;
            //3、签名检查
            try
            {
                if (!SignHelper.CheckDataDigest(checkInModel.logistics_interface, checkInModel.data_digest, BaseInfo.SecretKey))
                {
                    return ResponseMessageHandle("", "S02", "消息签名不符，请检查签名");
                }
            }
            catch (Exception ex)
            {
                var mess = new StringBuilder();
                mess.Append("报文消息签名检查失败:\n");
                mess.Append("\n报文内容:" + checkInModel.logistics_interface);
                mess.Append("\n错误信息:" + ex.Message + "\n" + ex.StackTrace);
                mess.ToString().WriteToLog(LogerType.Error);
                return ResponseMessageHandle("", "S02", "报文消息签名检查失败");
            }
            //4、报文转换为实体对象
            UpdateTaobaoOrderModel model;
            try
            {
                model = checkInModel.logistics_interface.ToObjectIgnoreNull<UpdateTaobaoOrderModel>();
            }
            catch (Exception ex)
            {
                var mess = new StringBuilder();
                mess.Append("报文内容格式不符合规范,Json格式转换失败:\n");
                mess.Append("\n报文内容:" + checkInModel.logistics_interface);
                mess.Append("\n错误信息:" + ex.Message + "\n" + ex.StackTrace);
                mess.ToString().WriteToLog(LogerType.Error);
                return ResponseMessageHandle("", "S01", "报文内容格式不符合规范,Json格式转换失败");
            }
            //5、检查物流公司编号是否与预置的相同
            if (model.logisticProviderID != BaseInfo.CpCode)
            {
                ("报文内容" + strRequestJson + " 物流公司编号与预先设定的不同：" + model.logisticProviderID).WriteToLog();
                return ResponseMessageHandle(model.logisticProviderID, "S03", "物流公司编号与预先设定的不同，请检查");
            }

            //5、更新信息写入数据库（包含1、取消订单；2、更新面单号）
            var ret = await t_ORDERService.EditOrder(model, BaseInfo.CpCode);
            var jsonRet = ret.ToJson();
            jsonRet.WriteToLog();
            return jsonRet;

        }

        [HttpPost("CreateSecretKey")]
        public ActionResult CreateSecretKey(CheckInModel checkInModel)
        {
            string pwd = SignHelper.CreateDataDigest(checkInModel.logistics_interface, BaseInfo.SecretKey);
            return Json(new { pwd = pwd });
        }
        [HttpGet("test")]
        public ActionResult Test()
        {
            LogHandle Logger = new LogHandle(typeof(OrderController));

            //bool b = await t_MySql_OrderService.InsertList(new List<KYOMS.Core20.Entity.MySqlDB.T_MySql_Order>
            //    {
            //        new KYOMS.Core20.Entity.MySqlDB.T_MySql_Order(){ C1="", C2="", C3="", CREATE_BY="11", CREATE_TIME=DateTime.Now, IS_SYNC_SUCCESS=1, MSG_CONTENT="{\"data\":\"dsss\"}", MSG_TYPE="JSON", ORDER_SOURCE="TAOBAO", OUTSYS_BILL_CODE="111111", OUTSYS_ORDER_NO="222222", REMARK="" },
            //        new KYOMS.Core20.Entity.MySqlDB.T_MySql_Order(){ C1="", C2="", C3="", CREATE_BY="22", CREATE_TIME=DateTime.Now, IS_SYNC_SUCCESS=1, MSG_CONTENT="{\"data\":\"dsss\"}", MSG_TYPE="JSON", ORDER_SOURCE="TAOBAO", OUTSYS_BILL_CODE="222222", OUTSYS_ORDER_NO="333333", REMARK="" },
            //        new KYOMS.Core20.Entity.MySqlDB.T_MySql_Order(){ C1="", C2="", C3="", CREATE_BY="33", CREATE_TIME=DateTime.Now, IS_SYNC_SUCCESS=1, MSG_CONTENT="{\"data\":\"dsss\"}", MSG_TYPE="JSON", ORDER_SOURCE="TAOBAO", OUTSYS_BILL_CODE="333333", OUTSYS_ORDER_NO="444444", REMARK="" }
            //    });
            //    t_MySql_OrderService.Logger.Set(b.ToString(), LogHandle.LogerType.Info);

            "2222222".WriteToLog();
            return Json(new { msg = true });
        }
        /// <summary>
        /// 消息回复
        /// </summary>
        /// <param name="txLogisticId"></param>
        /// <param name="errorCode">错误代码</param>
        /// <param name="errorMsg">错误内容</param>
        /// <param name="logType">日志类型，默认为Error</param>
        /// <param name="issuccess">成功失败标记</param>
        /// <returns></returns>
        private string ResponseMessageHandle(string txLogisticId, string errorCode, string errorMsg,
    LogerType logType = LogerType.Error, bool issuccess = false)
        {
            try
            {
                var response = new Response();
                response.txLogisticID = txLogisticId;
                response.success = issuccess;
                response.reason = errorCode;

                var result = new ResponseResult
                {
                    logisticProviderID = BaseInfo.CpCode,
                    responseItems = new List<Response> { response }
                };
                var jsonRet = result.ToJson();
                ("返回内容：" + errorMsg + jsonRet).WriteToLog(logType);
                return jsonRet;
            }
            catch (Exception ex)
            {
                var mess = new StringBuilder();
                mess.Append("回复消息在序列化时发生错误:\n");
                mess.Append("\n错误信息:" + ex.Message + "\n" + ex.StackTrace);
                mess.ToString().WriteToLog(LogerType.Error);
                return new { success = false, errorCode = "99", errorMsg = "回复消息在序列化时发生错误" }.ToJson();
            }
        }
    }
}
