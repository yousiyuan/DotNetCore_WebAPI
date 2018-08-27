using SmartSql.Abstractions.DbSession;
using System;
using System.Collections.Generic;
using System.Text;
using ZQOMS.Core20.Repository.Abstractions;

namespace ZQOMS.Core20.Repository
{
    public abstract class UnitOfWork<TEntity> : IUnitOfWork<TEntity> where TEntity : class, new()
    {
        private readonly IDbContext ctx;//非托管资源
        private GenericRepository<TEntity> _repository;//非托管资源

        public UnitOfWork(string dbConfig)
        {
            ctx = Activator.CreateInstance(typeof(DbContext), dbConfig) as IDbContext;
        }

        public IGenericRepository<TEntity> Repository
        {
            get
            {
                if (_repository == null)
                {
                    _repository = new GenericRepository<TEntity>(ctx);
                }
                return _repository;
            }
        }

        #region UnitOfWork事务操作
        private bool _isCommitted = false;
        public bool IsCommited
        {
            get { return _isCommitted; }
            set { _isCommitted = value; }
        }

        public IDbConnectionSession BeginTransaction()
        {
            return ctx.BeginTransaction();
        }

        public void Commit()
        {
            ctx.CommitTransaction();
        }

        public void RollBack()
        {
            ctx.RollbackTransaction();
        }
        #endregion

        public void Dispose()
        {
            //释放托管和非托管资源
            Dispose(true);
            //将对象从垃圾回收器链表中移除，从而在垃圾回收器工作时，只释放托管资源，而不执行此对象的析构函数
            //保证不会重复释放(已经被释放的)资源
            GC.SuppressFinalize(this);
        }

        private bool _isDisposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    // Release managed resources
                }
                // Release unmanaged resources
                if (ctx != null) ctx.Dispose();
                if (_repository != null) _repository.Dispose();

                this._isDisposed = true;
            }
        }
        ~UnitOfWork()
        {
            // Release unmanaged resources
            Dispose(false);
        }
    }

    public abstract class UnitOfWork : IUnitOfWork
    {
        private readonly IDbContext ctx;//非托管资源
        private GenericRepository _repository;//非托管资源

        public UnitOfWork(string dbConfig)
        {
            ctx = Activator.CreateInstance(typeof(DbContext), dbConfig) as IDbContext;
        }

        public IGenericRepository Repository
        {
            get
            {
                if (_repository == null)
                {
                    _repository = new GenericRepository(ctx);
                }
                return _repository;
            }
        }

        #region UnitOfWork事务操作
        private bool _isCommitted = false;
        public bool IsCommited
        {
            get { return _isCommitted; }
            set { _isCommitted = value; }
        }

        public IDbConnectionSession BeginTransaction()
        {
            return ctx.BeginTransaction();
        }

        public void Commit()
        {
            ctx.CommitTransaction();
        }

        public void RollBack()
        {
            ctx.RollbackTransaction();
        }
        #endregion

        public void Dispose()
        {
            //释放托管和非托管资源
            Dispose(true);
            //将对象从垃圾回收器链表中移除，从而在垃圾回收器工作时，只释放托管资源，而不执行此对象的析构函数
            //保证不会重复释放(已经被释放的)资源
            GC.SuppressFinalize(this);
        }

        private bool _isDisposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    // Release managed resources
                }
                // Release unmanaged resources
                if (ctx != null) ctx.Dispose();
                if (_repository != null) _repository.Dispose();

                this._isDisposed = true;
            }
        }
        ~UnitOfWork()
        {
            // Release unmanaged resources
            Dispose(false);
        }
    }
}
