using DMTools.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.Config
{
    public class D3Config
    {
        /// <summary>
        /// 方案名称
        /// </summary>
        public string ConfigName { get; set; }
        public List<D3ConfigItem> d3ConfigItems { get; set; }



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

        public List<string> strfunList { get; set; }

        public List<D3TimeSetting> d3TimeSettings { get; set; }
    }
}
