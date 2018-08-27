using KYOMS.Core20.Common.ZooKeeper;
using KYOMS.Core20.Common.ZooKeeper.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace KYOMS.Core20.Common.Zookeeper
{
    /// <summary>
    /// Zookeeper统一配置中心管理类
    /// </summary>
    public class ZookeeperConfigCenterManager
    {
        #region 私有成员变量
        private static readonly Object Locker = new object();
        private static ZookeeperConfigCenterManager zookeeperConfigCenter;
        private static string SettingsPath = "";
        private static ZookeeperConfigCenterManagerEvents zookeeperConfigCenterManagerEvents;
        private static ZooKeeperConfiger configer;
        #endregion

        /// <summary>
        /// ZookeeperConfigCenterManager事件类
        /// </summary>
        public ZookeeperConfigCenterManagerEvents ZookeeperConfigCenterManagerEvents
        {
            get
            {
                if (zookeeperConfigCenterManagerEvents == null)
                {
                    zookeeperConfigCenterManagerEvents = new ZookeeperConfigCenterManagerEvents();
                }
                return zookeeperConfigCenterManagerEvents;
            }
        }

        /// <summary>
        /// 私有化构造函数,禁止使用new创建该实例
        /// </summary>
        private ZookeeperConfigCenterManager()
        { }

        /// <summary>
        /// 自定义配置创建ZookeeperConfigCenterManager
        /// </summary>
        /// <param name="ConfigurationFile">配置文件相对路径</param>
        /// <returns></returns>
        public static ZookeeperConfigCenterManager GetInStance(string ConfigurationFile)
        {
            return _getInstance(ConfigurationFile);
        }

        /// <summary>
        /// 默认配置创建ZookeeperConfigCenterManager
        /// </summary>
        public static ZookeeperConfigCenterManager GetInStance()
        {
            return _getInstance();
        }

        /// <summary>
        /// 单例模式创建ZookeeperConfigCenterManager
        /// </summary>
        /// <param name="_configurationFile">配置文件相对路径</param>
        /// <returns></returns>
        private static ZookeeperConfigCenterManager _getInstance(string _configurationFile = null)
        {
            zookeeperConfigCenterManagerEvents = new ZookeeperConfigCenterManagerEvents();
            if (string.IsNullOrEmpty(_configurationFile))
                SettingsPath = GetFileInfo("config/zsettings.config").FullName;
            else
                SettingsPath = GetFileInfo(_configurationFile).FullName;
            if (!File.Exists(SettingsPath))
                throw new FileNotFoundException("未找到配置文件：" + SettingsPath);

            if (zookeeperConfigCenter == null)
            {
                lock (Locker)
                {
                    if (zookeeperConfigCenter == null)
                    {
                        zookeeperConfigCenter = new ZookeeperConfigCenterManager();
                    }
                }
            }
            zookeeperConfigCenterManagerEvents.ZookpeerWriteLog("单例模式创建对象完成，" + SettingsPath, LogerType.Info);
            return zookeeperConfigCenter;
        }


        /// <summary>
        /// 初始化ZooKeeper对象，使其具有高可用性
        /// </summary>
        /// <param name="action">回调函数</param>
        private void InitZooKeeperConfiger(Action action)
        {
            List<ZUrl> list = ConfigurationInfo.ZookeeprtServer.ConnectUrls.Where(o => { return o.Mode == ConfigurationInfo.Mode; }).ToList();
            foreach (var item in list)
            {
                try
                {
                    configer = new ZooKeeperConfiger(item.Text);
                    ConfigurationInfo.Version.SerVer = Convert.ToDouble(configer.GetStringData(ConfigurationInfo.Version.SerVerPath));//服务器最新版本
                    ZookeeperConfigCenterManagerEvents.ZookpeerWriteLog(string.Format("使用节点{0}连接zookeeper成功\n", item.Text), LogerType.Info);
                    action?.Invoke();
                    break;
                }
                catch (Exception ex)
                {
                    ConfigurationInfo.Version.SerVer = 0;
                    ZookeeperConfigCenterManagerEvents.ZookpeerWriteLog(string.Format("使用节点{0}连接zookeeper时出现错误:{1}\n", item.Text, ex.Message), LogerType.Error);
                    continue;
                }
            }
        }

        /// <summary>
        /// 启动统一配置替换
        /// </summary>
        public void Start()
        {
            //初始化ZooKeeper
            InitZooKeeperConfiger(() =>
            {
                double ver = ConfigurationInfo.Version.SerVer;
                double locVer = ConfigurationInfo.Version.LocVer;
                bool isUpdate = CheckVersion();
                if (!isUpdate)//无需更新配置
                {
                    ZookeeperConfigCenterManagerEvents.ZookpeerWriteLog(string.Format("无需更新配置，服务器版本：{0}，本地版本：{1}\n", ver, locVer), LogerType.Info);
                    return;
                }
                ZookeeperConfigCenterManagerEvents.ZookpeerWriteLog(string.Format("检测到配置文件更新，服务器版本：{0},本地版本：{1}\n", ver, locVer), LogerType.Info);
                var sw = new Stopwatch();
                sw.Start();
                ReplaceTemplate();
                RsyncVersion(ver);
                sw.Stop();
                ZookeeperConfigCenterManagerEvents.ZookpeerWriteLog(string.Format("配置文件替换完毕，本次运行耗时：{0}毫秒\n", sw.ElapsedMilliseconds), LogerType.Info);
            });
        }

        /// <summary>
        /// 修改配置暂存集合
        /// </summary>
        static Dictionary<string, string> contents = new Dictionary<string, string>();

        private ZConfiguration configurationInfo;

        /// <summary>
        ///读取zsettings.config配置文件信息
        /// </summary>
        private ZConfiguration ConfigurationInfo
        {
            get
            {
                if (configurationInfo == null)//让该属性只反序列化一次，只验证一次
                {
                    FileStream file = Activator.CreateInstance(typeof(FileStream), new object[] { SettingsPath, FileMode.Open, FileAccess.Read }) as FileStream;
                    configurationInfo = DeserializeXml<ZConfiguration>(file);
                    VerifySettings(configurationInfo);
                }
                return configurationInfo;
            }
        }

        /// <summary>
        /// 验证配置
        /// </summary>
        /// <param name="zConfiguration"></param>
        private void VerifySettings(ZConfiguration zConfiguration)
        {
            try
            {
                if (zConfiguration == null)
                    throw new Exception("读取配置文件出错");
                if (zConfiguration.Node.Groups.Count <= 0)
                    throw new Exception("配置文件中, node节点至少包含一个Group节点");
                /*var Repeat = (from o in zConfiguration.Node.Groups group o by o.Name into g where g.Count() > 1 select g).ToList();
                if (Repeat.Count > 0)
                    throw new Exception(string.Format("配置文件中,Group节点不能包含多个重复的Name属性值:{0}", Repeat[0].Key));*/

                var NoFieldsList = zConfiguration.Node.Groups.Where(o => { return o.Fields.Count <= 0; }).ToList();
                if (NoFieldsList.Count > 0)
                    throw new Exception(string.Format("配置文件中, name={0}的Group节点必须至少包含一个field节点", NoFieldsList[0].Name));

                if (zConfiguration.ZookeeprtServer.ConnectUrls.Count <= 0)
                    throw new Exception("配置文件中, zookeeperServer节点必须至少包含一个url节点");

                var modeMatchCount = zConfiguration.ZookeeprtServer.ConnectUrls.Count(o => { return o.Mode.Trim().ToLower() == ConfigurationInfo.Mode.Trim().ToLower(); });
                if (modeMatchCount <= 0)
                    throw new Exception("配置文件中, configuration节点属性mode:" + zConfiguration.Mode + ",不包含在zookeeperServer节点的url节点mode中");

            }
            catch (Exception e)
            {
                ZookeeperConfigCenterManagerEvents.ZookpeerWriteLog(e.Message,LogerType.Error);
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 私有,检查版本更新并返回最新版本号
        /// </summary>
        /// <param name="ver">服务器版本号</param>
        /// <param name="locVer">本地配置版本号</param>
        /// <returns></returns>
        private bool CheckVersion()
        {
            var conf = ConfigurationInfo;
            //判断本地版本是否为最新
            if (conf.Version.SerVer > conf.Version.LocVer)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 私有,备份当前版本
        /// </summary>
        private void Backup()
        {
            try
            {
                List<string> BackupList = new List<string>();
                var conf = ConfigurationInfo;
                Node node = conf.Node;
                foreach (ZooKeeper.Model.Group group in node.Groups)
                {
                    foreach (Field field in group.Fields)
                    {
                        if (!BackupList.Contains(field.Path) && File.Exists(GetFileInfo(field.Path).FullName))
                        {
                            BackupList.Add(field.Path);//添加至备份列表
                        }
                    }
                }

                //开始备份
                foreach (var item in BackupList)
                {
                    FileInfo finfo = GetFileInfo(item);
                    var newFile = finfo.Name.Replace(finfo.Extension, conf.Version.LocVer + finfo.Extension);
                    var bakPath = finfo.Directory + @"\bak\";
                    if (!Directory.Exists(bakPath))
                        Directory.CreateDirectory(bakPath);
                    File.Copy(finfo.FullName, bakPath + newFile, true);
                }
            }
            catch (Exception e)
            {
                ZookeeperConfigCenterManagerEvents.ZookpeerWriteLog("备份当前版本异常:\r\n" + e.Message + e.StackTrace,LogerType.Error);
            }
        }

        /// <summary>
        ///  私有,替换模版
        /// </summary>
        private void ReplaceTemplate()
        {
            try
            {
                var conf = ConfigurationInfo;
                Node node = conf.Node;
                foreach (ZooKeeper.Model.Group group in node.Groups)
                {
                    foreach (Field field in group.Fields)
                    {
                        if (File.Exists(GetFileInfo(field.Tpl).FullName))
                        {
                            MatchReplace(conf.ZookeeprtServer, node, group, field);
                        }
                    }
                }
                //先备份当前版本，再执行替换
                Backup();
                foreach (string item in contents.Keys)
                {
                    string[] files = item.Split(new char[] { '*' }, StringSplitOptions.RemoveEmptyEntries);

                    File.WriteAllText(GetFileInfo(files[1]).FullName, contents[item]);//根据Zookeeper，生成新的配置文件到指定目录
                }
                contents.Clear();
            }
            catch (Exception e)
            {
                ZookeeperConfigCenterManagerEvents.ZookpeerWriteLog("替换模版异常:\r\n" + e.Message + e.StackTrace,LogerType.Error);
            }
        }

        /// <summary>
        /// 正则匹配并替换
        /// </summary>
        /// <param name="zookeeprtServer">zookeeprtServe配置信息</param>
        /// <param name="node">zookeeprt节点根目录</param>
        /// <param name="group">zookeeprt节点子目录</param>
        /// <param name="field">zookeeprt节点Key</param>
        private void MatchReplace(ZookeeprtServer zookeeprtServer, Node node, ZooKeeper.Model.Group group, Field field)
        {
            try
            {
                string dirKey = field.Tpl + "*" + field.Path;//生成字典Key
                string tplContent = "";
                if (string.IsNullOrEmpty(tplContent) && !contents.ContainsKey(dirKey))
                {
                    tplContent = LoadFileContent(field.Tpl);//加载模版文件内容
                    contents.Add(dirKey, tplContent);//添加内容到字典
                }
                tplContent = contents[dirKey];

                var sw = new Stopwatch();//监测Zookeeper查询Path节点耗时
                sw.Start();
                string value = GetZookeeperField(zookeeprtServer, node, group, field);
                sw.Stop();
                ZookeeperConfigCenterManagerEvents.ZookpeerWriteLog(string.Format("Zookeeper查询完毕，查询用时：{0}毫秒", (sw.ElapsedMilliseconds)),LogerType.Info);
                contents[dirKey] = Regex.Replace(tplContent, @"\${" + field.Name.Trim() + @"\}", value);//替换配置文件内容
            }
            catch (Exception e)
            {
                ZookeeperConfigCenterManagerEvents.ZookpeerWriteLog("正则匹配并替换异常:\r\n" + e.Message + e.StackTrace,LogerType.Error);
            }
        }

        /// <summary>
        /// 同步版本号与服务器保持一致
        /// </summary>
        /// <param name="ver">服务器版本号</param>
        private void RsyncVersion(double ver)
        {
            try
            {
                var conf = ConfigurationInfo;
                conf.Version.LocVer = ver;
                SerializerXml(conf, SettingsPath);
            }
            catch (Exception e)
            {
                ZookeeperConfigCenterManagerEvents.ZookpeerWriteLog("更新本地版本号与redis保持一致异常:\r\n" + e.Message + e.StackTrace,LogerType.Error);
            }
        }

        /// <summary>
        /// 私有,初始化Zookeeper，根据path和版本查询value
        /// </summary>
        /// <param name="zookeeprtServe">zookeeprtServe配置信息</param>
        /// <param name="node">zookeeprt节点根目录</param>
        /// <param name="groupr">zookeeprt节点子目录</param>
        /// <param name="field">zookeeprt节点Key</param>
        /// <returns></returns>
        private string GetZookeeperField(ZookeeprtServer zookeeprtServe, Node node, ZooKeeper.Model.Group group, Field field)
        {
            //请求Zookpeer前事件
            zookeeperConfigCenterManagerEvents.RequestZookpeerBefor(zookeeprtServe, node, group, field);
            string path = node.BaseUrl + group.Name + "/" + field.Name;//组合Zookpeer路径
            try
            {
                string result = "";
                result = configer.GetStringData(path);//Zookpeer中根据path查询value
                //请求Zookpeer后事件
                zookeeperConfigCenterManagerEvents.RequestZookpeerAfter(result);
                return result;
            }
            catch (Exception e)
            {
                zookeeperConfigCenterManagerEvents.RequestZookpeerError(e);
                ZookeeperConfigCenterManagerEvents.ZookpeerWriteLog("读取Zookeeper异常:\r\n" + e.Message + e.StackTrace+"\n"+ path,LogerType.Error);
                return "";
            }
        }

        /// <summary>
        /// 私有,序列化对象，并保存到文件
        /// </summary>
        /// <typeparam name="T">序列化的泛型类型</typeparam>
        /// <param name="obj">序列化的泛型对象</param>
        /// <param name="savePath">序列化后保存的文件位置</param>
        private void SerializerXml<T>(T obj, string savePath)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                var memoryStream = new MemoryStream();
                var streamWriter = new StreamWriter(memoryStream, System.Text.Encoding.UTF8);
                XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                namespaces.Add(string.Empty, string.Empty);
                serializer.Serialize(streamWriter, obj, namespaces);

                memoryStream.Seek(0, SeekOrigin.Begin);
                var streamReader = new StreamReader(memoryStream, System.Text.Encoding.UTF8);
                var utf8EncodedXml = streamReader.ReadToEnd();

                memoryStream.Close();
                streamWriter.Close();
                streamReader.Close();

                File.WriteAllText(savePath, utf8EncodedXml.ToString());
            }
            catch (Exception e)
            {
                ZookeeperConfigCenterManagerEvents.ZookpeerWriteLog("序列化对象异常:\r\n" + e.Message + e.StackTrace,LogerType.Error);
            }

        }

        /// <summary>
        /// 私有,反序列化对象
        /// </summary>
        /// <typeparam name="T">反序列化的泛型类型</typeparam>
        /// <param name="fileStream">反序列化的字节流</param>
        /// <returns></returns>
        private T DeserializeXml<T>(FileStream fileStream)
        {
            try
            {
                XmlSerializer xmlSearializer = new XmlSerializer(typeof(T));
                T info = (T)xmlSearializer.Deserialize(fileStream);
                fileStream.Close();
                return info;
            }
            catch (Exception e)
            {
                ZookeeperConfigCenterManagerEvents.ZookpeerWriteLog("反序列化对象异常:\r\n" + e.Message + e.StackTrace,LogerType.Error);
                return default(T);
            }
        }

        /// <summary>
        /// 获取文件信息
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <returns></returns>
        private static FileInfo GetFileInfo(string fileName)
        {
            return new FileInfo(AppDomain.CurrentDomain.BaseDirectory + fileName);
        }

        /// <summary>
        /// 私有,加载文件内容
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        private string LoadFileContent(string filePath)
        {
            try
            {
                filePath = GetFileInfo(filePath).FullName;
                if (!File.Exists(filePath))
                    return "";
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        string content = sr.ReadToEnd();
                        return content;
                    }
                }
            }
            catch (Exception e)
            {
                ZookeeperConfigCenterManagerEvents.ZookpeerWriteLog("加载文件内容异常:\r\n" + e.Message + e.StackTrace,LogerType.Error);
                return "";
            }
        }
    }
    public enum LogerType
    {
        Error = 0,
        Info = 1,
        Debug = 2,
        Fatal = 3,
        Warn = 4
    }
}
