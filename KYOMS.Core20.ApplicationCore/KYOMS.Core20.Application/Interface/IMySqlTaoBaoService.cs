using KYOMS.Core20.Common.LogCommon;
using KYOMS.Core20.DE.Model;
using KYOMS.Core20.Entity.MySqlDB;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KYOMS.Core20.Application.Interface
{
    public interface IMySqlTaoBaoService : IBase
    {
        Task<bool> AddOrder(TmsOrderModel taobaoOrderModel, string logisticsInterface);
        Task<bool> InsertList(IList<T_MySql_Order> t_MySql_Orders);
        Task<bool> AddTaoBao(TaobaoOrderModel taobaoOrderModel);
    }
}
