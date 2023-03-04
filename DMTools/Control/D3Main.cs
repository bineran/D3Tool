
using DMTools.Config;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMTools.Control
{
    public partial class D3Main
    {
        int handle;
       public Dm.Idmsoft objdm { get; set; } =new Dm.dmsoft();
        public D3KeyState d3KeyState { get; set; } = new D3KeyState();
        public D3KeySetting d3KeySetting { get; set; } = new D3KeySetting();
        public D3Main()
        {
         

        }
        public D3Main(int _handle) {
            this.handle = _handle;
        
        }
        public void Init()
        {
            BindForm();
            StartBackgroundTask();
        }
        public void BindForm()
        {
            try
            {
                if (objdm.IsBind(this.handle) == 1)
                {
                    objdm.UnBindWindow();
                }
                objdm.delay(50);

                //long BindWindow(hwnd,display,mouse,keypad,mode)
                var c = objdm.BindWindow(this.handle, "normal", "normal", "dx", 0);
                //var c = objdm.BindWindowEx(this.Handle, "normal", "normal", "dx", "dx.public.memory",1);
                objdm.SetKeypadDelay("dx", 0);
                objdm.SetKeypadDelay("normal", 0);
                objdm.SetKeypadDelay("windows", 0);

                objdm.SetMouseDelay("dx", 0);
                objdm.SetMouseDelay("normal", 0);
                objdm.SetMouseDelay("windows", 0);
            }catch
            (Exception e)
            {
                
            }

        }
        public List<D3Fun> FunList { get; set; }=new List<D3Fun>();
        public Keys StopAllKey { get; set; }

        private Tuple<D3Fun, D3Fun> GetNextAndRunFun(List<D3Fun> d3Funs)
        {
            
            var runIndex = -1;
            for (int i = 0; i < d3Funs.Count; i++)
            {
                var fun = d3Funs[i];
                if (fun.RunState)
                {
                    runIndex = i;
                    break;
                }
            }
            D3Fun runFun= null;
            if (runIndex > -1)
            {
                runFun= d3Funs[runIndex];
            }
            runIndex++;
            int nextIndex= runIndex;
            if (nextIndex == d3Funs.Count)
            {
                nextIndex = 0;
            }
            return new Tuple<D3Fun, D3Fun>(d3Funs[nextIndex], runFun);
        }
        private void StartAndStop(D3Fun fun)
        {
            if (fun.StartBeforeStopOther)
            {
                foreach (var f in this.FunList)
                {
                    if (f != fun)
                    {
                        if (f.OtherStopFlag)
                        {
                            if (f.RunState)
                                f.Stop();
                        }
                    }
                }
            }
            fun.StartAndStop();
        }
        public bool ProcessKeys(Keys keys)
        {
            var tmpList = this.FunList.Where(r => r.HotKey1 == keys || r.HotKey2 == keys).ToList();
            if (tmpList.Count == 0)
                return false;

            if (tmpList.Count == 1)
            {
                StartAndStop(tmpList[0]);
            }
            else
            {
                var tu=GetNextAndRunFun(tmpList);
                if (tu != null)
                {
                    if(tu.Item2!=null)
                        tu.Item2.Stop();
                    tu.Item1.Start();

                }

            }
            return true;
  
        }
        public D3Param NewD3Param(D3Timers d3FunSetting)
        {
            D3Param d3Param = new D3Param();
            d3Param.objdm = this.objdm;
            d3Param.Handle = this.handle;
            d3Param.d3KeyState = this.d3KeyState;
            d3Param.d3KeySetting = this.d3KeySetting;
            d3Param.d3Timers = d3FunSetting;
            return d3Param;
        }
        public void StopAll()
        {
            foreach (var f in FunList)
            {
                f.Stop();
            }

        }


    }
}
