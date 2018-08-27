using System;
using System.IO;
using System.Linq;
using log4net;
using log4net.Config;
using log4net.Core;
using log4net.Repository;

namespace KYOMS.Core20.Common.LogCommon
{
    public class Loger : ILogger
    {
        readonly ILogger _instance;

        public string Name => "OmsLogRepository";

        public ILoggerRepository Repository =>_instance.Repository;

        public Loger(Type t)
        {
            var Repository = LoggerManager.GetAllRepositories().FirstOrDefault(p => p.Name == Name);
            if (Repository == null)
                Repository = LoggerManager.CreateRepository(Name);
            FileInfo fi = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + ("/config/log4net.config"));
            XmlConfigurator.Configure(Repository, fi);
            _instance = Repository.GetLogger(Name);
        }

        public void Error(string text)
        {
            Log(null, Level.Error, text, null);
        }

        public void Info(string text)
        {
            Log(null, Level.Info, text, null);
        }

        public void Debug(string text)
        {
            Log(null, Level.Debug, text, null);
        }

        public void Fatal(string text)
        {
            Log(null, Level.Fatal, text, null);
        }

        public void Warn(string text)
        {
            Log(null, Level.Warn, text, null);
        }

        public void Log(Type callerStackBoundaryDeclaringType, Level level, object message, Exception exception)
        {
            _instance.Log(callerStackBoundaryDeclaringType, level, message, exception);
            
        }

        public void Log(LoggingEvent logEvent)
        {
            _instance.Log(logEvent);
        }

        public bool IsEnabledFor(Level level)
        {
            return _instance.IsEnabledFor(level);
        }
    }
}
