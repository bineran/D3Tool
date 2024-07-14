using DMTools.libs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using DMTools.Config;
using DMTools.Control;
//using Dm;
using Idmsoft = DMTools.libs.Idmsoft;
using System.Diagnostics;
using System.IO;
namespace DMTools.FunList
{

    [KeyName(@"
时间1：速度 建议100以内，
文本1：重铸石图片
文本2：机会石图片
整数1：背包范围左上角X
整数2：背包范围左上角Y
整数3：背包范围右下角X
整数4：背包范围右下角Y
启动的时候：最发保证地图是白色的。然后打开背包。鼠标放在图上然后再启动
")]
    public class POEFF : BaseD3
    {
       public  const EnumD3 enumD3Name = EnumD3.POE点腐化地图;


        List<(int, int, int)> bagPoints = new List<(int, int, int)>();
        public POEFF(D3Param d3Param, List<KeyTimeSetting> Times, EnumD3 enumD3) : base(d3Param, Times,enumD3)
        {

            this.StartEvent += POEJN_StartEvent;
        }

        public string CheckClipboardMemo()
        {
            objdm.KeyDown(17);
            objdm.KeyPress((int)Keys.C);
            objdm.KeyUp(17);
            return objdm.GetClipboard();
 

        }

        public void POEJN_StartEvent()
        {
            var dicinfo = new System.IO.DirectoryInfo(FileConfig.Poe_GH_PATH);
            objdm.SetPath(FileConfig.Poe_GH_PATH);
            string picName = "";
            foreach (var f in dicinfo.GetFiles("*.png"))
            {
                if (f.Extension == ".png")
                {
                    var newName = f.Name.ToLower().Trim().Replace(".png", ".bmp");
                    if (!File.Exists(newName))
                    {
                        objdm.ImageToBmp(f.Name, newName);
                        System.IO.File.Delete(f.FullName);
                    }

                }


            }

            foreach (var f in dicinfo.GetFiles("*.bmp"))
            {
                if (f.Extension == ".png")
                {
                    var newName = f.Name.ToLower().Trim().Replace(".png", ".bmp");
                    if (!File.Exists(newName))
                        objdm.ImageToBmp(f.Name, newName);

                }
                if (f.Extension == ".bmp")
                {
                    picName += f.Name + "|";
                }


            }
            if (this.Times.Count == 0)
            {
                return;
            }
            var xy = this.GetPointXY();
            var ks1 = this.Times[0];
            int x0 = xy.Item1, y0 = xy.Item2;
            var  czs = ks1.Str1;
            var jhs = ks1.Str2;

            object ox1;
            object oy1;
            object ox2;
            object oy2;
            while (true)
            {
                var ret1 = objdm.FindPic(ks1.Int1, ks1.Int2, ks1.Int3, ks1.Int4, czs, "030303", this.d3Param.sysConfig.sim, 0, out ox1, out oy1);
                var ret2 = objdm.FindPic(ks1.Int1, ks1.Int2, ks1.Int3, ks1.Int4, jhs, "030303", this.d3Param.sysConfig.sim, 0, out ox2, out oy2);
                if (ret1 > -1 && ret2 > -1)
                {
                    int x1, y1, x2, y2;
                    x1 = Convert.ToInt32(ox1);
                    y1 = Convert.ToInt32(oy1);
                    x2 = Convert.ToInt32(ox2);
                    y2 = Convert.ToInt32(oy2);
                    Sleep(ks1.D1);
                    objdm.MoveTo(x1, y1);//移动重铸石
                    Sleep(ks1.D2);
                    var strMemo = CheckClipboardMemo();
                    if (!strMemo.Contains("重铸石"))
                    {
                        continue;
                    }

                    objdm.SetClipboard("");
                    objdm.RightClick();
                    Sleep(ks1.D1);
                    objdm.MoveTo(x0, y0);//移动到图上
                    Sleep(ks1.D2);

                    objdm.LeftClick();
                    Sleep(ks1.D2);
                    objdm.MoveTo(x2, y2);//移动机会石
                    Sleep(ks1.D2 );
                    strMemo = CheckClipboardMemo();
                    if (!strMemo.Contains("机会石"))
                    {
                        continue;
                    }

                    objdm.SetClipboard("");
                    objdm.RightClick();
                    Sleep(ks1.D2);
                    objdm.MoveTo(x0, y0);//移动到图上
                    Sleep(ks1.D1);
                    objdm.LeftClick();
                    Sleep(ks1.D2);
                }
                else
                {
                    break;
                }
            }


        }



    }
    
 
}
