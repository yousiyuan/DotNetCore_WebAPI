using log4net.Core;
using log4net.Repository.Hierarchy;
using System;
using System.IO;

namespace KYOMS.Core20.Common.Log4NetCore
{
    /// <summary>
    /// 创建基本的日志对象
    /// </summary>
    internal class BasicLogger
    {
        protected Hierarchy Repository;
        private readonly string _repositoryname;
        private readonly string _log4Netconfig;
        internal BasicLogger(string repositoryname, string log4Netconfig)
        {
            _repositoryname = repositoryname;
            _log4Netconfig = log4Netconfig;
            Repository = RepositoryFactory.GetRepository(repositoryname);

            if (!string.IsNullOrWhiteSpace(log4Netconfig))
            {
                var fileinfo = new FileInfo($"{AppDomain.CurrentDomain.BaseDirectory}{log4Netconfig}");
                if (fileinfo.Exists)
                    log4net.Config.XmlConfigurator.Configure(Repository, fileinfo);
                else
                    _log4Netconfig = null;
            }
        }

        internal virtual ILogger GetCustomLogger(string loggername)
        {
            if (string.IsNullOrWhiteSpace(_log4Netconfig))
            {
                throw new ArgumentNullException("作用于日志对象的配置文件不存在", new Exception());
            }
            var logger = LoggerManager.GetLogger(_repositoryname, loggername);
            return logger;
        }
    }
}
