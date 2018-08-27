using KYOMS.Core20.DE.Model;
using System.Threading.Tasks;

namespace KYOMS.Core20.Application.Interface
{
    public interface IHC_ORDERService: IBase
    {
        Task<string> CanlOrder(CancelOrderHC cancelOrder, string OrderNo);
        Task<QueryOrderInfoToResponse> QueryOrderInfon(QueryOrder_HC queryOrder_HC);
    }
}
