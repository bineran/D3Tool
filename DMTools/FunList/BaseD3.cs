using Dm;
using DMTools.Config;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NLog;
namespace DMTools.FunList
{
    public  abstract partial class BaseD3 : ID3Function
    {
        public EnumD3 enumD3Name { get; set; } = EnumD3.默认;
        public readonly Logger log=LogManager.GetCurrentClassLogger();
        public virtual event Action StartEvent;
        public virtual event Action StopEvent;
        public Idmsoft objdm { get; set; }
        public int Handle { get; set; }
        public D3FunSetting d3FunSetting { get; set; }
        public D3KeyState d3KeyState { get; set; }
        CancellationTokenSource cs = new CancellationTokenSource();
        
        public BaseD3(Idmsoft objdm, int handle)
        {
            this.objdm = objdm;
            Handle = handle;
            Init();
        }
       

        public bool RunState
        {
            get
            {
                return StartTaskList.Count > 0;
            }
        }
        public List<Task> StartTaskList { get; set; } = new List<Task>();
        public List<Task> StopTaskList { get; set; } = new List<Task>();
        public  void Stop()
        {
            cs.Cancel();
            StartTaskList=new List<Task>();
            foreach (var t in StopTaskList)
            {
                t.Start();
            }
        }
        public virtual void Start()
        {
            cs.Cancel();
            cs = new CancellationTokenSource();
            StartTaskList.Clear();
            StopTaskList.Clear();
           
            if (this.StartEvent != null)
            {
                AddStartTask(this.StartEvent);
            }
            if (this.StopEvent != null)
            {
                AddStopTask(this.StopEvent);
            }
        }

        /// <summary>
        /// 子类用override  会优先调用父类的这个方法，
        /// </summary>
        /// <param name="t_Time"></param>
        public virtual void StartBefore(D3FunSetting _d3FunSetting,D3KeyState _d3KeyState)
        {
            this.d3KeyState =_d3KeyState;
            this.d3FunSetting = _d3FunSetting;
        }

       

    
    }
}
