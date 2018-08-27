using System.Xml.Serialization;

namespace KYOMS.Core20.Common.ZooKeeper.Model
{
    [XmlRootAttribute("configuration")]
    public class ZConfiguration
    {
        /// <summary>
        /// 配置文件本地和服务器版本信息
        /// </summary>
        [XmlElementAttribute("version")]
        public Version Version { get; set; }
        /// <summary>
        /// zookeeper服务器配置信息
        /// </summary>
        [XmlElementAttribute("zookeeperServer")]
        public ZookeeprtServer ZookeeprtServer { get; set; }
        /// <summary>
        /// zookeeper节点集合信息
        /// </summary>
        [XmlElementAttribute("node")]
        public Node Node { get; set; }

        /// <summary>
        /// 当前版本标识，用于开发人员区分当前zsettings.config文件版本
        /// </summary>
        [XmlAttribute("devVer")]
        public string DevVer { get; set; }

        /// <summary>
        /// 当前Zookeeper使用的环境，生产或测试
        /// </summary>
        [XmlAttribute("mode")]
        public string Mode { get; set; }
    }
}
