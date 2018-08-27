using KYOMS.Core20.DE.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KYOMS.Core20.Application.Interface
{
   public interface IHC_MySql_OrderService : IBase
    {
        Task<bool> AddHC(AddOrder_HC order_HC, string logisticsInterface);
    }
}
