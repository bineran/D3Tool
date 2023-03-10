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
        public string TSRemark { get; set; } = "";
        /// <summary>
        ///顺序执行,大于0且是点击的才执行
        /// </summary>
        public int Rank { get; set; } =0;


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
        public Keys KeyCode { get; set; }
    }
    /// <summary>
    /// 按钮点击类型
    /// </summary>
    public enum KeyClickType
    {
        /// <summary>
        /// 不做操作
        /// </summary>
        不做操作=0,
        /// <summary>
        /// 每隔几秒按
        /// </summary>
        点击=10,
        /// <summary>
        /// 按住
        /// </summary>
        按下=20,
        /// <summary>
        /// 按住
        /// </summary>
        弹起=30,
        /// <summary>
        /// 颜色匹配按键
        /// </summary>
        颜色匹配点击 = 40,
        /// <summary>
        /// 颜色不匹配按键
        /// </summary>
        颜色不匹配点击 = 50,
        /// <summary>
        /// 图片找到点击
        /// </summary>
        图片找到点击 = 60,
        /// <summary>
        /// 图片未找到点击
        /// </summary>
        图片未找到点击 = 70
    }
    
}
