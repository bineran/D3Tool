using Dm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyHelper.FunList
{
    public class TestA : BaseD3
    {
        public TestA(Idmsoft objdm, int handle) : base(objdm, handle)
        {
           
            this.StartEvent += TestA_StartEvent;
            this.StopEvent += TestA_StopEvent;
        }

        private void TestA_StartEvent()
        {
            
        }

        private void TestA_StopEvent()
        {
           
        }
    }
}
