using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KYOMS.Core20.DE.AliModel
{
    public class ShowTrack
    {
        public ShowTrack(string time,string desc)
        {
            this._time = time;
            this._desc = desc;
        }
        private string _time="";
        private string _desc = "";
        /// <summary>
        /// 发生时间
        /// </summary>
        public string Date
        {
            set { _time = value; }
            get { return _time; }
        }

        /// <summary>
        /// 描述
        /// </summary>
        public string Desc
        {
            set { _desc = value; }
            get { return _desc; }
        }
    }
}