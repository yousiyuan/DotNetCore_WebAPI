using KYOMS.Core20.Common.Config;
using SmartSql.ZooKeeperConfig;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace KYOMS.Core20.Common.ZooKeeper
{
    public class ZooKeeperConfiger : ZooKeeperConfigLoader
    {
        private static string ConnStr = ConfigHelper.GetWebConfigString("ZooKeeperConnectionString");
        public ZooKeeperConfiger() : base(ConnStr)
        {

        }
        public ZooKeeperConfiger(string connStr) : base(connStr)
        {

        }
        /// <summary>
        /// 从ZooKeeper获取字节数组数据
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public byte[] GetBytesData(string path)
        {
            byte[] bytes = ZooClient.getDataAsync(path).Result.Data;
            return bytes;
        }

        /// <summary>
        /// 从ZooKeeper获取Stream数据
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public Stream GetStreamData(string path)
        {
            byte[] bytes = GetBytesData(path);
            using (Stream stream = new MemoryStream(bytes))
            {
                stream.Read(bytes, 0, bytes.Length);
                stream.Seek(0, SeekOrigin.Begin);
                return stream;
            }
        }

        /// <summary>
        /// 从ZooKeeper获取字符串格式数据
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string GetStringData(string path)
        {
            byte[] bytes = GetBytesData(path);
            string str = Encoding.UTF8.GetString(bytes);
            return !string.IsNullOrEmpty(str) ? str.Trim() : "";
        }
    }
}
