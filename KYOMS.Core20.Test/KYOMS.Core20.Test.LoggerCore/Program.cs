﻿using System;
using KYOMS.Core20.Common.Log4NetCore;

namespace KYOMS.Core20.Test.LoggerCore
{
    class Program
    {
        static void Main(string[] args)
        {

            //设置日志项目名称和端口号
            AppDomain.CurrentDomain.SetData("ProjectName", "LoggerCore");
            AppDomain.CurrentDomain.SetData("Port", 6637);

            //测试日志
            string logStr = "logistics_interface=%7B%22chargingInfo%22%3A%7B%22basicWeight%22%3A%7B%22fee%22%3A%225%22%2C%22weight%22%3A%223%22%7D%2C%22stepWeight%22%3A%7B%22fee%22%3A%222%22%2C%22weight%22%3A%221%22%7D%2C%22totalFee%22%3A%2223%22%7D%2C%22courierInfo%22%3A%7B%22company%22%3A%22%E5%A4%A9%E5%A4%A9%E5%BF%AB%E9%80%92%22%2C%22courierNo%22%3A%22199900%22%2C%22courierPhone%22%3A%221381111111%22%2C%22extendFields%22%3A%5B%7B%22key%22%3A%22PACKAGE_NO%22%2C%22value%22%3A%22DWD0018%22%7D%5D%2C%22name%22%3A%22%E5%BC%A0%E4%B8%89%22%2C%22station%22%3A%22%E4%BD%99%E6%9D%AD%E7%BD%91%E7%82%B9%22%2C%22stationPhone%22%3A%221381111111%22%7D%2C%22cpCode%22%3A%22SF%22%2C%22dicCode%22%3A%22DCP00098765%22%2C%22expressPrintCode%22%3A%22AeduSjkd%22%2C%22logisticsId%22%3A%22LBXDCP1023892594%22%2C%22mailNo%22%3A%226201710180538%22%2C%22opRequire%22%3A%7B%22opDesc%22%3A%22%E5%A4%A9%E7%8C%AB%E5%85%A8%E6%B8%A0%E9%81%93%E7%9A%84%E4%BD%9C%E4%B8%9A%E7%B1%BB%E5%9E%8B%22%2C%22opType%22%3A%22TMQQD%22%7D%2C%22orderBizType%22%3A%221%22%2C%22orderCreateTime%22%3A%222014-07-28+10%3A05%3A34%22%2C%22orderExtendFields%22%3A%5B%7B%22key%22%3A%22A001%22%2C%22value%22%3A%225000%22%7D%5D%2C%22orderSource%22%3A%22TB%22%2C%22packageInfo%22%3A%7B%22itemList%22%3A%5B%7B%22extendFields%22%3A%22is_precious%3A1%3BitemId%3A45571155013%3BserviceFlag%3A2%3B%22%2C%22insuredAmount%22%3A%22199900%22%2C%22itemName%22%3A%22%E6%AF%9B%E8%A1%A3%22%2C%22itemNum%22%3A%221%22%7D%5D%2C%22packageHeight%22%3A%2250%22%2C%22packageLength%22%3A%2250%22%2C%22packageVolume%22%3A%22500000%22%2C%22packageWeight%22%3A%22200000%22%2C%22packageWidth%22%3A%2250%22%7D%2C%22placeOrderType%22%3A%221%22%2C%22receiver%22%3A%7B%22address%22%3A%22%E5%9B%9B%E5%B7%9D%E7%9C%81%E6%88%90%E9%83%BD%E5%B8%82%E9%AB%98%E6%96%B0%E5%8C%BA%E6%96%B0%E8%88%AA%E8%B7%AF283%E5%8F%B7%22%2C%22city%22%3A%22%E6%88%90%E9%83%BD%22%2C%22county%22%3A%22%E9%AB%98%E6%96%B0%22%2C%22mobile%22%3A%2215956874256%22%2C%22name%22%3A%22%E6%9D%8E%E6%9F%90%22%2C%22phone%22%3A%22024-84332654%22%2C%22province%22%3A%22%E5%9B%9B%E5%B7%9D%22%2C%22receiverDivisionId%22%3A%2267991%22%2C%22receiverTown%22%3A%22%E6%96%87%E4%B8%80%E8%B7%AF%22%2C%22userId%22%3A%22123456%22%2C%22zipCode%22%3A%22634569%22%7D%2C%22remark%22%3A%22%22%2C%22routeInfo%22%3A%7B%22after%22%3A%7B%22address%22%3A%22%E6%9D%AD%E5%B7%9E%E5%B8%82%E6%96%87%E4%B8%80%E8%A5%BF%E8%B7%AF999%E5%8F%B7%22%2C%22city%22%3A%22%E6%9D%AD%E5%B7%9E%22%2C%22contactNumber%22%3A%2213812345678%22%2C%22contacts%22%3A%22%E5%BC%A0%E4%B8%89%22%2C%22county%22%3A%22%E9%AB%98%E5%85%B4%E5%8C%BA%22%2C%22cpCode%22%3A%22CP001%22%2C%22prov%22%3A%22%E6%B5%99%E6%B1%9F%22%2C%22town%22%3A%22%E6%96%87%E4%B8%80%E8%A5%BF%E8%B7%AF%22%7D%2C%22before%22%3A%7B%22address%22%3A%22669%E5%8F%B7%22%2C%22city%22%3A%22%E6%9D%AD%E5%B7%9E%22%2C%22contactNumber%22%3A%22138111111989%22%2C%22contacts%22%3A%22%E5%BC%A0%E4%B8%89%22%2C%22county%22%3A%22%E9%AB%98%E5%85%B4%E5%8C%BA%22%2C%22cpCode%22%3A%22T002%22%2C%22prov%22%3A%22%E6%B5%99%E6%B1%9F%22%2C%22town%22%3A%22%E6%96%87%E4%B8%80%E8%A5%BF%E8%B7%AF%22%7D%2C%22carrierCode%22%3A%22T123568%22%2C%22extendFields%22%3A%5B%7B%22key%22%3A%22SF_PRODUCT_CODE%22%2C%22value%22%3A%221%22%7D%5D%2C%22shortAddress%22%3A%22%E6%B5%99%EF%BC%8D%E6%9D%AD%22%2C%22siteCode%22%3A%22HX1%22%2C%22siteName%22%3A%22%E6%9D%AD%E8%90%A71%E7%AB%99%22%7D%2C%22sender%22%3A%7B%22Town%22%3A%22%E8%A1%97%E9%81%93%22%2C%22address%22%3A%22%E6%B5%99%E6%B1%9F%E7%9C%81%E6%9D%AD%E5%B7%9E%E5%B8%82%E4%BD%99%E6%9D%AD%E5%8C%BA%E6%96%87%E4%B8%80%E8%A5%BF%E8%B7%AF823%E5%8F%B7%22%2C%22city%22%3A%22%E6%9D%AD%E5%B7%9E%22%2C%22county%22%3A%22%E4%BD%99%E6%9D%AD%22%2C%22customerId%22%3A%2212792%22%2C%22customerNo%22%3A%22JD007689%22%2C%22mobile%22%3A%2213989203824%22%2C%22name%22%3A%22%E5%88%98%E6%9F%90%22%2C%22phone%22%3A%220571-84292224%22%2C%22province%22%3A%22%E6%B5%99%E6%B1%9F%22%2C%22senderAddressId%22%3A%227638290%22%2C%22senderDivisionId%22%3A%2267991%22%2C%22senderTown%22%3A%22%E8%A1%97%E9%81%93%22%2C%22userId%22%3A%22123456%22%2C%22zipCode%22%3A%22638293%22%7D%2C%22serviceDetail%22%3A%7B%22codBuyServiceFee%22%3A%22300%22%2C%22codSplitFee%22%3A%22100%22%2C%22codTotalServiceFee%22%3A%22500%22%2C%22customerOrderTime%22%3A%222014-07-30+14%3A00%3A00%22%2C%22endSignTime%22%3A%222015-09-11+17%3A00%3A00%22%2C%22expressScheduleType%22%3A%22101%22%2C%22extendFields%22%3A%5B%7B%22key%22%3A%22A001%22%2C%22value%22%3A%225000%22%7D%5D%2C%22goodsValue%22%3A%2210000%22%2C%22gotEndTime%22%3A%22204-11-30+19%3A00%22%2C%22gotInTime%22%3A%2230%22%2C%22gotStartTime%22%3A%22204-11-30+18%3A00%22%2C%22grabOrderTime%22%3A%222014-07-30+14%3A00%3A00%22%2C%22prevMailNo%22%3A%22%22%2C%22promiseSignTime%22%3A%222015-09-11+16%3A30%3A00%22%2C%22scheduleDesc%22%3A%22%E9%80%81%E8%BE%BE%E6%97%B6%E9%97%B4%E5%9C%A82014-07-30+10%3A00%3A00%E5%88%B02014-07-30+14%3A00%3A00%E4%B9%8B%E9%97%B4%22%2C%22scheduleType%22%3A%22104%22%2C%22sendEndTime%22%3A%222014-07-30+13%3A05%3A34%22%2C%22sendStartTime%22%3A%222014-07-30+12%3A05%3A34%22%2C%22temperatureRequirement%22%3A%220-18%22%2C%22totalFee%22%3A%22600%22%2C%22totalInsuredAmount%22%3A%2223930%22%7D%2C%22serviceFlag%22%3A%2234%2C37%22%2C%22tradeNo%22%3A%2222227880099%22%7D&data_digest=CgtasrQSpiCO6Vjk6cHJug%3D%3D&msg_type=TMS_CREATE_ORDER_ONLINE_NOTIFY&msg_id=DCP00000000000002354032&ecCompanyId=TAOBAO";
            while (true)
            {
                logStr.WriteToLog();
                logStr.WriteToLog(LogerType.Debug);
                logStr.WriteToLog(LogerType.Warn);
            }

        }
    }
}