
using DMTools.Config;
using DMTools.FunList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DMTools.PlugIn;
namespace DMTools.Control
{
    public partial class D3Fun
    {
        public D3Param d3Param { get; set; }
        public  SortedList<EnumD3, ID3Function> slD3Function = new SortedList<EnumD3, ID3Function>();
        public SortedList<EnumD3, List<D3TimeSetting>> Times { get; set; } = new SortedList<EnumD3, List<D3TimeSetting>>();
        
        public D3Fun(params EnumD3[] enumD3s)
        {
            InitD3Function();
            if (enumD3s != null)
            {
                foreach (var e in enumD3s)
                {
                    if (slD3Function.ContainsKey(e))
                    {
                        this.funList.Add(slD3Function[e]);
                    }
                }
            }
        }
        public D3Fun(D3Param _d3Param,params EnumD3[] enumD3s)
        {
            this.d3Param= _d3Param;
            InitD3Function();
            if (enumD3s != null)
            { 
                foreach(var e in enumD3s)
                {
                    if (slD3Function.ContainsKey(e))
                    {
                        this.funList.Add(slD3Function[e]);
                    }
                }
            }
        }
        public void InitD3Function()
        {
            slD3Function = new SortedList<EnumD3, ID3Function>();
            var types = Assembly.GetAssembly(typeof(BaseD3)).GetTypes()
                         .Where(r => r.BaseType == typeof(BaseD3) && !r.IsInterface
                                                 && !r.IsAbstract);
            this.Times = this.d3Param.SLTimes;
            foreach (var t in types)
            {
                var field = t.GetField("enumD3Name");
                if (field == null) continue;
                var enumD3 = (EnumD3)field.GetRawConstantValue();
                if (!slD3Function.ContainsKey(enumD3))
                {
                    var d3Function = Activator.CreateInstance(t, this.d3Param, enumD3) as ID3Function;
                    if (d3Function != null)
                    {
                        slD3Function.Add(enumD3, d3Function);
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

        public Idmsoft objdm { get { return this.d3Param.objdm; } }
        public int Handle { get { return this.d3Param.Handle; } }
        



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
