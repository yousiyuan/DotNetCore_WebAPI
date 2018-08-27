using log4net.Appender;
using log4net.Core;
using log4net.Filter;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.Concurrent;

namespace KYOMS.Core20.Common.Log4NetCore
{
    /// <inheritdoc />
    /// <summary>
    /// 自定义装饰Logger对象
    /// </summary>
    internal class RollingFileLogger : BasicLogger
    {
        private readonly ConcurrentQueue<IAppender> _appenderContainer;

        //默认配置
        private const int MaxSizeRollBackups = 50;//在发生文件分割时，最多保留的历史文件个数
        private const string LayoutPattern = "%d %-5p %c [%t]  %m%n";
        //private const string DatePattern = "yyyyMMdd\".txt\"";
        private const string MaximumFileSize = "1MB";//设置按文件大小分割的阈值

        /// <summary>
        /// 创建日志工厂对象
        /// </summary>
        /// <param name="repositoryname">日志对象库名称</param>
        /// <param name="log4Netconfig">配置文件执行路径</param>
        /// <param name="filename">日志文件名称</param>
        internal RollingFileLogger(string repositoryname, string log4Netconfig, string filename)
            : base(repositoryname, log4Netconfig)
        {
            //读取配置文件
            IAppender[] appenders = Repository.GetAppenders();
            //初始化配置
            _appenderContainer = InitializeSetting(appenders, filename);
        }

        internal override ILogger GetCustomLogger(string loggerName)
        {
            Logger logger = Repository.LoggerFactory.CreateLogger(Repository, loggerName);
            logger.Hierarchy = Repository;
            logger.Parent = Repository.Root;
            logger.Level = Level.Debug;//日志输出等级(默认)
            logger.Additivity = false;
            while (_appenderContainer.Count > 0)
            {
                _appenderContainer.TryDequeue(out var result);
                logger.AddAppender(result);
            }
            logger.Repository.Configured = true;
            return logger;
        }

        private ConcurrentQueue<IAppender> InitializeSetting(IAppender[] appenders, string filename)
        {
            //定义用于创建Logger对象的配置信息
            var appenderlist = new ConcurrentQueue<IAppender>();

            //自定义配置
            if (appenders.Length <= 0)
            {
                var rollingFileAppender = GetNewFileApender("RollingFileAppender", $"{filename}", MaxSizeRollBackups, true, true, MaximumFileSize, RollingFileAppender.RollingMode.Size, LayoutPattern);
                appenderlist.Enqueue(rollingFileAppender);

                PatternLayout layout = new PatternLayout("%d{HH:mm:ss} %-5level %logger %thread %m%n");
                ConsoleAppender consoleAppender = new ConsoleAppender
                {
                    Name = "ConsoleAppender",
                    Layout = layout,
                    Threshold = Level.All,
                };
                layout.ActivateOptions();
                consoleAppender.ActivateOptions();
                appenderlist.Enqueue(consoleAppender);

                return appenderlist;
            }

            //重写配置
            for (var i = 0; i < appenders.Length; i++)
            {
                if (appenders[i] is RollingFileAppender)
                {
                    var appender = appenders[i] as RollingFileAppender;
                    var rollingFileAppender = GetNewFileApender("RollingFileAppender", $"{filename}", appender.MaxSizeRollBackups, appender.AppendToFile, true, appender.MaximumFileSize, RollingFileAppender.RollingMode.Size, LayoutPattern);
                    appenderlist.Enqueue(rollingFileAppender);
                }
                if (appenders[i] is ConsoleAppender)
                {
                    var consoleAppender = appenders[i] as ConsoleAppender;
                    appenderlist.Enqueue(consoleAppender);
                }
            }
            return appenderlist;
        }

        private static RollingFileAppender GetNewFileApender(string appenderName, string file, int maxSizeRollBackups, bool appendToFile = true, bool staticLogFileName = false, string maximumFileSize = "5MB", RollingFileAppender.RollingMode rollingMode = RollingFileAppender.RollingMode.Composite, string layoutPattern = "%d [%t] %-5p %c  - %m%n")
        {
            var appender = new RollingFileAppender
            {
                Name = appenderName,
                Encoding= System.Text.Encoding.UTF8,
                File = file,
                AppendToFile = appendToFile,
                LockingModel = new FileAppender.MinimalLock(),
                RollingStyle = rollingMode,
                //DatePattern = datePattern,
                StaticLogFileName = staticLogFileName,
                MaxSizeRollBackups = maxSizeRollBackups,
                MaximumFileSize = maximumFileSize,
                CountDirection = 0,
                Threshold = Level.All
            };
            var layout = new PatternLayout(layoutPattern)
            {
                Header = "[Header]" + Environment.NewLine,
                Footer = "[Footer]" + Environment.NewLine
            };
            appender.Layout = layout;
            var filter = new LevelRangeFilter
            {
                LevelMin = Level.All,
                LevelMax = Level.Fatal
            };
            appender.AddFilter(filter);

            layout.ActivateOptions();
            filter.ActivateOptions();
            appender.ActivateOptions();
            return appender;
        }
    }
}
