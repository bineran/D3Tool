using Dm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using DMTools.Config;

namespace DMTools.FunList
{
   
    public class D3FJ:BaseD3
    {
       public const EnumD3 enumD3Name = EnumD3.分解传奇;

        SortedList<int, BagPoint> bagPointList;

        public D3FJ(Idmsoft objdm, int handle) : base(objdm, handle)
        {
            this.StartEvent += D3FJ_StartEvent;
        }

  

        private void D3FJ_StartEvent()
        {
            var xy = this.GetPointXY();

            foreach (var item in bagPointList)
            {
                if (item.Key == 1)
                {
                    objdm.MoveTo(item.Value.centerX, item.Value.centerY);
                }
                if (isInventorySpaceEmpty(item.Key))
                {
                    continue;
                }

                objdm.MoveTo(item.Value.centerX, item.Value.centerY);
                Thread.Sleep(20);
                objdm.LeftClick();
                Thread.Sleep(20);
                var maxtime = DateTime.Now.AddMilliseconds(200);
                while (true)
                {

                    if (isDialogBoXOnScreen())
                    {
                        objdm.KeyPress(13);
                        Thread.Sleep(10);
                    }
                    if (DateTime.Now > maxtime)
                    {
                        break;
                    }
                    if (isInventorySpaceEmpty(item.Key))
                    {
                        break;
                    }
                    Thread.Sleep(10);
                }
            }
            objdm.MoveTo(xy.Item1, xy.Item2);
        }

        public override void StartBefore(D3FunSetting d3FunSetting)
        {
            bagPointList = new SortedList<int, BagPoint>();
            for (int i = 1; i <= 60; i++)
            {
                var bp = getInventorySpaceXY(D3W, D3H, i, "bag");
                bagPointList.Add(i, bp);
            }
        }
      
 
 

        bool isDialogBoXOnScreen()
        {

            int x1 = D3W / 2 - (3440 / 2 - 1655) * D3H / 1440;
            int y1 = 500 * D3H / 1440;

            int x2 = D3W / 2 + (3440 / 2 - 1800) * D3H / 1440;
            int y2 = 500 * D3H / 1440;
            var c1 = GetPointRGB(x1, y1);
            var c2 = GetPointRGB(x2, y2);
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
        bool isInventorySpaceEmpty(int ID)
        {
            int _spaceSizeInnerW = 64;
            int _spaceSizeInnerH = 63;
            var m = bagPointList[ID];

            var c = GetPointRGB(Convert.ToInt32(Math.Round(m.leftX + 0.2 * _spaceSizeInnerW)), Convert.ToInt32(Math.Round(m.leftY + 0.2 * _spaceSizeInnerH)));


            if (c.Item1 > 25 || c.Item2 > 25 || c.Item3 > 25)
            {
                return false;
            }
            return true;
        }

  
    }
    public class BagPoint
    {
        public int centerX { get; set; }
        public int centerY { get; set; }
        public int leftX { get; set; }
        public int leftY { get; set; }
    }
}
