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
namespace DMTools.FunList
{

    [KeyName("建议1920*1080 打开他分解对话框，并点击分解按钮后执行")]
    public class D3FJ:BaseD3
    {
       public  const EnumD3 enumD3Name = EnumD3.分解传奇;

       private SortedList<int, BagPoint> bagPointList=new SortedList<int, BagPoint>();

        public D3FJ(D3Param d3Param, List<KeyTimeSetting> Times, EnumD3 enumD3) : base(d3Param, Times,enumD3)
        {
            this.StartEvent += D3FJ_StartEvent;

        }
        public override void Init()
        {
            base.Init();
            bagPointList = new SortedList<int, BagPoint>();
            for (int i = 1; i <= 60; i++)
            {
                var bp = getInventorySpaceXY(D3W, D3H, i, "bag");
                bagPointList.Add(i, bp);
            }
        }
        /// <summary>
        /// 判断是否打开储物箱
        /// </summary>
        /// <returns></returns>
        public bool CheckCWX( Idmsoft objdm )
        {
            List<PointCheckColor> points = new List<PointCheckColor>();
            points.AddRange(new[]{
                new PointCheckColor(287, 58, "15af60"),
                new PointCheckColor(274, 93, "15ad71"),
                new PointCheckColor(240, 71, "10ba7b")
            });
            return CheckPointListColor(points);

        }
        /// <summary>
        /// 赌博
        /// </summary>
        /// <returns></returns>
        public bool CheckKLX(Idmsoft objdm )
        {
            List<PointCheckColor> points = new List<PointCheckColor>();
            points.AddRange(new[]{
            new PointCheckColor(85,38, "1f0600"),
            new PointCheckColor(64,70, "240600"),
            new PointCheckColor(106,97, "240803")
            });
            return CheckPointListColor(points);

        }

        public bool CheckPointListColor( List<PointCheckColor> points)
        {
            List<Task<bool>   > tasks = new List<Task<bool>>();
         
            foreach (var point in points)
            {
              
                //var sss = objdm.GetColor(NewX(point.x), NewY(point.y));
                tasks.Add(Task.Run(() =>
                {
                    //var objdm = this.CreateDM();
                    return objdm.GetColor(NewX(point.x), NewY(point.y)) == point.color;
                }));
                //ssss += "," + sss;
            }
            Task.WaitAll(tasks.ToArray());
            return tasks.All(r => r.Result);
        }

        private void D3FJ_StartEvent()
        {
            //var objdm=this.CreateDM();
            var xy = this.GetPointXY();
            if (this.CheckCWX(objdm) || this.CheckKLX(objdm))
            {
                AddRightClickForTask(new KeyTimeSetting() { keyClickType = KeyClickType.点击, D1 = 15 });
            }
            else
            {
                ZBFJ(objdm);
            }
        
            objdm.MoveTo(xy.Item1, xy.Item2);

        }
 

