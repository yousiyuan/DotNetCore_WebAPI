using System;
using System.Diagnostics;
using System.IO;

namespace KYOMS.Core20.Common.LogCommon
{
    public class LogHandle
    {
        public readonly Loger _logger = null;
        /// <summary>
        /// 版本：生产环境true，测试环境false，默认为false。生产环境，Info和Debug不记录，测试环境全记录
        /// </summary>
        private readonly bool _version;
        public LogHandle(Type t, bool isfinalversion = false)
        {
            _logger = new Loger(t);
            _version = isfinalversion;
        }
        public enum LogerType
        {
            Error = 0,
            Info = 1,
            Debug = 2,
            Fatal = 3,
            Warn = 4
        }
        /// <summary>
        /// Log记录，可选参数为LogerType，默认值为Debug
        /// </summary>
        /// <param name="message">内容</param>
        /// <param name="logType">日志类型</param>
        public void Set(string message, LogerType logType = LogerType.Debug)
        {
            //方法名，内容
            var str = "\r\nMethod Name: " + (new StackTrace()).GetFrame(1).GetMethod().Name + "\r\nRecord Msg: " + message;
            switch (logType)
            {
                case LogerType.Error:
                    {
                        _logger.Error(str);
                        break;
                    }
                case LogerType.Info:
                    {
                        if (!_version) _logger.Info(str);
                        break;
                    }
                case LogerType.Fatal:
                    {
                        _logger.Fatal(str);
                        break;
                    }
                case LogerType.Warn:
                    {
                        _logger.Warn(str);
                        break;
                    }
                default:
                    {
                        if (!_version) _logger.Debug(str);
                        break;
                    }
            }
        }
    }
}
