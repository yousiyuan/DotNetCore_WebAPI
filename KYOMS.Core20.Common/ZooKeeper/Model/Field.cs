using System.Xml.Serialization;

namespace KYOMS.Core20.Common.ZooKeeper.Model
{
    public class Field
    {
        /// <summary>
        /// 为改配置文件节点属性名称，和tpl中的${name}对应并且和zookeeper中的配置项属性对应
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }
        /// <summary>
        /// 表示该配置文件的模版地址
        /// </summary>
        [XmlAttribute("tpl")]
        public string Tpl { get; set; }
        /// <summary>
        /// 表示该配置文件替换后保存的位置
        /// </summary>
        [XmlAttribute("path")]
        public string Path { get; set; }
    }
}
