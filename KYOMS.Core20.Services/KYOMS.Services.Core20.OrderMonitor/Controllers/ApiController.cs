using Dapper;
using KYOMS.Core20.Application;
using KYOMS.Core20.Common.Config;
using KYOMS.Services.Core20.OrderMonitor.Entity;
using KYOMS.Services.Core20.OrderMonitor.Utility;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace KYOMS.Services.Core20.OrderMonitor.Controllers
{

    [Route("api/orderMonitor")]
    public class ApiController : BaseController
    {
        OrderMonitorService orderMonitorService = new OrderMonitorService();
        [HttpPost("getdata")]
        public ActionResult List(SearchEntity searchEntity)
        {
            try
            {
                var data = orderMonitorService.GetList(searchEntity).Result.AsList();
                int rows = orderMonitorService.GetCount(searchEntity).Result;
                JsonMsg json = new JsonMsg()
                {
                    Data = data,
                    Msg = "success",
                    Status = 0
                };
                return Json(new { rows = data, Msg = "success", Status = 0, total = rows }, JsonSettings);
            }
            catch (System.Exception e)
            {
                return Json(new { rows = 0, Msg = e.Message + "<br/>" + e.StackTrace, Status = 1, total = 0 }, JsonSettings);
            }
        }
        [HttpGet("test")]
        public async Task<ActionResult> test(SearchEntity searchEntity)
        {
            //int rows2 = await orderMonitorService.Insert(new System.Collections.ArrayList
            //{
            //    new test1(){批次号="1",运单号="11",创建时间=DateTime.Now },
            //    new test1(){批次号="2",运单号="22",创建时间=DateTime.Now },
            //    new test1(){批次号="3",运单号="33",创建时间=DateTime.Now },
            //});
            return Json(new { rows = ConfigHelper.GetWebConfigString("MySqlConnectionString1"), Msg = AppDomain.CurrentDomain.BaseDirectory, Status = 0, total = "1111"}, JsonSettings);
        }
    }
}
