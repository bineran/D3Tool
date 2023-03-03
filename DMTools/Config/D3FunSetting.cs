using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMTools.Config
{
    public class D3FunSetting
    {
        
        public D3KeySetting Keys { get; set; } = new D3KeySetting();
        public D3TimeSetting Time1 { get; set; }
        public D3TimeSetting Time2 { get; set; }
        public D3TimeSetting Time3 { get; set; }
        public D3TimeSetting Time4 { get; set; }
        public D3TimeSetting TimeMove { get; set; }
        public D3TimeSetting TimeStand { get; set; }
        public D3TimeSetting TimeDrug { get; set; }
      


        public Keys HotKey { get; set; }
 
    }
}
