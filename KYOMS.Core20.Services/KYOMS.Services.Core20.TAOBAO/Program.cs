using KYOMS.Core20.Common.Config;
using KYOMS.Core20.Common.Log4NetCore;
using KYOMS.Core20.Common.Zookeeper;
using KYOMS.Core20.Common.ZooKeeper.Model;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;

namespace KYOMS.Services.Core20.TAOBAO
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //设置日志项目名称和端口号
            AppDomain.CurrentDomain.SetData("ProjectName", "TaoBao");
            AppDomain.CurrentDomain.SetData("Port", ConfigHelper.GetWebConfigInt("port"));

            var zookeeperConfigCenterManager = ZookeeperConfigCenterManager.GetInStance();
            zookeeperConfigCenterManager.ZookeeperConfigCenterManagerEvents.OnRequestZookpeerBefor += ZookeeperConfigCenterManager_OnRequestZookpeerBefor;
            zookeeperConfigCenterManager.ZookeeperConfigCenterManagerEvents.OnRequestZookpeerAfter += ZookeeperConfigCenterManagerEvents_OnRequestZookpeerAfter;
            zookeeperConfigCenterManager.ZookeeperConfigCenterManagerEvents.OnRequestZookpeerError += ZookeeperConfigCenterManagerEvents_OnRequestZookpeerError;
            zookeeperConfigCenterManager.ZookeeperConfigCenterManagerEvents.OnZookpeerWriteLog += ZookeeperConfigCenterManagerEvents_OnZookpeerWriteLog;
            zookeeperConfigCenterManager.Start();

            BuildWebHost(args).Run();
        }
        private static void ZookeeperConfigCenterManagerEvents_OnZookpeerWriteLog(string logText, KYOMS.Core20.Common.Zookeeper.LogerType logerType)
        {
            logText.WriteToLog(Enum.Parse<KYOMS.Core20.Common.Log4NetCore.LogerType>(logerType.ToString()));
        }
        private static void ZookeeperConfigCenterManagerEvents_OnRequestZookpeerError(Exception exception)
        {
            Console.WriteLine("调用Zookpeer异常信息:" + exception.Message);
            exception.Message.WriteToLog(KYOMS.Core20.Common.Log4NetCore.LogerType.Error);
        }

        private static void ZookeeperConfigCenterManagerEvents_OnRequestZookpeerAfter(string zookeeperResult)
        {
            Console.WriteLine("Zookpeer返回结果:" + zookeeperResult);
            ("Zookpeer返回结果:" + zookeeperResult).WriteToLog();
        }
        private static void ZookeeperConfigCenterManager_OnRequestZookpeerBefor(ZookeeprtServer zookeeprtServe, Node node, Group group, Field field)
        {
            string path = node.BaseUrl + group.Name + "/" + field.Name;
            Console.WriteLine("当前Zookeeper路径:" + path);
            ("当前Zookeeper路径:" + path).WriteToLog();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