        /// <summary>
        /// 校验是否是背包中的物品
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="size"></param>
        /// <param name="picname"></param>
        /// <returns></returns>
        public bool CheckNoFJImage(Idmsoft objdm, int x,int y,int size,string picname)
        {
            object outx;
            object outy;
           var ret= objdm.FindPic(x - size, y - size, x + size, y + size, picname, "000000", 0.9, 0, out outx, out outy);
            if(ret > -1) {
                return true;
            }
            return false;
        }
        public string BagImage()
        {
            var di = new  DirectoryInfo(FileConfig.D3_BAG_PATH);
           var fs= di.GetFiles("*.bmp");
            var str = "";
            foreach (var f in fs)
            {
                if (str.Length > 0)
                {
                    str += "|";
                }
                str += f.Name;
            }
            return str;
        }
        public void ZBFJ(Idmsoft objdm)
        {
            objdm.SetPath(FileConfig.D3_BAG_PATH);
            var picname=BagImage();
            foreach (var item in bagPointList)
            {
                if (item.Key == 1)
                {
                    objdm.MoveTo(item.Value.centerX, item.Value.centerY);
                }
                if (isInventorySpaceEmpty(objdm, item.Key))
                {
                    continue;
                }
                if (CheckNoFJImage(objdm,item.Value.centerX, item.Value.centerY, 11, picname))
                {
                    continue;
                }

                objdm.MoveTo(item.Value.centerX, item.Value.centerY);
                Sleep(20);
                objdm.LeftClick();
                Sleep(20);
                var maxtime = DateTime.Now.AddMilliseconds(200);
                while (true)
                {

                    if (isDialogBoXOnScreen(objdm))
                    {
                        objdm.KeyPress(13);
                        Sleep(10);
                    }
                    if (DateTime.Now > maxtime)
                    {
                        //var imgaize = 5;
                        //var filename = "aaa";
                        //objdm.Capture(item.Value.centerX - imgaize, item.Value.centerY - imgaize,
                        //    item.Value.centerX + imgaize,
                        //    item.Value.centerY + imgaize, filename);
                        break;
                    }
                    if (isInventorySpaceEmpty(objdm, item.Key))
                    {
                        break;
                    }
                    Sleep(10);
                }
            }
        }
        bool isDialogBoXOnScreen(Idmsoft objdm)
        {

            int x1 = D3W / 2 - (3440 / 2 - 1655) * D3H / 1440;
            int y1 = 500 * D3H / 1440;

            int x2 = D3W / 2 + (3440 / 2 - 1800) * D3H / 1440;
            int y2 = 500 * D3H / 1440;
            var c1 = GetPointRGB(  x1, y1);
            var c2 = GetPointRGB( x2, y2);
            if (c1.Item1 > c1.Item2 && c1.Item2 > c1.Item3 && c1.Item3 < 5 && c1.Item2 < 15 && c1.Item1 > 25 && c2.Item1 > c2.Item2 && c2.Item2 > c2.Item3 && c2.Item3 < 5 && c2.Item2 < 15 && c2.Item1 > 25)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        /// <summary>
        /// [格子中心x，格子中心y，格子左上角x，格子左上角y]
        /// </summary>
        /// <param name="D3W"></param>
        /// <param name="D3H"></param>
        /// <param name="ID">(1-60 不能传0)</param>
        /// <param name="zone">bag or zone</param>
        /// <returns></returns>
        private BagPoint getInventorySpaceXY(int D3W, int D3H, int ID, string zone)
        {
            int _spaceSizeInnerW = 64;
            int _spaceSizeInnerH = 63;
            int[] _spaceBagX = new int[10] { 2753, 2820, 2887, 2954, 3021, 3089, 3156, 3223, 3290, 3357 };
            int[] _spaceBagY = new int[6] { 747, 813, 880, 946, 1013, 1079 };
            int[] _spaceKanaiX = new int[3] { 242, 318, 394 };
            int[] _spaceKanaiY = new int[3] { 503, 579, 655 };
            int x1, y1, x2, y2, targetColumn, targetRow;
            if (zone == "bag")
            {
                targetColumn = (ID % 10 == 0 ? 10 : ID % 10) - 1;
                targetRow = ID % 10 == 0 ? ID / 10 - 1 : ID / 10;
                x1 = Convert.ToInt32(Math.Round(D3W - ((3440 - _spaceBagX[targetColumn] - _spaceSizeInnerW / 2) * D3H / 1440d)));
                y1 = Convert.ToInt32(Math.Round((_spaceBagY[targetRow] + _spaceSizeInnerH / 2) * D3H / 1440d));
                x2 = Convert.ToInt32(Math.Round(D3W - ((3440 - _spaceBagX[targetColumn]) * D3H / 1440d)));
                y2 = Convert.ToInt32(Math.Round((_spaceBagY[targetRow]) * D3H / 1440d));
            }
            else
            {//魔盒
                targetColumn = (ID % 3 == 0 ? 3 : ID % 3) - 1;
                targetRow = ID % 3 == 0 ? ID / 3 - 1 : ID / 3;
                x1 = Convert.ToInt32(Math.Round(D3W - ((3440 - _spaceKanaiX[targetColumn] - _spaceSizeInnerW / 2) * D3H / 1440d)));
                y1 = Convert.ToInt32(Math.Round((_spaceKanaiY[targetRow] + _spaceSizeInnerH / 2) * D3H / 1440d));
                x2 = Convert.ToInt32(Math.Round(D3W - ((3440 - _spaceKanaiX[targetColumn]) * D3H / 1440d)));
                y2 = Convert.ToInt32(Math.Round((_spaceKanaiY[targetRow]) * D3H / 1440d));
            }
            return new BagPoint() { centerX = x1, centerY = y1, leftX = x2, leftY = y2 };
        }
       
        /// <summary>
        /// 判断是否空背包
        /// </summary>
        /// <param name="D3W"></param>
        /// <param name="D3H"></param>
        /// <param name="ID"></param>
        /// <param name="zone"></param>
        /// <returns></returns>
        bool isInventorySpaceEmpty(Idmsoft objdm, int ID)
        {
            int _spaceSizeInnerW = 64;
            int _spaceSizeInnerH = 63;
            var m = bagPointList[ID];

            var c = GetPointRGB( Convert.ToInt32(Math.Round(m.leftX + 0.2 * _spaceSizeInnerW)), Convert.ToInt32(Math.Round(m.leftY + 0.2 * _spaceSizeInnerH)));


            if (c.Item1 > 25 || c.Item2 > 25 || c.Item3 > 25)
            {
                return false;
            }
            return true;
        }

  
    }
    public class PointCheckColor
    {
        public PointCheckColor(int x, int y, string color)
        { 
            this.color = color;
            this.x= x;
            this.y= y;
        }
        public int x { get; set; }
        public int y { get; set; }
        public string color { get; set; }
    }
    public class BagPoint
    {

        public int ID { get; set; }
        public int centerX { get; set; }
        public int centerY { get; set; }
        public int leftX { get; set; }
        public int leftY { get; set; }
        public int rightX { get; set; }
        public int rightY { get; set; }
    }
}
