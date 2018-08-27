using StackExchange.Redis;
using StackExchange.Redis.Extensions.Jil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace KYOMS.Core20.Common.Utility
{
    public class RedisUtility
    {
        private ConnectionMultiplexer redis;
        private JilSerializer jil = new JilSerializer();
        private static RedisUtility instance;
        private static object _lock = new object();
        private static int expirationTime = 60 * 15;
        /// <summary>
        /// 用于单例模式获取RedisUtility类的实例，一遍读写redis
        /// </summary>
        /// <returns>返回值</returns>
        public static RedisUtility GetInstance(string configuration, int expirationTime = -1)
        {
            if (instance == null)
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new RedisUtility(configuration);
                        if (expirationTime > 0)
                        {
                            RedisUtility.expirationTime = expirationTime;
                        }
                    }
                }
            }
            return instance;
        }
        private RedisUtility(string configuration)
        {
            string[] EndPoint = configuration.Split(',').Where(a => a.Contains(".") && a.Contains(":")).Select(a => a.Trim()).ToArray();
            var options = new ConfigurationOptions
            {

                Proxy = Proxy.Twemproxy
            };
            foreach (string item in EndPoint)
            {
                try
                {
                    options.EndPoints.Add(item);
                }
                catch (Exception oe)
                {
                    string err = oe.Message + oe.StackTrace;
                }
            }
            redis = ConnectionMultiplexer.Connect(options);
            //redis = ConnectionMultiplexer.Connect(configuration);
        }



        /// <summary>
        /// 用于redis直接操作中的对象存入redis哈希类型过程中从真实对象转化为系统接口所需类型
        /// </summary>
        /// <returns>返回值</returns>
        public HashEntry[] ToHashEntries(object obj)
        {
            PropertyInfo[] properties = obj.GetType().GetProperties();
            return properties.Select(property => new HashEntry(property.Name, jil.Serialize(property.GetValue(obj)))).ToArray();
        }

        /// <summary>
        /// 用于redis直接取出的哈希类型过程中从系统接口类型转化为真实对象
        /// </summary>
        /// <returns>返回值</returns>
        public T ConvertFromRedis<T>(HashEntry[] hashEntries)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            var obj = Activator.CreateInstance(typeof(T));
            foreach (var property in properties)
            {
                HashEntry entry = hashEntries.FirstOrDefault(g => g.Name.ToString().Equals(property.Name));
                if (entry.Equals(new HashEntry())) continue;
                property.SetValue(obj, Convert.ChangeType(Jil.JSON.Deserialize((string)entry.Value, property.PropertyType), property.PropertyType));
            }
            return (T)obj;
        }


        /// <summary>
        /// 直接操作redis，非高级人员禁用
        /// </summary>
        /// <returns>返回值</returns>
        public IDatabase GetTrueRedisIDatabase(int databaseNumber = -1, object asyncState = null)
        {
            return redis.GetDatabase(databaseNumber, asyncState);
        }



        /// <summary>
        /// 基数类型，快速获取基数，比如数据集 {1, 3, 5, 7, 5, 7, 8}， 那么这个数据集的基数集为 {1, 3, 5 ,7, 8}, 基数(不重复元素)为5。 基数估计就是在误差可接受的范围内，快速计算基数。
        /// HyperLogLogLengthAsync是基数类型集合里的元素数量，即基数。
        /// </summary>
        /// <returns>返回值</returns>
        public long HyperLogLogLength<T>(string[] key, int databaseNumber = -1, object asyncState = null)
        {
            try
            {
                IDatabase db = redis.GetDatabase(databaseNumber, asyncState);
                return db.HyperLogLogLength(key.Select(a => (RedisKey)a).ToArray());
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// 基数类型，快速获取基数，比如数据集 {1, 3, 5, 7, 5, 7, 8}， 那么这个数据集的基数集为 {1, 3, 5 ,7, 8}, 基数(不重复元素)为5。 基数估计就是在误差可接受的范围内，快速计算基数。
        /// HyperLogLogMergeAsync是合并几个基数类型集合。
        /// </summary>
        /// <returns>返回值</returns>
        public async Task<bool> HyperLogLogMergeAsync<T>(string keyNew, string[] keyOlds, int databaseNumber = -1, object asyncState = null)
        {
            bool returnValue = false;
            try
            {
                IDatabase db = redis.GetDatabase(databaseNumber, asyncState);
                await db.HyperLogLogMergeAsync(keyNew, keyOlds.Select(a => (RedisKey)a).ToArray());
                returnValue = true;
            }
            catch { }
            return returnValue;
        }

        /// <summary>
        /// 基数类型，快速获取基数，比如数据集 {1, 3, 5, 7, 5, 7, 8}， 那么这个数据集的基数集为 {1, 3, 5 ,7, 8}, 基数(不重复元素)为5。 基数估计就是在误差可接受的范围内，快速计算基数。
        /// HyperLogLogMergeAsync是合并几个基数类型集合。
        /// </summary>
        /// <returns>返回值</returns>
        public async Task<bool> HyperLogLogMergeAsync<T>(string keyNew, string keyOld1, string keyOld2, int databaseNumber = -1, object asyncState = null)
        {
            bool returnValue = false;
            try
            {
                IDatabase db = redis.GetDatabase(databaseNumber, asyncState);
                await db.HyperLogLogMergeAsync(keyNew, keyOld1, keyOld2);
                returnValue = true;
            }
            catch { }
            return returnValue;
        }






        /// <summary>
        /// 基数类型，快速获取基数，比如数据集 {1, 3, 5, 7, 5, 7, 8}， 那么这个数据集的基数集为 {1, 3, 5 ,7, 8}, 基数(不重复元素)为5。 基数估计就是在误差可接受的范围内，快速计算基数。
        /// HyperLogLogLengthAsync是基数类型集合里的元素数量，即基数。
        /// </summary>
        /// <returns>返回值</returns>
        public async Task<long> HyperLogLogLengthAsync<T>(string[] key, int databaseNumber = -1, object asyncState = null)
        {
            try
            {
                IDatabase db = redis.GetDatabase(databaseNumber, asyncState);
                return await db.HyperLogLogLengthAsync(key.Select(a => (RedisKey)a).ToArray());
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// 基数类型，快速获取基数，比如数据集 {1, 3, 5, 7, 5, 7, 8}， 那么这个数据集的基数集为 {1, 3, 5 ,7, 8}, 基数(不重复元素)为5。 基数估计就是在误差可接受的范围内，快速计算基数。
        /// HyperLogLogLength是基数类型集合里的元素数量，即基数。
        /// </summary>
        /// <returns>返回值</returns>
        public long HyperLogLogLength<T>(string key, int databaseNumber = -1, object asyncState = null)
        {
            try
            {
                IDatabase db = redis.GetDatabase(databaseNumber, asyncState);
                return db.HyperLogLogLength(key);
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// 基数类型，快速获取基数，比如数据集 {1, 3, 5, 7, 5, 7, 8}， 那么这个数据集的基数集为 {1, 3, 5 ,7, 8}, 基数(不重复元素)为5。 基数估计就是在误差可接受的范围内，快速计算基数。
        /// HyperLogLogLengthAsync是基数类型集合里的元素数量，即基数。
        /// </summary>
        /// <returns>返回值</returns>
        public async Task<long> HyperLogLogLengthAsync<T>(string key, int databaseNumber = -1, object asyncState = null)
        {
            try
            {
                IDatabase db = redis.GetDatabase(databaseNumber, asyncState);
                return await db.HyperLogLogLengthAsync(key);
            }
            catch
            {
                return -1;
            }
        }


        /// <summary>
        /// 基数类型，快速获取基数，比如数据集 {1, 3, 5, 7, 5, 7, 8}， 那么这个数据集的基数集为 {1, 3, 5 ,7, 8}, 基数(不重复元素)为5。 基数估计就是在误差可接受的范围内，快速计算基数。
        /// HyperLogLogAdd是在基数类型里增加一个元素，如果在原来的基数集里已经有了，不会改变返回的基数，如果没有则会造成基数+1的结果。
        /// </summary>
        /// <returns>返回值</returns>
        public bool HyperLogLogAdd<T>(string key, T[] t, int databaseNumber = -1, object asyncState = null)
        {
            try
            {
                IDatabase db = redis.GetDatabase(databaseNumber, asyncState);
                var ts = t.Select(a => (RedisValue)jil.Serialize(a)).ToArray();
                return db.HyperLogLogAdd(key, ts);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 基数类型，快速获取基数，比如数据集 {1, 3, 5, 7, 5, 7, 8}， 那么这个数据集的基数集为 {1, 3, 5 ,7, 8}, 基数(不重复元素)为5。 基数估计就是在误差可接受的范围内，快速计算基数。
        /// HyperLogLogAdd是在基数类型里增加一个元素，如果在原来的基数集里已经有了，不会改变返回的基数，如果没有则会造成基数+1的结果。
        /// </summary>
        /// <returns>返回值</returns>
        public async Task<bool> HyperLogLogAddAsync<T>(string key, T[] t, int databaseNumber = -1, object asyncState = null)
        {
            try
            {
                IDatabase db = redis.GetDatabase(databaseNumber, asyncState);
                var ts = t.Select(async a => await jil.SerializeAsync(a)).ToArray();
                return await db.HyperLogLogAddAsync(key, await jil.SerializeAsync(t));
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// 基数类型，快速获取基数，比如数据集 {1, 3, 5, 7, 5, 7, 8}， 那么这个数据集的基数集为 {1, 3, 5 ,7, 8}, 基数(不重复元素)为5。 基数估计就是在误差可接受的范围内，快速计算基数。
        /// HyperLogLogAdd是在基数类型里增加一个元素，如果在原来的基数集里已经有了，不会改变返回的基数，如果没有则会造成基数+1的结果。
        /// </summary>
        /// <returns>返回值</returns>
        public bool HyperLogLogAdd<T>(string key, T t, int databaseNumber = -1, object asyncState = null)
        {
            try
            {
                IDatabase db = redis.GetDatabase(databaseNumber, asyncState);
                return db.HyperLogLogAdd(key, jil.Serialize(t));
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 基数类型，快速获取基数，比如数据集 {1, 3, 5, 7, 5, 7, 8}， 那么这个数据集的基数集为 {1, 3, 5 ,7, 8}, 基数(不重复元素)为5。 基数估计就是在误差可接受的范围内，快速计算基数。
        /// HyperLogLogAdd是在基数类型里增加一个元素，如果在原来的基数集里已经有了，不会改变返回的基数，如果没有则会造成基数+1的结果。
        /// </summary>
        /// <returns>返回值</returns>
        public async Task<bool> HyperLogLogAddAsync<T>(string key, T t, int databaseNumber = -1, object asyncState = null)
        {
            try
            {
                IDatabase db = redis.GetDatabase(databaseNumber, asyncState);
                return await db.HyperLogLogAddAsync(key, await jil.SerializeAsync(t));
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 哈希类型，可以当做字典类型，也可以直接用来存储对象
        /// 用来返回该哈希类型的所有字段的value内容的数组
        /// </summary>
        /// <returns>返回值</returns>
        public string[] HashValues(string key, int databaseNumber = -1, object asyncState = null)
        {
            try
            {
                IDatabase db = redis.GetDatabase(databaseNumber, asyncState);
                return ((db.HashValues(key))).Select(a => (string)a).ToArray();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 哈希类型，可以当做字典类型，也可以直接用来存储对象
        /// 用来返回该哈希类型的所有字段的value内容的数组
        /// </summary>
        /// <returns>返回值</returns>
        public async Task<string[]> HashValuesAsync(string key, int databaseNumber = -1, object asyncState = null)
        {
            try
            {
                IDatabase db = redis.GetDatabase(databaseNumber, asyncState);
                return (await (db.HashValuesAsync(key))).Select(a => (string)a).ToArray();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 哈希类型，可以当做字典类型，也可以直接用来存储对象
        /// 用来返回该哈希类型的所有字段的名称数组
        /// </summary>
        /// <returns>返回值</returns>
        public string[] HashKeys(string key, int databaseNumber = -1, object asyncState = null)
        {
            try
            {
                IDatabase db = redis.GetDatabase(databaseNumber, asyncState);
                return ((db.HashKeys(key))).Select(a => (string)a).ToArray();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 哈希类型，可以当做字典类型，也可以直接用来存储对象
        /// 用来返回该哈希类型的所有字段的名称数组
        /// </summary>
        /// <returns>返回值</returns>
        public async Task<string[]> HashKeysAsync(string key, int databaseNumber = -1, object asyncState = null)
        {
            try
            {
                IDatabase db = redis.GetDatabase(databaseNumber, asyncState);
                return (await (db.HashKeysAsync(key))).Select(a => (string)a).ToArray();
            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        /// 哈希类型，可以当做字典类型，也可以直接用来存储对象
        /// HashLength用来返回该哈希类型的字段数量
        /// </summary>
        /// <returns>返回值</returns>
        public long HashLength(string key, int databaseNumber = -1, object asyncState = null)
        {
            try
            {
                IDatabase db = redis.GetDatabase(databaseNumber, asyncState);
                return db.HashLength(key);
            }
            catch
            {
                return -1;
            }
        }
        /// <summary>
        /// 哈希类型，可以当做字典类型，也可以直接用来存储对象
        /// HashLength用来返回该哈希类型的字段数量
        /// </summary>
        /// <returns>返回值</returns>
        public async Task<long> HashLengthAsync(string key, int databaseNumber = -1, object asyncState = null)
        {
            try
            {
                IDatabase db = redis.GetDatabase(databaseNumber, asyncState);
                return await db.HashLengthAsync(key);
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// 哈希类型，可以当做字典类型，也可以直接用来存储对象
        /// HashExists用来检查某个field是否在该哈希类型里存在
        /// </summary>
        /// <returns>返回值</returns>
        public bool HashExists(string key, string field, int databaseNumber = -1, object asyncState = null)
        {
            try
            {
                IDatabase db = redis.GetDatabase(databaseNumber, asyncState);
                return db.HashExists(key, field);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 哈希类型，可以当做字典类型，也可以直接用来存储对象
        /// HashExists用来检查某个field是否在该哈希类型里存在
        /// </summary>
        /// <returns>返回值</returns>
        public async Task<bool> HashExistsAsync(string key, string field, int databaseNumber = -1, object asyncState = null)
        {
            try
            {
                IDatabase db = redis.GetDatabase(databaseNumber, asyncState);
                return await db.HashExistsAsync(key, field);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 哈希类型，可以当做字典类型，也可以直接用来存储对象
        /// HashDelete<T>带field用于删除一个哈希类型内第二级别的key，也就是删除一个字段field
        /// </summary>
        /// <returns>返回值</returns>
        public bool HashDelete(string key, string field, int databaseNumber = -1, object asyncState = null)
        {
            try
            {
                IDatabase db = redis.GetDatabase(databaseNumber, asyncState);
                return db.HashDelete(key, field);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 哈希类型，可以当做字典类型，也可以直接用来存储对象
        /// HashDelete<T>带field用于删除一个哈希类型内第二级别的key，也就是删除一个字段field
        /// </summary>
        /// <returns>返回值</returns>
        public async Task<bool> HashDeleteAsync(string key, string field, int databaseNumber = -1, object asyncState = null)
        {
            try
            {
                IDatabase db = redis.GetDatabase(databaseNumber, asyncState);
                return await db.HashDeleteAsync(key, field);
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// 哈希类型，可以当做字典类型，也可以直接用来存储对象
        /// HashGet<T>带field用于取得一个哈希类型内第二级别的key，也就是修改字段field的内容
        /// </summary>
        /// <returns>返回值</returns>
        public T HashGet<T>(string key, string field, int databaseNumber = -1, object asyncState = null)
        {
            try
            {
                IDatabase db = redis.GetDatabase(databaseNumber, asyncState);
                return jil.Deserialize<T>(db.HashGet(key, field));
            }
            catch
            {
                if (typeof(T).IsPrimitive)
                {
                    throw new ArgumentNullException("相关内容不存在"); // throw
                }
                return default(T);
            }
        }

        /// <summary>
        /// 哈希类型，可以当做字典类型，也可以直接用来存储对象
        /// HashGet<T>带field用于取得一个哈希类型内第二级别的key，也就是修改字段field的内容
        /// </summary>
        /// <returns>返回值</returns>
        public async Task<T> HashGetAsync<T>(string key, string field, int databaseNumber = -1, object asyncState = null)
        {
            try
            {
                IDatabase db = redis.GetDatabase(databaseNumber, asyncState);
                return await jil.DeserializeAsync<T>(await db.HashGetAsync(key, field));
            }
            catch
            {
                if (typeof(T).IsPrimitive)
                {
                    throw new ArgumentNullException("相关内容不存在"); // throw
                }
                return default(T);
            }
        }

        /// <summary>
        /// 哈希类型，可以当做字典类型，也可以直接用来存储对象
        /// HashGetAll<T>用于将一个之前以哈希类型存在redis的对象完整取回来，不适合大对象
        /// </summary>
        /// <returns>返回值</returns>
        public T HashGetAll<T>(string key, int databaseNumber = -1, object asyncState = null)
        {
            try
            {
                IDatabase db = redis.GetDatabase(databaseNumber, asyncState);
                return ConvertFromRedis<T>(db.HashGetAll(key));
            }
            catch
            {
                if (typeof(T).IsPrimitive)
                {
                    throw new ArgumentNullException("相关内容不存在"); // throw
                }
                return default(T);
            }
        }

        /// <summary>
        /// 哈希类型，可以当做字典类型，也可以直接用来存储对象
        /// HashGetAll<T>用于将一个之前以哈希类型存在redis的对象完整取回来，不适合大对象
        /// </summary>
        /// <returns>返回值</returns>
        public async Task<T> HashGetAllAsync<T>(string key, int databaseNumber = -1, object asyncState = null)
        {
            try
            {
                IDatabase db = redis.GetDatabase(databaseNumber, asyncState);
                return ConvertFromRedis<T>(await db.HashGetAllAsync(key));
            }
            catch
            {
                if (typeof(T).IsPrimitive)
                {
                    throw new ArgumentNullException("相关内容不存在"); // throw
                }
                return default(T);
            }
        }

        /// <summary>
        /// 哈希类型，可以当做字典类型，也可以直接用来存储对象
        /// HashSet<T>带field用于修改一个哈希类型内第二级别的key，也就是修改字段的内容
        /// </summary>
        /// <returns>返回值</returns>
        public bool HashSet<T>(string key, string field, T t, int databaseNumber = -1, object asyncState = null)
        {
            try
            {
                IDatabase db = redis.GetDatabase(databaseNumber, asyncState);
                return db.HashSet(key, field, jil.Serialize(t));
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 哈希类型，可以当做字典类型，也可以直接用来存储对象
        /// HashSet<T>带field用于修改一个哈希类型内第二级别的key，也就是修改字段的内容
        /// </summary>
        /// <returns>返回值</returns>
        public async Task<bool> HashSetAsync<T>(string key, string field, T t, int databaseNumber = -1, object asyncState = null)
        {
            try
            {
                IDatabase db = redis.GetDatabase(databaseNumber, asyncState);
                return await db.HashSetAsync(key, field, await jil.SerializeAsync(t));
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 哈希类型，可以当做字典类型，也可以直接用来存储对象
        /// HashSet<T>用于将一个对象存为一个哈希类型
        /// </summary>
        /// <returns>返回值</returns>
        public bool HashSet<T>(string key, T t, int databaseNumber = -1, object asyncState = null)
        {
            try
            {
                IDatabase db = redis.GetDatabase(databaseNumber, asyncState);
                db.HashSet((RedisKey)key, ToHashEntries(t));
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 哈希类型，可以当做字典类型，也可以直接用来存储对象
        /// HashSet<T>用于将一个对象存为一个哈希类型
        /// </summary>
        /// <returns>返回值</returns>
        public async Task<bool> HashSetAsync<T>(string key, T t, int databaseNumber = -1, object asyncState = null)
        {
            try
            {
                IDatabase db = redis.GetDatabase(databaseNumber, asyncState);
                await db.HashSetAsync((RedisKey)key, ToHashEntries(t));
                return true;
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// 批量出队
        /// </summary>
        /// <returns>返回值</returns>
        public T[] DeQueueRange<T>(string key, long length = 1, int databaseNumber = -1, object asyncState = null)
        {
            try
            {
                if (length < 1)
                {
                    length = 1;
                }
                IDatabase db = redis.GetDatabase(databaseNumber, asyncState);
                var v = db.ListRange(key, 0, length);
                var list = new List<T>();
                foreach (var item in v)
                {
                    var tmp = jil.Deserialize<T>(item);
                    if (!list.Contains(tmp))
                    {
                        list.Add(tmp);
                    }
                }
                return list.ToArray();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 批量出队
        /// </summary>
        /// <returns>返回值</returns>
        public async Task<T[]> DeQueueRangeAsync<T>(string key, long length = 1, int databaseNumber = -1, object asyncState = null)
        {
            try
            {
                if (length < 1)
                {
                    length = 1;
                }
                IDatabase db = redis.GetDatabase(databaseNumber, asyncState);
                var v = await db.ListRangeAsync(key, 0, length);
                var list = new List<T>();
                foreach (var item in v)
                {
                    var tmp = await jil.DeserializeAsync<T>(item);
                    if (!list.Contains(tmp))
                    {
                        list.Add(tmp);
                    }
                }
                return list.ToArray();
            }
            catch
            {
                return null;
            }
        }



        /// <summary>
        /// 获取当前瞬时某队列的长度
        /// </summary>
        /// <returns>返回值</returns>
        public long ListLength(string key, int databaseNumber = -1, object asyncState = null)
        {
            try
            {
                IDatabase db = redis.GetDatabase(databaseNumber, asyncState);
                return db.ListLength(key);
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// 获取当前瞬时某队列的长度
        /// </summary>
        /// <returns>返回值</returns>
        public async Task<long> ListLengthAsync(string key, int databaseNumber = -1, object asyncState = null)
        {
            try
            {
                IDatabase db = redis.GetDatabase(databaseNumber, asyncState);
                return await db.ListLengthAsync(key);
            }
            catch
            {
                return -1;
            }
        }


        /// <summary>
        /// 单项目出队
        /// </summary>
        /// <returns>返回值</returns>
        public T DeQueue<T>(string key, int databaseNumber = -1, object asyncState = null)
        {
            try
            {
                IDatabase db = redis.GetDatabase(databaseNumber, asyncState);
                return jil.Deserialize<T>(db.ListLeftPop(key));
            }
            catch
            {
                if (typeof(T).IsPrimitive)
                {
                    throw new ArgumentNullException("相关内容不存在"); // throw
                }
                return default(T);
            }
        }


        /// <summary>
        /// 单项目出队
        /// </summary>
        /// <returns>返回值</returns>
        public async Task<T> DeQueueAsync<T>(string key, int databaseNumber = -1, object asyncState = null)
        {
            try
            {
                IDatabase db = redis.GetDatabase(databaseNumber, asyncState);
                return await jil.DeserializeAsync<T>(await db.ListLeftPopAsync(key));
            }
            catch
            {
                if (typeof(T).IsPrimitive)
                {
                    throw new ArgumentNullException("相关内容不存在"); // throw
                }
                return default(T);
            }
        }

        /// <summary>
        /// 单项目入队
        /// </summary>
        public long EnQueue<T>(string key, T t, int databaseNumber = -1, object asyncState = null)
        {
            try
            {
                IDatabase db = redis.GetDatabase(databaseNumber, asyncState);
                return db.ListRightPush(key, jil.Serialize(t));
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// 单项目入队
        /// </summary>
        public async Task<long> EnQueueAsync<T>(string key, T t, int databaseNumber = -1, object asyncState = null)
        {
            try
            {
                IDatabase db = redis.GetDatabase(databaseNumber, asyncState);
                return await db.ListRightPushAsync(key, await jil.SerializeAsync(t));
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// 批量入队
        /// </summary>
        /// <returns>返回值</returns>
        public async Task<long> EnQueueAsync<T>(string key, T[] t, int databaseNumber = -1, object asyncState = null)
        {
            try
            {
                IDatabase db = redis.GetDatabase(databaseNumber, asyncState);
                return await db.ListRightPushAsync(key, t.Select(a => (RedisValue)jil.Serialize(a)).ToArray());
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// 批量入队
        /// </summary>
        /// <returns>返回值</returns>
        public long EnQueue<T>(string key, T[] t, int databaseNumber = -1, object asyncState = null)
        {
            try
            {
                IDatabase db = redis.GetDatabase(databaseNumber, asyncState);
                return db.ListRightPush(key, t.Select(a => (RedisValue)jil.Serialize(a)).ToArray());
            }
            catch
            {
                return -1;
            }
        }

        public bool Set<T>(string key, T t, int? expirationTime = null, int databaseNumber = -1, object asyncState = null)
        {
            try
            {
                IDatabase db = redis.GetDatabase(databaseNumber, asyncState);
                bool returnValue = false;
                if (expirationTime == null)
                {
                    returnValue = db.StringSet(key, jil.Serialize(t));
                }
                else
                {
                    if (expirationTime.Value < 1)
                    {
                        returnValue = db.StringSet(key, jil.Serialize(t), TimeSpan.FromSeconds(RedisUtility.expirationTime));
                    }
                    else
                    {
                        returnValue = db.StringSet(key, jil.Serialize(t), TimeSpan.FromSeconds(expirationTime.Value));
                    }
                }

                return returnValue;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public async Task<bool> SetAsync<T>(string key, T t, int? expirationTime = null, int databaseNumber = -1, object asyncState = null)
        {
            try
            {
                bool returnValue = false;
                IDatabase db = redis.GetDatabase(databaseNumber, asyncState);
                if (expirationTime == null)
                {
                    returnValue = await db.StringSetAsync(key, await jil.SerializeAsync(t));
                }
                else
                {
                    if (expirationTime.Value < 1)
                    {
                        returnValue = await db.StringSetAsync(key, await jil.SerializeAsync(t), TimeSpan.FromSeconds(RedisUtility.expirationTime));
                    }
                    else
                    {
                        returnValue = await db.StringSetAsync(key, await jil.SerializeAsync(t), TimeSpan.FromSeconds(expirationTime.Value));
                    }
                }

                return returnValue;
            }
            catch
            {
                return false;
            }
        }

        public bool Set<T>(KeyValuePair<string, T>[] kvps, int databaseNumber = -1, object asyncState = null)
        {
            try
            {
                //var dd = kvps.ToDictionary(k => (RedisKey)k.Key, （v=>v.Value).ToArray();
                IDatabase db = redis.GetDatabase(databaseNumber, asyncState);
                var dict = new Dictionary<RedisKey, RedisValue>();
                for (long i = 0; i < kvps.LongLength; i++)
                {
                    dict.Add(kvps[i].Key, jil.Serialize(kvps[i].Value));
                }
                return db.StringSet(dict.ToArray());
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> SetAsync<T>(KeyValuePair<string, T>[] kvps, int databaseNumber = -1, object asyncState = null)
        {
            try
            {
                //var dd = kvps.ToDictionary(k => (RedisKey)k.Key, （v=>v.Value).ToArray();
                IDatabase db = redis.GetDatabase(databaseNumber, asyncState);
                var dict = new Dictionary<RedisKey, RedisValue>();
                for (long i = 0; i < kvps.LongLength; i++)
                {
                    dict.Add(kvps[i].Key, await jil.SerializeAsync(kvps[i].Value));
                }
                return await db.StringSetAsync(dict.ToArray());
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> SetAsync<T>(IDictionary<string, T> kvps, int databaseNumber = -1, object asyncState = null)
        {
            try
            {
                IDatabase db = redis.GetDatabase(databaseNumber, asyncState);
                var dict = new Dictionary<RedisKey, RedisValue>();
                foreach (var item in kvps)
                {
                    dict.Add(item.Key, await jil.SerializeAsync(item.Value));
                }
                return await db.StringSetAsync(dict.ToArray());
            }
            catch
            {
                return false;
            }
        }
        public bool Set<T>(IDictionary<string, T> kvps, int databaseNumber = -1, object asyncState = null)
        {
            try
            {
                IDatabase db = redis.GetDatabase(databaseNumber, asyncState);
                var dict = new Dictionary<RedisKey, RedisValue>();
                foreach (var item in kvps)
                {
                    dict.Add(item.Key, jil.Serialize(item.Value));
                }
                return db.StringSet(dict.ToArray());
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 批量送key、value键值对到redis，kvps每一个元素就是一对键值对，获取的适合只能一个个用StringGet或Get<string>获取
        /// </summary>
        /// <param name="kvps">表示要连接的服务器名</param>
        /// <returns>返回值</returns>
        public async Task<bool> StringSetAsync(KeyValuePair<string, string>[] kvps, int databaseNumber = -1, object asyncState = null)
        {
            return await SetAsync<string>(kvps, databaseNumber, asyncState);
        }

        /// <summary>
        /// 批量送key、value键值对到redis，kvps每一个元素就是一对键值对，获取的适合只能一个个用StringGet或Get<string>获取
        /// </summary>
        /// <param name="kvps">表示要连接的服务器名</param>
        /// <returns>返回值</returns>
        public bool StringSet(KeyValuePair<string, string>[] kvps, int databaseNumber = -1, object asyncState = null)
        {
            return Set<string>(kvps, databaseNumber, asyncState);
        }

        /// <summary>
        /// 批量送key、value键值对到redis，kvps每一个元素就是一对键值对，获取的适合只能一个个用StringGet获取或通过Get<string>配合keys批量获取
        /// </summary>
        /// <param name="kvps">表示要连接的服务器名</param>
        /// <returns>返回值</returns>
        public async Task<bool> StringSetAsync(IDictionary<string, string> kvps, int databaseNumber = -1, object asyncState = null)
        {
            return await SetAsync<string>(kvps, databaseNumber, asyncState);
        }

        /// <summary>
        /// 批量送key、value键值对到redis，kvps每一个元素就是一对键值对，获取的适合只能一个个用StringGet获取或通过Get<string>配合keys批量获取
        /// </summary>
        /// <param name="kvps">表示要连接的服务器名</param>
        /// <returns>返回值</returns>
        public bool StringSet(IDictionary<string, string> kvps, int databaseNumber = -1, object asyncState = null)
        {
            return Set<string>(kvps, databaseNumber, asyncState);
        }

        /// <summary>
        /// 向redis存入指定key的对应基本键值对类型的value（value必然都是字符串方式）
        /// </summary>
        /// <param name="key">表示要存的基本类型键值对的key</param>
        /// <param name="value">表示要村的实际内容</param>
        /// <param name="databaseNumber">redis池实例化的是可以有多个节点，以ip：端口号组成，用逗号分隔，databaseNumber默认为-1，databaseNumber小于等于0是第一号节点，1是第二个号节点，依次类推</param>
        /// <returns>返回值</returns>
        public async Task<bool> StringSetAsync(string key, string value, int? expirationTime = null, int databaseNumber = -1, object asyncState = null)
        {
            bool returnValue = false;
            try
            {
                IDatabase db = redis.GetDatabase(databaseNumber, asyncState);
                if (expirationTime == null)
                {
                    returnValue = await db.StringSetAsync(key, value);
                }
                else
                {
                    if (expirationTime.Value < 1)
                    {
                        returnValue = await db.StringSetAsync(key, value, TimeSpan.FromSeconds(RedisUtility.expirationTime));
                    }
                    else
                    {
                        returnValue = await db.StringSetAsync(key, value, TimeSpan.FromSeconds(expirationTime.Value));
                    }
                }
            }
            catch (Exception oe)
            {
                string err = oe.Message;
            }
            return returnValue;
        }

        /// <summary>
        /// 向redis存入指定key的对应基本键值对类型的value（value必然都是字符串方式）
        /// </summary>
        /// <param name="key">表示要存的基本类型键值对的key</param>
        /// <param name="value">表示要村的实际内容</param>
        /// <param name="databaseNumber">redis池实例化的是可以有多个节点，以ip：端口号组成，用逗号分隔，databaseNumber默认为-1，databaseNumber小于等于0是第一号节点，1是第二个号节点，依次类推</param>
        /// <returns>返回值</returns>
        public bool StringSet(string key, string value, int? expirationTime = null, int databaseNumber = -1, object asyncState = null)
        {
            bool returnValue = false;
            try
            {
                IDatabase db = redis.GetDatabase(databaseNumber, asyncState);
                if (expirationTime == null)
                {
                    returnValue = db.StringSet(key, value);
                }
                else
                {
                    if (expirationTime.Value < 1)
                    {
                        returnValue = db.StringSet(key, value, TimeSpan.FromSeconds(RedisUtility.expirationTime));
                    }
                    else
                    {
                        returnValue = db.StringSet(key, value, TimeSpan.FromSeconds(expirationTime.Value));
                    }
                }
            }
            catch
            {
            }
            return returnValue;
        }

        /// <summary>
        /// 从redis获取指定key的对应基本键值对类型的value值（value可以是任意类型）
        /// </summary>
        /// <param name="key">表示要获取的基本类型键值对的key群</param>
        /// <param name="databaseNumber">redis池实例化的是可以有多个节点，以ip：端口号组成，用逗号分隔，databaseNumber默认为-1，databaseNumber小于等于0是第一号节点，1是第二个号节点，依次类推</param>
        /// <returns>返回值</returns>
        public async Task<T> GetAsync<T>(string key, int databaseNumber = -1, object asyncState = null)
        {
            try
            {
                IDatabase db = redis.GetDatabase(databaseNumber, asyncState);
                return jil.Deserialize<T>(await db.StringGetAsync(key));
                //return (T)Convert.ChangeType(jil.Deserialize((await db.StringGetAsync(key))), typeof(T));
            }
            catch
            {
                if (typeof(T).IsPrimitive)
                {
                    throw new ArgumentNullException("相关内容不存在"); // throw
                }
                return default(T);
            }
        }

        /// <summary>
        /// 从redis获取指定key的对应基本键值对类型的value值（value可以是任意类型）
        /// </summary>
        /// <param name="key">表示要获取的基本类型键值对的key群</param>
        /// <param name="databaseNumber">redis池实例化的是可以有多个节点，以ip：端口号组成，用逗号分隔，databaseNumber默认为-1，databaseNumber小于等于0是第一号节点，1是第二个号节点，依次类推</param>
        /// <returns>返回值</returns>
        public T Get<T>(string key, int databaseNumber = -1, object asyncState = null)
        {
            try
            {
                IDatabase db = redis.GetDatabase(databaseNumber, asyncState);
                RedisValue rv = db.StringGet(key);

                return jil.Deserialize<T>(rv);
                //return (T)Convert.ChangeType(jil.Deserialize((await db.StringGetAsync(key))), typeof(T));
            }
            catch (Exception e)
            {
                if (typeof(T).IsPrimitive)
                {
                    throw new ArgumentNullException("相关内容不存在"); // throw
                }
                return default(T);
            }
        }

        /// <summary>
        /// 批量从redis获取指定key群的对应基本键值对类型的value群值（value必然都是字符串方式），返回的value群次序于提供的key群是顺序一致的
        /// </summary>
        /// <param name="keys">表示要获取的基本类型键值对的key群</param>
        /// <param name="databaseNumber">redis池实例化的是可以有多个节点，以ip：端口号组成，用逗号分隔，databaseNumber默认为-1，databaseNumber小于等于0是第一号节点，1是第二个号节点，依次类推</param>
        /// <returns>返回值</returns>
        public async Task<T[]> GetAsync<T>(string[] keys, int databaseNumber = -1, object asyncState = null)
        {
            try
            {
                IDatabase db = redis.GetDatabase(databaseNumber, asyncState);
                return (await db.StringGetAsync(keys.Select(a => (RedisKey)a).ToArray())).Select(a => jil.Deserialize<T>(a)).ToArray();
                //return value.Select(a=>(T)Convert.ChangeType((T)jil.Deserialize(a), typeof(T))).ToArray();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 批量从redis获取指定key群的对应基本键值对类型的value群值（value必然都是字符串方式），返回的value群次序于提供的key群是顺序一致的
        /// </summary>
        /// <param name="keys">表示要获取的基本类型键值对的key群</param>
        /// <param name="databaseNumber">redis池实例化的是可以有多个节点，以ip：端口号组成，用逗号分隔，databaseNumber默认为-1，databaseNumber小于等于0是第一号节点，1是第二个号节点，依次类推</param>
        /// <returns>返回值</returns>
        public T[] Get<T>(string[] keys, int databaseNumber = -1, object asyncState = null)
        {
            try
            {
                IDatabase db = redis.GetDatabase(databaseNumber, asyncState);
                return (db.StringGet(keys.Select(a => (RedisKey)a).ToArray())).Select(a => jil.Deserialize<T>(a)).ToArray();
                //return value.Select(a=>(T)Convert.ChangeType((T)jil.Deserialize(a), typeof(T))).ToArray();
            }
            catch
            {
                return null;
            }
        }



        /// <summary>
        /// 根据key从redis获取对应的基本键值对类型的value值（快捷字符串方式）
        /// </summary>
        /// <param name="key">表示要获取的基本类型键值对的key</param>
        /// <param name="databaseNumber">redis池实例化的是可以有多个节点，以ip：端口号组成，用逗号分隔，databaseNumber默认为-1，databaseNumber小于等于0是第一号节点，1是第二个号节点，依次类推</param>
        /// <returns>返回值</returns>
        public async Task<string> StringGetAsync(string key, int databaseNumber = -1, object asyncState = null)
        {
            try
            {
                IDatabase db = redis.GetDatabase(databaseNumber, asyncState);
                return await db.StringGetAsync(key);
            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        /// 根据key从redis获取对应的基本键值对类型的value值（快捷字符串方式）
        /// </summary>
        /// <param name="key">表示要获取的基本类型键值对的key</param>
        /// <param name="databaseNumber">redis池实例化的是可以有多个节点，以ip：端口号组成，用逗号分隔，databaseNumber默认为-1，databaseNumber小于等于0是第一号节点，1是第二个号节点，依次类推</param>
        /// <returns>返回值</returns>
        public string StringGet(string key, int databaseNumber = -1, object asyncState = null)
        {
            try
            {
                IDatabase db = redis.GetDatabase(databaseNumber, asyncState);
                return db.StringGet(key);
            }
            catch
            {
                return null;
            }
        }
    }

}
