using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ZQOMS.Core20.Repository.Abstractions
{
    public interface IGenericRepository<T> : IRepository<T> where T : class, new()
    {
        int Insert(string sqlId, object parameters);
        Task<int> InsertAsync(string sqlId, object parameters);
        int Update(string sqlId, object parameters);
        Task<int> UpdateAsync(string sqlId, object parameters);
        int Delete(string sqlId, object parameters);
        Task<int> DeleteAsync(string sqlId, object parameters);
        List<T> FindList(string sqlId, object parameters);
        Task<List<T>> FindListAsync(string sqlId, object parameters);
        T FindById(string sqlId, object parameters);
        Task<T> FindByIdAsync(string sqlId, object parameters);
        int Count(string sqlId, object parameters);
        Task<int> CountAsync(string sqlId, object parameters);
        /// <summary>
        /// SQL分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="totalRecord">总条数</param>
        /// <param name="sqlId">Sql语句</param>
        /// <param name="parameters">筛选条件</param>
        /// <returns>返回分页数据</returns>
        List<T> FindPageList(int pageIndex, int pageSize, int totalRecord, string sqlId, object parameters);
        /// <summary>
        /// SQL分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="totalRecord">总条数</param>
        /// <param name="sqlId">Sql语句</param>
        /// <param name="parameters">筛选条件</param>
        /// <returns>返回分页数据</returns>
        Task<List<T>> FindPageListAsync(int pageIndex, int pageSize, int totalRecord, string sqlId, object parameters);
    }
    public interface IGenericRepository : IRepository
    {
        int Insert<T>(string sqlId, object parameters) where T : class, new();
        Task<int> InsertAsync<T>(string sqlId, object parameters) where T : class, new();
        int Update<T>(string sqlId, object parameters) where T : class, new();
        Task<int> UpdateAsync<T>(string sqlId, object parameters) where T : class, new();
        int Delete<T>(string sqlId, object parameters) where T : class, new();
        Task<int> DeleteAsync<T>(string sqlId, object parameters) where T : class, new();
        List<T> FindList<T>(string sqlId, object parameters) where T : class, new();
        Task<List<T>> FindListAsync<T>(string sqlId, object parameters) where T : class, new();
        T FindById<T>(string sqlId, object parameters) where T : class, new();
        Task<T> FindByIdAsync<T>(string sqlId, object parameters) where T : class, new();
        int Count<T>(string sqlId, object parameters) where T : class, new();
        Task<int> CountAsync<T>(string sqlId, object parameters) where T : class, new();
        /// <summary>
        /// SQL分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="totalRecord">总条数</param>
        /// <param name="sqlId">Sql语句</param>
        /// <param name="parameters">筛选条件</param>
        /// <returns>返回分页数据</returns>
        List<T> FindPageList<T>(int pageIndex, int pageSize, int totalRecord, string sqlId, object parameters) where T : class, new();
        /// <summary>
        /// SQL分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="totalRecord">总条数</param>
        /// <param name="sqlId">Sql语句</param>
        /// <param name="parameters">筛选条件</param>
        /// <returns>返回分页数据</returns>
        Task<List<T>> FindPageListAsync<T>(int pageIndex, int pageSize, int totalRecord, string sqlId, object parameters) where T : class, new();
    }
}
