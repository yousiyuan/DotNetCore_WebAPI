using System.Collections.Generic;
using System.Xml.Serialization;

namespace KYOMS.Core20.Common.ZooKeeper.Model
{
    public class Node
    {
        /// <summary>
        /// zookeeper路配置文件父路径
        /// </summary>
        [XmlAttribute("baseUrl")]
        public string BaseUrl { get; set; }
        
        /// <summary>
        /// zookeeper配置节点分组信息集合
        /// </summary>
        [XmlElementAttribute("group")]
        public List<Group> Groups { get; set; }
    }
}
