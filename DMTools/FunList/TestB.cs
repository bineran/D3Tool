using Dm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.FunList
{
    public class TestB : BaseD3
    {
        public TestB(Idmsoft objdm, int handle) : base(objdm, handle)
        {
           
            this.StartEvent += TestB_StartEvent;
            this.StopEvent += TestB_StopEvent;
        }

        private void TestB_StartEvent()
        {
           
            log.Info("TestBBBBB_StartEvent===========" + DateTime.Now.ToNowString());
            this.AddStartTask(() =>
            {
                while (true)
                {
                    log.Info("TestBBBBB_StartEvent1111===========" + DateTime.Now.ToNowString());

                    Sleep(3000);
                }
            });
        }

        private void TestB_StopEvent()
        {
            log.Info("TestB_StopEvent*****************" + DateTime.Now.ToNowString());
        }
    }
}
