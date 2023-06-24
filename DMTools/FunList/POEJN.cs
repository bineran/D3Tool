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

    [KeyName("建议1920*1080 开光环")]
    public class POEJN : BaseD3
    {
       public  const EnumD3 enumD3Name = EnumD3.POE开光环;


        List<(int, int, int)> bagPoints = new List<(int, int, int)>();
        public POEJN(D3Param d3Param, List<KeyTimeSetting> Times, EnumD3 enumD3) : base(d3Param, Times,enumD3)
        {

            this.StartEvent += POEJN_StartEvent;
        }

        public void POEJN_StartEvent()
        {
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
            foreach (var ts in list)
            {
                if (ts.keyClickType== KeyClickType.点击 && ts.KeyCode== Keys.Q && bagPoints.Any(r => r.Item1 == ts.Int1))
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
