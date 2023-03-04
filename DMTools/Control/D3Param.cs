using DMTools.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.Control
{
    public  class D3Param
    {
        public D3Param() { }
        public Dm.Idmsoft objdm { get; set; }
        public int Handle { get; set; }
        public D3KeySetting d3KeySetting { get; set; }
        public D3Timers d3Timers { get; set; }
        public D3KeyState d3KeyState { get; set; }

    }
}
