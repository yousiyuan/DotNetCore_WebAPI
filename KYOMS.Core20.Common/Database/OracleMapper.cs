using KYOMS.Core20.Common.Database.Interface;
using Microsoft.Extensions.Logging.Abstractions;
using SmartSql;
using SmartSql.Abstractions;
using SmartSql.Abstractions.Cache;
using SmartSql.Abstractions.Config;
using SmartSql.Abstractions.DataSource;
using SmartSql.Abstractions.DbSession;
using SmartSql.SqlMap;
using SmartSql.ZooKeeperConfig;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace KYOMS.Core20.Common.Database
{
    public class OracleMapper : ISqlMapper
    {
        public ISmartSqlMapper mapper = null;
        private static ISmartSqlMapper MapperInstance = null;
        public OracleMapper()
        {
            try
            {
                string configFile = AppDomain.CurrentDomain.BaseDirectory + (@"config/OracleSmartSqlMap.config");
                if (mapper == null)
                {
                    if (MapperInstance == null)
                    {
                        MapperInstance = MapperContainer.Instance.GetSqlMapper(configFile);
                    }
                    mapper = MapperInstance;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }
        public OracleMapper(string configFile = @"config/OracleSmartSqlMap.config")
        {
            try
            {
                if (string.IsNullOrEmpty(configFile))
                {
                    configFile = AppDomain.CurrentDomain.BaseDirectory + (@"config/SmartSqlMap.config");
                }
                else
                {
                    configFile = AppDomain.CurrentDomain.BaseDirectory + configFile;
                }
                if (mapper == null)
                {
                    if (MapperInstance == null)
                    {
                        MapperInstance = MapperContainer.Instance.GetSqlMapper(configFile);
                    }
                    mapper = MapperInstance;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }
        public SmartSqlMapConfig SqlMapConfig => MapperInstance.SqlMapConfig;

        public IDataSourceManager DataSourceManager => MapperInstance.DataSourceManager;

        public ICacheManager CacheManager => MapperInstance.CacheManager;

        public ISqlBuilder SqlBuilder => MapperInstance.SqlBuilder;

        public DbProviderFactory DbProviderFactory => MapperInstance.DbProviderFactory;

        public IDbConnectionSessionStore SessionStore => MapperInstance.SessionStore;

        public IConfigLoader ConfigLoader => MapperInstance.ConfigLoader;

        public IDbConnectionSession BeginSession(DataSourceChoice sourceChoice = DataSourceChoice.Write)
        {
            return MapperInstance.BeginSession(sourceChoice);
        }

        public IDbConnectionSession BeginTransaction()
        {
            return MapperInstance.BeginTransaction();
        }

        public IDbConnectionSession BeginTransaction(IsolationLevel isolationLevel)
        {
            return MapperInstance.BeginTransaction(isolationLevel);
        }

        public void CommitTransaction()
        {
            MapperInstance.CommitTransaction();
        }

        public IDbConnectionSession CreateDbSession(DataSourceChoice commandMethod)
        {
            return MapperInstance.CreateDbSession(commandMethod);
        }
        public void Dispose()
        {
            MapperInstance.Dispose();
        }

        public void EndSession()
        {
            MapperInstance.EndSession();
        }

        public int Execute(RequestContext context)
        {
            return MapperInstance.Execute(context);
        }

        public Task<int> ExecuteAsync(RequestContext context)
        {
            return MapperInstance.ExecuteAsync(context);
        }

        public T ExecuteScalar<T>(RequestContext context)
        {
            return MapperInstance.ExecuteScalar<T>(context);
        }

        public Task<T> ExecuteScalarAsync<T>(RequestContext context)
        {
            return MapperInstance.ExecuteScalarAsync<T>(context);
        }

        public void LoadConfig(SmartSqlMapConfig smartSqlMapConfig)
        {
            MapperInstance.LoadConfig(smartSqlMapConfig);
        }

        public IEnumerable<T> Query<T>(RequestContext context)
        {
            return MapperInstance.Query<T>(context);
        }

        public IEnumerable<T> Query<T>(RequestContext context, DataSourceChoice sourceChoice)
        {
            return MapperInstance.Query<T>(context, sourceChoice);
        }

        public Task<IEnumerable<T>> QueryAsync<T>(RequestContext context)
        {
            return MapperInstance.QueryAsync<T>(context);
        }

        public Task<IEnumerable<T>> QueryAsync<T>(RequestContext context, DataSourceChoice sourceChoice)
        {
            return MapperInstance.QueryAsync<T>(context, sourceChoice);
        }

        public T QuerySingle<T>(RequestContext context)
        {
            return MapperInstance.QuerySingle<T>(context);
        }

        public T QuerySingle<T>(RequestContext context, DataSourceChoice sourceChoice)
        {
            return MapperInstance.QuerySingle<T>(context, sourceChoice);
        }

        public Task<T> QuerySingleAsync<T>(RequestContext context)
        {
            return MapperInstance.QuerySingleAsync<T>(context);
        }

        public Task<T> QuerySingleAsync<T>(RequestContext context, DataSourceChoice sourceChoice)
        {
            return MapperInstance.QuerySingleAsync<T>(context, sourceChoice);
        }

        public void RollbackTransaction()
        {
            MapperInstance.RollbackTransaction();
        }
    }
}
