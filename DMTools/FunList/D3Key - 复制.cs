using DMTools.libs;
using DMTools.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DMTools.Config;

namespace DMTools.FunList
{
    [KeyName(@"
int1 长度
")]
    public class ChineseName : BaseD3
    {
        public  const EnumD3 enumD3Name = EnumD3.随机生成中文名称;
        public ChineseName(D3Param d3Param, List<KeyTimeSetting> Times, EnumD3 enumD3) : base(d3Param, Times,enumD3)
        {
            this.StartEvent += D3Key_StartEvent;
        }
        private void D3Key_StartEvent()
        {
            var length = 3;
            if(this.Times!=null && this.Times.Count > 0)
            {
                length = this.Times[0].Int1;
            }
            var str= GenerateChineseWords.GenerateChineseWord(length);
            objdm.LeftDoubleClick();
            this.Sleep(100);
     
            objdm.SendString(this.Handle, str);
        }


      

    }
    public class GenerateChineseWords
    {
        /// <summary>
        /// 随机产生常用汉字
        /// </summary>
        /// <param name="count">要产生汉字的个数</param>
        /// <returns>常用汉字</returns>
        public static string GenerateChineseWord(int count)
        {
            string chineseWords = "";
            System.Random rm = new System.Random();
            Encoding gb = Encoding.GetEncoding("gb2312");

            for (int i = 0; i < count; i++)
            {
                // 获取区码(常用汉字的区码范围为16-55)
                int regionCode = rm.Next(16, 56);

                // 获取位码(位码范围为1-94 由于55区的90,91,92,93,94为空,故将其排除)
                int positionCode;
                if (regionCode == 55)
                {
                    // 55区排除90,91,92,93,94
                    positionCode = rm.Next(1, 90);
                }
                else
                {
                    positionCode = rm.Next(1, 95);
                }

                // 转换区位码为机内码
                int regionCode_Machine = regionCode + 160;// 160即为十六进制的20H+80H=A0H
                int positionCode_Machine = positionCode + 160;// 160即为十六进制的20H+80H=A0H

                // 转换为汉字
                byte[] bytes = new byte[] { (byte)regionCode_Machine, (byte)positionCode_Machine };
                chineseWords += gb.GetString(bytes);
            }
            return chineseWords;
        }
    }
}
