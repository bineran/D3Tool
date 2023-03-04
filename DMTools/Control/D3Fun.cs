
using DMTools.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace DMTools.Control
{
    public partial class D3Fun
    {
        /// <summary>
        /// 启动前先停止其它功能 默认为true
        /// </summary>
        public bool StartBeforeStopOther { get; set; } = true;
        /// <summary>
        /// 阻止其它功能停止此功能 默认为true
        /// </summary>
        public bool OtherStopFlag { get; set; } = false;
        public D3FunSetting d3FunSetting { get; set; } = new D3FunSetting();
        public Dm.Idmsoft objdm { get; set; }
        public int Handle { get; set; }
        
        public D3Fun() { }


        public void StartAndStop()
        {
            if (this.RunState)
            {
                Stop();
            }
            else
            {
                Start();
            }
        }
        public bool RunState
        {
            get
            {
                return funList.Any(r => r.RunState);
            }
        }

        public void Start()
        {
            StartBackgroundTask();
            foreach (var f in funList)
            {
                f.StartBefore(this.d3FunSetting,this.d3KeyState);
                f.Start();
            }
        }
        public void Stop()
        {
            cs.Cancel();
            foreach (var f in funList)
            {
                f.Stop();
            }
        }
        
 
        public List<ID3Function> funList=new List<ID3Function>();
  
        public Keys HotKey1
        {
           get{ return d3FunSetting.HotKey1; }
        }
        public Keys HotKey2
        {
            get { return d3FunSetting.HotKey2; }
        }
    }
}
