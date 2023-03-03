using Dm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.FunList
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
            log.Info("TestA_StartEvent===========" + DateTime.Now.ToNowString());
            this.AddStartTask(() =>
            {
                while (true) { 

                    
                    log.Info("TestA_StartEvent1111===========" + DateTime.Now.ToNowString());
                   
                    Sleep(3000);
                }
            });
        }

        private void TestA_StopEvent()
        {
            log.Info("TestA_StopEvent===========" + DateTime.Now.ToNowString());
        }
    }
}
