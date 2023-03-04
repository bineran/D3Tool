using Dm;
using DMTools.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.FunList
{
    public class TestB : BaseD3
    {
        public const EnumD3 enumD3Name = EnumD3.按1234LRMS;
        public TestB(D3Param d3Param) : base(d3Param)
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
