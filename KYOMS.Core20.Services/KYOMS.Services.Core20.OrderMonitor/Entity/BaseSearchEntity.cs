using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KYOMS.Services.Core20.OrderMonitor.Entity
{
    public class BaseSearchEntity
    {

        private int pageIndex = 20;
        public int PageIndex
        {
            get
            {
                return (pageIndex - 1) * PageSize;
            }
            set
            {
                pageIndex = value;
            }
        }
        public int PageSize { get; set; }
    }
}
