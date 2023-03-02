using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyHelper.FunList
{
    public interface ID3Function
    {
        Dm.Idmsoft objdm { get; set; }
        int Handle { get; set; }
        void Start();
        void Stop();
        T_Time t_Time { get; set; }
        void StartBefore(T_Time t_Time);
    }
}
