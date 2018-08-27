using KYOMS.Core20.Common.Log4NetCore;
using KYOMS.Core20.Common.LogCommon;
using KYOMS.Core20.Common.ZooKeeper.Model;
using System;

namespace KYOMS.Core20.Common.Zookeeper
{
    /// <summary>
    /// 监控Zookeeper请求的事件封装,不能被继承
    /// </summary>
    public sealed class ZookeeperConfigCenterManagerEvents
    {
        #region 请求Zookpeer之前事件
        //public delegate void RequestZookpeerBeforHandler(ZookeeprtServer zookeeprtServe, Node node, Group group, Field field);

        /// <summary>
        /// 请求Zookpeer之前事件
        /// </summary>
        public event Action<ZookeeprtServer, Node, Group, Field> OnRequestZookpeerBefor;

        internal void RequestZookpeerBefor(ZookeeprtServer zookeeprtServe, Node node, Group group, Field field)
        {
            OnRequestZookpeerBefor?.Invoke(zookeeprtServe, node, group, field);
        }
        #endregion

        #region 请求Zookpeer之后事件
        /// <summary>
        /// 请求Zookpeer之后事件
        /// </summary>
        public event Action<string> OnRequestZookpeerAfter;

        internal void RequestZookpeerAfter(string zookeeperResult)
        {
            OnRequestZookpeerAfter?.Invoke(zookeeperResult);
        }
        #endregion

        #region 请求Zookpeer出错事件

        /// <summary>
        /// 请求Zookpeer之后事件
        /// </summary>
        public event Action<Exception> OnRequestZookpeerError;

        internal void RequestZookpeerError(Exception exception)
        {
            OnRequestZookpeerError?.Invoke(exception);
        }
        #endregion
        #region Zookpeer写入log事件
        /// <summary>
        /// Zookpeer写入log事件
        /// </summary>
        public event Action<string, LogerType> OnZookpeerWriteLog;

        internal void ZookpeerWriteLog(string logText, LogerType logerType)
        {
            OnZookpeerWriteLog?.Invoke(logText, logerType);
        }
        #endregion
    }
}
