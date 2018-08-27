using KYOMS.Core20.Common.Database;
using KYOMS.Core20.Common.Database.Interface;
using KYOMS.Core20.Common.Log4NetCore;
using KYOMS.Core20.Common.LogCommon;
using SmartSql.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KYOMS.Core20.Respository.Core
{
    public class BaseRepository<T>
    {
        /*public string ProjectName
        {
            get
            {
                return AppDomain.CurrentDomain.GetData("ProjectName").ToString();
            }
        }
        public int Port
        {
            get
            {
                return Convert.ToInt32(AppDomain.CurrentDomain.GetData("Port"));
            }
        }

        private static readonly Object Locker = new object();

        private static LoggerHandle logger;

        /// <summary>
        /// 日志实例，读取和使用Zookeeper中的默认日志配置
        /// </summary>
        public LoggerHandle Logger
        {
            get
            {
                if (logger == null)
                {
                    lock (Locker)
                    {
                        if (logger == null)
                        {
                            logger = new LoggerHandle(null, $"log//{ProjectName}_{IpAddressHandle.GetLocalIp()}_{Port}_{DateTime.Now:yyyyMMdd}");
                        }
                    }
                }
                return logger;
            }
        }*/

        public readonly ISqlMapper sqlMapper = null;
        public BaseRepository(Type type)
        {
            sqlMapper = Activator.CreateInstance(type) as ISqlMapper;
        }

        public BaseRepository(Type type, string configPath)
        {
            sqlMapper = Activator.CreateInstance(type, configPath) as ISqlMapper;
        }

        #region 同步
        public int Execute(SqlRequestContext sqlRequestContext)
        {
            return sqlMapper.Execute(sqlRequestContext);
        }
        public int Insert(string scope, object request, string sqlid = "Insert")
        {
            return Execute(new SqlRequestContext()
            {
                Scope = scope,
                SqlId = sqlid,
                Request = request
            });
        }
        public int Update(string scope, object request, string sqlid = "Update")
        {
            return Execute(new SqlRequestContext()
            {
                Scope = scope,
                SqlId = sqlid,
                Request = request
            });
        }
        public int Delete(string scope, object request, string sqlid = "Delete")
        {
            return Execute(new SqlRequestContext()
            {
                Scope = scope,
                SqlId = sqlid,
                Request = request
            });
        }
        public T2 QuerySingle<T2>(SqlRequestContext sqlRequestContext)
        {
            return sqlMapper.QuerySingle<T2>(sqlRequestContext);
        }
        public T2 QueryFirst<T2>(string scope, object request, string sqlid = "QueryFirst")
        {
            return QuerySingle<T2>(new SqlRequestContext()
            {
                Scope = scope,
                SqlId = sqlid,
                Request = request
            });
        }
        public IEnumerable<T2> Query<T2>(SqlRequestContext sqlRequestContext)
        {
            return sqlMapper.Query<T2>(sqlRequestContext);
        }
        public IEnumerable<T2> GetList<T2>(string scope, object request, string sqlid = "QueryList")
        {
            return Query<T2>(new SqlRequestContext()
            {
                Scope = scope,
                SqlId = sqlid,
                Request = request
            });
        }
        #endregion

        #region 异步
        public async Task<int> ExecuteAsync(SqlRequestContext sqlRequestContext)
        {
            return await sqlMapper.ExecuteAsync(sqlRequestContext);
        }
        public async Task<int> InsertAsync(string scope, object request, string sqlid = "Insert")
        {
            return await ExecuteAsync(new SqlRequestContext()
            {
                Scope = scope,
                SqlId = sqlid,
                Request = request
            });
        }
        public async Task<int> UpdateAsync(string scope, object request, string sqlid = "Update")
        {
            return await ExecuteAsync(new SqlRequestContext()
            {
                Scope = scope,
                SqlId = sqlid,
                Request = request
            });
        }
        public async Task<int> DeleteAsync(string scope, object request, string sqlid = "Delete")
        {
            return await ExecuteAsync(new SqlRequestContext()
            {
                Scope = scope,
                SqlId = sqlid,
                Request = request
            });
        }
        public async Task<T2> QuerySingleAsync<T2>(SqlRequestContext sqlRequestContext)
        {
            return await sqlMapper.QuerySingleAsync<T2>(sqlRequestContext);
        }
        public async Task<T2> QueryFirstAsync<T2>(string scope, object request, string sqlid = "QueryFirst")
        {
            return await QuerySingleAsync<T2>(new SqlRequestContext()
            {
                Scope = scope,
                SqlId = sqlid,
                Request = request
            });
        }
        public async Task<IEnumerable<T2>> QueryAsync<T2>(SqlRequestContext sqlRequestContext)
        {
            return await sqlMapper.QueryAsync<T2>(sqlRequestContext);
        }
        public async Task<IEnumerable<T2>> GetListAsync<T2>(string scope, object request, string sqlid = "QueryList")
        {
            return await QueryAsync<T2>(new SqlRequestContext()
            {
                Scope = scope,
                SqlId = sqlid,
                Request = request
            });
        }
        #endregion
    }
}
