using System;
using System.Collections.Generic;
using System.Text;
using ZQOMS.Core20.Repository;

namespace ZQOMS.Core20.Repository
{
    public class UnitOfMySqlWork<TEntity> : UnitOfWork<TEntity> where TEntity : class, new()
    {
        public UnitOfMySqlWork(string dbConfig) : base(dbConfig)
        {
        }
    }

    public class UnitOfMySqlWork : UnitOfWork
    {
        public UnitOfMySqlWork(string dbConfig) : base(dbConfig)
        {
        }
    }
}
