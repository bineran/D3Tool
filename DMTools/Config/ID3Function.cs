
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.Config
{
    public interface ID3Function
    {
        Dm.Idmsoft objdm { get; set; }
        int Handle { get; set; }
        void Start();
        void Stop();
        D3FunSetting d3FunSetting { get; set; }
        void StartBefore(D3FunSetting d3FunSetting);
    }
}
