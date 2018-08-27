using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace KYOMS.Core20.Common.ZooKeeper.Model
{
    public class ZUrl
    {
        /// <summary>
        /// 当前Zookeeper使用的环境，生产或测试
        /// </summary>
        [XmlAttribute("mode")]
        public string Mode { get; set; }

        /// <summary>
        /// Zookeeper连接字符串
        /// </summary>
        [XmlText]
        public string Text { get; set; }

        /// <summary>
        /// zookeeper登录用户名
        /// </summary>
        [XmlAttribute("user")]
        public string User { get; set; }

        /// <summary>
        /// zookeeper登录密码
        /// </summary>
        [XmlAttribute("pwd")]
        public string Pwd { get; set; }
    }
}
