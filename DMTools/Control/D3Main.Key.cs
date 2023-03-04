using DMTools.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMTools.Control
{
    public partial class D3Main
    {
        
        CancellationTokenSource cs = new CancellationTokenSource();
 

        public void SetState(int key, bool downState)
        {
            var d3KeyState = this.d3KeyState;
            var keys = this.d3KeySetting;
            if (key == keys.Key1)
                d3KeyState.iskey1 = downState;
            else if(key==keys.Key2)
                d3KeyState.iskey2 = downState;
            else if (key == keys.Key3)
                d3KeyState.iskey3 = downState;
            else if (key == keys.Key4)
                d3KeyState.iskey4 = downState;
            else if (key == keys.KeyStand)
                d3KeyState.isStand = downState;
            else if (key == keys.KeyDrug)
                d3KeyState.isDrug = downState;
            else if (key == keys.KeyMove)
                d3KeyState.isMove = downState;
            else if (key == keys.KeyPause)
                d3KeyState.isPause = downState;
        }
        /// <summary>
        /// 开启后台监控按钮状态
        /// </summary>
        public void StartBackgroundTask()
        {
            var keys = this.d3KeySetting;
            List<int> alkey = new List<int>();
            alkey.Add(keys.Key1); alkey.Add(keys.Key2); alkey.Add(keys.Key3); alkey.Add(keys.Key4);
            alkey.Add(keys.KeyMove); alkey.Add(keys.KeyStand); alkey.Add(keys.KeyDrug); alkey.Add(keys.KeyPause);
            cs.Cancel();
            cs=new CancellationTokenSource();
            foreach (var key in alkey)
            {
                Task.Run(() =>
                {
                    while (true)
                    {
                        if(cs.IsCancellationRequested)
                        { break; }
                        SetState(key, objdm.GetKeyState(key) == 1);

                        Task.Delay(25).Wait();
                    }
                },cs.Token);
            }
            Task.Run(() =>
            {
                while (true)
                {
                    if (cs.IsCancellationRequested)
                    { break; }
                    var hd = objdm.GetMousePointWindow();
                    if (hd != this.handle)
                    {
                        this.d3KeyState.isD3 = false;
                    }
                    else
                    {
                        this.d3KeyState.isD3 = true;
                    }

                    Task.Delay(25).Wait();
                }
            }, cs.Token);

           

        }
        
    }
}
