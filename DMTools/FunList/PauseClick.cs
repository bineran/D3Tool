using DMTools.libs;
using DMTools.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMTools.Config;

namespace DMTools.FunList
{
    [KeyName(@"按住暂停键时执行点击的设置")]
    public class PauseClick : BaseD3
    {
        public  const EnumD3 enumD3Name = EnumD3.暂停时点击;
        public PauseClick(D3Param d3Param, List<KeyTimeSetting> Times, EnumD3 enumD3) : base(d3Param, Times,enumD3)
        {
           
            this.StartEvent += Pause_StartEvent;

        }

        private void Pause_StartEvent()
        {
            AddPauseClick();
        }


    }
}
