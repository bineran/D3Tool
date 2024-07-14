using DMTools.Config;
using DMTools.Control;
using DMTools.libs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.FunList
{
    public class POEGZ : BaseD3
    {

        public SortedDictionary<Enum_通货, Point> thPoints = new SortedDictionary<Enum_通货, Point>();

        public POEGZ(D3Param _d3Param, List<KeyTimeSetting> Times, EnumD3 enumD3) : base(_d3Param, Times, enumD3)
        {
        }

        public Point wpPoint { get; set; } = new Point();

        #region 获取当前物品状态
        public string GetItemInfo()
        {
            objdm.MoveTo(wpPoint.X, wpPoint.Y);
            Sleep(SleepTime * speed);
            objdm.SetClipboard("");

            objdm.KeyDown(17);
            objdm.MoveTo(wpPoint.X, wpPoint.Y);
            Sleep(SleepTime * speed);
            objdm.KeyPress((int)Keys.C);
            objdm.KeyUp(17);
            return objdm.GetClipboard();
        }
        #endregion


        #region 分析当前物品信息
        #endregion

        #region 计算下一步操作
        #endregion
        public Enum_通货 LastEnum_通货 = Enum_通货.空;
        #region 使用通货
        public void syth(Enum_通货 enumth)
        {
            LastEnum_通货 = enumth;
            switch (enumth)
            {
                case Enum_通货.改造石:

                    if (!this.d3KeyState[Keys.ShiftKey])
                    {
                        objdm.KeyDown((int)Keys.ShiftKey);
                    }
                    if (LastEnum_通货 == Enum_通货.改造石)
                    {
                        objdm.MoveTo(wpPoint.X, wpPoint.Y);
                        Sleep(SleepTime * speed);
                        objdm.LeftClick();
                        Sleep(SleepTime * speed);
                    }
                    else
                    {
                        SingleTH(thPoints[enumth]);
                    }

                    break;
                default:
                    if (this.d3KeyState[Keys.ShiftKey])
                    {
                        objdm.KeyUp((int)Keys.ShiftKey);
                    }
                    SingleTH(thPoints[enumth]);
                    break;
            }
        }
        #endregion
        public int SleepTime = 10;
        public int speed = 5;
        public void SingleTH(Point point)
        {
            Sleep(SleepTime * speed);
            objdm.MoveR(point.X, point.Y);
            Sleep(SleepTime * speed);
            objdm.RightClick();
            Sleep(SleepTime * speed);
            objdm.MoveTo(wpPoint.X, wpPoint.Y);
            Sleep(SleepTime * speed);
            objdm.LeftClick();
            Sleep(SleepTime * speed);
        }


    }
    public enum Enum_类型
    { 
        普通,
        魔法,
        稀有,
        传奇
    }
    public enum Enum_通货
    {
        空,
        改造石,
        兑变石,
        增幅石,
        富豪石,
        崇高石,
        重铸石,
        机会石,
        点金石
        
    }
}
