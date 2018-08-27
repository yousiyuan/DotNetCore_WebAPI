using System.Collections.Generic;
using System.Xml.Serialization;

namespace KYOMS.Core20.Common.ZooKeeper.Model
{
    public class Group
    {
        /// <summary>
        /// 配置文件节点路径名称
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }
        /// <summary>
        /// 配置节点下的配置属性对象集合
        /// </summary>
        [XmlElementAttribute("field")]
        public List<Field> Fields { get; set; }
    }
}
