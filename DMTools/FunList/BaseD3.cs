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
using System.Runtime.Intrinsics.Arm;
//using Dm;

namespace DMTools.FunList
{
    public  abstract partial class BaseD3 : ID3Function
    {
        //public  const  EnumD3 enumD3Name  = EnumD3.默认;

        public virtual EnumD3 enumD3 { get; set; } = EnumD3.默认;
        public readonly Logger log=LogManager.GetCurrentClassLogger();
        public virtual event Action StartEvent;
        public virtual event Action StopEvent;
        public DMP objDMP { get; set; } =new DMP();
        public Idmsoft objdm { get { return this.objDMP.DM; } }
        public Idmsoft CreateDM()
        {
            DMP dMP=new DMP();
            Idmsoft objdm = dMP.DM;
            return this.BindForm(objdm, this.Handle,this.d3Param.sysConfig);
        }
        public  Idmsoft BindForm(Idmsoft objdm, int handle, SysConfig sysConfig)
        {
            try
            {
                //if (display == "normal" && display == "normal" && display == "normal" && mode == 0)
                //{
                //    return;
                //}
                if (objdm.IsBind(handle) == 1)
                {
                    objdm.UnBindWindow();
                }
                objdm.delay(50);

                var c = objdm.BindWindow(handle, sysConfig.display, sysConfig.mouse, sysConfig.keypad, sysConfig.mode);


            }
            catch (Exception e)
            {

            }
            return objdm;

        }
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
        public FunTaskParam funTaskParam { get; set; }
        public CancellationTokenSource cs { get; set; }
        
        public BaseD3(D3Param _d3Param,EnumD3 enumD3)
        {
            this.cs=new CancellationTokenSource();

            this.d3Param = _d3Param;
            this.enumD3 = enumD3;
            funTaskParam=new FunTaskParam();
            funTaskParam.Handle = d3Param.Handle;
            funTaskParam.sysConfig = d3Param.sysConfig;
            funTaskParam.cancellationTokenSource = this.cs;
            Init();
        }
    


        public bool RunState  
        { 
            get 
            {
                if (this.StartTaskList.Count == 0)
                    return false;
                var x = 0;
                for (int i=0;i< this.StartTaskList.Count;i++)
                {
                    var item = this.StartTaskList[i];
                    if (item.Status == TaskStatus.RanToCompletion)
                    {
                        x++;
                    }
                }
             
                return this.StartTaskList.Count > 0 && this.StartTaskList.Count!=x; 
            } 
        } 

       
        public List<Task> StartTaskList { get; set; } = new List<Task>();
        public List<Task> StopTaskList { get; set; } = new List<Task>();
        public  void Stop()
        {
            log.Info($"============Stop  begin ");
            cs.Cancel();
            for (int i = 0; i < StartTaskList.Count; i++)
            {
                log.Info($"i={i}, Status:{StartTaskList[i].Status}");
            }
            Task.WaitAll(StartTaskList.ToArray());
            StartTaskList=new List<Task>();
            if (StopTaskList.Count > 0)
            {
                var t1=DateTime.Now;
               // log.Info("Stop11111=====" + DateTime.Now.ToString());
                foreach (var t in StopTaskList)
                {
                    t.Start();
                }
                Task.WaitAll(StopTaskList.ToArray());
              //  log.Info("Stop22222=====" + DateTime.Now.ToString());

                StopTaskList =new List<Task>();
            }
            log.Info($"============Stop  end ");
        }
        public virtual void Start()
        {
            log.Info($"============Start  begin ");
            cs.Cancel();
            Task.WaitAll(StartTaskList.ToArray());
            cs = new CancellationTokenSource();
            StartTaskList.Clear();
            StopTaskList.Clear();
            if (this.StartEvent != null)
            {
                var task = StartNewTask(this.StartEvent,true);
                StartTaskList.Add(task);
            }
            if (this.StopEvent != null)
            {
                AddStopTask(this.StopEvent);
        
            }
            log.Info($"============Start  end ");
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
