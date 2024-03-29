﻿using DMTool.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMTool.FunList
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
                if (objdm != null)
                {
                    objdm.GetClientSize(this.Handle, out width, out height);
                    D3W = Convert.ToInt32(width);
                    D3H = Convert.ToInt32(height);
                }
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
        public Tuple<int, int, int,string> GetPointRGB(int pointX, int pointY,bool print=false)
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
        public void ImageToBmp(string img)
        {
            var pic_nameNew = img.ToLower().Replace(".png", ".bmp");
            if (!System.IO.File.Exists(pic_nameNew))
            {
                objdm.ImageToBmp(img, pic_nameNew);
            }
        }
        public int D3W { get; set; }
        public int D3H { get; set; }
        private void DMKeyPress(Keys key)
        {
            if (key == BaseD3.MouseLeft)
                objdm.LeftClick();
            else if (key == BaseD3.MouseRight)
                objdm.RightClick();
            else if (key == BaseD3.MouseShiftLeft)
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
        public void AddPointColorTask(D3TimeSetting ts)
        {
            var x = ts.Int1;
            var y = ts.Int2;
            var key = (int)ts.KeyCode;
            var keyCode = ts.KeyCode;
            var sleepInt = ts.D1;
            var action = new Action(()=>
            {
                var color = objdm.GetColor(x, y);
                if (color.ToLower() == ts.Str1.Trim().ToLower())
                {
                    this.DMKeyPress(keyCode);
                }
            });
            StartNewForTask(action, sleepInt);
        }
        /// <summary>
        /// 指定位置等于指定颜色按键
        /// </summary>
        /// <param name="ts"></param>
        public void AddPointNoColorTask(D3TimeSetting ts)
        {
            var x = ts.Int1;
            var y = ts.Int2;
            var keyCode = ts.KeyCode;
            var key = (int)ts.KeyCode;
            var sleepInt = ts.D1;
            this.GetPointRGB(x, y,true);

            var action = new Action(() =>
            {
                var color = objdm.GetColor(x, y);
                if (color.ToLower() != ts.Str1.Trim().ToLower())
                {
                    DMKeyPress(keyCode);
                }
            });
            StartNewForTask(action, sleepInt);
        }
        

        /// <summary>
        /// 查找指定范围的图片 不存在即点击
        /// </summary>
        /// <param name="ts"></param>
        public void AddImageClickTask(D3TimeSetting ts,bool imgFlag=true)
        {

            try
            {
                var files = ts.Str1.Split('|');
                var pngPath = Application.StartupPath + "Image\\png\\";
                var bmpPath0 = Application.StartupPath + "Image\\bmp";
                var bmpPath = bmpPath0 + "\\";
                objdm.SetPath(bmpPath0);
                string allpic = "";
                foreach (var f in files)
                {
                    try
                    {
                        if (File.Exists(pngPath + f))
                        {
                            var newName = f.ToLower().Replace(".png", ".bmp");
                            if (!System.IO.File.Exists(bmpPath + newName))
                            {
                                objdm.ImageToBmp(pngPath + f, bmpPath + newName);
                            }
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
                    catch
                    { }
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
                var action = new Action(() =>
                {
                    object x;
                    object y;
                    var ret=objdm.FindPic(x1, y1, x2, y2, allpic, "101010", 0.4, 0, out x, out y);
                    int ix = 0, iy = 0;
                    ix = Convert.ToInt32(x);
                    iy = Convert.ToInt32(y);
                    if (imgFlag && ret>=0)
                    {
                        this.DMKeyPress(KeyCode);
                    }
                    if (!imgFlag && ret==-1)
                    {
                        this.DMKeyPress(KeyCode);
                    }
                });
                StartNewForTask(action, sleepInt);
            }
            catch { }


        }
    }
}

