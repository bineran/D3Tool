using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.Config
{

    public class D3TimeSetting
    {

        public KeyClickType keyClickType { get; set; } = KeyClickType.None;

        public decimal Timer1 { get; set; }
        public decimal Timer2 { get; set; }
        public decimal Timer3 { get; set; }

        public decimal Timer4 { get; set; }

        public string Str1 { get; set; }
        public string Str2 { get; set; }
        public string Str3 { get; set; }
        public string Str4 { get; set; }
        public string Int1 { get; set; }
        public string Int2 { get; set; }
        public string Int3 { get; set; }
        public string Int4 { get; set; }
    }
    /// <summary>
    /// 按钮点击类型
    /// </summary>
    public enum KeyClickType
    { 
        /// <summary>
        /// 不做操作
        /// </summary>
        None,
        /// <summary>
        /// CD好了就按
        /// </summary>
        CD,
        /// <summary>
        /// 每隔几秒按
        /// </summary>
        SLEEEP,
        /// <summary>
        /// 按住
        /// </summary>
        DOWN,
        /// <summary>
        /// 按顺序
        /// </summary>
        Sort
    }
}
