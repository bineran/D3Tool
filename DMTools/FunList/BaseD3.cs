using DMTools.libs;
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
using System.Reflection.Metadata;
namespace DMTools.FunList
{
    public  abstract partial class BaseD3 : ID3Function
    {
        //public  const  EnumD3 enumD3Name  = EnumD3.默认;

        public virtual EnumD3 enumD3 { get; set; } = EnumD3.默认;
        public readonly Logger log=LogManager.GetCurrentClassLogger();
        public virtual event Action StartEvent;
        public virtual event Action StopEvent;
        public DMP objDMP { get; set; } = new DMP();
        public Idmsoft objdm { get { return this.objDMP.DM; } }
        public int Handle { get { return this.d3Param.Handle; } }
        public D3Param d3Param { get; set; }
        public D3KeyState d3KeyState { get { return d3Param.d3KeyState; } }
        public List<KeyTimeSetting> Times
        {
            get
            {
                return this.d3Param.SLTimes[this.enumD3];
            }
        }
        CancellationTokenSource cs = new CancellationTokenSource();
        
        public BaseD3(D3Param _d3Param,EnumD3 enumD3)
        {
            this.d3Param = _d3Param;
            this.enumD3 = enumD3;
            Init();
        }
        public Idmsoft CreateAndBindDm()
        {
            DMP dMP = new DMP();
            dMP.DM.SetShowErrorMsg(0);
            D3Main.BindForm(dMP.DM, this.Handle);
            return dMP.DM;
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
