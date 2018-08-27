using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KYOMS.Services.Core20.HC.Common
{
    public class BaseInfo
    {
        public static int Port
        {
            get
            {
                return Convert.ToInt32(AppDomain.CurrentDomain.GetData("Port")); ;
            }
        }
    }
}
