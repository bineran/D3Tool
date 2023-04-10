
using DMTools.Config;
using DMTools.FunList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DMTools.libs;
using Microsoft.VisualBasic.Logging;
using NLog.Fluent;

namespace DMTools.Control
{
    public partial class D3Fun
    {
        NLog.Logger log= NLog.LogManager.GetCurrentClassLogger();
        public const string enumD3Name = "enumD3Name";
        public D3Param d3Param { get; set; }
        public  List<(D3ConfigFun, ID3Function)> slD3Function = new List<(D3ConfigFun, ID3Function)>();

       
        public D3Fun(D3Param _d3Param)
        {
            this.d3Param= _d3Param;
            InitD3Function();

            foreach (var e in slD3Function)
            {
                this.funList.Add(e.Item2);
            }
            
        }
        public void InitD3Function()
        {
            slD3Function = new List<(D3ConfigFun, ID3Function)>();

            var types = Assembly.GetAssembly(typeof(BaseD3)).GetTypes()
                         .Where(r => r.BaseType == typeof(BaseD3) && !r.IsInterface
                                                 && !r.IsAbstract);

            foreach (var d3ConfigFun in this.d3Param.ConfigFuns)
            {
                var t = types.FirstOrDefault(r => r.GetField(D3Fun.enumD3Name)?.GetRawConstantValue() != null
                  && ((EnumD3)r.GetField(D3Fun.enumD3Name)?.GetRawConstantValue()) == d3ConfigFun.enumD3);
                if (t != null)
                {
                   
                    var d3Function = Activator.CreateInstance(t, this.d3Param, d3ConfigFun.Times, d3ConfigFun.enumD3) as ID3Function;
                    if (d3Function != null)
                    {
                        slD3Function.Add((d3ConfigFun, d3Function));
                    }
                }

            }
        }
        /// <summary>
        /// 启动前先停止其它功能 默认为true
        /// </summary>
        public bool StartBeforeStopOther { get; set; } = true;
        /// <summary>
        /// 阻止其它功能停止此功能 默认为true
        /// </summary>
        public bool OtherStopFlag { get; set; } = false;
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool EnabledFlag { get; set; }


        public int Handle { get { return this.d3Param.Handle; } }
        



        public void StartAndStop()
        {
            if (this.RunState)
            {
                log.Info("StartAndStop.Stop");
                Stop();
            }
            else
            {
                log.Info("StartAndStop.Start");
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
        
            foreach (var f in funList)
            {

                f.Start();
            }
        }
        public void Stop()
        {
       
            foreach (var f in funList)
            {
                f.Stop();
            }

        }
        
 
        public List<ID3Function> funList { get; set; }=new List<ID3Function>();
  
        public Keys HotKey1
        {
            get;set;
        }
        public Keys HotKey2
        {
            get;set;
        }

    }
}
