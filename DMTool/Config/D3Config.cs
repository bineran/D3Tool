using DMTool.Control;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMTool.Config
{
    public class D3Config
    {
        [JsonIgnore]
        public List<Keys> ALLHotKeys { get; set; }=new List<Keys>();
        [JsonIgnore]
        public string FilePath { get; set; }
        /// <summary>
        /// 方案名称
        /// </summary>
        public string ConfigName { get; set; }
        public List<D3ConfigItem> d3ConfigItems { get; set; }=new List<D3ConfigItem>();

        public List<D3ConfigKey>   ConfigKeys { get; set; }=new List<D3ConfigKey>();
        public string WindowClass { get; set; } = "";
    }
    public class D3ConfigItem
    {
        public string ItemName { get; set; }
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
        public bool EnabledFlag { get; set; } = true;
        public Keys HotKey1 { get; set; }
        public Keys HotKey2 { get; set; }
        
        public List<D3ConfigFun> d3ConfigFuns { get; set; } = new List<D3ConfigFun>();

    }
    public class D3ConfigFun
    {
        public EnumD3 enumD3 { get; set; }

        public bool EnableFlag { get; set; }
        public List<D3TimeSetting> Times { get; set; }=new List<D3TimeSetting>();
    }
    public class D3ConfigKey
    { 
        public string KeyInfo { get; set; }
        public string KeyName { get; set; }
        public Keys KeyCode { get; set; }
    }
}
