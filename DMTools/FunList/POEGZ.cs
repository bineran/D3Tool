//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace DMTools.FunList
//{
 
//    public class POEGZ : BaseD3
//    {
//        public const EnumD3 enumD3Name = EnumD3.POE改造;
//        public Point pointWP;
//        public enum EnumWP
//        {
//            普通,
//            魔法,
//            稀有,
//            传奇
//        }
    
//        public POEGZ(D3Param d3Param, List<KeyTimeSetting> Times, EnumD3 enumD3) : base(d3Param, Times, enumD3)
//        {
          

//        }
//        public void Right(Point point) {
      
//            objdm.MoveTo(point.X, point.Y);
//            objdm.RightClick();

//            objdm.MoveTo(pointWP.X, pointWP.Y);
//            objdm.KeyDown(16);
//            objdm.KeyDown(17);
//            var result = objdm.GetClipboard();
//            objdm.LeftClick();
//            objdm.KeyPressChar("c");
//            var result=objdm.GetClipboard();
//            FX(result);
//        }
//        public void FX(string str)
//        {
//            EnumWP enumWP = EnumWP.魔法;
//            if (str.Contains("稀 有 度: 魔法"))
//            {
//                enumWP = EnumWP.魔法;
//            }
//            else if (str.Contains("稀 有 度: 普通"))
//            {
//                enumWP = EnumWP.普通;
//            }
//            else if (str.Contains("稀 有 度: 稀有"))
//            {
//                enumWP = EnumWP.稀有;
//            }
//            else if (str.Contains("稀 有 度: 传奇"))
//            {
//                enumWP = EnumWP.传奇;
//            }


            

//        }
        
//    }
