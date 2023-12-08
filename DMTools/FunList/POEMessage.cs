using DMTools.libs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using DMTools.Config;
using DMTools.Control;
//using Dm;
using Idmsoft = DMTools.libs.Idmsoft;
namespace DMTools.FunList
{

    [KeyName("建议1920*1080 打开他分解对话框，并点击分解按钮后执行")]
    public class POEMessage : BaseD3
    {
        public const EnumD3 enumD3Name = EnumD3.回车发消息;

        private SortedList<int, BagPoint> bagPointList = new SortedList<int, BagPoint>();

        public POEMessage(D3Param d3Param, List<KeyTimeSetting> Times, EnumD3 enumD3) : base(d3Param, Times, enumD3)
        {
            this.StartEvent += D3FJ_StartEvent;

        }

  
        private void D3FJ_StartEvent()
        {
            var ts = this.Times.First();
            if (ts != null)
            {
                objdm.KeyPress((int)Keys.Enter);
                this.Sleep(ts.D1);
                objdm.SendString(this.Handle, ts.Str1);
                if (ts.D3 > 0)
                {
                    this.Sleep(ts.D2);
                    objdm.KeyPress((int)Keys.Enter);
                }
                if (ts.D3>0)
                {
                    this.Sleep(ts.D3);
                    objdm.KeyPress((int)Keys.Enter);
                }
            }
        

        }

    }


}
