using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMTools
{
	public partial class FormMain
	{
        MouseHook mouseHook;
        KeyboardHook kh;
        #region 全局钩子
        public void InitKeyHook()
        {

            kh = new KeyboardHook();
            kh.OnKeyDownEvent += Kh_OnKeyDownEvent;
            kh.SetHook();
        }
        public void InitMouseHook()
        {
            mouseHook = new MouseHook();
            mouseHook.MouseWheel += mouseHook_MouseWheel;
            mouseHook.MouseUp += mouseHook_MouseUp;
            mouseHook.MouseDown += mouseHook_MouseDown;
            mouseHook.Start();
        }
        DateTime S_Time, X_Time; DateTime D_Time;
        /// <summary>
        /// 左键状态
        /// </summary>
        public static bool isLeft { get; private set; } = false;
        /// <summary>
        /// 右键状态
        /// </summary>
        public static bool isRight { get; private set; } = false;
        void mouseHook_MouseUp(object? sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                isRight = false;
                this.ProcessKey(ConvertKeys.HotKeyRightUp);

            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                isLeft = false;
                this.ProcessKey(ConvertKeys.HotKeyLeftUp);
            }
            return;
            // MessageBox.Show("mouseHook_MouseUp");
        }
        void mouseHook_MouseDown(object? sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    ReplayMouseEventArgs(e);

                    isRight = true;
                    this.ProcessKey(ConvertKeys.HotKeyRightDown);
                }
                else if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    ReplayMouseEventArgs(e);
                    isLeft = true;
                    this.ProcessKey(ConvertKeys.HotKeyLeftDown);
                }
            }
            catch
            { }
        }

        bool isMouseWheelRun = false;
        void mouseHook_MouseWheel(object? sender, MouseEventArgs e)
        {
            try
            {
                if (isMouseWheelRun)
                {
                    return;
                }
                isMouseWheelRun = !isMouseWheelRun;
                if (e.Delta == 120)
                {
                    if ((DateTime.Now - S_Time).TotalSeconds > 0.5)
                    {
                        S_Time = DateTime.Now;
                        this.ProcessKey(Keys.Control | Keys.PageUp);
                    }
                }
                else
                {
                    if ((DateTime.Now - X_Time).TotalSeconds > 0.5)
                    {
                        X_Time = DateTime.Now;
                        this.ProcessKey(Keys.Control | Keys.PageDown);
                    }
                }

            }
            catch
            { }
            finally { isMouseWheelRun = !isMouseWheelRun; }

        }
  
        public Keys ConvertKey(KeyEventArgs e )
        {

            //if (e.Control && (e.Shift || e.Alt))
            //{
            //   var al= this.d3Config.d3ConfigItems.Where(r => r.EnabledFlag);

            //    foreach (var key in ConvertKeys.alGKeys)
            //    {
            //        if (e.Alt && e.Shift && e.KeyData == (Keys.Control | Keys.Shift | Keys.Alt | key))
            //        {
            //            return Keys.Control | key;
            //        }
            //        else if (e.Shift && e.KeyData == (Keys.Control | Keys.Shift | key))
            //        {
            //            return Keys.Control | key;
            //        }
            //        else if (e.Alt && e.KeyData == ( Keys.Control | Keys.Alt | key))
            //        {
            //            return Keys.Control | key;
            //        }
            //    }
            //}
            if (e.Control || e.Shift || e.Alt  )
            {

                var items=this.d3Config.d3ConfigItems.Where(r => r.EnabledFlag);
                List<Keys> keys= new List<Keys>();
                foreach (var item in items)
                {
                    if (item.HotKey1 != Keys.OemClear)
                    {
                        keys.Add(item.HotKey1);
                    }
                    if (item.HotKey2 != Keys.OemClear)
                    {
                        keys.Add(item.HotKey2);
                    }
                }


                foreach (var key in keys)
                {
                    if (e.Control  && e.KeyData == (Keys.Control|  key))
                    {
                        return  key;
                    }
                    else if (e.Shift && e.KeyData == ( Keys.Shift | key))
                    {
                        return  key;
                    }
                    else if (e.Alt && e.KeyData == (Keys.Alt | key))
                    {
                        return  key;
                    }
                    if (e.Control && e.Shift && e.KeyData == (Keys.Control | Keys.Shift| key))
                    {
                        return key;
                    }
                    if (e.Control && e.Alt && e.KeyData == (Keys.Control | Keys.Alt | key))
                    {
                        return key;
                    }
                    if (e.Shift && e.Alt && e.KeyData == (Keys.Shift | Keys.Alt | key))
                    {
                        return key;
                    }
                    if (e.Control && e.Shift && e.Alt && e.KeyData == (Keys.Control | Keys.Shift | Keys.Alt | key))
                    {
                        return key;
                    }

                }
            }
            Trace.WriteLine(e.KeyData);
            return e.KeyData;
        }
        private void Kh_OnKeyDownEvent(object? sender, KeyEventArgs e)
        {


            this.ProcessKey(ConvertKey(e));
        }
        #endregion
    }
}
