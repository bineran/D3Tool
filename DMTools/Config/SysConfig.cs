using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.Config
{
    public  class SysConfig
    {
        public string delta_color { get; set; } = "101010";
        public double sim { get; set; } = 0.5;
        public int image_size { get; set; } = 10;
        public int color_sim { get; set; } = 1;
    }
}
