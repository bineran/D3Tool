using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public static bool isleft { get; private set; } = false;
        /// <summary>
        /// 右键状态
        /// </summary>
        public static bool isRight { get; private set; } = false;
        void mouseHook_MouseUp(object? sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                isRight = false;
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                isleft = false;
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
                    isRight = true;
                }
                else if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    isleft = true;
                }
            }
            catch
            { }
        }
        void mouseHook_MouseWheel(object? sender, MouseEventArgs e)
        {
            try
            {
                if (e.Delta == 120)
                {
                    //向上
                    if ((DateTime.Now - S_Time).TotalSeconds > 0.6)
                    {
                        S_Time = DateTime.Now;
                        this.ProcessKey(Keys.Control | Keys.PageUp);
                    }
                }
                else
                {
                    //向下
                    if ((DateTime.Now - X_Time).TotalSeconds > 0.6)
                    {
                        X_Time = DateTime.Now;
                        this.ProcessKey(Keys.Control | Keys.PageDown);

                    }
                }
            }
            catch
            { }

        }
        private void Kh_OnKeyDownEvent(object? sender, KeyEventArgs e)
        {
            if ((DateTime.Now - D_Time).TotalSeconds < 0.6)
            {
                return;
            }
            e.Handled = this.ProcessKey(e.KeyData);
        }
        #endregion
    }
}
