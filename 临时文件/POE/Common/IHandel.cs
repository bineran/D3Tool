using System;
using System.Collections.Generic;
using System.Text;
using Idmsoft = DMTools.libs.Idmsoft;
namespace POE.Common
{
    public interface IHandel
    {
       public Idmsoft objdm { get;  set; }
        public void Do();
        
        
    }
}
