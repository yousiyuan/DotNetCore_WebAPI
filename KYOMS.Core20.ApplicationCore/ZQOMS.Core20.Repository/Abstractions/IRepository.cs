using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ZQOMS.Core20.Repository.Abstractions
{
    public interface IRepository<T> : IDisposable where T : class, new()
    {
        int Execute(string sqlId, object parameters);
        Task<int> ExecuteAsync(string sqlId, object parameters);
        IEnumerable<T> Query(string sqlId, object parameters);
        Task<IEnumerable<T>> QueryAsync(string sqlId, object parameters);
        T QuerySingle(string sqlId, object parameters);
        Task<T> QuerySingleAsync(string sqlId, object parameters);
        object QueryScalar(string sqlId, object parameters);
        Task<object> QueryScalarAsync(string sqlId, object parameters);
    }
    public interface IRepository : IDisposable
    {
        int Execute<T>(string sqlId, object parameters) where T : class, new();
        Task<int> ExecuteAsync<T>(string sqlId, object parameters) where T : class, new();
        IEnumerable<T> Query<T>(string sqlId, object parameters) where T : class, new();
        Task<IEnumerable<T>> QueryAsync<T>(string sqlId, object parameters) where T : class, new();
        T QuerySingle<T>(string sqlId, object parameters) where T : class, new();
        Task<T> QuerySingleAsync<T>(string sqlId, object parameters) where T : class, new();
        object QueryScalar<T>(string sqlId, object parameters) where T : class, new();
        Task<object> QueryScalarAsync<T>(string sqlId, object parameters) where T : class, new();
    }
}
