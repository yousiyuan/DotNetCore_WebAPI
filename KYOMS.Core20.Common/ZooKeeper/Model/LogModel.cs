using KYOMS.Core20.Common.LogCommon;
using System;
using System.Collections.Generic;
using System.Text;

namespace KYOMS.Core20.Common.ZooKeeper.Model
{
    public class LogModel
    {
        public string LogText { get; set; }
        public LogHandle.LogerType logerType { get; set; }
    }
}
