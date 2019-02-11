using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace D3Tool
{
    public class T_Plan
    {
        public T_Plan() {
            IsEnabled = true;
        }
        public string Path { get; set; }
        public string Name { get; set; }
        public T_Time t1 { get; set; }
        public T_Time t2 { get; set; }
        public T_Time t3 { get; set; }
        public T_Time t4 { get; set; }
        public T_Key Keys { get; set; }
        public bool IsEnabled { get; set; }
        public int CursorPosX { get; set; }
        public int CursorPosY { get; set; }
        public DateTime CursorPosTime { get; set; }
        

    }
}
