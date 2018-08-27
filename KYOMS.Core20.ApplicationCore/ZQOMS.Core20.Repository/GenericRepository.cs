using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SmartSql.Abstractions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZQOMS.Core20.Repository.Abstractions;

namespace ZQOMS.Core20.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, new()
    {
        private IDbContext _dbContext;//非托管资源
        private string _tableName;
        public GenericRepository(IDbContext dbContext)
        {
            _tableName = typeof(T).Name;
            _dbContext = dbContext;
        }

        #region IRepository<T>成员
        public int Execute(string sqlId, object parameters)
        {
            return _dbContext.Execute(new RequestContext { SqlId = sqlId, Request = parameters, Scope = _tableName });
        }
        public Task<int> ExecuteAsync(string sqlId, object parameters)
        {
            return _dbContext.ExecuteAsync(new RequestContext { SqlId = sqlId, Request = parameters, Scope = _tableName });
        }
        public T QuerySingle(string sqlId, object parameters)
        {
            return _dbContext.QuerySingle<T>(new RequestContext { SqlId = sqlId, Request = parameters, Scope = _tableName });
        }
        public Task<T> QuerySingleAsync(string sqlId, object parameters)
        {
            return _dbContext.QuerySingleAsync<T>(new RequestContext { SqlId = sqlId, Request = parameters, Scope = _tableName });
        }
        public IEnumerable<T> Query(string sqlId, object parameters)
        {
            return _dbContext.Query<T>(new RequestContext { SqlId = sqlId, Request = parameters, Scope = _tableName });
        }
        public Task<IEnumerable<T>> QueryAsync(string sqlId, object parameters)
        {
            return _dbContext.QueryAsync<T>(new RequestContext { SqlId = sqlId, Request = parameters, Scope = _tableName });
        }
        public object QueryScalar(string sqlId, object parameters)
        {
            return _dbContext.ExecuteScalar<object>(new RequestContext { SqlId = sqlId, Request = parameters, Scope = _tableName });
        }
        public Task<object> QueryScalarAsync(string sqlId, object parameters)
        {
            return _dbContext.ExecuteScalarAsync<object>(new RequestContext { SqlId = sqlId, Request = parameters, Scope = _tableName });
        }

        //在释放数据库对象时需要显式调用
        public void Dispose()
        {
            //释放托管和非托管资源
            Dispose(true);
            //将对象从垃圾回收器链表中移除，从而在垃圾回收器工作时，只释放托管资源，而不执行此对象的析构函数
            GC.SuppressFinalize(this);
        }
        #endregion

        #region IGenericRepository<T>成员
        public int Insert(string sqlId, object parameters)
        {
            return Execute(sqlId, parameters);
        }
        public Task<int> InsertAsync(string sqlId, object parameters)
        {
            return ExecuteAsync(sqlId, parameters);
        }
        public int Update(string sqlId, object parameters)
        {
            return Execute(sqlId, parameters);
        }
        public Task<int> UpdateAsync(string sqlId, object parameters)
        {
            return ExecuteAsync(sqlId, parameters);
        }
        public int Delete(string sqlId, object parameters)
        {
            return Execute(sqlId, parameters);
        }
        public Task<int> DeleteAsync(string sqlId, object parameters)
        {
            return ExecuteAsync(sqlId, parameters);
        }
        public List<T> FindList(string sqlId, object parameters)
        {
            var result = Query(sqlId, parameters);
            if (result == null)
                return new List<T>(0);
            return result.ToList();
        }
        public async Task<List<T>> FindListAsync(string sqlId, object parameters)
        {
            var result = await QueryAsync(sqlId, parameters);
            if (result == null)
                return new List<T>(0);
            return result.ToList();
        }
        public T FindById(string sqlId, object parameters)
        {
            return QuerySingle(sqlId, parameters);
        }
        public Task<T> FindByIdAsync(string sqlId, object parameters)
        {
            return QuerySingleAsync(sqlId, parameters);
        }
        public int Count(string sqlId, object parameters)
        {
            var result = QueryScalar(sqlId, parameters);
            if (result == null)
                return 0;
            return Convert.ToInt32(result);
        }
        public async Task<int> CountAsync(string sqlId, object parameters)
        {
            var result = await QueryScalarAsync(sqlId, parameters);
            if (result == null)
                return await Task.FromResult(0);
            return Convert.ToInt32(result);
        }
        public List<T> FindPageList(int pageIndex, int pageSize, int totalRecord, string sqlId, object parameters)
        {
            var maxPageIndex = Math.Ceiling(Convert.ToDecimal(totalRecord / pageSize));//向上取整，最大页码
            if (pageIndex > maxPageIndex)
                pageIndex = (int)maxPageIndex;

            var minIndex = (pageIndex - 1) * pageSize;
            var maxIndex = pageIndex * pageSize;

            Hashtable ht = new Hashtable();
            var jsObj = JObject.FromObject(parameters);
            var jtoks = jsObj.Children().ToList();
            foreach (JProperty jtok in jtoks)
            {
                ht[jtok.Name] = jtok.Value;
            }
            ht["MinIndex"] = minIndex;
            ht["MaxIndex"] = maxIndex;
            var result = FindList(sqlId, ht);
            return result;
        }
        public async Task<List<T>> FindPageListAsync(int pageIndex, int pageSize, int totalRecord, string sqlId, object parameters)
        {
            var maxPageIndex = Math.Ceiling(Convert.ToDecimal(totalRecord / pageSize));//向上取整，最大页码
            if (pageIndex > maxPageIndex)
                pageIndex = (int)maxPageIndex;

            var minIndex = (pageIndex - 1) * pageSize;
            var maxIndex = pageIndex * pageSize;

            Hashtable ht = new Hashtable();
            var jsObj = JObject.FromObject(parameters);
            var jtoks = jsObj.Children().ToList();
            foreach (JProperty jtok in jtoks)
            {
                ht[jtok.Name] = jtok.Value;
            }
            ht["MinIndex"] = minIndex;
            ht["MaxIndex"] = maxIndex;
            var result = await FindListAsync(sqlId, ht);
            return result;
        }
        #endregion

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
                if (_dbContext != null)
                    _dbContext.Dispose();
            }
            //标识此对象已释放
            this.isDisposed = true;
        }
        ~GenericRepository()
        {
            //释放非托管资源
            Dispose(false);
        }
    }

    public class GenericRepository : IGenericRepository
    {
        private IDbContext _dbContext;//非托管资源
        public GenericRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region IRepository成员
        public int Execute<T>(string sqlId, object parameters) where T : class, new()
        {
            return _dbContext.Execute(new RequestContext { SqlId = sqlId, Request = parameters, Scope = typeof(T).Name });
        }
        public Task<int> ExecuteAsync<T>(string sqlId, object parameters) where T : class, new()
        {
            return _dbContext.ExecuteAsync(new RequestContext { SqlId = sqlId, Request = parameters, Scope = typeof(T).Name });
        }
        public IEnumerable<T> Query<T>(string sqlId, object parameters) where T : class, new()
        {
            return _dbContext.Query<T>(new RequestContext { SqlId = sqlId, Request = parameters, Scope = typeof(T).Name });
        }
        public Task<IEnumerable<T>> QueryAsync<T>(string sqlId, object parameters) where T : class, new()
        {
            return _dbContext.QueryAsync<T>(new RequestContext { SqlId = sqlId, Request = parameters, Scope = typeof(T).Name });
        }
        public object QueryScalar<T>(string sqlId, object parameters) where T : class, new()
        {
            return _dbContext.ExecuteScalar<object>(new RequestContext { SqlId = sqlId, Request = parameters, Scope = typeof(T).Name });
        }
        public Task<object> QueryScalarAsync<T>(string sqlId, object parameters) where T : class, new()
        {
            return _dbContext.ExecuteScalarAsync<object>(new RequestContext { SqlId = sqlId, Request = parameters, Scope = typeof(T).Name });
        }
        public T QuerySingle<T>(string sqlId, object parameters) where T : class, new()
        {
            return _dbContext.QuerySingle<T>(new RequestContext { SqlId = sqlId, Request = parameters, Scope = typeof(T).Name });
        }
        public Task<T> QuerySingleAsync<T>(string sqlId, object parameters) where T : class, new()
        {
            return _dbContext.QuerySingleAsync<T>(new RequestContext { SqlId = sqlId, Request = parameters, Scope = typeof(T).Name });
        }
        //在释放数据库对象时需要显式调用
        public void Dispose()
        {
            //释放托管和非托管资源
            Dispose(true);
            //将对象从垃圾回收器链表中移除，从而在垃圾回收器工作时，只释放托管资源，而不执行此对象的析构函数
            GC.SuppressFinalize(this);
        }
        #endregion

        #region IGenericRepository成员
        public int Count<T>(string sqlId, object parameters) where T : class, new()
        {
            var result = QueryScalar<T>(sqlId, parameters);
            if (result == null)
                return 0;
            return Convert.ToInt32(result);
        }
        public async Task<int> CountAsync<T>(string sqlId, object parameters) where T : class, new()
        {
            var result = await QueryScalarAsync<T>(sqlId, parameters);
            if (result == null)
                return await Task.FromResult(0);
            return Convert.ToInt32(result);
        }
        public int Delete<T>(string sqlId, object parameters) where T : class, new()
        {
            return Execute<T>(sqlId, parameters);
        }
        public Task<int> DeleteAsync<T>(string sqlId, object parameters) where T : class, new()
        {
            return ExecuteAsync<T>(sqlId, parameters);
        }
        public T FindById<T>(string sqlId, object parameters) where T : class, new()
        {
            return QuerySingle<T>(sqlId, parameters);
        }
        public Task<T> FindByIdAsync<T>(string sqlId, object parameters) where T : class, new()
        {
            return QuerySingleAsync<T>(sqlId, parameters);
        }
        public List<T> FindList<T>(string sqlId, object parameters) where T : class, new()
        {
            var result = Query<T>(sqlId, parameters);
            if (result == null)
                return new List<T>(0);
            return result.ToList();
        }
        public async Task<List<T>> FindListAsync<T>(string sqlId, object parameters) where T : class, new()
        {
            var result = await QueryAsync<T>(sqlId, parameters);
            if (result == null)
                return new List<T>(0);
            return result.ToList();
        }
        public int Insert<T>(string sqlId, object parameters) where T : class, new()
        {
            return Execute<T>(sqlId, parameters);
        }
        public Task<int> InsertAsync<T>(string sqlId, object parameters) where T : class, new()
        {
            return ExecuteAsync<T>(sqlId, parameters);
        }

        public int Update<T>(string sqlId, object parameters) where T : class, new()
        {
            return Execute<T>(sqlId, parameters);
        }
        public Task<int> UpdateAsync<T>(string sqlId, object parameters) where T : class, new()
        {
            return ExecuteAsync<T>(sqlId, parameters);
        }
        public List<T> FindPageList<T>(int pageIndex, int pageSize, int totalRecord, string sqlId, object parameters) where T : class, new()
        {
            var maxPageIndex = Math.Ceiling(Convert.ToDecimal(totalRecord / pageSize));//向上取整，最大页码
            if (pageIndex > maxPageIndex)
                pageIndex = (int)maxPageIndex;

            var minIndex = (pageIndex - 1) * pageSize;
            var maxIndex = pageIndex * pageSize;

            Hashtable ht = new Hashtable();
            var jsObj = JObject.FromObject(parameters);
            var jtoks = jsObj.Children().ToList();
            foreach (JProperty jtok in jtoks)
            {
                ht[jtok.Name] = jtok.Value;
            }
            ht["MinIndex"] = minIndex;
            ht["MaxIndex"] = maxIndex;
            var result = FindList<T>(sqlId, ht);
            return result;
        }
        public async Task<List<T>> FindPageListAsync<T>(int pageIndex, int pageSize, int totalRecord, string sqlId, object parameters) where T : class, new()
        {
            var maxPageIndex = Math.Ceiling(Convert.ToDecimal(totalRecord / pageSize));//向上取整，最大页码
            if (pageIndex > maxPageIndex)
                pageIndex = (int)maxPageIndex;

            var minIndex = (pageIndex - 1) * pageSize;
            var maxIndex = pageIndex * pageSize;

            Hashtable ht = new Hashtable();
            var jsObj = JObject.FromObject(parameters);
            var jtoks = jsObj.Children().ToList();
            foreach (JProperty jtok in jtoks)
            {
                ht[jtok.Name] = jtok.Value;
            }
            ht["MinIndex"] = minIndex;
            ht["MaxIndex"] = maxIndex;
            var result = await FindListAsync<T>(sqlId, ht);
            return result;
        }
        #endregion

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
                if (_dbContext != null)
                    _dbContext.Dispose();
            }
            //标识此对象已释放
            this.isDisposed = true;
        }
        ~GenericRepository()
        {
            //释放非托管资源
            Dispose(false);
        }
    }
}
