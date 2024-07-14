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

    [KeyName("建议1920*1080 开光环")]
    public class POEJN : BaseD3
    {
       public  const EnumD3 enumD3Name = EnumD3.POE开光环;


        List<(int, int, int)> bagPoints = new List<(int, int, int)>();
        public POEJN(D3Param d3Param, List<KeyTimeSetting> Times, EnumD3 enumD3) : base(d3Param, Times,enumD3)
        {

            this.StartEvent += POEJN_StartEvent;
        }

        public void POEGH()
        {

            var xy = this.GetPointXY();
           if( this.D3W<1920  || this.D3H<1080)
            {
                return;
            }


            int x0 = 1444, y0 = 1049;
            var list = this.Times.Where(r => r.keyClickType != KeyClickType.不做操作
            && r.D1 > 0
            && r.Rank > 0
            ).OrderBy(r => r.Rank).ToList();

            if (list.Count > 0)
            {
                var ts = list[0];
                if (ts.keyClickType == KeyClickType.颜色不匹配点击 && ValidatePointColor(ts))
                    return;
                if (ts.keyClickType == KeyClickType.颜色匹配点击 && !ValidatePointColor(ts))
                    return;
                if (ts.keyClickType == KeyClickType.图片未找到点击 && ValidateImage(ts))
                    return;
                if (ts.keyClickType == KeyClickType.图片找到点击 && !ValidateImage(ts))
                    return;
            }
            else
            {
                return;
            }

            int x1 = bagPoints[0].Item2;
            int y1=bagPoints[0].Item3;
            int x2 = bagPoints[bagPoints.Count-1].Item2;
            int y2 = bagPoints[bagPoints.Count - 1].Item3;

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
            if(picName.Length > 0)
            {
                picName.Remove(picName.Length-1);
            }

            objdm.MoveTo(x0, y0);
            Sleep(50);
            objdm.LeftClick();
            Sleep(list[0].D1);
            var ret = objdm.FindPicEx(1410, 70, 1610, 937, picName, "101010", this.d3Param.sysConfig.sim, 0);
            if(ret != null && ret.Length>0)
            {
                var ps = ret.Split("|");
                List<(int,int)> list2 = new List<(int, int)>();
                foreach (var f in ps)
                { 
                    var s3 = f.Split(',');
                    list2.Add((Convert.ToInt32(s3[1]) + list[0].Int3, Convert.ToInt32(s3[2]) + list[0].Int4));
                }
                Sleep(50);
                objdm.KeyDown(17);
               var listNew= list2.OrderBy(r=>r.Item1).ToList();
                foreach (var f in listNew)
                {
                    objdm.MoveTo(f.Item1, f.Item2);
                    Sleep(list[0].D2);
                    objdm.RightClick();
                    Sleep(30);
                }
                objdm.KeyUp(17);
            }
            //foreach (var ts in list)
            //{
            //    if (ts.keyClickType == KeyClickType.点击 && ts.KeyCode == Keys.Q && ts.KeyCode2 == Keys.Q && bagPoints.Any(r => r.Item1 == ts.Int1))
            //    {
            //        var item = bagPoints.FirstOrDefault(r => r.Item1 == ts.Int1);
            //        Sleep(ts.D1);
            //        objdm.MoveTo(x0, y0);
            //        Sleep(ts.D1);
            //        objdm.LeftClick();
            //        Sleep(ts.D1);
            //        objdm.MoveTo(item.Item2, item.Item3);
            //        Sleep(ts.D1);
            //        objdm.LeftClick();
            //        Sleep(ts.D1);
            //        if (ts != list[list.Count - 1])
            //        {
            //            objdm.KeyPress(81);//q
            //            Sleep(ts.D1);
            //        }

            //    }
            //    if (ts.keyClickType == KeyClickType.点击 && ts.KeyCode == Keys.Q && ts.KeyCode2 != Keys.Q && bagPoints.Any(r => r.Item1 == ts.Int1))
            //    {
            //        if (ts == list[list.Count - 1])
            //        {
            //            var item = bagPoints.FirstOrDefault(r => r.Item1 == ts.Int1);
            //            Sleep(ts.D1);
            //            objdm.MoveTo(x0, y0);
            //            Sleep(ts.D1);
            //            objdm.LeftClick();
            //            Sleep(ts.D1);
            //            objdm.MoveTo(item.Item2, item.Item3);
            //            Sleep(ts.D1);
            //            objdm.LeftClick();
            //            Sleep(ts.D1);
            //            if (ts != list[list.Count - 1])
            //            {
            //                objdm.KeyPress(81);//q
            //                Sleep(ts.D1);
            //            }
            //        }

            //    }
            //    if (ts.keyClickType == KeyClickType.点击 && ts.KeyCode != Keys.Q)
            //    {
            //        if (ConvertKeys.NoMouseKey(ts.KeyCode))
            //        {
            //            Sleep(ts.D1);
            //            objdm.KeyPress((int)ts.KeyCode);

            //        }
            //    }
            //}
            objdm.MoveTo(x0, y0);
            Sleep(50);
            objdm.RightClick();
            Sleep(20);
            // objdm.KeyPress(27);//ESC
            objdm.MoveTo(xy.Item1, xy.Item2);
          
        }
        public void POEJN_StartEvent()
        {
            POEGH();
            return;
            int x0 = 1444, y0 = 1049;
            var list = this.Times.Where(r => r.keyClickType != KeyClickType.不做操作 
            && r.D1>0
            && r.Rank>0
            ).OrderBy(r=>r.Rank).ToList();
            if (list.Count > 0)
            {
                var ts = list[0];
                if (ts.keyClickType == KeyClickType.颜色不匹配点击 && ValidatePointColor(ts))
                    return;
                if (ts.keyClickType == KeyClickType.颜色匹配点击 && !ValidatePointColor(ts))
                    return;
                if (ts.keyClickType == KeyClickType.图片未找到点击 && ValidateImage(ts))
                    return;
                if (ts.keyClickType == KeyClickType.图片找到点击 && !ValidateImage(ts))
                    return;
            }
            objdm.MoveTo(x0, y0);
            Sleep(50);
            objdm.LeftClick();
            Sleep(list[0].D1);
            objdm.KeyDown(17);//ctrl
            foreach (var ts in list)
            {
                if (ts.keyClickType == KeyClickType.点击 && ts.KeyCode2 != Keys.Q && ts.KeyCode == Keys.Q && bagPoints.Any(r => r.Item1 == ts.Int1))
                {
                    var item = bagPoints.FirstOrDefault(r => r.Item1 == ts.Int1);
                    objdm.MoveTo(item.Item2, item.Item3);
                    Sleep(ts.D1);
                    if (ts == list[list.Count - 1])
                    {
                        objdm.KeyUp(17);
                        Sleep(50);
                        objdm.LeftClick();
                        break;
                    }
                    else
                    {
                        objdm.LeftClick();
                    }

                }
       
            }
            foreach (var ts in list)
            {
                if (ts.keyClickType== KeyClickType.点击 && ts.KeyCode== Keys.Q && ts.KeyCode2 == Keys.Q && bagPoints.Any(r => r.Item1 == ts.Int1))
                {
                    var item = bagPoints.FirstOrDefault(r => r.Item1 == ts.Int1);
                    Sleep(ts.D1);
                    objdm.MoveTo(x0, y0);
                    Sleep(ts.D1);
                    objdm.LeftClick();
                    Sleep(ts.D1);
                    objdm.MoveTo(item.Item2,item.Item3);
                    Sleep(ts.D1);
                    objdm.LeftClick();
                    Sleep(ts.D1);
                    if (ts != list[list.Count - 1])
                    {
                        objdm.KeyPress(81);//q
                        Sleep(ts.D1);
                    }
                 
                }
                if (ts.keyClickType == KeyClickType.点击 && ts.KeyCode == Keys.Q && ts.KeyCode2 != Keys.Q && bagPoints.Any(r => r.Item1 == ts.Int1))
                {
                    if (ts == list[list.Count - 1])
                    {
                        var item = bagPoints.FirstOrDefault(r => r.Item1 == ts.Int1);
                        Sleep(ts.D1);
                        objdm.MoveTo(x0, y0);
                        Sleep(ts.D1);
                        objdm.LeftClick();
                        Sleep(ts.D1);
                        objdm.MoveTo(item.Item2, item.Item3);
                        Sleep(ts.D1);
                        objdm.LeftClick();
                        Sleep(ts.D1);
                        if (ts != list[list.Count - 1])
                        {
                            objdm.KeyPress(81);//q
                            Sleep(ts.D1);
                        }
                    }

                }
                if (ts.keyClickType == KeyClickType.点击 && ts.KeyCode != Keys.Q)
                {
                    if (ConvertKeys.NoMouseKey(ts.KeyCode))
                    {
                        Sleep(ts.D1);
                        objdm.KeyPress((int)ts.KeyCode);

                    }
                }
            }


        }

        public override void Init()
        {
            base.Init();
            int h = 65;
            int w = 66;
            int x = 1444;
            int y = 899;


            for (int i = 0; i < 15; i++)
            {

                for (int j = 0; j < 3; j++)
                {
                    int tx = x + j * w;
                    int ty = y - i * h;
                    if (tx > 0 && ty > 0)
                    {
                        bagPoints.Add((i * 3 + j + 1, tx, ty));
                    }
                }
            }
        }
       
  
    }
    
 
}
