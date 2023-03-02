using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KeyHelper.Common
{
    public class D3PlanSetting
    {
        
        public D3KeySetting Keys { get; set; } = new D3KeySetting();
        public D3TimeSetting Time1 { get; set; }
        public D3TimeSetting Time2 { get; set; }
        public D3TimeSetting Time3 { get; set; }
        public D3TimeSetting Time4 { get; set; }
        public D3TimeSetting TimeMove { get; set; }
        public D3TimeSetting TimeStand { get; set; }
        public D3TimeSetting TimeDrug { get; set; }
        public bool RunState
        {
            get 
            {
                return StartThreadList.Count > 0;
            }
        }
        public List<Thread> StartThreadList { get; set; }
        public List<Thread> StopThreadList { get; set; }
        
        public int HotKey { get; set; }
    }
}
