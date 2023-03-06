using Dm;
using DMTools.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.FunList
{
    [KeyName(@"按住暂停键时不停点左键，不需要配置，只需要启用功能即可
")]
    public class PauseClick : BaseD3
    {
        public  const EnumD3 enumD3Name = EnumD3.暂停时点左键;
        public PauseClick(D3Param d3Param,EnumD3 enumD3) : base(d3Param,enumD3)
        {
           
            this.StartEvent += Pause_StartEvent;

        }

        private void Pause_StartEvent()
        {
            AddPauseClick();
        }


    }
}
