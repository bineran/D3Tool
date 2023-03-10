using DMTools.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMTools.libs;

namespace DMTools.Control
{
    public  class D3Param
    {
        public D3Param() { }
        public Idmsoft objdm { get; set; }
        public int Handle { get; set; }
        public D3KeyCodes KeyCodes { get; set; }
        public SortedList<EnumD3, List<KeyTimeSetting>> SLTimes { get; set; }
        public D3KeyState d3KeyState { get; set; }
        public EnumD3 enumD3 { get; set; }

    }
}
