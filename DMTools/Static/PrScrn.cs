using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.Static
{

    public class PrScrnHelper
    {
        [DllImport("PrScrn.dll", EntryPoint = "PrScrn")]

        public extern static int PrScrn();//与dll中一致   
    }
}
