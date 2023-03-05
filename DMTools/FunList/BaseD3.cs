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
using DMTools.Control;

namespace DMTools.FunList
{
    public  abstract partial class BaseD3 : ID3Function
    {
        public const EnumD3 enumD3Name  = EnumD3.默认;
        public readonly Logger log=LogManager.GetCurrentClassLogger();
        public virtual event Action StartEvent;
        public virtual event Action StopEvent;
        public Idmsoft objdm { get { return this.d3Param.objdm; } }
        public int Handle { get { return this.d3Param.Handle; } }
        public D3Param d3Param { get; set; }
        public D3KeyState d3KeyState { get { return d3Param.d3KeyState; } }
        public D3Timers d3TimerSetting { get { return d3Param.d3Timers; } }
        CancellationTokenSource cs = new CancellationTokenSource();
        
        public BaseD3(D3Param _d3Param)
        {
            this.d3Param = _d3Param;
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
            Task.WaitAll(StartTaskList.ToArray());
            StartTaskList=new List<Task>();
            if (StopTaskList.Count > 0)
            {
                foreach (var t in StopTaskList)
                {
                    t.Start();
                }
                Task.WaitAll(StopTaskList.ToArray());
                StopTaskList=new List<Task>();
            }


        }
        public virtual void Start()
        {
            cs.Cancel();
            Task.WaitAll(StartTaskList.ToArray());
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
        public virtual void StartBefore(D3Param _d3Param)
        {
            this.d3Param = _d3Param;
        }




    }
}
