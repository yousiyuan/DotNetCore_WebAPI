using KYOMS.Core20.Common.LogCommon;
using KYOMS.Core20.Entity.MySqlDB;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KYOMS.Core20.Application.Interface
{
    public interface IOrderMonitorService : IBase
    {
        Task<IEnumerable<T_MySql_ORDER_MONITOR>> GetList(object obj);
        Task<int> GetCount(object obj);
    }
}
