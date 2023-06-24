﻿
using DMTools.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.Config
{
    public interface ID3Function
    {
         EnumD3 enumD3 { get; set; }
        void Start();
        void Stop();
        D3Param d3Param { get; set; }
   
        bool RunState { get; }
    }
}
