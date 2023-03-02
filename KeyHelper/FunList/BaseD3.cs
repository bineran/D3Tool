using Dm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KeyHelper.FunList
{
    public abstract class BaseD3 : ID3Function
    {
        public Idmsoft objdm { get; set; }
        public int Handle { get; set; }
        public T_Time t_Time { get; set; }
        public BaseD3(Idmsoft objdm, int handle)
        {
            this.objdm = objdm;
            Handle = handle;
            Init();
        }
        object width = new object();
        object height = new object();
        object outX = new object();
        object outY = new object();
        public void Init()
        {
            object width = new object();
            object height = new object();
            objdm.GetClientSize(this.Handle, out width, out height);
            D3W = Convert.ToInt32(width);
            D3H = Convert.ToInt32(height);
        }
        /// <summary>
        /// 返回当前鼠标的X，Y
        /// </summary>
        /// <param name="pointX"></param>
        /// <param name="pointY"></param>
        /// <returns></returns>
        public Tuple<int, int> GetPointXY()
        {
            var color = objdm.GetCursorPos(out outX, out outY);
            var x = Convert.ToInt32(outX);
            var y = Convert.ToInt32(outY);
            return new Tuple<int, int>(x, y);
        }
        public Tuple<int, int, int> GetPointRGB(int pointX, int pointY)
        {
            var color = objdm.GetColor(pointX, pointY);
            var r = Convert.ToInt32(color.Substring(0, 2), 16);
            var g = Convert.ToInt32(color.Substring(2, 2), 16);
            var b = Convert.ToInt32(color.Substring(4, 2), 16);
            return new Tuple<int, int, int>(r, g, b);
        }

        public int D3W { get; set; }
        public int D3H { get; set; }

        public virtual void Start()
        { }


        public void ImageToBmp(string img) {
            var pic_nameNew = img.ToLower().Replace(".png", ".bmp");
            if (!System.IO.File.Exists(pic_nameNew))
            {
                objdm.ImageToBmp(img, pic_nameNew);
            }
        }

        /// <summary>
        /// 子类用override  会优先调用父类的这个方法，
        /// </summary>
        /// <param name="t_Time"></param>
        public virtual void StartBefore(T_Time t_Time)
        { 
            this.t_Time= t_Time;
        }


        public virtual void Stop() { }
    }
}
