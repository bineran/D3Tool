using DMTools.libs;
using DMTools.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DMTools.Config;

namespace DMTools.FunList
{
    [KeyName(@"
一直按键：类型：按下
定时按键：类型：点击、时间1
顺序按：则会将所有设置过顺序的进行从小到大的按类型，并休眠 时间1 执行，只会执行一次
只有1-5交替换：D1是按键1的时间，D2是药剂修正时间（1-100），D3是按键2的时间
按下：如果设了按键2，则当设定的按键2是按下的状态，会弹起设定的按下的按键1
")]
    public class D3Key : BaseD3
    {
        public  const EnumD3 enumD3Name = EnumD3.按键;
        public D3Key(D3Param d3Param, List<KeyTimeSetting> Times, EnumD3 enumD3) : base(d3Param, Times,enumD3)
        {
            this.StartEvent += D3Key_StartEvent;
        }
        private void D3Key_StartEvent()
        {
            this.StartKeyRank();
            StartKeyDown();
            StartKeyPress();
          
            StartPointColor();
            StartPointNoColor();
            StartImageTask();
            StartAfterKey();
            //Test();
        }


        public void Test()
        { 
            while(true)
            {
                objdm.KeyPressChar("A");
                this.Sleep(500);
            }
        }

    }
}
