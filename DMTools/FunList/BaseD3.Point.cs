using DMTools.libs;
using DMTools.Config;
using DMTools.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Dm;

namespace DMTools.FunList
{
    public abstract partial class BaseD3 
    {
        object width = new object();
        object height = new object();
        object outX = new object();
        object outY = new object();
        public virtual void Init()
        {

            try
            {
                objDMP=new DMP();
                
                this.objdm.SetShowErrorMsg(0);
                this.BindForm(this.objdm, this.Handle,this.d3Param.sysConfig);
                this.objdm.GetClientSize(this.Handle, out width, out height);
                D3W = Convert.ToInt32(width);
                D3H = Convert.ToInt32(height);

            }
            catch (Exception ex) { log.Error(ex); }
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
        public Tuple<int, int, int,string> GetPointRGB(  int pointX, int pointY,bool print=false)
        {
            var color = objdm.GetColor(pointX, pointY);
            var r = Convert.ToInt32(color.Substring(0, 2), 16);
            var g = Convert.ToInt32(color.Substring(2, 2), 16);
            var b = Convert.ToInt32(color.Substring(4, 2), 16);
            if (print)
            {
                log.Info($"************* Handle:{this.Handle},  x:{pointX},y:{pointY},color:{color},R:{r}、G:{g}、B:{b}");
            }
            return new Tuple<int, int, int,string>(r, g, b, color);
        }

        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="objdm"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static KeyTimeSetting SavePointImage(Idmsoft objdm,SysConfig sysConfig)
        {

            object ox;
            object oy;
            objdm.GetCursorPos(out ox, out oy);
            int x=Convert.ToInt32(ox);
            int y=Convert.ToInt32(oy);
            objdm.MoveTo(0, 0);
            Task.Delay(100).Wait();
            var path = FileConfig.DM_BMP_PATH;
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            objdm.SetPath(path);
            var imgaize = sysConfig.image_size;
            var filename = "Auto-"+DateTime.Now.ToString("yyyyMMdd-hhmmss") +  ".bmp";
            objdm.Capture(x - imgaize, y - imgaize,
                        x + imgaize,
                        y + imgaize, filename);
            KeyTimeSetting ts = new KeyTimeSetting();
            ts.Str1 = filename;
            ts.D1 = 1000;
            ts.keyClickType = KeyClickType.图片未找到点击;
            ts.Int1 = x - sysConfig.image_size-2;
            ts.Int2 = y - sysConfig.image_size - 2;
            ts.Int3 = x + sysConfig.image_size + 2;
            ts.Int4 = y + sysConfig.image_size + 2;
            ts.KeyCode = ConvertKeys.HotKeyDebug;
            ts.Rank = 0;
            return ts;
        }
        public static KeyTimeSetting GetKTSPointColor(Idmsoft objdm)
        {
            object ox;
            object oy;
            objdm.GetCursorPos(out ox, out oy);
            int x = Convert.ToInt32(ox);
            int y = Convert.ToInt32(oy);
            var color = objdm.GetColor(x, y);

            KeyTimeSetting ts = new KeyTimeSetting();
            ts.Int1 = x; ts.Int2 = y;
            ts.D1 = 200;
            ts.Str1 = color;
            ts.keyClickType = KeyClickType.颜色不匹配点击;
            ts.KeyCode = ConvertKeys.HotKeyDebug;
            ts.Rank = 0;


            return ts;
        }
        public int D3W { get; set; }
        public int D3H { get; set; }
        public int NewX(int x)
        {
            return x.newWidth(this.D3W);
        }
        public int NewY(int y)
        {
            return y.newHeight(this.D3H);
        }
        private void DMKeyPress( Keys key)
        {
            if (key == ConvertKeys.MouseLeft)
                objdm.LeftClick();
            else if (key == ConvertKeys.MouseRight)
                objdm.RightClick();
            else if (key == ConvertKeys.MouseShiftLeft)
            {
                objdm.KeyDown(this.d3Param.KeyCodes.KeyStand);
                objdm.LeftClick();
                objdm.KeyUp(this.d3Param.KeyCodes.KeyStand);
            }
            else
                objdm.KeyPress((int)key);
        }
        /// <summary>
        /// 指定位置等于指定颜色按键
        /// </summary>
        /// <param name="ts"></param>
        public void AddPointColorTask(KeyTimeSetting ts,bool ColorFalg=true)
        {
            var x = ts.Int1;
            var y = ts.Int2;
            var key = (int)ts.KeyCode;
            var keyCode = ts.KeyCode;
            var sleepInt = ts.D1;
            var tagColor = ts.Str1;
            //var objdm = CreateDM();
            this.GetPointRGB(x, y, true);
     
            var action = () =>
            {
                var ret = objdm.CmpColor(x, y, tagColor,this.d3Param.sysConfig.color_sim);
                if (ret==0 && ColorFalg )
                {
                    this.DMKeyPress( keyCode);
                }
                else if (ret == 1 && ColorFalg==false)
                {
                    this.DMKeyPress( keyCode);
                }
            };
            StartTaskList.Add(StartNewForTask(action, sleepInt));
            if (ts.KeyCode == ConvertKeys.MouseShiftLeft)
            {
                AddStopTaskKeysUpStand();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ts"></param>
        public void AddImageClickTask(KeyTimeSetting ts, bool imgFlag = true)
        {

            try
            {
                //var objdm = CreateDM();
                var files = ts.Str1.Split('|');
                objdm.SetPath(FileConfig.DM_BMP_PATH);
                string allpic = "";
                foreach (var f in files)
                {

                    var sourceFile = "";
                    if (f.ToLower().Contains(".png"))
                    {
                        sourceFile = f.DmPngPath();
                    }
                    var newName = f.ToLower().Trim().Replace(".png", ".bmp");
                    var tagFile = newName.DmBmpPath(); ;

                    if (!File.Exists(tagFile) && File.Exists(sourceFile))
                    {
                        objdm.ImageToBmp(sourceFile, tagFile);
                    }
                    if (File.Exists(tagFile))
                    {
                        if (allpic.Length > 0)
                        {
                            allpic += "|" + newName;
                        }
                        else
                        {
                            allpic = newName;
                        }
                    }

                }
                if (allpic.Length == 0)
                {
                    return;
                }
                var x1 = ts.Int1;
                var y1 = ts.Int2;
                var x2 = ts.Int3;
                var y2 = ts.Int4;
                var KeyCode = ts.KeyCode;
                var key = (int)ts.KeyCode;
                var sleepInt = ts.D1;
                //if (files.Length == 1)
                //{
                //    var tmpTagName = files[0].ToLower().Trim().Replace(".png", ".bmp").Replace(".bmp", "-tag.bmp");
                //    if (!File.Exists(tmpTagName.DmBmpPath()))
                //    {
                //        //x = 0:y = 0
                        
                //        //objdm.Capture(x1, y1, x2, y2, tmpTagName);

                //    }

                //}



                var action = () =>
                 {
                     object x;
                     object y;
                     var ret = objdm.FindPic(x1, y1, x2, y2, allpic, this.d3Param.sysConfig.delta_color, this.d3Param.sysConfig.sim, 0, out x, out y);
                     //int ix = 0, iy = 0;
                     //ix = Convert.ToInt32(x);
                     //iy = Convert.ToInt32(y);
                     if (imgFlag && ret >= 0)
                     {
                         this.DMKeyPress( KeyCode);
                     }
                     if (!imgFlag && ret == -1)
                     {
                         this.DMKeyPress( KeyCode);
                     }
                 };
                StartTaskList.Add(StartNewForTask(action, sleepInt));
                if (ts.KeyCode == ConvertKeys.MouseShiftLeft)
                {
                    AddStopTaskKeysUpStand();
                }
            }
            catch(Exception ex) {
                log.Error(ex);
            }


        }

        public void StartPointColor()
        {
            var kl = this.Times.Where(r => r.keyClickType == KeyClickType.颜色匹配点击
             && 0 <= r.Int1 && r.Int1 <= D3W
             && 0 <= r.Int2 && r.Int2 <= D3H
             && r.KeyCode > 0
             && r.Str1.TrimLength() > 0
             && r.D1 > 0
             );
            foreach (var k in kl)
            {
                AddPointColorTask(k);
            }
        }
        public void StartPointNoColor()
        {
            var kl = this.Times.Where(r => r.keyClickType == KeyClickType.颜色不匹配点击
             && 0 <= r.Int1 && r.Int1 <= D3W
             && 0 <= r.Int2 && r.Int2 <= D3H
             && r.KeyCode > 0
             && r.Str1.TrimLength() > 0
             && r.D1 > 0
             );
            foreach (var k in kl)
            {
                AddPointColorTask(k, false);
            }
        }
        public void StartImageTask()
        {
            var kl = this.Times.Where(r =>
            (r.keyClickType == KeyClickType.图片找到点击
            || r.keyClickType == KeyClickType.图片未找到点击)
             && 0 <= r.Int1 && r.Int1 <= D3W
             && 0 <= r.Int2 && r.Int2 <= D3H
             && 0 <= r.Int3 && r.Int3 <= D3W
             && 0 <= r.Int4 && r.Int4 <= D3H
             && r.KeyCode > 0
             && r.Str1.TrimLength() > 0
             && r.D1 > 0
             );

            foreach (var k in kl)
            {

                AddImageClickTask(k, k.keyClickType == KeyClickType.图片找到点击);
            }
        }
    }
}

