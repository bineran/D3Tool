using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Image = System.Drawing.Image;

namespace DMTools
{
    public static class FileConfig
    {
        public static readonly string DM_BMP_PATH= Application.StartupPath + "Image\\bmp";
        public static readonly string DM_PNG_PATH = Application.StartupPath + "Image\\png";

        public static readonly string D3_BAG_PATH= DM_BMP_PATH + "\\bag";
        public static readonly string Poe_BAG_PATH = DM_BMP_PATH + "\\poebag";
        public static string DmBmpPath(this string imgFileName)
        {
          return  DM_BMP_PATH + "\\" + imgFileName;
        }
        public static string DmPngPath(this string imgFileName)
        {
            return DM_PNG_PATH + "\\" + imgFileName;
        }
        public static Image ImageFromDm_Bmp_Path(this string imgFileName)
        {
            return Image.FromFile(imgFileName.DmBmpPath());
        }
    }
}
