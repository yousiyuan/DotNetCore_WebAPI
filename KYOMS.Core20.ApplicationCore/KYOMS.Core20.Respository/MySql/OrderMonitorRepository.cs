using KYOMS.Core20.Common.Database;
using KYOMS.Core20.Entity.MySqlDB;
using KYOMS.Core20.Respository.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KYOMS.Core20.Respository
{
    public class OrderMonitorRepository<T> : BaseRepository<T>
    {
        private string _scope = "T_MySql_ORDER_MONITOR";
        public OrderMonitorRepository() : base(typeof(MySqlMapper))
        {

        }
        public async Task<IEnumerable<T_MySql_ORDER_MONITOR>> GetList(object obj)
        {
            return await GetListAsync<T_MySql_ORDER_MONITOR>(_scope, obj, "GetOrderMonitor");
        }
        public async Task<int> GetCount(object obj)
        {
            return await base.QueryFirstAsync<int>(_scope, obj, "Total");
        }
    }
}
