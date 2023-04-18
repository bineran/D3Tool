

//using Dm;
using DMTools.Config;
using DMTools.libs;
using Microsoft.VisualBasic.Devices;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Idmsoft = DMTools.libs.Idmsoft;
namespace DMTools.Control
{
    public partial class D3Main
    {
        int handle;
        public DMP objDMP { get; set; }=new DMP();
        public Idmsoft objdm { get { return objDMP.DM; } }
        static NLog.Logger log= NLog.LogManager.GetCurrentClassLogger();
        public D3KeyState d3KeyState { get; set; } = new D3KeyState();
        public D3KeyCodes d3KeySetting { get; set; } = new D3KeyCodes();
        public SysConfig sysConfig { get;init; } =new SysConfig();
        public D3Main(int _handle,D3KeyCodes d3KeySetting, SysConfig sysConfig) {
            this.handle = _handle;
            this.d3KeySetting= d3KeySetting;
            this.sysConfig= sysConfig; 

        }
        private bool IsInit { get; set; } = false;
        public void Init()
        {
            IsInit = true;
 
            StartBackgroundTask();
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
                        if (!f.OtherStopFlag)
                        {
                                f.Stop();
                        }
                    }
                }
            }
            fun.StartAndStop();
        }

        public bool ProcessKeys(Keys keys)
        {
            var tmpList = this.FunList.Where(r => r.EnabledFlag &&( r.HotKey1 == keys || r.HotKey2 == keys) && r.funList.Count>0).ToList();
            if (tmpList.Count == 0)
            {
                log.Info("未找到可以执行的方法");
                return false;
            }
      
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
                IsInit = false;
            }
            return true;
  
        }
        public D3Param NewD3Param(List<D3ConfigFun> d3ConfigFuns,SysConfig sysConfig)
        {
            D3Param d3Param = new D3Param();
            d3Param.sysConfig= sysConfig;
            d3Param.Handle = this.handle;
            d3Param.d3KeyState = this.d3KeyState;
            d3Param.KeyCodes = this.d3KeySetting;
            d3Param.ConfigFuns = d3ConfigFuns;
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
            var items=d3Config.d3ConfigItems.Where(r => r.EnabledFlag
            
            && r.d3ConfigFuns.Count > 0 && r.d3ConfigFuns.Any(p=>p.EnableFlag));
    
            var d3KeySetting = ConvertD3KeySetting(d3Config);
            D3Main d3Main=new D3Main(hd,d3KeySetting, d3Config.sysConfig);

            foreach (var item in items)
            {

                //strfunList[]
                var d3param = d3Main.NewD3Param(item.d3ConfigFuns, d3Config.sysConfig);
                D3Fun d3Fun = new D3Fun(d3param);
                d3Fun.EnabledFlag = item.EnabledFlag;
                d3Fun.StartBeforeStopOther = item.StartBeforeStopOther;
                d3Fun.OtherStopFlag = item.OtherStopFlag;
                d3Fun.HotKey1 = item.HotKey1;
                d3Fun.HotKey2 = item.HotKey2;
                d3Main.FunList.Add(d3Fun);

            }
            if (d3Main.FunList.Count > 0)
            {
                return d3Main;
            }
            else
                return null;

        }
        private static D3KeyCodes ConvertD3KeySetting(D3Config d3Config)
        {
            var ps = typeof(D3KeyCodes).GetProperties().Where(r => r.PropertyType == typeof(int)).ToList();
            D3KeyCodes d3KeySetting= new D3KeyCodes();
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

        private static List<EnumD3> ConvertEnumd3List(D3ConfigItem item)
        {
            List<EnumD3> enumD3s = new List<EnumD3>();
            foreach (var t in item.d3ConfigFuns.Where(r=>r.EnableFlag))
            {
               
                    enumD3s.Add(t.enumD3);
                
             
            }
            return enumD3s;
        }

    }
}
