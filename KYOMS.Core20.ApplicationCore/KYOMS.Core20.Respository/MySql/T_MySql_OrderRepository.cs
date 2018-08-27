using Dapper;
using KYOMS.Core20.Common.BIZ;
using KYOMS.Core20.Common.Database;
using KYOMS.Core20.Entity.MySqlDB;
using KYOMS.Core20.Respository.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KYOMS.Core20.Respository
{
    public class T_MySql_OrderRepository<T> : BaseRepository<T>
    {
        private string _scope = "T_MySql_Order";
        public T_MySql_OrderRepository() : base(typeof(MySqlMapper))
        {

        }
        /// <summary>
        /// charu
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async  Task<bool> Insert(T_MySql_Order t_MySql_Order)
        {
            //先获取动态表名
            var tableName = MysqlTableName.GetMySqlOrderTableName();
            return await base.InsertAsync(_scope, new { tableName, t_MySql_Order.OUTSYS_ORDER_NO, t_MySql_Order.OUTSYS_BILL_CODE, t_MySql_Order.ORDER_SOURCE, t_MySql_Order.MSG_CONTENT, t_MySql_Order.MSG_TYPE, t_MySql_Order.REMARK, t_MySql_Order.IS_SYNC_SUCCESS, t_MySql_Order.CREATE_BY, t_MySql_Order.CREATE_TIME, C1 = "", C2 = "", C3 = "" }) > 0;
        }

        /// <summary>
        /// charu
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<bool> Insert2(IList<T_MySql_Order> t_MySql_Orders)
        {
            //先获取动态表名
            var tableName = MysqlTableName.GetMySqlOrderTableName();
            return await base.InsertAsync(_scope, new { tableName, t_MySql_Orders = t_MySql_Orders }, "Insert2") > 0;
        }

        public async Task<bool> Update(IDictionary<string, object> parameters)
        {
            //先获取动态表名
            var tableName = MysqlTableName.GetMySqlOrderTableName();
            parameters.Add("tableName", tableName);
            return await base.UpdateAsync(_scope, parameters) > 0;
        }

        public async Task<bool> Delete(IDictionary<string, object> parameters)
        {
            //先获取动态表名
            var tableName = MysqlTableName.GetMySqlOrderTableName();
            parameters.Add("tableName", tableName);
            return await base.DeleteAsync(_scope, parameters) > 0;
        }

        public async Task<IEnumerable<T_MySql_Order>> Query(IDictionary<string, object> parameters)
        {
            //先获取动态表名
            var tableName = MysqlTableName.GetMySqlOrderTableName();
            parameters.Add("tableName", tableName);

            return await base.GetListAsync<T_MySql_Order>(_scope, parameters, "Query");
        }
    }
}
