using DMTools.Config;
using DMTools.Control;
using DMTools.libs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.FunList.Tasks
{
    public class BaseTask
    {
        public int Handle { get { return this.d3Param.Handle; } }
        public SysConfig sysConfig { get { return this.d3Param.sysConfig; } }
        public Idmsoft objdm { get; set; }
        public D3Param d3Param { get; set; }
        public BaseTask(D3Param d3Param)
        {
            this.d3Param = d3Param;
            InitDM();
        }
        private void InitDM()
        {
            DMP dMP = new DMP();
            Idmsoft objdm = dMP.DM;
            objdm.BindWindow(Handle,sysConfig.display, sysConfig.mouse, sysConfig.keypad, sysConfig.mode);
        }
 

    }
}
