
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

        public D3Main(int _handle,D3KeySetting d3KeySetting) {
            this.handle = _handle;
            this.d3KeySetting= d3KeySetting; 
  
        }
        private bool IsInit { get; set; } = false;
        public void Init()
        {
            IsInit = true;
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
        public bool RunState
        {
            get
            {
                return this.FunList.Any(r => r.RunState);

            }
        }
        public List<D3Fun> FunList { get; set; }=new List<D3Fun>();
       

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
            var tmpList = this.FunList.Where(r => r.EnabledFlag &&( r.HotKey1 == keys || r.HotKey2 == keys)).ToList();
            if (tmpList.Count == 0)
                return false;
            if(IsInit==false)
            {
                Init();
            }
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
            if (this.FunList.All(r => !r.RunState))
            {
                cs.Cancel();
                IsInit = false;
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
            cs.Cancel();
            IsInit = false;

        }

        public static D3Main BuildD3Main(D3Config d3Config,int hd) {
            var items=d3Config.d3ConfigItems.Where(r => r.EnabledFlag && r.strfunList.Count > 0);
            Dm.Idmsoft objdm=new Dm.dmsoft();
            var d3KeySetting = ConvertD3KeySetting(d3Config);
            D3Main d3Main=new D3Main(hd,d3KeySetting);
          
            foreach (var  item in items)
            {
                List<EnumD3> enumD3s = ConvertEnumd3List(item);

                if (enumD3s.Count > 0)
                {
                    var d3Timers = ConvertD3Timers(item);
                    var d3param = d3Main.NewD3Param(d3Timers);
                    D3Fun d3Fun = new D3Fun(d3param, enumD3s.ToArray());
                    d3Fun.EnabledFlag = item.EnabledFlag;
                    d3Fun.StartBeforeStopOther=item.StartBeforeStopOther;
                    d3Fun.OtherStopFlag= item.OtherStopFlag;
                    d3Fun.HotKey1= item.HotKey1;
                    d3Fun.HotKey2 = item.HotKey2;
                    d3Main.FunList.Add(d3Fun);
                }
            }
            if (d3Main.FunList.Count > 0)
            {
                return d3Main;
            }
            else
                return null;

        }
        private static D3KeySetting ConvertD3KeySetting(D3Config d3Config)
        {
            var ps = typeof(D3KeySetting).GetProperties().Where(r => r.PropertyType == typeof(int)).ToList();
            D3KeySetting d3KeySetting= new D3KeySetting();
            foreach (var k in d3Config.ConfigKeys)
            {
                var p = ps.FirstOrDefault(r => r.Name == k.KeyName);
                if (p != null)
                {

   
                    p.SetValue(d3KeySetting,(int)k.KeyCode);
                }
            }
            return d3KeySetting;
        }
        private static D3Timers ConvertD3Timers(D3ConfigItem item)
        {
            D3Timers d3Timers = new D3Timers();
            var ps = typeof(D3Timers).GetProperties().Where(r => r.PropertyType == typeof(D3TimeSetting)).ToList();

            foreach (var t in item.d3TimeSettings)
            {
                var p = ps.FirstOrDefault(r => r.Name == t.KeyName);
                if (p != null)
                {
                    p.SetValue(d3Timers,t );
                }
            }
            return d3Timers;

        }
        private static List<EnumD3> ConvertEnumd3List(D3ConfigItem item)
        {
            List<EnumD3> enumD3s = new List<EnumD3>();
            foreach (var str in item.strfunList)
            {
                if (Enum.TryParse(str, out EnumD3 myStatus))
                {
                    enumD3s.Add(myStatus);
                }
             
            }
            return enumD3s;
        }

    }
}
