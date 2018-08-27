
using SmartSql.Abstractions;
using SmartSql.Abstractions.Cache;
using SmartSql.Abstractions.Config;
using SmartSql.Abstractions.DataSource;
using SmartSql.Abstractions.DbSession;
using SmartSql.SqlMap;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace KYOMS.Core20.Common.Database.Interface
{
    public interface ISqlMapper
    {
        SmartSqlMapConfig SqlMapConfig { get; }
        IDataSourceManager DataSourceManager { get; }
        ICacheManager CacheManager { get; }
        ISqlBuilder SqlBuilder { get; }
        DbProviderFactory DbProviderFactory { get; }
        IDbConnectionSessionStore SessionStore { get; }
        IConfigLoader ConfigLoader { get; }
        IDbConnectionSession BeginSession(DataSourceChoice sourceChoice = DataSourceChoice.Write);
        IDbConnectionSession BeginTransaction();
        IDbConnectionSession BeginTransaction(IsolationLevel isolationLevel);
        void CommitTransaction();
        IDbConnectionSession CreateDbSession(DataSourceChoice commandMethod);
        void Dispose();
        void EndSession();
        int Execute(RequestContext context);
        Task<int> ExecuteAsync(RequestContext context);
        T ExecuteScalar<T>(RequestContext context);
        Task<T> ExecuteScalarAsync<T>(RequestContext context);
        void LoadConfig(SmartSqlMapConfig smartSqlMapConfig);
        IEnumerable<T> Query<T>(RequestContext context);
        IEnumerable<T> Query<T>(RequestContext context, DataSourceChoice sourceChoice);
        Task<IEnumerable<T>> QueryAsync<T>(RequestContext context);
        Task<IEnumerable<T>> QueryAsync<T>(RequestContext context, DataSourceChoice sourceChoice);
        T QuerySingle<T>(RequestContext context);
        T QuerySingle<T>(RequestContext context, DataSourceChoice sourceChoice);
        Task<T> QuerySingleAsync<T>(RequestContext context);
        Task<T> QuerySingleAsync<T>(RequestContext context, DataSourceChoice sourceChoice);
        void RollbackTransaction();
    }
}
