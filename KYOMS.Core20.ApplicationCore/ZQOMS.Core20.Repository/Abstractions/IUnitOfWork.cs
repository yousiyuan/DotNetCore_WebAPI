using SmartSql.Abstractions.DbSession;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZQOMS.Core20.Repository.Abstractions
{
    /// <summary>
    /// 工作单元
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// 事务的提交状态
        /// </summary>
        bool IsCommited { get; set; }

        /// <summary>
        /// 开启事务
        /// </summary>
        IDbConnectionSession BeginTransaction();

        /// <summary>
        /// 提交事务
        /// </summary>
        /// <returns></returns>
        void Commit();

        /// <summary>
        /// 回滚事务
        /// </summary>
        void RollBack();

        /// <summary>
        /// 仓储对象
        /// </summary>
        IGenericRepository Repository { get; }
    }

    /// <summary>
    /// 对泛型类型支持的工作单元
    /// </summary>
    public interface IUnitOfWork<T> : IDisposable where T : class, new()
    {
        /// <summary>
        /// 事务的提交状态
        /// </summary>
        bool IsCommited { get; set; }

        /// <summary>
        /// 开启事务
        /// </summary>
        IDbConnectionSession BeginTransaction();

        /// <summary>
        /// 提交事务
        /// </summary>
        /// <returns></returns>
        void Commit();

        /// <summary>
        /// 回滚事务
        /// </summary>
        void RollBack();
        
        /// <summary>
        /// 泛型仓储对象
        /// </summary>
        IGenericRepository<T> Repository { get; }
    }
}
