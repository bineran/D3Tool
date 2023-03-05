using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMTools.Config
{
    public class D3Timers
    {

        [KeyName("1")]
        public D3TimeSetting Time1 { get; set; }
        [KeyName("2")]
        public D3TimeSetting Time2 { get; set; }
        [KeyName("3")]
        public D3TimeSetting Time3 { get; set; }
        [KeyName("4")]
        public D3TimeSetting Time4 { get; set; }
        [KeyName("移动键")]
        public D3TimeSetting TimeMove { get; set; }
        [KeyName("原地站立键")]
        public D3TimeSetting TimeStand { get; set; }
        [KeyName("喝药键")]
        public D3TimeSetting TimeDrug { get; set; }
        [KeyName("鼠标左键")]
        public D3TimeSetting TimeLeft { get; set; }
        [KeyName("鼠标右键")]
        public D3TimeSetting TimeRight { get; set; }
    }
}
