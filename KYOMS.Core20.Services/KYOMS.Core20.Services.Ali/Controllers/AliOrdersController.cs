using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using KYOMS.Core20.Entity;
using KYOMS.Core20.Entity.Oracle;
using KYOMS.Core20.Services.Ali.AliHelper;
using KYOMS.Core20.Services.Ali.Common;
using KYOMS.Core20.DE.AliModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KYOMS.Core20.Services.Ali.Controllers
{
    // TODO: 菜鸟网络文档根据msg_type查询
    // http://pac.i56.taobao.com/apidoc/index.htm?spm=0.0.0.0.KJlY7U

    [Route("api/[controller]")]
    public class AliOrdersController : ApiController
    {
        // TODO：POST api/aliorders/addorder
        //创建订单
        //msg_type	HY_CREATE_ORDER_NOTIFY
        //链接		http://pac.i56.taobao.com/apiinfo/showDetail.htm?apiId=HY_CREATE_ORDER_NOTIFY
        /// <summary>
        /// ALI在线下单服务
        /// </summary>
        /// <param name="model">请求报文</param>
        /// <returns>返回一个已完成的任务</returns>
        [HttpPost("AddOrder")]
        public async Task AddOrder([FromBody] object model)
        {
            //获取请求报文
            var requestContent = "";
            if (Request.ContentType == "application/octet-stream")
            {
                var bytePacket = (byte[])model;
                requestContent = Encoding.UTF8.GetString(bytePacket);
            }
            else
            {
                if (model == null) model = "";
                requestContent = model.ToString();
            }
            if (string.IsNullOrWhiteSpace(requestContent))
            {
                var log = $"请求类型是{Request.ContentType}的请求要求报文不允许为空，AddOrder添加订单操作终止";
                LogWarn($"{log} 日期：{DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                await WriteAsync(log);
                return;
            }
            LogInfo($"{Request.ContentType}添加订单AddOrder原始请求报文：{requestContent}");

            //解析报文
            var dic = requestContent.UrlFormat();
            var logisticsInterface = dic.ContainsKey("logistics_interface") ? dic["logistics_interface"] : "";
            var dataDigest = dic.ContainsKey("data_digest") ? dic["data_digest"] : "";
            
            //签名检查
            if (!AliHandler.CheckRequest(logisticsInterface, dataDigest))
            {
                var log = AliHandler.ErrDetail(2001, "sub");
                LogError($"身份验证失败，订单数据原始报文：{logisticsInterface}\r\n接收到的签名：{dataDigest}");
                await WriteAsync(log);
                return;
            }
            
            //序列化订单数据
            var logisticId = "";
            Aorder aorder;
            try
            {
                aorder = AliHandler.JsonToClass(logisticsInterface);
                logisticId = aorder.logisticID;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                if (ex.InnerException?.InnerException != null)
                {
                    msg = ex.InnerException.InnerException.Message;
                }
                var log = AliHandler.ErrDetail(logisticId, 3001, "sub");
                LogError($"序列化订单数据失败，原始报文：{logisticsInterface}", msg, ex.StackTrace);
                await WriteAsync(log);
                return;
            }

            //映射提交订单的实体
            LogInfo($"提交订单原始数据：{JsonConvert.SerializeObject(aorder)}");
            var mySqlOrder = AliHandler.CreateMap(aorder, logisticsInterface);
            if (mySqlOrder == null)
            {
                var log = AliHandler.ErrDetail(logisticId, 3001, "sub");
                await WriteAsync(log);
                return;
            }
            if (!string.IsNullOrEmpty(aorder.mailNo))
            {
                //如果运单号为“6”开头则表示电子面单或菜鸟仓配面单，不通过本接口写入数据库
                var sub = aorder.mailNo.Substring(0, 1);
                if (sub == "6")
                {
                    LogInfo($"运单号为“6”开头则表示电子面单或菜鸟仓配面单，不通过本接口写入数据库，提交订单动作终止。运单号：{aorder.mailNo}AL订单号：{aorder.logisticID}");
                    return;
                }
            }

            //提交订单数据进入MySql库
            var hasSuccess = await AppService.Add(mySqlOrder);
            if (!hasSuccess)
            {
                var log = AliHandler.ErrDetail(logisticId, 3001, "sub");
                await WriteAsync(log);
                return;
            }
            var result = AliHandler.ErrDetail(logisticId, 1000, "sub");
            LogInfo($"{result}\r\n运单号：{aorder.mailNo}写入数据库成功！");
            await WriteAsync(result);
        }

        // TODO：POST api/aliorders/editorder
        //编辑订单
        //msg_type	HY_UPDATE_ORDER_NOTIFY
        //链接	http://pac.i56.taobao.com/apiinfo/showDetail.htm?apiId=HY_UPDATE_ORDER_NOTIFY
        /// <summary>
        /// ALI在线编辑订单服务
        /// </summary>
        /// <param name="model">请求报文</param>
        /// <returns>返回一个已完成的任务</returns>
        [HttpPost("EditOrder")]
        public async Task EditOrder([FromBody] object model)
        {
            //获取请求报文
            var requestContent = "";
            if (Request.ContentType == "application/octet-stream")
            {
                var bytePacket = (byte[])model;
                requestContent = Encoding.UTF8.GetString(bytePacket);
            }
            else
            {
                if (model == null) model = "";
                requestContent = model.ToString();
            }
            if (string.IsNullOrWhiteSpace(requestContent))
            {
                var log = $"请求类型是{Request.ContentType}的请求要求报文不允许为空，EditOrder编辑订单操作终止";
                LogWarn($"{log} 日期：{DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                await WriteAsync(log);
                return;
            }
            LogInfo($"编辑订单EditOrder原始请求报文：{requestContent}");

            //解析报文
            var dic = requestContent.UrlFormat();
            var logisticsInterface = dic.ContainsKey("logistics_interface") ? dic["logistics_interface"] : "";
            var dataDigest = dic.ContainsKey("data_digest") ? dic["data_digest"] : "";

            //签名检查
            if (!AliHandler.CheckRequest(logisticsInterface, dataDigest))
            {
                var log = AliHandler.ErrDetail(2001, "sub");
                LogError($"身份验证失败，订单数据原始报文：{logisticsInterface}\r\n接收到的签名：{dataDigest}");
                await WriteAsync(log);
                return;
            }

            //序列化编辑订单数据
            var logisticId = "";
            var blSite = false;
            Aorder aorder;
            try
            {
                aorder = AliHandler.JsonToClass(logisticsInterface);
                logisticId = aorder.logisticID;
            }
            catch (Exception e)
            {
                var msg = e.Message;
                LogError($"序列化编辑订单数据失败，原始报文：{logisticsInterface}", msg, e.StackTrace);
                await WriteAsync(AliHandler.ErrDetail(logisticId, 3001, "sub"));
                return;
            }

            var dbOrder = await AppService.QueryById(aorder.logisticID);
            if (dbOrder == null)
            {
                LogError($"系统中不存在ALI单号是 {aorder.logisticID} 运单号是 {aorder.mailNo} 的订单，编辑订单操作终止！");
                await WriteAsync(AliHandler.ErrDetail(logisticId, 1002, "edit"));
                return;
            }

            //自联物流允许修改运单号
            if (dbOrder.ORDER_STATUS == 70 && "ZX".Equals(dbOrder.BIZ_TYPE))
            {
                dbOrder.BILL_NO = aorder.mailNo;
                try
                {
                    await AppService.UpdateOrder(dbOrder);
                    await AppService.UpdateOrder(new T_ORDER_WAYBILL_MAP
                    {
                        MAIL_NO = dbOrder.BILL_NO,
                        ORDER_NO = dbOrder.ORDER_NO
                    });
                    var log = AliHandler.ErrDetail(logisticId, 1000, "edit");
                    LogInfo(log);
                    await WriteAsync(log);
                    return;
                }
                catch (Exception e)
                {
                    var msg = e.Message;
                    LogError($"{logisticId}自联物流编辑订单数据失败，编辑订单操作原始报文：{logisticsInterface}", msg, e.StackTrace);
                    await WriteAsync(AliHandler.ErrDetail(logisticId, 3001, "edit"));
                    return;
                }
            }
            //10待受理，20已分派 40 已接单 45 接单失败
            if (dbOrder.ORDER_STATUS != 10 && dbOrder.ORDER_STATUS != 20 && dbOrder.ORDER_STATUS != 40 && dbOrder.ORDER_STATUS != 45)
            {
                var log = AliHandler.ErrDetail(logisticId, 1001, "edit");
                LogInfo(log);
                await WriteAsync(log);
                return;
            }
            //10待受理 20已分派但网点未接单，如果修改地址则可能会改到网点
            if (dbOrder.ORDER_STATUS == 10 || dbOrder.ORDER_STATUS == 20)
            {
                if (dbOrder.SENDER_ADDRESS != aorder.Saddress ||
                    dbOrder.SENDER_CITY != aorder.Scity ||
                    dbOrder.SENDER_COUNTY != aorder.Scounty ||
                    dbOrder.SENDER_PROVINCE != aorder.Sprovince ||
                    dbOrder.RECEIVER_ADDRESS != aorder.Raddress ||
                    dbOrder.RECEIVER_CITY != aorder.Rcity ||
                    dbOrder.RECEIVER_COUNTY != aorder.Rcounty ||
                    dbOrder.RECEIVER_PROVINCE != aorder.Rprovince)
                {
                    //如已有网点编号 则直接状态变为已分派
                    if ((!string.IsNullOrEmpty(dbOrder.PICKUP_SITECODE) && !"-1".Equals(dbOrder.PICKUP_SITECODE)))
                    {
                        if (dbOrder.PICKUP_SITE == aorder.businessNetworkNo)
                        {
                            blSite = true;
                        }
                        else
                        {
                            dbOrder.PICKUP_SITE = aorder.businessNetworkNo;
                            dbOrder.PICKUP_SITECODE = aorder.businessNetworkNo;
                            string sitename = await AppService.QuerySiteName(dbOrder.PICKUP_SITECODE);
                            if (!string.IsNullOrEmpty(sitename))
                            {
                                if (dbOrder.ORDER_STATUS == 10)
                                {
                                    dbOrder.ORDER_STATUS = 20;
                                }
                                dbOrder.PICKUP_SITECODE = sitename;
                                dbOrder.ASSIGNED_SITE_CODE = sitename;
                                dbOrder.PUSH_OUTSYS_STATUS = 1;
                                blSite = true;
                            }
                        }

                    }

                }
                else
                {
                    blSite = true;//无修改地址
                }
                dbOrder.SENDER_ADDRESS = aorder.Saddress;
                dbOrder.SENDER_CITY = aorder.Scity;
                dbOrder.SENDER_COMPANYNAME = aorder.ScompanyName;
                dbOrder.SENDER_COUNTY = aorder.Scounty;
                dbOrder.SENDER_MOBILE = aorder.Smobile;
                dbOrder.SENDER_NAME = aorder.Sname;
                dbOrder.SENDER_PHONE = aorder.Sphone;
                dbOrder.SENDER_PROVINCE = aorder.Sprovince;
                dbOrder.SENDER_POSTCODE = aorder.SpostCode;
                if (dbOrder.SENDER_POSTCODE != null && dbOrder.SENDER_POSTCODE.Length > 6)
                {
                    dbOrder.SENDER_POSTCODE = dbOrder.SENDER_POSTCODE.Substring(0, 6);
                }

                dbOrder.RECEIVER_ADDRESS = aorder.Raddress;
                dbOrder.RECEIVER_CITY = aorder.Rcity;
                dbOrder.RECEIVER_COMPANYNAME = aorder.RcompanyName;
                dbOrder.RECEIVER_COUNTY = aorder.Rcounty;
                dbOrder.RECEIVER_MOBILE = aorder.Rmobile;
                dbOrder.RECEIVER_NAME = aorder.Rname;
                dbOrder.RECEIVER_PHONE = aorder.Rphone;
                dbOrder.RECEIVER_PROVINCE = aorder.Rprovince;
                dbOrder.RECEIVER_POSTCODE = aorder.RpostCode;
            }

            dbOrder.BILL_NO = aorder.mailNo;
            dbOrder.OUTSYS_MEMBERTYPE = aorder.memberType;
            //dbOrder.WAYBILL_NO 多对多
            dbOrder.DELIVERY_TYPE = aorder.deliveryType;
            dbOrder.OUTSYS_PAYTYPE = aorder.payType;
            dbOrder.TRANSPORT_TYPE = aorder.transportType;
            //dbOrder.CUSTOMER_CODE = aorder.logisticCompanyID;
            dbOrder.MATERIAL_COST = aorder.materialCost;
            dbOrder.VISIT_RECEIVE = aorder.vistReceive;
            dbOrder.WAIT_NOTIFYSEND = aorder.waitNotifySend;
            dbOrder.WAIT_NOTIFYSENDPRICE = aorder.waitNotifySendPrice;
            dbOrder.TRANSPORT_PRICE = aorder.transportPrice;
            dbOrder.BACKSIGNBILL = aorder.backSignBill;
            dbOrder.FUEL_SURCHARGE = aorder.fuelSurcharge;
            dbOrder.CARGO_NAME = aorder.cargoName;
            dbOrder.SMS_NOTIFY = aorder.smsNotify;

            #region 费用会改，如果没选这项服务则费用节点不会传
            if (!string.IsNullOrEmpty(aorder.packageService))
            {
                dbOrder.PACKAGE_SERVICE = aorder.packageService;
            }
            if (aorder.materialCostPrice > 0)
            {
                dbOrder.MATERIAL_COSTPRICE = aorder.materialCostPrice;
            }
            if (aorder.vistReceivePrice > 0)
            {
                dbOrder.VISIT_RECEIVEPRICE = aorder.vistReceivePrice;
            }
            if (aorder.deliveryPrice > 0)
            {
                dbOrder.DELIVERY_PRICE = aorder.deliveryPrice;
            }
            if (aorder.insuranceValue > 0)
            {
                dbOrder.INSURANCE_VALUE = aorder.insuranceValue;
            }
            if (aorder.insurancePrice > 0)
            {
                dbOrder.INSURANCE_PRICE = aorder.insurancePrice;
            }
            if (aorder.backSignBillPrice > 0)
            {
                dbOrder.BACKSIGNBILL_PRICE = aorder.backSignBillPrice;
            }
            if (aorder.packageServicePrice > 0)
            {
                dbOrder.PACKAGE_SERVICEPRICE = aorder.packageServicePrice;
            }
            if (aorder.codPrice > 0)
            {
                dbOrder.COD_PRICE = aorder.codPrice;
            }
            if (aorder.otherPrice > 0)
            {
                dbOrder.OTHER_PRICE = aorder.otherPrice;
            }
            if (aorder.fuelSurchargePrice > 0)
            {
                dbOrder.FUEL_SURCHARGE_PRICE = aorder.fuelSurchargePrice;
            }
            if (aorder.smsNotifyPrice > 0)
            {
                dbOrder.SMS_NOTIFYPRICE = aorder.smsNotifyPrice;
            }
            if (aorder.codValue > 0)
            {
                dbOrder.COD_VALUE = aorder.codValue;
            }
            #endregion

            dbOrder.LEAST_EXPENSES = aorder.leastExpenses;
            dbOrder.WEIGHT_RATE = aorder.weightRate;
            dbOrder.VOLUME_RATE = aorder.volumeRate;

            dbOrder.TOTAL_PRICE = aorder.totalPrice;
            dbOrder.REMARK = aorder.remark;
            dbOrder.TOTAL_WEIGHT = aorder.totalWeight;
            dbOrder.TOTAL_NUMBER = aorder.totalNumber;
            dbOrder.TOTAL_VOLUME = aorder.totalVolume;
            dbOrder.UPDATE_TIME = aorder.gmtUpdated;

            //自联物流的件数是0，传入BOS会有问题
            if ("ZX".Equals(dbOrder.BIZ_TYPE) && dbOrder.TOTAL_NUMBER == 0)
            {
                dbOrder.TOTAL_NUMBER = 1;
            }
            if (dbOrder.PUSH_BOS_STATUS_VER > 0)
            {
                dbOrder.NEED_ADD_RECORD_BOS = 1;
            }
            try
            {
                await AppService.UpdateOrder(dbOrder);
                if (!blSite)
                {
                    await AppService.UpdateOrder(new T_ORDER_WAYBILL_MAP
                    {
                        MAIL_NO = dbOrder.BILL_NO,
                        ORDER_NO = dbOrder.ORDER_NO
                    });
                    await UpdateSite(dbOrder);
                }
                //string s = dbHelper.GetSql("T_ORDER.Update", dbOrder);
                var log = AliHandler.ErrDetail(logisticId, 1000, "edit");
                LogInfo(log);
                await WriteAsync(log);
            }
            catch (Exception e)
            {
                var msg = e.Message;
                LogError($"{logisticId}编辑订单信息发生异常，编辑订单操作原始报文：{logisticsInterface}", msg, e.StackTrace);
                await WriteAsync(AliHandler.ErrDetail(logisticId, 3001, "edit"));
            }
        }

        // TODO：POST api/aliorders/cancelorder
        //取消订单
        //msg_type	HY_CANCEL_ORDER_NOTIFY
        //链接	http://pac.i56.taobao.com/apiinfo/showDetail.htm?apiId=HY_CANCEL_ORDER_NOTIFY
        /// <summary>
        /// ALI在线撤销订单服务
        /// </summary>
        /// <param name="model">请求报文</param>
        /// <returns>返回一个已完成的任务</returns>
        [HttpPost("CancelOrder")]
        public async Task CancelOrder([FromBody] object model)
        {
            //获取请求报文
            var requestContent = "";
            if (Request.ContentType == "application/octet-stream")
            {
                var bytePacket = (byte[])model;
                requestContent = Encoding.UTF8.GetString(bytePacket);
            }
            else
            {
                if (model == null) model = "";
                requestContent = model.ToString();
            }
            if (string.IsNullOrWhiteSpace(requestContent))
            {
                var log = $"请求类型是{Request.ContentType}的请求要求报文不允许为空，CancelOrder撤销订单操作终止";
                LogWarn($"{log} 日期：{DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                await WriteAsync(log);
                return;
            }
            LogInfo($"取消订单CancelOrder原始请求报文：{requestContent}");

            //解析报文
            var dic = requestContent.UrlFormat();
            var logisticsInterface = dic.ContainsKey("logistics_interface") ? dic["logistics_interface"] : "";
            var dataDigest = dic.ContainsKey("data_digest") ? dic["data_digest"] : "";

            //签名检查
            if (!AliHandler.CheckRequest(logisticsInterface, dataDigest))
            {
                var log = AliHandler.ErrDetail(2001, "sub");
                LogError($"身份验证失败，订单数据原始报文：{logisticsInterface}\r\n接收到的签名：{dataDigest}");
                await WriteAsync(log);
                return;
            }

            string error = "ERROR";
            string logisticID = "";
            Hashtable ht = null;

            try
            {
                JObject jo = JObject.Parse(logisticsInterface);
                string CustomerID = jo["logisticCompanyID"].ToString();
                logisticID = jo["logisticID"].ToString();
                string remark = jo["remark"].ToString();
                DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                long TimeSta = long.Parse(jo["gmtCancel"].ToString() + "0000");
                TimeSpan toNow = new TimeSpan(TimeSta);
                DateTime gmtCancel = DateTime.Parse(dtStart.Add(toNow).ToString());
                //int iCount = dbHelper.mapper.QueryForObject<int>("T_ORDER.GetCountByOutSysNo", logisticID);
                //if (iCount > 0)
                //{
                
                T_ORDER dbOrder = await AppService.QueryById(logisticID);//dbHelper.mapper.QueryForObject<T_ORDER>("T_ORDER.FindByOrderNo", logisticID);
                if (dbOrder == null)
                {
                    LogInfo($"{logisticID}订单不存在,取消订单操作原始报文：{logisticsInterface}");
                    await WriteAsync(AliHandler.ErrDetail(logisticID, 1002, "cancel"));
                    return;
                }
                decimal iStatus = dbOrder.ORDER_STATUS;
                int count = await AppService.QueryCount(dbOrder.BILL_NO); //dbHelper.mapper.QueryForObject<int>("T_WAYBILL.Count", dbOrder.BILL_NO);
                //状态中不存在0
                if (iStatus == 0)
                {
                    LogInfo($"{logisticID}订单状态不存在,取消订单操作原始报文：{logisticsInterface}");
                    await WriteAsync(AliHandler.ErrDetail(logisticID, 1002, "cancel"));
                    return;
                }
                if (iStatus == 30)
                {
                    LogInfo($"{logisticID}此单号已撤销,请勿重复操作,取消订单操作原始报文：{logisticsInterface}");
                    await WriteAsync(AliHandler.ErrDetail(logisticID, 1003, "cancel"));
                    return;
                }
                //10待受理，20已分派 40 已接单 45 接单失败
                if (iStatus != 10 && iStatus != 20 && iStatus != 40 && iStatus != 45 && (iStatus == 70 && count > 0))
                {
                    LogInfo($"{logisticID}此单已确认,无法进行此操作,取消订单操作原始报文：{logisticsInterface}");
                    await WriteAsync(AliHandler.ErrDetail(logisticID, 1001, "cancel"));
                    return;
                }

                var podr = new T_ORDER
                {
                    ORDER_STATUS = 30,
                    ORDER_CANCEL_TIME = gmtCancel,
                    ORDER_CANCEL_REMARK = remark,
                    NEED_ADD_RECORD_BOS = 1,
                    OUTSYS_CODE = logisticID,
                    ORDER_CANCEL_BY = AppSettings.CreateBy
                };
                await AppService.UpdateOrderByOutCode(podr);//dbHelper.mapper.Update("T_ORDER.UpdateORDER_STATUSByOutCode", ht);

                LogInfo($"{logisticID}订单已经撤销，撤销订单操作原始报文：{logisticsInterface}");
                await WriteAsync(AliHandler.ErrDetail(logisticID, 1000, "cancel"));
            }
            catch (Exception e)
            {
                LogFatal($"{logisticID}订单撤销异常终止，撤销订单操作原始报文：{logisticsInterface}", e.Message, e.StackTrace);
                await WriteAsync(AliHandler.ErrDetail(logisticID, 3001, "cancel"));
            }
        }

        // TODO：POST api/aliorders/queryorder
        //查询订单
        //msg_type	HY_QUERY_ORDER
        //链接	http://pac.i56.taobao.com/apiinfo/showDetail.htm?apiId=HY_QUERY_ORDER
        /// <summary>
        /// ALI在线查询订单服务
        /// </summary>
        /// <param name="model">请求报文</param>
        /// <returns>返回一个已完成的任务</returns>
        [HttpPost("QueryOrder")]
        public async Task QueryOrder([FromBody] object model)
        {
            //获取请求报文
            var requestContent = "";
            if (Request.ContentType == "application/octet-stream")
            {
                var bytePacket = (byte[])model;
                requestContent = Encoding.UTF8.GetString(bytePacket);
            }
            else
            {
                if (model == null) model = "";
                requestContent = model.ToString();
            }
            if (string.IsNullOrWhiteSpace(requestContent))
            {
                var log = $"请求类型是{Request.ContentType}的请求要求报文不允许为空，CancelOrder查询订单操作终止";
                LogWarn($"{log} 日期：{DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                await WriteAsync(log);
                return;
            }
            LogInfo($"查询订单QueryOrder原始请求报文：{requestContent}");

            //解析报文
            var dic = requestContent.UrlFormat();
            var logisticsInterface = dic.ContainsKey("logistics_interface") ? dic["logistics_interface"] : "";
            var dataDigest = dic.ContainsKey("data_digest") ? dic["data_digest"] : "";

            var requestType = "query";
            //身份验证
            if (!AliHandler.CheckRequest(logisticsInterface, dataDigest))
            {
                var log = AliHandler.ErrDetail(2001, requestType);
                LogError($"身份验证失败，订单数据原始报文：{logisticsInterface}\r\n接收到的签名：{dataDigest}");
                await WriteAsync(log);
                return;
            }
            
            string error = "ERROR";
            string logisticID = "";
            string mailNo = "";
            string CustomerID = "";
            string returnOrder = "";

            try
            {
                JObject jo = JObject.Parse(logisticsInterface);
                CustomerID = jo["logisticCompanyID"].ToString();
                logisticID = jo["logisticID"].ToString();
                //Hashtable ht = new Hashtable();
                //ht.Add("OUTSYS_UID", CustomerID);
                //ht.Add("OUTSYS_CODE", logisticID);
                //dbHelper.mapper.QueryForObject<T_ORDER>("T_ORDER.FindByOutSysNo", ht);
                
                T_ORDER order = await AppService.QueryByOutSysNo(logisticID, CustomerID);
                if (order == null)
                {
                    await WriteAsync(AliHandler.ErrDetail(1002, requestType));
                    return;
                }
                //string StatusCode = FindStatusCode(order.StatusCode);
                if (order.ORDER_STATUS == 30)
                {
                    await WriteAsync(AliHandler.ErrDetail(1003, requestType));
                    return;
                }
                string StatusCode = AliHandler.FindStatusCode(order.ORDER_STATUS.ToString());
                T_WAYBILL wayBill = null;
                //mailNo = dbHelper.mapper.QueryForObject<string>("T_ORDER_WAYBILL_MAP.GetBillCode", order.ORDER_NO);
                //if (string.IsNullOrEmpty(order.))
                //wayBill = dbHelper.mapper.QueryForObject<T_WAYBILL>("T_WAYBILL.GetByOrderNo", order.ORDER_NO);
                wayBill = await AppService.QueryWayBillByOrderNo(order.ORDER_NO);
                if (wayBill != null)
                {
                    mailNo = wayBill.BILL_CODE;
                    StringBuilder MywayBill = new StringBuilder();
                    MywayBill.Append("{\"result\":true,\"resultCode\":\"1000\",\"resultInfo\":\"true\",\"reason\":\"\",");
                    MywayBill.Append("\"responseParam\":{\"logisticCompanyID\":\"" + CustomerID);
                    MywayBill.Append("\",\"logisticID\":\"" + logisticID);
                    MywayBill.Append("\",\"businessNetworkNo\":\"" + wayBill.SEND_SITE);
                    MywayBill.Append("\",\"mailNo\":\"" + mailNo);
                    MywayBill.Append("\",\"sender\":{\"companyName\":\"" + wayBill.SENDER_COMPANYNAME);
                    MywayBill.Append("\",\"name\":\"" + wayBill.SEND_MAN + "\",\"postCode\":\"" + wayBill.SENDER_POSTCODE);

                    MywayBill.Append("\",\"phone\":\"" + wayBill.SEND_MAN_PHONE + "\",\"mobile\":\"" + wayBill.SEND_MAN_PHONE + "\",\"province\":\"" + wayBill.SENDER_PROVINCE);
                    MywayBill.Append("\",\"city\":\"" + wayBill.SENDER_CITY + "\",\"county\":\"" + wayBill.SENDER_COUNTY + "\",\"address\":\"" + wayBill.SEND_MAN_ADDRESS);
                    MywayBill.Append("\"},\"receiver\":{\"companyName\":\"" + wayBill.ACCEPT_COMPANYNAME + "\",\"name\":\"" + wayBill.ACCEPT_MAN);
                    MywayBill.Append("\",\"postCode\":\"" + wayBill.ACCEPT_POSTCODE + "\",\"phone\":\"" + wayBill.ACCEPT_MAN_PHONE + "\",\"mobile\":\"" + wayBill.ACCEPT_MAN_MOBILE);
                    MywayBill.Append("\",\"province\":\"" + wayBill.ACCEPT_PROVINCE + "\",\"city\":\"" + wayBill.ACCEPT_CITY + "\",\"county\":\"" + wayBill.ACCEPT_COUNTY);
                    MywayBill.Append("\",\"address\":\"" + wayBill.ACCEPT_MAN_ADDRESS + "\"},\"statusType\":\"" + StatusCode);

                    MywayBill.Append("\",\"cargoName\":\"" + wayBill.GOODS_NAME + "\",\"totalWeight\":" + wayBill.FEE_WEIGHT + ",\"totalVolume\":" + wayBill.CUBE);
                    MywayBill.Append(",\"totalNumber\":" + wayBill.PIECE_NUMBER + ",\"totalPrice\":" + wayBill.TOTAL_PRICE + ",\"transportPrice\":" + wayBill.FREIGHT);
                    MywayBill.Append(",\"weightRate\":" + wayBill.WEIGHT_RATE + ",\"volumeRate\":" + wayBill.VOLUME_RATE + ",\"leastExpenses\":" + wayBill.LEAST_EXPENSES);
                    MywayBill.Append(",\"payType\":\"" + wayBill.OUTSYS_PAYTYPE + "\",\"transportType\":\"" + wayBill.TRANSPORT_TYPE + "\",\"insuranceValue\":" + wayBill.SUPPORTVALUE);
                    MywayBill.Append(",\"insurancePrice\":" + wayBill.INSURANCE + ",\"codType\":\"" + order.COD_TYPE + "\",\"codValue\":" + wayBill.GOODS_PAYMENT);

                    MywayBill.Append(",\"codPrice\":" + wayBill.CODPRICE + ",\"vistReceive\":\"" + order.VISIT_RECEIVE + "\",\"vistReceivePrice\":\"" + order.VISIT_RECEIVEPRICE);
                    MywayBill.Append("\",\"deliveryType\":\"" + order.DELIVERY_TYPE + "\",\"deliveryPrice\":" + wayBill.DELIVERYPRICE + ",\"backSignBill\":\"" + wayBill.BL_RETURN_BILL + "\",\"backSignBillPrice\":" + wayBill.BACKSIGNBILLPRICE);
                    MywayBill.Append(",\"packageService\":\"" + order.PACKAGE_SERVICE + "\",\"packageServicePrice\":" + order.PACKAGE_SERVICEPRICE);
                    MywayBill.Append(",\"waitewaitNotifySend\":\"" + order.WAIT_NOTIFYSEND + "\",\"waitewaitNotifySendPrice\":" + order.WAIT_NOTIFYSENDPRICE);
                    MywayBill.Append(",\"smsNotify\":\"" + order.SMS_NOTIFY + "\",\"smsNotifyPrice\":" + order.SMS_NOTIFYPRICE + ",\"fuelSurcharge\":\"" + order.FUEL_SURCHARGE);
                    MywayBill.Append("\",\"fuelSurchargePrice\":" + order.FUEL_SURCHARGE_PRICE + ",\"materialCost\":\"" + order.MATERIAL_COST + "\",\"materialCostPrice\":" + order.MATERIAL_COSTPRICE);
                    MywayBill.Append(",\"otherPrice\":" + order.OTHER_PRICE + ",\"comments\":\"" + "" + "\",\"remark\":\"" + wayBill.REMARK + "\"}}");

                    returnOrder = MywayBill.ToString();
                }
                else
                {
                    StringBuilder MyOrder = new StringBuilder();
                    MyOrder.Append("{\"result\":true,\"resultCode\":\"1000\",\"resultInfo\":\"成功\",\"reason\":\"\",");
                    MyOrder.Append("\"responseParam\":{\"logisticCompanyID\":\"" + CustomerID);
                    MyOrder.Append("\",\"logisticID\":\"" + logisticID);
                    MyOrder.Append("\",\"businessNetworkNo\":\"" + order.PICKUP_SITECODE);
                    MyOrder.Append("\",\"mailNo\":\"" + mailNo);
                    MyOrder.Append("\",\"sender\":{\"companyName\":\"" + order.SENDER_COMPANYNAME);
                    MyOrder.Append("\",\"name\":\"" + order.SENDER_NAME + "\",\"postCode\":\"" + order.SENDER_POSTCODE);
                    MyOrder.Append("\",\"phone\":\"" + order.SENDER_PHONE + "\",\"mobile\":\"" + order.SENDER_MOBILE + "\",\"province\":\"" + order.SENDER_PROVINCE);
                    MyOrder.Append("\",\"city\":\"" + order.SENDER_CITY + "\",\"county\":\"" + order.SENDER_COUNTY + "\",\"address\":\"" + order.SENDER_ADDRESS);
                    MyOrder.Append("\"},\"receiver\":{\"companyName\":\"" + order.RECEIVER_COMPANYNAME + "\",\"name\":\"" + order.RECEIVER_NAME);
                    MyOrder.Append("\",\"postCode\":\"" + order.RECEIVER_POSTCODE + "\",\"phone\":\"" + order.RECEIVER_PHONE + "\",\"mobile\":\"" + order.RECEIVER_MOBILE);
                    MyOrder.Append("\",\"province\":\"" + order.RECEIVER_PROVINCE + "\",\"city\":\"" + order.RECEIVER_CITY + "\",\"county\":\"" + order.RECEIVER_COUNTY);
                    MyOrder.Append("\",\"address\":\"" + order.RECEIVER_ADDRESS + "\"},\"statusType\":\"" + StatusCode);
                    MyOrder.Append("\",\"cargoName\":\"" + order.CARGO_NAME + "\",\"totalWeight\":" + order.TOTAL_WEIGHT + ",\"totalVolume\":" + order.TOTAL_VOLUME);
                    MyOrder.Append(",\"totalNumber\":" + order.TOTAL_NUMBER + ",\"totalPrice\":" + order.TOTAL_PRICE + ",\"transportPrice\":" + order.TRANSPORT_PRICE);
                    MyOrder.Append(",\"weightRate\":" + order.WEIGHT_RATE + ",\"volumeRate\":" + order.VOLUME_RATE + ",\"leastExpenses\":" + order.LEAST_EXPENSES);
                    MyOrder.Append(",\"payType\":\"" + order.OUTSYS_PAYTYPE + "\",\"transportType\":\"" + order.TRANSPORT_TYPE + "\",\"insuranceValue\":" + order.INSURANCE_VALUE);
                    MyOrder.Append(",\"insurancePrice\":" + order.INSURANCE_PRICE + ",\"codType\":\"" + order.COD_TYPE + "\",\"codValue\":" + order.COD_VALUE);
                    MyOrder.Append(",\"codPrice\":" + order.COD_PRICE + ",\"vistReceive\":\"" + order.VISIT_RECEIVE + "\",\"vistReceivePrice\":\"" + order.VISIT_RECEIVEPRICE);
                    MyOrder.Append("\",\"deliveryType\":\"" + order.DELIVERY_TYPE + "\",\"deliveryPrice\":" + order.DELIVERY_PRICE + ",\"backSignBill\":\"" + order.BACKSIGNBILL + "\",\"backSignBillPrice\":" + order.BACKSIGNBILL_PRICE);
                    MyOrder.Append(",\"packageService\":\"" + order.PACKAGE_SERVICE + "\",\"packageServicePrice\":" + order.PACKAGE_SERVICEPRICE);
                    MyOrder.Append(",\"waitewaitNotifySend\":\"" + order.WAIT_NOTIFYSEND + "\",\"waitewaitNotifySendPrice\":" + order.WAIT_NOTIFYSENDPRICE);
                    MyOrder.Append(",\"smsNotify\":\"" + order.SMS_NOTIFY + "\",\"smsNotifyPrice\":" + order.SMS_NOTIFYPRICE + ",\"fuelSurcharge\":\"" + order.FUEL_SURCHARGE);
                    MyOrder.Append("\",\"fuelSurchargePrice\":" + order.FUEL_SURCHARGE_PRICE + ",\"materialCost\":\"" + order.MATERIAL_COST + "\",\"materialCostPrice\":" + order.MATERIAL_COSTPRICE);
                    MyOrder.Append(",\"otherPrice\":" + order.OTHER_PRICE + ",\"comments\":\"" + "" + "\",\"remark\":\"" + order.REMARK + "\"}}");
                    returnOrder = MyOrder.ToString();
                }
                LogInfo($"{logisticID}订单查询成功，查询订单操作原始报文：{logisticsInterface}");
                await WriteAsync(returnOrder);
            }
            catch
            {
                await WriteAsync(AliHandler.ErrDetail(3001, "query"));
                return;
            }
        }

        #region Methods
        async Task<bool> UpdateSite(T_ORDER dbOrder)
        {
            try
            {
                List<SiteDetail> sd = null;
                List<MQuerySite> lQuerySite = new List<MQuerySite>();
                MQuerySite site = null;
                site = new MQuerySite();
                site.orderid = dbOrder.ORDER_NO + "-send";
                site.address = dbOrder.SENDER_PROVINCE + dbOrder.SENDER_CITY + dbOrder.SENDER_COUNTY + dbOrder.SENDER_ADDRESS;
                site.operSource = "OMS";
                if (!lQuerySite.Contains(site))
                {
                    lQuerySite.Add(site);
                }
                site = new MQuerySite();
                site.orderid = dbOrder.ORDER_NO + "-rec";
                site.address = dbOrder.RECEIVER_PROVINCE + dbOrder.RECEIVER_CITY + dbOrder.RECEIVER_COUNTY + dbOrder.RECEIVER_ADDRESS;
                site.operSource = "OMS";
                if (!lQuerySite.Contains(site))
                {
                    lQuerySite.Add(site);
                }
                sd = AliHandler.GetSite(lQuerySite);
                SiteDetail resultsite = null;
                T_NAVIGATE_DISTANCE distance;
                bool bdistance = false;//是否插入距离表，如果2端距离都没有则不插入
                if (sd != null && sd.Count > 0)
                {
                    distance = new T_NAVIGATE_DISTANCE();
                    distance.MAIL_NO = dbOrder.BILL_NO;
                    distance.ORDER_NO = dbOrder.ORDER_NO;
                    //distance.PICKUP_DISTANCE = "0";
                    //distance.SEND_DISTANCE = "0";
                    resultsite = sd.FirstOrDefault(p => p.orderid == dbOrder.ORDER_NO + "-send");
                    if (resultsite != null)
                    {
                        distance.PICKUP_DISTANCE = resultsite.distance;
                        //distance.PICKUP_DISTANCE = resultsite.distance == null ? "0" : resultsite.distance;
                        if (!string.IsNullOrEmpty(resultsite.bizAreaValue))
                        {
                            if (dbOrder.PICKUP_SITE != resultsite.bizAreaValue && !string.IsNullOrEmpty(distance.PICKUP_DISTANCE))//如果是修改订单，网点可能没做修改
                            {
                                bdistance = true;
                            }
                            dbOrder.PICKUP_SITE = resultsite.bizAreaValue;//取件网点编号
                            string sitename = await AppService.QuerySiteName(resultsite.bizAreaValue);//dbHelper.mapper.QueryForObject<string>("T_SITE_INFO.GetName", resultsite.bizAreaValue);
                            if (!string.IsNullOrEmpty(sitename))
                            {
                                if (dbOrder.ORDER_STATUS == 10)
                                {
                                    dbOrder.ORDER_STATUS = 20;
                                }
                                dbOrder.PICKUP_SITECODE = sitename;//名称
                                dbOrder.ASSIGNED_SITE_CODE = sitename;//名称
                                dbOrder.PUSH_OUTSYS_STATUS = 1;
                                dbOrder.PUSH_OUTSYS_FAIL_NUM = 0;
                                dbOrder.ASSIGNSITE_TYPE = 0;
                            }
                        }
                    }
                    resultsite = sd.FirstOrDefault(p => p.orderid == dbOrder.ORDER_NO + "-rec");
                    if (resultsite != null)
                    {
                        distance.SEND_DISTANCE = resultsite.distance;
                        if (!string.IsNullOrEmpty(resultsite.bizAreaValue))
                        {
                            if (dbOrder.SENDTO_SITE != resultsite.bizAreaValue && !string.IsNullOrEmpty(distance.SEND_DISTANCE))//如果是修改订单，网点可能没做修改
                            {
                                bdistance = true;
                            }
                            dbOrder.SENDTO_SITE = resultsite.bizAreaValue;
                            string sitename = await AppService.QuerySiteName(resultsite.bizAreaValue);//dbHelper.mapper.QueryForObject<string>("T_SITE_INFO.GetName", resultsite.bizAreaValue);
                            if (!string.IsNullOrEmpty(sitename))
                            {
                                dbOrder.SENDTO_SITECODE = sitename;//名称 
                            }
                        }
                    }

                    int i = await AppService.UpdateSite(dbOrder);//dbHelper.mapper.Update("T_ORDER.UpdateSite", dbOrder);
                    if (bdistance)
                    {
                        await AppService.Add(distance); //dbHelper.mapper.Insert("T_NAVIGATE_DISTANCE.Insert", distance);
                    }
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        #endregion

    }
}
