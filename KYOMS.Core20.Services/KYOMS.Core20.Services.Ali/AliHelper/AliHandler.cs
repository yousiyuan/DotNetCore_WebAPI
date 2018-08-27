using KYOMS.Core20.Common;
using KYOMS.Core20.Common.Extend;
using KYOMS.Core20.Common.Log4NetCore;
using KYOMS.Core20.DE.AliModel;
using KYOMS.Core20.DE.Enum;
using KYOMS.Core20.Entity.MySqlDB;
using KYOMS.Core20.Services.Ali.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace KYOMS.Core20.Services.Ali.AliHelper
{
    public static class AliHandler
    {
        //private static readonly LoggerHandle AliLog;

        static AliHandler()
        {
            //if (AliLog == null)
            //    AliLog = new LoggerHandle(null, AppSettings.Instance.LogFileName());
        }

        public static string ErrDetail(int errCode, string requesType)
        {
            return ErrDetail("", errCode, requesType);
        }
        public static string ErrDetail(string logisticId, int errCode, string requesType)
        {
            string err = "";
            string op = "";
            switch (requesType)
            {
                case "sub": op = "提交"; break;
                case "edit": op = "修改"; break;
                case "query": op = "查询"; break;
                default: op = "撤销"; break;
            }
            switch (errCode)
            {
                case 1000:
                    err = "{\"logisticID\":\"" + logisticId + "\",\"result\":true,\"resultCode\":\"" + errCode + "\",\"resultInfo\":\"" + op + "订单成功\",\"reason\":\"成功\"}";
                    break;
                case 1001:
                    err = requesType == "sub" ? "{\"logisticID\":\"" + logisticId + "\",\"result\":false,\"resultCode\":\"" + errCode + "\",\"resultInfo\":\"" + op + "订单失败\",\"reason\":\"要" + op + "的订单已存在\"}" : "{\"logisticID\":\"" + logisticId + "\",\"result\":false,\"resultCode\":\"" + errCode + "\",\"resultInfo\":\"" + op + "订单失败\",\"reason\":\"此单已确认,无法进行此操作.\"}";
                    break;
                case 1002: err = "{\"logisticID\":\"" + logisticId + "\",\"result\":false,\"resultCode\":\"" + errCode + "\",\"resultInfo\":\"" + op + "订单失败\",\"reason\":\"订单不存在\"}"; break;
                case 1003: err = "{\"logisticID\":\"" + logisticId + "\",\"result\":false,\"resultCode\":\"" + errCode + "\",\"resultInfo\":\"" + op + "订单失败\",\"reason\":\"订单已撤销\"}"; break;
                case 2001:
                    err = "{\"logisticID\":\"" + logisticId + "\",\"result\":false,\"resultCode\":\"" + errCode + "\",\"resultInfo\":\"" + op + "订单失败\",\"reason\":\"合作伙伴身份验证失败,请检查验证信息\"}";
                    break;
                case 3001:
                    err = "{\"logisticID\":\"" + logisticId + "\",\"result\":false,\"resultCode\":\"" + errCode + "\",\"resultInfo\":\"" + op + "订单失败\",\"reason\":\"参数错误,请检查信息是否填写完整,此错误可能是因JSON格式不规范造成\"}";
                    break;
                case 4001:
                    err = "{\"logisticID\":\"" + logisticId + "\",\"result\":false,\"resultCode\":\"" + errCode + "\",\"resultInfo\":\"" + op + "订单失败\",\"reason\":\"业务不支持\"}";
                    break;
                default:
                    err = "{\"logisticID\":\"" + logisticId + "\",\"result\":false,\"resultCode\":\"" + errCode + "\",\"resultInfo\":\"" + op + "订单失败\",\"reason\":\"未知错误,请稍后重试\"}";
                    break;
            }
            return err;
        }
        /// <summary>
        /// 签名验证
        /// </summary>
        /// <param name="content">请求报文</param>
        /// <param name="dataDigest">收到的签名</param>
        public static bool CheckRequest(string content, string dataDigest)
        {
            var sk = AppSettings.SecretKey;
            string keyContent = content + sk;
            //对报文做MD5处理
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(keyContent));
            keyContent = Convert.ToBase64String(data);
            string msg = DateTime.Now + "【用户登录】";
            msg += "\r\n收到的Key:" + dataDigest;
            msg += "\r\n产生的Key:" + keyContent;
            msg += dataDigest == keyContent ? "\r\n收到的Key校验合法,验证通过" : "\r\n收到的Key校验不合法,验证失败";
            //msg.SetLog($"验证报文:{msg}", LogerType.Info);
            msg.WriteToLog();

            return dataDigest == keyContent;
        }
        //requesType为接口请求类型,提交/查询等
        public static Aorder JsonToClass(string json)
        {
            Aorder order = new Aorder();

            JObject j = JObject.Parse(json);
            List<JToken> baseinfo = j.Children().ToList();
            List<JToken> sender = j["sender"].Children().ToList();
            List<JToken> receiver = j["receiver"].Children().ToList();
            string sendvalue = "";
            string receivervalue = "";
            string infovalue = "";
            #region 赋值

            #region 基础信息赋值
            foreach (JProperty info in baseinfo)
            {
                infovalue = "";
                // if (info.Name == "CustomerID" && info.Value.ToString() != "")
                //{
                //    infovalue = (string)info.Value;
                //    if (infovalue.Length > 100)
                //    {
                //        infovalue = infovalue.Substring(0, 100);
                //    }
                //    CustomerID = infovalue;
                //    continue;
                //}
                // if (info.Name == "subOrderID" && info.Value.ToString() != "")
                //{
                //    infovalue = (string)info.Value;
                //    if (infovalue.Length > 100)
                //    {
                //        infovalue = infovalue.Substring(0, 100);
                //    }
                //    subOrderID = infovalue;
                //    continue;
                //}
                if (info.Name == "businessNetworkNo" && info.Value.ToString() != "")
                {
                    infovalue = (string)info.Value;
                    if (infovalue.Length > 30)
                    {
                        infovalue = infovalue.Substring(0, 30);
                    }
                    order.businessNetworkNo = infovalue;
                    continue;
                }
                else if (info.Name == "toNetworkNo" && info.Value.ToString() != "")
                {
                    infovalue = (string)info.Value;
                    if (infovalue.Length > 30)
                    {
                        infovalue = infovalue.Substring(0, 30);
                    }
                    order.toNetworkNo = infovalue;
                    continue;
                }
                else if (info.Name == "gmtCommit" && info.Value.ToString() != "")
                {
                    //转换阿里传递的时间戳
                    DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                    long TimeSta = long.Parse(info.Value + "0000");
                    TimeSpan toNow = new TimeSpan(TimeSta);
                    order.gmtCommit = DateTime.Parse(dtStart.Add(toNow).ToString());
                    continue;
                }
                else if (info.Name == "gmtUpdated" && info.Value.ToString() != "")
                {
                    //转换阿里传递的时间戳
                    DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                    long TimeSta = long.Parse(info.Value + "0000");
                    TimeSpan toNow = new TimeSpan(TimeSta);
                    order.gmtUpdated = DateTime.Parse(dtStart.Add(toNow).ToString());
                    continue;
                }
                else if (info.Name == "transportPrice" && info.Value.ToString() != "")
                {
                    order.transportPrice = (decimal)info.Value;
                    continue;
                }
                else if (info.Name == "materialCost" && info.Value.ToString() != "")
                {
                    infovalue = (string)info.Value;
                    if (infovalue.Length > 2)
                    {
                        infovalue = infovalue.Substring(0, 2);
                    }
                    order.materialCost = infovalue;
                    continue;
                }
                else if (info.Name == "materialCostPrice" && info.Value.ToString() != "")
                {
                    order.materialCostPrice = (decimal)info.Value;
                    continue;
                }
                else if (info.Name == "vistReceive" && info.Value.ToString() != "")
                {
                    infovalue = (string)info.Value;
                    if (infovalue.Length > 50)
                    {
                        infovalue = infovalue.Substring(0, 50);
                    }
                    order.vistReceive = infovalue;
                    continue;
                }
                else if (info.Name == "vistReceivePrice" && info.Value.ToString() != "")
                {
                    order.vistReceivePrice = (decimal)info.Value;
                    continue;
                }
                else if (info.Name == "deliveryType" && info.Value.ToString() != "")
                {
                    infovalue = (string)info.Value;
                    if (infovalue.Length > 2)
                    {
                        infovalue = infovalue.Substring(0, 2);
                    }
                    order.deliveryType = infovalue;
                    continue;
                }
                else if (info.Name == "deliveryPrice" && info.Value.ToString() != "")
                {
                    order.deliveryPrice = (decimal)info.Value;
                    continue;
                }
                else if (info.Name == "insuranceValue" && info.Value.ToString() != "")
                {
                    order.insuranceValue = (decimal)info.Value;
                    continue;
                }
                else if (info.Name == "insurancePrice" && info.Value.ToString() != "")
                {
                    order.insurancePrice = (decimal)info.Value;
                    continue;
                }
                else if (info.Name == "backSignBill" && info.Value.ToString() != "")
                {
                    infovalue = (string)info.Value;
                    if (infovalue.Length > 2)
                    {
                        infovalue = infovalue.Substring(0, 2);
                    }
                    order.backSignBill = infovalue;
                    continue;
                }
                else if (info.Name == "backSignBillPrice" && info.Value.ToString() != "")
                {
                    order.backSignBillPrice = (decimal)info.Value;
                    continue;
                }
                else if (info.Name == "waitNotifySend" && info.Value.ToString() != "")
                {
                    infovalue = (string)info.Value;
                    if (infovalue.Length > 1)
                    {
                        infovalue = infovalue.Substring(0, 1);
                    }
                    order.waitNotifySend = infovalue;
                    continue;
                }
                else if (info.Name == "waitNotifySendPrice" && info.Value.ToString() != "")
                {
                    order.waitNotifySendPrice = (decimal)info.Value;
                    continue;
                }
                else if (info.Name == "packageService" && info.Value.ToString() != "")
                {
                    infovalue = (string)info.Value;
                    if (infovalue.Length > 50)
                    {
                        infovalue = infovalue.Substring(0, 50);
                    }
                    order.packageService = infovalue;
                    continue;
                }
                else if (info.Name == "packageServicePrice" && info.Value.ToString() != "")
                {
                    order.packageServicePrice = (decimal)info.Value;
                    continue;
                }
                else if (info.Name == "totalPrice" && info.Value.ToString() != "")
                {
                    order.totalPrice = (decimal)info.Value;
                    continue;
                }
                else if (info.Name == "payType" && info.Value.ToString() != "")
                {
                    infovalue = (string)info.Value;
                    if (infovalue.Length > 10)
                    {
                        infovalue = infovalue.Substring(0, 10);
                    }
                    order.payType = infovalue;
                    continue;
                }
                else if (info.Name == "transportType" && info.Value.ToString() != "")
                {
                    infovalue = (string)info.Value;
                    if (infovalue.Length > 20)
                    {
                        infovalue = infovalue.Substring(0, 20);
                    }
                    order.transportType = infovalue;
                    continue;
                }
                else if (info.Name == "codType" && info.Value.ToString() != "")
                {
                    infovalue = (string)info.Value;
                    if (infovalue.Length > 10)
                    {
                        infovalue = infovalue.Substring(0, 10);
                    }
                    order.codType = infovalue;
                    continue;
                }
                else if (info.Name == "codPrice" && info.Value.ToString() != "")
                {
                    order.codPrice = (decimal)info.Value;
                    continue;
                }
                else if (info.Name == "otherPrice" && info.Value.ToString() != "")
                {
                    order.otherPrice = (decimal)info.Value;
                    continue;
                }
                else if (info.Name == "remark" && info.Value.ToString() != "")
                {
                    infovalue = (string)info.Value;
                    if (infovalue.Length > 1000)
                    {
                        infovalue = infovalue.Substring(0, 1000);
                    }
                    order.remark = infovalue;
                    continue;
                }
                else if (info.Name == "logisticID" && info.Value.ToString() != "")
                {
                    infovalue = (string)info.Value;
                    if (infovalue.Length > 100)
                    {
                        infovalue = infovalue.Substring(0, 100);
                    }
                    order.logisticID = infovalue;
                    continue;
                }
                else if (info.Name == "mailNo" && info.Value.ToString() != "")
                {
                    infovalue = (string)info.Value;
                    if (infovalue.Length > 100)
                    {
                        infovalue = infovalue.Substring(0, 100);
                    }
                    order.mailNo = infovalue;
                    continue;
                }
                else if (info.Name == "weightRate" && info.Value.ToString() != "")
                {
                    order.weightRate = (decimal)info.Value;
                    continue;
                }
                else if (info.Name == "volumeRate" && info.Value.ToString() != "")
                {
                    order.volumeRate = (decimal)info.Value;
                    continue;
                }
                else if (info.Name == "leastExpenses" && info.Value.ToString() != "")
                {
                    order.leastExpenses = Convert.ToDecimal(info.Value);
                    continue;
                }
                //else if (info.Name == "waitewaitNotifySend" && info.Value.ToString() != "")
                //{
                //    waitewaitNotifySend = infovalue;
                //    waitNotifySend = infovalue;
                //    continue;
                //}
                //else if (info.Name == "waitewaitNotifySendPrice" && info.Value.ToString() != "")
                //{
                //    waitewaitNotifySendPrice = (decimal)info.Value;
                //    waitNotifySendPrice = (decimal)info.Value;
                //    continue;
                //}
                else if (info.Name == "smsNotify" && info.Value.ToString() != "")
                {
                    infovalue = (string)info.Value;
                    if (infovalue.Length > 1)
                    {
                        infovalue = infovalue.Substring(0, 1);
                    }
                    order.smsNotify = infovalue;
                    continue;
                }
                else if (info.Name == "smsNotifyPrice" && info.Value.ToString() != "")
                {
                    order.smsNotifyPrice = (decimal)info.Value;
                    continue;
                }
                else if (info.Name == "fuelSurcharge" && info.Value.ToString() != "")
                {
                    infovalue = (string)info.Value;
                    if (infovalue.Length > 1)
                    {
                        infovalue = infovalue.Substring(0, 1);
                    }
                    order.fuelSurcharge = infovalue;
                    continue;
                }
                else if (info.Name == "fuelSurchargePrice" && info.Value.ToString() != "")
                {
                    order.fuelSurchargePrice = (decimal)info.Value;
                    continue;
                }
                //else if (info.Name == "comments" && info.Value.ToString() != "")
                //{
                //    infovalue = (string)info.Value;
                //    if (infovalue.Length > 100)
                //    {
                //        infovalue = infovalue.Substring(0, 100);
                //    }
                //    comments = infovalue;
                //    continue;
                //}
                else if (info.Name == "totalWeight" && info.Value.ToString() != "")
                {
                    order.totalWeight = (decimal)info.Value;
                    continue;
                }
                else if (info.Name == "totalVolume" && info.Value.ToString() != "")
                {
                    order.totalVolume = (decimal)info.Value;
                    continue;
                }
                else if (info.Name == "totalNumber" && info.Value.ToString() != "")
                {
                    order.totalNumber = (int)info.Value;
                    continue;
                }
                else if (info.Name == "totalPrice" && info.Value.ToString() != "")
                {
                    order.totalPrice = (decimal)info.Value;
                    continue;
                }
                else if (info.Name == "weightRate" && info.Value.ToString() != "")
                {
                    order.weightRate = (decimal)info.Value;
                    continue;
                }
                else if (info.Name == "volumeRate" && info.Value.ToString() != "")
                {
                    order.volumeRate = (decimal)info.Value;
                    continue;
                }
                else if (info.Name == "promotion" && info.Value.ToString() != "")
                {
                    infovalue = (string)info.Value;
                    if (infovalue.Length > 200)
                    {
                        infovalue = infovalue.Substring(0, 200);
                    }
                    order.promotion = infovalue;
                    continue;
                }
                else if (info.Name == "aliUID" && info.Value.ToString() != "")
                {
                    infovalue = (string)info.Value;
                    if (infovalue.Length > 100)
                    {
                        infovalue = infovalue.Substring(0, 100);
                    }
                    order.aliUID = infovalue;
                    continue;
                }
                else if (info.Name == "memberType" && info.Value.ToString() != "")
                {
                    infovalue = (string)info.Value;
                    if (infovalue.Length > 30)
                    {
                        infovalue = infovalue.Substring(0, 30);
                    }
                    order.memberType = infovalue;
                    continue;
                }
                else if (info.Name == "bizType" && info.Value.ToString() != "")
                {
                    infovalue = (string)info.Value;
                    if (infovalue.Length > 30)
                    {
                        infovalue = infovalue.Substring(0, 30);
                    }
                    order.bizType = infovalue;
                    continue;
                }
                else if (info.Name == "cargoName" && info.Value.ToString() != "")
                {
                    infovalue = (string)info.Value;
                    if (infovalue.Length > 100)
                    {
                        infovalue = infovalue.Substring(0, 100);
                    }
                    order.cargoName = infovalue;
                    continue;
                }
                else if (info.Name == "codValue" && info.Value.ToString() != "")
                {
                    order.codValue = (decimal)info.Value;
                    continue;
                }
                else if (info.Name == "logisticCompanyID" && info.Value.ToString() != "")
                {
                    infovalue = (string)info.Value;
                    if (infovalue.Length > 20)
                    {
                        infovalue = infovalue.Substring(0, 20);
                    }
                    order.logisticCompanyID = infovalue;
                    continue;
                }
                //else if (info.Name == "serviceType" && info.Value.ToString() != "")
                //{
                //    infovalue = (string)info.Value;
                //    if (infovalue.Length > 100)
                //    {
                //        infovalue = infovalue.Substring(0, 100);
                //    }
                //    serviceType = infovalue;
                //    continue;
                //}
            }
            #endregion
            #region 发货人赋值
            foreach (JProperty send in sender)
            {
                sendvalue = "";
                if (send.Name == "companyName" && send.Value.ToString() != "")
                {
                    sendvalue = (string)send.Value;
                    if (sendvalue.Length > 100)
                    {
                        sendvalue = sendvalue.Substring(0, 100);
                    }
                    order.ScompanyName = sendvalue;
                }
                else if (send.Name == "name" && send.Value.ToString() != "")
                {
                    sendvalue = (string)send.Value;
                    if (sendvalue.Length > 100)
                    {
                        sendvalue = sendvalue.Substring(0, 100);
                    }
                    order.Sname = sendvalue;
                }
                else if (send.Name == "postCode" && send.Value.ToString() != "")
                {
                    sendvalue = (string)send.Value;
                    if (sendvalue.Length > 10)
                    {
                        sendvalue = sendvalue.Substring(0, 10);
                    }
                    order.SpostCode = sendvalue;
                }
                else if (send.Name == "mobile" && send.Value.ToString() != "" && send.Value.ToString().Length > 5)
                {
                    sendvalue = (string)send.Value;
                    if (sendvalue.Length > 50)
                    {
                        sendvalue = sendvalue.Substring(0, 50);
                    }
                    order.Smobile = sendvalue;
                }
                else if (send.Name == "phone" && send.Value.ToString() != "")
                {
                    sendvalue = (string)send.Value;
                    if (sendvalue.Length > 50)
                    {
                        sendvalue = sendvalue.Substring(0, 50);
                    }
                    order.Sphone = sendvalue;
                }
                else if (send.Name == "province" && send.Value.ToString() != "")
                {
                    sendvalue = (string)send.Value;
                    if (sendvalue.Length > 50)
                    {
                        sendvalue = sendvalue.Substring(0, 50);
                    }
                    order.Sprovince = sendvalue;
                }
                else if (send.Name == "city" && send.Value.ToString() != "")
                {
                    sendvalue = (string)send.Value;
                    if (sendvalue.Length > 50)
                    {
                        sendvalue = sendvalue.Substring(0, 50);
                    }
                    order.Scity = sendvalue;
                }
                else if (send.Name == "county" && send.Value.ToString() != "")
                {
                    sendvalue = (string)send.Value;
                    if (sendvalue.Length > 50)
                    {
                        sendvalue = sendvalue.Substring(0, 50);
                    }
                    order.Scounty = sendvalue;
                }
                else if (send.Name == "address" && send.Value.ToString() != "")
                {
                    sendvalue = (string)send.Value;
                    if (sendvalue.Length > 300)
                    {
                        sendvalue = sendvalue.Substring(0, 300);
                    }
                    order.Saddress = sendvalue;
                }
            }
            #endregion
            #region 收货人信息赋值
            foreach (JProperty rec in receiver)
            {
                receivervalue = "";
                if (rec.Name == "companyName" && rec.Value.ToString() != "")
                {
                    receivervalue = (string)rec.Value;
                    if (receivervalue.Length > 100)
                    {
                        receivervalue = receivervalue.Substring(0, 100);
                    }
                    order.RcompanyName = receivervalue;
                }
                else if (rec.Name == "name" && rec.Value.ToString() != "")
                {
                    receivervalue = (string)rec.Value;
                    if (receivervalue.Length > 100)
                    {
                        receivervalue = receivervalue.Substring(0, 100);
                    }
                    order.Rname = receivervalue;
                }
                else if (rec.Name == "postCode" && rec.Value.ToString() != "")
                {
                    receivervalue = (string)rec.Value;
                    if (receivervalue.Length > 10)
                    {
                        receivervalue = receivervalue.Substring(0, 10);
                    }
                    order.RpostCode = receivervalue;
                }
                else if (rec.Name == "mobile" && rec.Value != null && rec.Value.ToString().Length > 5)
                {
                    receivervalue = (string)rec.Value;
                    if (receivervalue.Length > 50)
                    {
                        receivervalue = receivervalue.Substring(0, 50);
                    }
                    order.Rmobile = receivervalue;
                }
                else if (rec.Name == "phone" && rec.Value.ToString() != "")
                {
                    receivervalue = (string)rec.Value;
                    if (receivervalue.Length > 50)
                    {
                        receivervalue = receivervalue.Substring(0, 50);
                    }
                    order.Rphone = receivervalue;
                }
                else if (rec.Name == "province" && rec.Value.ToString() != "")
                {
                    receivervalue = (string)rec.Value;
                    if (receivervalue.Length > 50)
                    {
                        receivervalue = receivervalue.Substring(0, 50);
                    }
                    order.Rprovince = receivervalue;
                }
                else if (rec.Name == "city" && rec.Value.ToString() != "")
                {
                    receivervalue = (string)rec.Value;
                    if (receivervalue.Length > 50)
                    {
                        receivervalue = receivervalue.Substring(0, 50);
                    }
                    order.Rcity = receivervalue;
                }
                else if (rec.Name == "county" && rec.Value.ToString() != "")
                {
                    receivervalue = (string)rec.Value;
                    if (receivervalue.Length > 50)
                    {
                        receivervalue = receivervalue.Substring(0, 50);
                    }
                    order.Rcounty = receivervalue;
                }
                else if (rec.Name == "address" && rec.Value.ToString() != "")
                {
                    receivervalue = (string)rec.Value;
                    if (receivervalue.Length > 50)
                    {
                        receivervalue = receivervalue.Substring(0, 50);
                    }
                    order.Raddress = receivervalue;
                }
            }
            #endregion
            #endregion
            return order;
        }
        /// <summary>
        /// 映射数据
        /// </summary>
        /// <param name="order"></param>
        /// <param name="logisticsInterface"></param>
        /// <returns></returns>
        public static T_MySql_Order CreateMap(Aorder order, string logisticsInterface)
        {
            try
            {
                var mySqlOrder = new T_MySql_Order();
                mySqlOrder.OUTSYS_BILL_CODE = string.IsNullOrEmpty(order.mailNo) ? "" : order.mailNo;
                mySqlOrder.OUTSYS_ORDER_NO = string.IsNullOrEmpty(order.logisticID) ? "" : order.logisticID;
                mySqlOrder.ORDER_SOURCE = ENUM_ORDER_SOURCE.ALI.ToString();
                mySqlOrder.MSG_TYPE = "JSON";
                //mySqlOrder.CREATE_BY = "";
                mySqlOrder.MSG_CONTENT = string.IsNullOrEmpty(logisticsInterface) ? "" : logisticsInterface;
                mySqlOrder.IS_SYNC_SUCCESS = 0;
                mySqlOrder.CREATE_TIME = DateTime.Now;

                mySqlOrder.CREATE_BY = mySqlOrder.ORDER_SOURCE;
                mySqlOrder.C1 = "";
                mySqlOrder.C2 = "";
                mySqlOrder.C3 = "";
                mySqlOrder.REMARK = "";
                return mySqlOrder;
            }
            catch (Exception e)
            {
                var message = new StringBuilder();
                message.Append("TAOBAO订单映射过程发生错误：" + logisticsInterface);
                message.Append(e.Message + e.StackTrace);
                return null;
            }
        }
        
        public static List<SiteDetail> GetSite(List<MQuerySite> lQuerySite)
        {
            String urlManager = AppSettings.QuerySite;
            // 唯一key值
            String key = AppSettings.QuerySitekey; // OMSConfig.QuerySitekey;//Key123  
            string json = lQuerySite.ToJson(); // JsonConvert.SerializeObject(lQuerySite);
            List<SiteDetail> sd = null;
            string msg = "";
            try
            {
                var ret = CallClient(urlManager, key, json);
                //string res = Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(ret));
                var result = JsonConvert.DeserializeObject<SiteResultInfo>(ret); //JsonHelper.DeserializeJsonToObject<SiteResultInfo>(ret);
                //0正常-1有错 如果批量里面存在有一个错的，那么code就会是-1
                if (!"0".Equals(result.RESULT_CODE))
                {
                    sd = result.RESULT;
                    if (sd != null && sd.Count > 0)
                    {
                        foreach (var s in sd)
                        {
                            if (s.statusFlag == "false")
                            {
                                msg += s.address;
                            }
                        }
                    }
                    //日志
                    $"{result.RESULT_DESC}错误地址{msg}".WriteToLog(LogerType.Error);

                }
                return result.RESULT;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private static string CallClient(String urlManager, String key, string json)
        {
            //对报文做MD5处理  HttpUtility.UrlEncode(
            string data = Convert.ToBase64String(Encoding.UTF8.GetBytes(UrlEncode(json, Encoding.UTF8)));
            string keyContent = Get16MD5(data + key);
            String URL = urlManager + "&data=" + data + "&md5_data=" + keyContent + "";

            MyHttpClient myWebClient = new MyHttpClient(URL);
            var ret = myWebClient.UploadString("", Encoding.UTF8);
            return ret;
        }


        /// <summary>
        /// .net与JAVA交互专用
        /// </summary>
        /// <param name="temp"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string UrlEncode(string temp, Encoding encoding)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < temp.Length; i++)
            {
                string t = temp[i].ToString();
                string k = HttpUtility.UrlEncode(t, encoding);
                if (t == k)
                {
                    stringBuilder.Append(t);
                }
                else
                {
                    stringBuilder.Append(k.ToUpper());
                }
            }
            return stringBuilder.ToString();
        }

        public static String Get16MD5(String str)
        {
            byte[] result = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(Encoding.UTF8.GetBytes(str));
            StringBuilder output = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                // convert from hexa-decimal to character  
                output.Append((result[i]).ToString("x2", System.Globalization.CultureInfo.InvariantCulture));
            }
            return output.ToString();
        }

        public static string FindStatusCode(string code)
        {
            string StatusCode = "";
            switch (code)
            {
                case "10":
                    StatusCode = "WAITACCEPT";
                    break;
                case "20":
                    StatusCode = "ACCEPT";
                    break;
                //case "0": StatusCode = "WAITACCEPT";
                //    break;
                case "40":
                    StatusCode = "ACCEPT";
                    break;
                case "45":
                    StatusCode = "UNACCEPT";
                    break;
                case "50":
                    StatusCode = "GOT";
                    break;
                case "60":
                    StatusCode = "NOGET";
                    break;
                //case "5": StatusCode = "GOT";
                //    break;
                case "70":
                    StatusCode = "SIGNSUCCESS";
                    break;
                case "80":
                    StatusCode = "SIGNSUCCESS";
                    break;
                case "90":
                    StatusCode = "SIGNFAILED";
                    break;
                case "30":
                    StatusCode = "CANCELLED";
                    break;
            }

            return StatusCode;
        }
    }
}
