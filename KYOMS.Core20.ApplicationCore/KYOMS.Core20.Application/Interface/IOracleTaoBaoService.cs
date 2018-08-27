using KYOMS.Core20.DE.Model;
using System.Threading.Tasks;

namespace KYOMS.Core20.Application.Interface
{
    public interface IOracleTaoBaoService : IBase
    {
        Task<ResponseResult> EditOrder(UpdateTaobaoOrderModel orderModel, string CpCode);
    }
}
