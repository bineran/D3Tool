using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Image = System.Drawing.Image;

namespace DMTools.Config
{

    public class KeyTimeSetting
    {

        public KeyTimeSetting() { KeyCode2 = Keys.None; }
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
        public Keys KeyCode2 { get; set; }
        [JsonIgnore]
        public Image ImageFile { get; set; }


    }

    
}
