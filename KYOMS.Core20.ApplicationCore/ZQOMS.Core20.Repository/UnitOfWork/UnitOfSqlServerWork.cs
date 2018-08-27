using System;
using System.Collections.Generic;
using System.Text;
using ZQOMS.Core20.Repository;

namespace ZQOMS.Core20.Repository
{
    public class UnitOfSqlServerWork<TEntity> : UnitOfWork<TEntity> where TEntity : class, new()
    {
        public UnitOfSqlServerWork(string dbConfig) : base(dbConfig)
        {
        }
    }
    public class UnitOfSqlServerWork : UnitOfWork
    {
        public UnitOfSqlServerWork(string dbConfig) : base(dbConfig)
        {
        }
    }
}
