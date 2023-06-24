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
using System.Diagnostics;
using Idmsoft = DMTools.libs.Idmsoft;

namespace DMTools.FunList
{
    public  abstract partial class BaseD3 : ID3Function
    {
        //public  const  EnumD3 enumD3Name  = EnumD3.默认;

        public  EnumD3 enumD3 { get; set; } = EnumD3.默认;
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
        public List<KeyTimeSetting> Times { get; set; }

        public CancellationTokenSource cs { get; set; }
        
        public BaseD3(D3Param _d3Param, List<KeyTimeSetting> Times,EnumD3 enumD3)
        {
            this.cs=new CancellationTokenSource();
            this.Times= Times;
            this.d3Param = _d3Param;
            this.enumD3 = enumD3;
            stopwatch = new Stopwatch();
            Init();
        }
    


        public bool RunState  
        { 
            get 
            {
                if (StartThreadList.Count > 0)
                {
                    return true;
                }
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
        public List<Thread> StartThreadList { get; set; } = new List<Thread>();

        public  void Stop()
        {
            stopwatch.Stop();
            if(stopwatch.ElapsedMilliseconds>0)
                log.Info($"------运行时长： {stopwatch.ElapsedMilliseconds * 1.0 / 1000}秒,     {stopwatch.ElapsedMilliseconds}毫秒");
            //log.Info($"============Stop  begin ");
            cs.Cancel();
            //for (int i = 0; i < StartTaskList.Count; i++)
            //{
            //    log.Info($"i={i}, Status:{StartTaskList[i].Status}");
            //}
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
            foreach (var t in StartThreadList)
            {
                try
                {
                    t.Abort();
                }
                catch
                { 
                    
                }
            }
            StartThreadList=new List<Thread>();
            //log.Info($"============Stop  end ");
        }
       private System.Diagnostics.Stopwatch stopwatch { get; set; }
        public virtual void Start()
        {
            stopwatch.Restart();
           // log.Info($"***Start  begin ");
            cs.Cancel();
            Task.WaitAll(StartTaskList.ToArray());
            cs = new CancellationTokenSource();
            StartTaskList.Clear();
            StopTaskList.Clear();
            this.d3KeyState.Rest();
            if (this.StartEvent != null)
            {
                var task = StartNewTask(this.StartEvent,true);
                StartTaskList.Add(task);
            }
            if (this.StopEvent != null)
            {
                AddStopTask(this.StopEvent);
        
            }
           // log.Info($"====Start  end ");
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
