﻿using DMTools.Config;
using DMTools.libs;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace DMTools.Forms
{
    public partial class FrmCapImg : Form
    {


        private const int MOUSEEVENTF_MOVE = 0x0001;

        public FrmCapImg()
        {

            InitializeComponent();
            this.pictureBox1.Image = LoadImg();
            this.KeyPreview = true;
            screenshotTimer = new Timer();
            screenshotTimer.Interval = 50;

            // 订阅定时器的Tick事件
            screenshotTimer.Tick += ScreenshotTimer_Tick;
            screenshotTimer.Start();
            
        }

        private void ScreenshotTimer_Tick(object? sender, EventArgs e)
        {
            CaptureScreen();
        }

        public Bitmap LoadImg()
        {
            Pen pen = new Pen(Color.Green, 4);
            Bitmap capturedScreen = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            using (Graphics g = Graphics.FromImage(capturedScreen))
            {
                g.CopyFromScreen(0, 0, 0, 0, new Size(capturedScreen.Width, capturedScreen.Height));
                g.DrawRectangle(pen, 0, 0, Screen.PrimaryScreen.Bounds.Width - 1, Screen.PrimaryScreen.Bounds.Height - 1);
            }
            return capturedScreen;
        }
        private void CaptureScreen()
        {
            this.panel2.Visible = true;

            // 创建一个位图，用于保存放大后的图片
            Bitmap zoomedImage = new Bitmap(200, 200);

            // 获取鼠标所在位置
            Point mousePosition = pictureBox1.PointToClient(Cursor.Position);

            // 计算放大区域的左上角位置
            int zoomSize = 50;
            int zoomLeft = (int)mousePosition.X - zoomSize / 2;
            int zoomTop = (int)mousePosition.Y - zoomSize / 2;

            // 获取屏幕的尺寸
            Screen screen = Screen.FromPoint(new System.Drawing.Point((int)mousePosition.X, (int)mousePosition.Y));
            int screenWidth = screen.Bounds.Width;
            int screenHeight = screen.Bounds.Height;

            // 检查范围越界的情况
            bool isOutOfRange = zoomLeft < 0 || zoomTop < 0 ||
                                zoomLeft + zoomSize > screenWidth ||
                                zoomTop + zoomSize > screenHeight;

            if (isOutOfRange)
            {
                // 越界处理逻辑，设置放大区域为50*50大小
                zoomLeft = Math.Max(0, Math.Min(zoomLeft, screenWidth - zoomSize));
                zoomTop = Math.Max(0, Math.Min(zoomTop, screenHeight - zoomSize));
                zoomSize = 50;
            }
        
            // 截取屏幕上的图片
            using (Graphics g = Graphics.FromImage(zoomedImage))
            {
                g.CopyFromScreen(zoomLeft, zoomTop, 0, 0, new System.Drawing.Size(zoomSize, zoomSize));
              
            }
            // 创建一个位图，用于保存截图
            Bitmap screenshot = new Bitmap(zoomSize, zoomSize);
            Pen pen = new Pen(Color.White, 2);
            // 截取屏幕上的图片
            var pn = this.panel2;
            using (Graphics g = Graphics.FromImage(screenshot))
            {
                g.CopyFromScreen(zoomLeft, zoomTop, 0, 0, new System.Drawing.Size(zoomSize, zoomSize));

                Color transparentColor = Color.FromArgb(128, 255, 0, 0); // 红色的线条，半透明
                Pen transparentPen = new Pen(transparentColor, 1);
                g.DrawLine(transparentPen, zoomSize / 2, zoomSize / 2 - 10, zoomSize / 2, zoomSize / 2 + 10);
                g.DrawLine(transparentPen, zoomSize / 2 - 10, zoomSize / 2, zoomSize / 2 + 10, zoomSize / 2);
                g.DrawRectangle(pen, 0, 0, pn.Size.Width - 1, pn.Size.Height - 1);
            }

            this.lblfbl.Text = $"{Screen.PrimaryScreen.Bounds.Width}*{Screen.PrimaryScreen.Bounds.Height}";
            this.lblpoint.Text = $"X:{mousePosition.X},Y:{mousePosition.Y}";
            this.lblcolor.Text = $"Color:{this.dMP.DM.GetColor(mousePosition.X, mousePosition.Y)}";
            // 显示截图到pictureBox控件
            this.pictureBox2.Image = screenshot;

            var e = mousePosition;



            // 自定义偏移量
            int offsetX = 50;
            int offsetY = 50;

            // 根据鼠标的位置和偏移量调整面板的位置
            if (e.X < screenWidth / 2 && e.Y < screenHeight / 2)
            {
                // 左上方
                pn.Location = new Point(e.X + offsetX, e.Y + offsetY);
                pn.BringToFront();
            }
            else if (e.X < screenWidth / 2 && e.Y >= screenHeight / 2)
            {
                // 左下方
                pn.Location = new Point(e.X + offsetX, e.Y - pn.Height - offsetY);
                pn.BringToFront();
            }
            else if (e.X >= screenWidth / 2 && e.Y < screenHeight / 2)
            {
                // 右上方
                pn.Location = new Point(e.X - pn.Width - offsetX, e.Y + offsetY);
                pn.BringToFront();
            }
            else if (e.X >= screenWidth / 2 && e.Y >= screenHeight / 2)
            {
                // 右下方
                pn.Location = new Point(e.X - pn.Width - offsetX, e.Y - pn.Height - offsetY);
                pn.BringToFront();
            }
        }


        private void FrmCapImg_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                this.screenshotTimer.Stop();
                this.Close();
                return;
            }
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.screenshotTimer.Stop();
                SetClipboard();
                this.Close();
                return;
            }


        }

        public void MS(Keys e)
        {
            Point currentMousePos = pictureBox1.PointToClient(Cursor.Position);

            // 根据方向键的按下来控制鼠标的移动
            if (e == Keys.Up)
            {
                currentMousePos.Y -= 1; // 向上移动10个像素
            }
            else if (e == Keys.Down)
            {
                currentMousePos.Y += 1; // 向下移动10个像素
            }
            else if (e == Keys.Left)
            {
                currentMousePos.X -= 1; // 向左移动10个像素
            }
            else if (e == Keys.Right)
            {
                currentMousePos.X += 1; // 向右移动10个像素
            }

            // 更新鼠标的位置
            Cursor.Position = pictureBox1.PointToScreen(currentMousePos);
            CaptureScreen();
        }
        DMP dMP = new DMP();

        public void SetClipboard()
        {

            var objdm = dMP.DM;
            object x;
            object y;
            objdm.GetCursorPos(out x, out y);
            int ix = Convert.ToInt32(x);
            int iy = Convert.ToInt32(y);
            List<KeyTimeSetting> alkts = new List<KeyTimeSetting>();
            KeyTimeSetting kt = new KeyTimeSetting();
            kt.Int1 = ix;
            kt.Int2 = iy;
            alkts.Add(kt);
            var hd = objdm.GetMousePointWindow();
            var color = objdm.GetColor(ix, iy);
            kt.Str1 = color;


            objdm.SetClipboard(alkts.ToJson());



        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            CaptureScreen();
        }



        private void FrmCapImg_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            MS(e.KeyCode);
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            SetClipboard();
            CaptureScreen();
            this.Close();
        }
        private Timer screenshotTimer;
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.None)
            {
                screenshotTimer.Start();
            }
            else
            {
                screenshotTimer.Stop();
            }
        }
    }
}
