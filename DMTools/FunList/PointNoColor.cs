using DMTools.libs;
using DMTools.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.FunList
{
    [KeyName(@"如果指定时点颜色不是配置的颜色，则按键
类型：点击
文本1：2e3238    颜色，可以通过微信截图去找指定点的颜色和位置
整数1：pointX,整数2：pointY
")]
    public class PointNoColor : BaseD3
    {
        public  const EnumD3 enumD3Name = EnumD3.颜色不相等时按键;
        public PointNoColor(D3Param d3Param,EnumD3 enumD3) : base(d3Param,enumD3)
        {
           
            this.StartEvent += Pause_StartEvent;

        }

        private void Pause_StartEvent()
        {
           var kl= this.Times.Where(r => r.keyClickType== Config.KeyClickType.点击 
            && 0<=r.Int1  && r.Int1 <=D3W
            && 0 <= r.Int2 && r.Int2 <= D3H
            && r.KeyCode>0
            && r.Str1.ToColor()!=null
              && r.D1 > 0
            );
            foreach (var k in kl)
            {
                AddPointNoColorTask(k);
            }
            
        }


    }
}
