using DMTools.libs;
using DMTools.Config;
using DMTools.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.FunList
{
    [KeyName(@"在指定范围  找不到  指定图片则点击指定按键
类型：点击
时间：找图的休眠时间
文本1：建议png格式 放至 Image\bmp目录下   多图以|分隔  例如 kj17.png|kj18.png|kj19.png|kj20.png
搜索范围：整数1：x1,整数2：y1,整数3：x2,整数4：y2
")]
    public class ImageNoClick : BaseD3
    {
        public  const EnumD3 enumD3Name = EnumD3.图片不存在按键;
        public ImageNoClick(D3Param d3Param,EnumD3 enumD3) : base(d3Param,enumD3)
        {
            this.StartEvent += ImageNoClick_StartEvent;
        }

        private void ImageNoClick_StartEvent()
        {
           var kl= this.Times.Where(r => r.keyClickType== KeyClickType.点击 
            && 0<=r.Int1  && r.Int1 <=D3W
            && 0 <= r.Int2 && r.Int2 <= D3H
            && 0 <= r.Int3 && r.Int3 <= D3W
            && 0 <= r.Int4 && r.Int4 <= D3H
            && r.KeyCode>0
            && r.Str1.TrimLength()>0
            && r.D1 > 0
            );

            foreach (var k in kl)
            {
                AddImageClickTask(k,false);
            }
            
        }


    }
}
