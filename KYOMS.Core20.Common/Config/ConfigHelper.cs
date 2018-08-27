using System;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace KYOMS.Core20.Common.Config
{
    public static class ConfigHelper
    {
        public static IConfigurationRoot configuration;
        public static IConfigurationRoot Configuration
        {
            get
            {
                if (configuration == null)
                {
                    configuration = new ConfigurationBuilder()
                    .AddInMemoryCollection()    //将配置文件的数据加载到内存中
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)   //指定配置文件所在的目录
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
                }
                return configuration;
            }
        }

        public static IConfigurationRoot allConfiguration;

        /// <summary>
        /// 加载根目录下所有Json配置文件
        /// </summary>
        public static IConfigurationRoot AllConfiguration
        {
            get
            {
                if (allConfiguration == null)
                {
                    string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                    var cb = new ConfigurationBuilder().AddInMemoryCollection().SetBasePath(baseDir);
                    string[] FileList = Directory.GetFiles(baseDir, "*.json", SearchOption.AllDirectories);
                    for (int i = 0; i < FileList.Length; i++)
                    {
                        FileInfo fileInfo = new FileInfo(FileList[i]);
                        cb.AddJsonFile(fileInfo.Name, optional: true, reloadOnChange: true);
                    }
                    allConfiguration = cb.Build();
                }
                return allConfiguration;
            }
        }

        /// <summary>
        /// 根据环境获得配置
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetWebConfigString(string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                return Configuration[key];
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 得到AppSettings中的配置Bool信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool GetWebConfigBool(string key)
        {
            var result = false;
            var cfgVal = GetWebConfigString(key);
            if (!string.IsNullOrEmpty(cfgVal))
            {
                try
                {
                    result = bool.Parse(cfgVal);
                }
                catch (FormatException)
                {
                    // Ignore format exceptions.
                    //throw ex;
                }
            }
            return result;
        }
        /// <summary>
        /// 得到AppSettings中的配置Decimal信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static decimal GetWebConfigDecimal(string key)
        {
            decimal result = 0;
            var cfgVal = GetWebConfigString(key);
            if (!string.IsNullOrEmpty(cfgVal))
            {
                try
                {
                    result = decimal.Parse(cfgVal);
                }
                catch (FormatException)
                {
                    // Ignore format exceptions.
                }
            }
            return result;
        }
        /// <summary>
        /// 得到AppSettings中的配置float信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static float GetWebConfigFloat(string key)
        {
            float result = 0;
            var cfgVal = GetWebConfigString(key);
            if (!string.IsNullOrEmpty(cfgVal))
            {
                try
                {
                    result = float.Parse(cfgVal);
                }
                catch (FormatException)
                {
                    // Ignore format exceptions.
                }
            }
            return result;
        }
        /// <summary>
        /// 得到AppSettings中的配置int信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int GetWebConfigInt(string key)
        {
            var result = 0;
            var cfgVal = GetWebConfigString(key);
            if (!string.IsNullOrEmpty(cfgVal))
            {
                try
                {
                    result = int.Parse(cfgVal);
                }
                catch (FormatException)
                {
                    // Ignore format exceptions.
                }
            }
            return result;
        }
    }
}
