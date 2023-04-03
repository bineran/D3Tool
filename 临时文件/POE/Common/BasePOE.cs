using DMTools.libs;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace POE.Common
{
    public class BasePOE : IHandel
    {
        public BasePOE() { 
            DMP dMP = new DMP();
            this.objdm = dMP.DM;
        }
        public Idmsoft objdm { get; set; }

        public virtual void Do() { }

    }
}
