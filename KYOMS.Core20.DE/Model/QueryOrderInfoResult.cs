
namespace KYOMS.Core20.DE.Model
{
    public class QueryOrderInfoResult
    { 
        public QueryOrderInfoToResponse responseParam { set; get; }
        public bool result { get; set; }
        public int resultCode { get; set; }
        public string resultInfo { get; set; }
        public string reason { get; set; }


    }
}
