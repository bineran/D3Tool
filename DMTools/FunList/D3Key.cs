using DMTools.libs;
using DMTools.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMTools.FunList
{
    [KeyName(@"
一直按键：类型：按下
定时按键：类型：点击、时间1
顺序按：则会将所有设置过顺序的进行从小到大的按类型，并休眠 时间1 执行，只会执行一次
")]
    public class D3Key : BaseD3
    {
        public  const EnumD3 enumD3Name = EnumD3.按键;
        public D3Key(D3Param d3Param,EnumD3 enumD3) : base(d3Param,enumD3)
        {
           
            this.StartEvent += D3Key_StartEvent;
        }
        private void D3Key_StartEvent()
        {
            StartKeyDown();
            StartKeyPress();
            StartKeyRank();
        }

    }
}
