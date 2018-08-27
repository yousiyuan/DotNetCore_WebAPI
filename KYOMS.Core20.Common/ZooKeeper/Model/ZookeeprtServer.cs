using System.Collections.Generic;
using System.Xml.Serialization;

namespace KYOMS.Core20.Common.ZooKeeper.Model
{
    /// <summary>
    /// Zookeeprt服务配置
    /// </summary>
    public class ZookeeprtServer
    {
        /// <summary>
        /// ZookeeprtServer连接字符串或URL集合，配置多个URL保证了高可用性
        /// </summary>
        [XmlElementAttribute("url")]
        public List<ZUrl> ConnectUrls { get; set; }

        /// <summary>
        /// zookeeper认证模式[world：有个单一的ID，anyone，表示任何人。auth：不使用任何ID，表示任何通过验证的用户,digest：使用 用户名：密码 字符串]
        /// </summary>
        [XmlAttribute("oauthMode")]
        public string OauthMode { get; set; }
    }
}
