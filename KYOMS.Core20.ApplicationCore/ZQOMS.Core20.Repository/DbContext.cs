using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;
using SmartSql;
using SmartSql.Abstractions;
using SmartSql.Abstractions.Cache;
using SmartSql.Abstractions.Config;
using SmartSql.Abstractions.DataSource;
using SmartSql.Abstractions.DbSession;
using SmartSql.SqlMap;
using ZQOMS.Core20.Repository.Abstractions;

namespace ZQOMS.Core20.Repository
{
    public class DbContext : IDbContext
    {
        private ISmartSqlMapper SqlMapper;//非托管资源
        public DbContext(string smartSqlMapConfigPath = "SmartSqlMapConfig.xml")
        {
            try
            {
                if (SqlMapper == null)
                    SqlMapper = MapperContainer.Instance.GetSqlMapper(smartSqlMapConfigPath);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
        public SmartSqlMapConfig SqlMapConfig => SqlMapper.SqlMapConfig;

        public IDataSourceManager DataSourceManager => SqlMapper.DataSourceManager;

        public ICacheManager CacheManager => SqlMapper.CacheManager;

        public ISqlBuilder SqlBuilder => SqlMapper.SqlBuilder;

        public DbProviderFactory DbProviderFactory => SqlMapper.DbProviderFactory;

        public IDbConnectionSessionStore SessionStore => SqlMapper.SessionStore;

        public IConfigLoader ConfigLoader => SqlMapper.ConfigLoader;

        public IDbConnectionSession BeginSession(DataSourceChoice sourceChoice = DataSourceChoice.Write)
        {
            return SqlMapper.BeginSession(sourceChoice);
        }

        public IDbConnectionSession BeginTransaction()
        {
            return SqlMapper.BeginTransaction();
        }

        public IDbConnectionSession BeginTransaction(IsolationLevel isolationLevel)
        {
            return SqlMapper.BeginTransaction(isolationLevel);
        }

        public void CommitTransaction()
        {
            SqlMapper.CommitTransaction();
        }

        public IDbConnectionSession CreateDbSession(DataSourceChoice commandMethod)
        {
            return SqlMapper.CreateDbSession(commandMethod);
        }

        public void Dispose()
        {
            //释放托管和非托管资源
            Dispose(true);
            //将对象从垃圾回收器链表中移除，从而在垃圾回收器工作时，只释放托管资源，而不执行此对象的析构函数
            GC.SuppressFinalize(this);
        }

        public void EndSession()
        {
            SqlMapper.EndSession();
        }

        public int Execute(RequestContext context)
        {
            return SqlMapper.Execute(context);
        }

        public Task<int> ExecuteAsync(RequestContext context)
        {
            return SqlMapper.ExecuteAsync(context);
        }

        public T ExecuteScalar<T>(RequestContext context)
        {
            return SqlMapper.ExecuteScalar<T>(context);
        }

        public Task<T> ExecuteScalarAsync<T>(RequestContext context)
        {
            return SqlMapper.ExecuteScalarAsync<T>(context);
        }

        public void LoadConfig(SmartSqlMapConfig smartSqlMapConfig)
        {
            SqlMapper.LoadConfig(smartSqlMapConfig);
        }

        public IEnumerable<T> Query<T>(RequestContext context)
        {
            return SqlMapper.Query<T>(context);
        }

        public IEnumerable<T> Query<T>(RequestContext context, DataSourceChoice sourceChoice)
        {
            return SqlMapper.Query<T>(context, sourceChoice);
        }

        public Task<IEnumerable<T>> QueryAsync<T>(RequestContext context)
        {
            return SqlMapper.QueryAsync<T>(context);
        }

        public Task<IEnumerable<T>> QueryAsync<T>(RequestContext context, DataSourceChoice sourceChoice)
        {
            return SqlMapper.QueryAsync<T>(context, sourceChoice);
        }

        public T QuerySingle<T>(RequestContext context)
        {
            return SqlMapper.QuerySingle<T>(context);
        }

        public T QuerySingle<T>(RequestContext context, DataSourceChoice sourceChoice)
        {
            return SqlMapper.QuerySingle<T>(context, sourceChoice);
        }

        public Task<T> QuerySingleAsync<T>(RequestContext context)
        {
            return SqlMapper.QuerySingleAsync<T>(context);
        }

        public Task<T> QuerySingleAsync<T>(RequestContext context, DataSourceChoice sourceChoice)
        {
            return SqlMapper.QuerySingleAsync<T>(context, sourceChoice);
        }

        public void RollbackTransaction()
        {
            SqlMapper.RollbackTransaction();
        }

        #region IDbContext成员
        /// <summary>
        /// 是否已释放资源的标志
        /// </summary>
        private bool isDisposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.isDisposed)
            {
                if (disposing)
                {
                    //释放托管资源
                }
                //释放非托管资源
                SqlMapper.Dispose();
            }
            //标识此对象已释放
            this.isDisposed = true;
        }
        #endregion

        ~DbContext()
        {
            //释放非托管资源
            Dispose(false);
        }
    }
}
