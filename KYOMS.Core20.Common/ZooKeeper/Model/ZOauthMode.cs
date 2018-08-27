using System;
using System.Collections.Generic;
using System.Text;

namespace KYOMS.Core20.Common.ZooKeeper.Model
{
    /// <summary>
    /// Zookeeper认证方式
    /// </summary>
    public static class ZOauthMode
    {
        /// <summary>
        /// 有个单一的ID，anyone，表示任何人。 
        /// </summary>
        public const string World = "world";
        /// <summary>
        /// 不使用任何ID，表示任何通过验证的用户（是通过ZK验证的用户？连接到此ZK服务器的用户？）
        /// </summary>
        public  const string Auth = "auth";
        /// <summary>
        /// 使用 用户名：密码 字符串生成MD5哈希值作为ACL标识符ID。权限的验证通过直接发送用户名密码字符串的方式完成
        /// </summary>
        public const string Digest = "digest";
    }
}
