using KYOMS.Core20.Common.Database;
using KYOMS.Core20.Entity;
using KYOMS.Core20.Respository.Core;
using System.Threading.Tasks;

namespace KYOMS.Core20.Respository
{
    public class OrderStatusTraceRespository<T> : BaseRepository<T>
    {
        private string _scope = "T_ORDER_STATUS_TRACE";

        public OrderStatusTraceRespository() : base(typeof(OracleMapper))

        {

        }
        public async Task<T_ORDER_STATUS_TRACE> findByOrderNo(string orderNo)
        {
            return await base.QueryFirstAsync<T_ORDER_STATUS_TRACE>(_scope, new { order_no = orderNo }, "FindByOrderNo");
        }
    }
}
