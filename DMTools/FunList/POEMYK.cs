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

    [KeyName("建议1920*1080 POE开卡")]
    public class POEMYK : BaseD3
    {
       public  const EnumD3 enumD3Name = EnumD3.POE开卡;

       private SortedList<int, BagPoint> bagPointList=new SortedList<int, BagPoint>();

        public POEMYK(D3Param d3Param, List<KeyTimeSetting> Times, EnumD3 enumD3) : base(d3Param, Times,enumD3)
        {
            this.StartEvent += D3FJ_StartEvent;

        }
        public override void Init()
        {
            base.Init();
            bagPointList = new SortedList<int, BagPoint>();
            for (int i = 1; i <= 60; i++)
            {
                var bp = getInventorySpaceXY(D3W, D3H, i);
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
                new PointCheckColor(1835,383, "212420"),
                new PointCheckColor(1292,387, "110705"),
                new PointCheckColor(1661,145, "1b1e1a"),
                new PointCheckColor( 398,67, "9b7b43"),
                new PointCheckColor( 535,43, "756151"),
                new PointCheckColor(  601,26, "deae74")
              

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
            ZBFJ((xy.Item1, xy.Item2));
            objdm.MoveTo(xy.Item1, xy.Item2);

        }


        public void ZBFJ((int x, int y) xy)
        {
            var t0 = new KeyTimeSetting() { Int1 = xy.x, Int2 = xy.y, D1 = 150,D2=50 };
            var ts = this.Times.Count > 0 ? this.Times[0] : t0;
            while (!cs.IsCancellationRequested)
            {
                foreach (var item in bagPointList)
                {
                    if (isInventorySpaceEmpty(item.Key))
                    {
                        continue;
                    }
                    objdm.SetClipboard("");

                    objdm.KeyDown(17);
                    objdm.MoveTo(item.Value.centerX, item.Value.centerY);
                    Sleep(ts.D2);
                    objdm.KeyPress((int)Keys.C);
                    objdm.KeyUp(17);
                    Sleep(ts.D2);


                    var str = objdm.GetClipboard();
                    if (str.StartsWith("物品类别: 命运卡"))
                    {
                        objdm.KeyDown(17);
                        Sleep(ts.D2);
                        objdm.LeftClick();
                        objdm.KeyUp(17);
                    }

                }

                foreach (var item in bagPointList)
                {
                    if (!isInventorySpaceEmpty(item.Key))
                    {
                        continue;
                    }

                    objdm.MoveTo(xy.x,xy.y);
                    Sleep(ts.D1);
                    objdm.RightClick();
                    objdm.MoveTo(item.Value.centerX, item.Value.centerY);
                    Sleep(ts.D1);
                    objdm.LeftClick();

                }

             
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
        private BagPoint getInventorySpaceXY(int D3W, int D3H, int ID)
        {
            int _spaceSizeInnerW = 53;
            int _spaceSizeInnerH = 52;
            int xCount = 12;
            int yCount = 5;
            int[] _spaceBagX = new int[xCount];
            int[] _spaceBagY = new int[yCount];
      
            for (int i = 0; i < xCount; i++)
            {
                _spaceBagX[i] = 1273 + i * _spaceSizeInnerW;
            }
            for (int i = 0; i < yCount; i++)
            {
                _spaceBagY[i] = 589 + i * _spaceSizeInnerH;
            }

            int rightX,rightY, x1, y1, leftX, leftY, targetColumn, targetRow;

            targetColumn = (ID % xCount == 0 ? xCount : ID % xCount) - 1;
            targetRow = ID % xCount == 0 ? ID / xCount - 1 : ID / xCount;
            leftX  = Convert.ToInt32(Math.Round(D3W - ((1920 - _spaceBagX[targetColumn]) * D3H / 1080d)));
            leftY = Convert.ToInt32(Math.Round((_spaceBagY[targetRow]) * D3H / 1080d));

            x1 = Convert.ToInt32(Math.Round(D3W - ((1920 - _spaceBagX[targetColumn] - _spaceSizeInnerW / 2) * D3H / 1080d)));
            y1 = Convert.ToInt32(Math.Round((_spaceBagY[targetRow] + _spaceSizeInnerH / 2) * D3H / 1080d));
            rightX = Convert.ToInt32(Math.Round(D3W - ((1920 - _spaceBagX[targetColumn]- _spaceSizeInnerW) * D3H / 1080d)));
            rightY = Convert.ToInt32(Math.Round((_spaceBagY[targetRow]+ _spaceSizeInnerH) * D3H / 1080d));


            return new BagPoint() { ID=ID, rightX =rightX,rightY=rightY,  centerX = x1, centerY = y1, leftX = leftX, leftY = leftY };
        }
       
        /// <summary>
        /// 判断是否空背包
        /// </summary>
        /// <param name="D3W"></param>
        /// <param name="D3H"></param>
        /// <param name="ID"></param>
        /// <param name="zone"></param>
        /// <returns></returns>
        bool isInventorySpaceEmpty( int ID)
        {
            var m = bagPointList[ID];
            //去边框 
            var tmpsize = 3;
            var c= objdm.GetColorNum(m.leftX+ tmpsize, m.leftY+ tmpsize, m.rightX- tmpsize, m.rightY- tmpsize, "000000-121212", 1);
            var tagCount = (m.rightX-m.leftX- tmpsize- tmpsize) * (m.rightY-m.leftY- tmpsize- tmpsize);
            if (c* 100.0 / tagCount > 80)
            {
                return true;
            }
           
            return false;
        }

  
    }
    
 
}
