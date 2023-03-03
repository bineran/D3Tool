using Dm;
using KeyHelper.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KeyHelper.FunList
{
    public abstract class BaseD3 : ID3Function
    {
         
        public virtual event Action StartEvent;
        public virtual event Action StopEvent;
        public Idmsoft objdm { get; set; }
        public int Handle { get; set; }
        public D3FunSetting d3FunSetting { get; set; }
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

        public bool RunState
        {
            get
            {
                return StartThreadList.Count > 0;
            }
        }
        public List<Thread> StartThreadList { get; set; } = new List<Thread>();
        public List<Thread> StopThreadList { get; set; } = new List<Thread>();
        public  void Stop()
        {
            foreach (var t in StartThreadList)
            {
               
                t.Abort();
            }
            foreach (var t in StopThreadList)
            {
                t.Start();
            }
        }
        public virtual void Start()
        {
            StartThreadList.Clear();
            StopThreadList.Clear();
            if (this.StartEvent != null)
            {
                StartThreadList.Add(RunThread(this.StartEvent));
            }
            if (this.StopEvent != null)
            {
                StartThreadList.Add(CreateThread(this.StopEvent));
            }
        }
        public Thread RunThread(Action action)
        {
            Thread t = new Thread(new ThreadStart(() =>
            {
                try
                {
                    action();
                }
                catch (Exception e)
                {

                }
            }));
            t.SetApartmentState(ApartmentState.STA);
            t.IsBackground = true;
            t.Start();
            return t;
        }

        public Thread CreateThread(Action action)
        {
            Thread t = new Thread(new ThreadStart(() =>
            {
                try
                {
                    action();
                }
                catch (Exception e)
                {

                }
            }));
            t.SetApartmentState(ApartmentState.STA);
            t.IsBackground = true;
            return t;
        }




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
        public virtual void StartBefore(D3FunSetting _d3FunSetting)
        { 
            this.d3FunSetting = _d3FunSetting;
        }


    
    }
}
