
using DMTool.Control;
using DMTool.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTool.Config
{
    public interface ID3Function
    {
    
        void Start();
        void Stop();
        D3Param d3Param { get; set; }
   
        bool RunState { get; }
    }
}
