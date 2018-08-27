using KYOMS.Core20.Application.Interface;
using KYOMS.Core20.Common.ZooKeeper;
using KYOMS.Core20.Entity.MySqlDB;
using KYOMS.Core20.Entity.Oracle;
using KYOMS.Core20.Respository;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KYOMS.Core20.Application
{

    public class OrderMonitorService : OrderMonitorRepository<OrderMonitorService>, IOrderMonitorService
    {
        public new async Task<IEnumerable<T_MySql_ORDER_MONITOR>> GetList(object obj)
        {
            return await base.GetList(obj);
        }
        public new async Task<int> GetCount(object obj)
        {
            return await base.GetCount(obj);
        }
    }
}
