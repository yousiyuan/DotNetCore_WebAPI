using System.Xml.Serialization;
using KYOMS.Core20.Common.Utility;

namespace KYOMS.Core20.Common.ZooKeeper.Model
{
    /// <summary>
    /// 版本
    /// </summary>
    public class Version
    {
        /// <summary>
        /// 本地配置文件版本号
        /// </summary>
        [XmlAttribute("locVer")]
        public double LocVer { get; set; }
        /// <summary>
        /// 从zookeeper中获取最新配置文件版本号路径
        /// </summary>
        [XmlAttribute("serVerPath")]
        public string SerVerPath { get; set; }

        /// <summary>
        /// zookeeper中获取最新配置文件版本号，由zookeeper查询后赋值
        /// </summary>
        public double SerVer { get; set; }
    }
}
