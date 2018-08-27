using log4net.Core;
using log4net.Repository.Hierarchy;
using System.Diagnostics;

namespace KYOMS.Core20.Common.Log4NetCore
{
    public enum LogerType
    {
        Error = 0,
        Info = 1,
        Debug = 2,
        Fatal = 3,
        Warn = 4
    }

    public class LoggerHandle
    {
        private readonly Logger _logger;
        /// <summary>
        /// 版本：生产环境true，测试环境false，默认为false。生产环境，Info和Debug不记录，测试环境全记录
        /// </summary>
        private readonly bool _version;

        public LoggerHandle(string log4Netconfig, bool isfinalversion = false, string logger = "Default", string repository = "OmsLogRepository")
        {
            _version = isfinalversion;

            var basiclogger = new BasicLogger(repository, log4Netconfig);
            _logger = basiclogger.GetCustomLogger(logger) as Logger;
        }

        public LoggerHandle(string log4Netconfig, string filename, bool isfinalversion = false, string logger = "Default", string repository = "OmsLogRepository")
        {
            _version = isfinalversion;
            var basiclogger = new RollingFileLogger(repository, log4Netconfig, filename);
            _logger = basiclogger.GetCustomLogger(logger) as Logger;
        }

        /// <summary>
        /// Log记录，可选参数为LogerType，默认值为Debug
        /// </summary>
        /// <param name="message">内容</param>
        /// <param name="logType">日志类型</param>
        public void Set(string message, LogerType logType = LogerType.Debug)
        {
            //方法名，内容
            //var str = "\r\nMethod Name: " + (new StackTrace()).GetFrame(1).GetMethod().Name + "\r\nRecord Msg: " + message;
            message = $"\r\n{message}";
            switch (logType)
            {
                case LogerType.Info:
                    if (!_version) _logger.Log(Level.Info, message, null);
                    break;
                case LogerType.Warn:
                    _logger.Log(Level.Warn, message, null);
                    break;
                case LogerType.Error:
                    _logger.Log(Level.Error, message, null);
                    break;
                case LogerType.Fatal:
                    _logger.Log(Level.Fatal, message, null);
                    break;
                default:
                    if (!_version) _logger.Log(Level.Debug, message, null);
                    break;
            }
        }
    }
}
