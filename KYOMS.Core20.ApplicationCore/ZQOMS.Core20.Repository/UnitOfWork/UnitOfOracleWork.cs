using System;
using System.Collections.Generic;
using System.Text;
using ZQOMS.Core20.Repository;

namespace ZQOMS.Core20.Repository
{
    public class UnitOfOracleWork<TEntity> : UnitOfWork<TEntity> where TEntity : class, new()
    {
        public UnitOfOracleWork(string dbConfig) : base(dbConfig)
        {
        }
    }
    public class UnitOfOracleWork : UnitOfWork
    {
        public UnitOfOracleWork(string dbConfig) : base(dbConfig)
        {
        }
    }
}
