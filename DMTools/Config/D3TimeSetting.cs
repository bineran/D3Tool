using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.Config
{

    public class D3TimeSetting
    {

        public KeyClickType keyClickType { get; set; } = KeyClickType.不做操作; 

        public int D1 { get; set; }
        public int D2 { get; set; }
        public int D3 { get; set; }
               
        public int D4 { get; set; }

        public string Str1 { get; set; }
        public string Str2 { get; set; }
        public string Str3 { get; set; }
        public string Str4 { get; set; }
        public int Int1 { get; set; }
        public int Int2 { get; set; }
        public int Int3 { get; set; }
        public int Int4 { get; set; }
        public string KeyName { get; set; }
        public string KeyInfo { get; set; }
    }
    /// <summary>
    /// 按钮点击类型
    /// </summary>
    public enum KeyClickType
    {

        /// <summary>
        /// 不做操作
        /// </summary>
        不做操作,
        /// <summary>
        /// CD好了就按
        /// </summary>
        CD好了就按,
        /// <summary>
        /// 每隔几秒按
        /// </summary>
        定时按,
        /// <summary>
        /// 按住
        /// </summary>
        按住,
        /// <summary>
        /// 按顺序
        /// </summary>
        按顺序
    }
}
