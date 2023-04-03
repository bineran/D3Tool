﻿using POE.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace POE.Services
{
    public class BuildMap : BasePOE
    {
        public void BuidMap()
        {

        }

        public override void Do()
        {
            base.Do();
            int x1 = 1642, y1 = 6, x2 = x1 + 270, y2 = y1+270;
            var path = Guid.NewGuid().ToString().ToTempImagePath();
            objdm.Capture(x1, y1, x2, y2, path);
            Bitmap map = new Bitmap(path);
            Ostu ostu = new Ostu(map);
            ostu.RetrunPicture().Save(path);
        }
    }
    public class Ostu
    {
        private const int GrayNum = 256;        //灰度值
        private int[] GrayArr = new int[GrayNum];   //灰度值数组
        private int[] PixArr;
        private double w1;  //背景灰度概率
        private double w0;  //前景灰度概率
        private int IMG_HEIGHT;
        private int IMG_WIDTH;
        private int PixNum;
        private Bitmap pic;
        public Ostu(Bitmap pic)
        {
            this.pic = pic;
            IMG_HEIGHT = pic.Height;
            IMG_WIDTH = pic.Width;
            PixNum = IMG_HEIGHT * IMG_WIDTH;
            PixArr = new int[PixNum];
            SetGrayArr();


        }
        public Ostu(string picpath)
        {
           this.pic= new Bitmap(picpath);
            IMG_HEIGHT = pic.Height;
            IMG_WIDTH = pic.Width;
            PixNum = IMG_HEIGHT * IMG_WIDTH;
            PixArr = new int[PixNum];
            SetGrayArr();


        }
        /// <summary>
        /// 获取灰度值数组，并求每种灰度所占的比例
        /// </summary>
        /// <returns>无</returns>
        private void SetGrayArr()
        {

            for (int i = 0; i < IMG_HEIGHT; i++)
            {
                for (int j = 0; j < IMG_WIDTH; j++)
                {
                    Color color = pic.GetPixel(j, i);
                    int gray = (int)(0.39 * color.R + 0.50 * color.G + 0.11 * color.B);
                    PixArr[i * IMG_WIDTH + j] = gray;            //设置像素数组
                    GrayArr[PixArr[i * IMG_WIDTH + j]]++;   //求灰度数组,元素为每种灰度的数量
                }
            }


        }
        /// <summary>
        /// 获取阈值
        /// </summary>
        /// <returns>阈值</returns>
        private int GetVal()
        {
            double u0;  //背景灰度均值
            double u1;  //前景灰度均值
            double maxVal = 0;  //类间方差最大值
            int endval = 0;
            for (int i = 0; i < GrayNum; i++)
            {
                w1 = w0 = u0 = u1 = 0;
                for (int j = 0; j < GrayNum; j++)
                {
                    if (j <= i)
                    {
                        w0 += GrayArr[j] / (PixNum * 1.0);  //每种灰度的概率
                        u0 += GrayArr[j] / (PixNum * 1.0) * j;
                    }
                    else
                    {
                        w1 += GrayArr[j] / (PixNum * 1.0);
                        u1 += GrayArr[j] / (PixNum * 1.0) * j;
                    }
                }
                u0 = u0 / w0;
                u1 = u1 / w1;
                double val = w1 * w0 * Math.Pow(u0 - u1, 2);
                if (maxVal < val)
                {
                    maxVal = val;
                    endval = i;  //阈值
                }
            }
            return endval;
        }
        /// <summary>
        /// 图像二值化
        /// </summary>
        /// <returns>无</returns>
        private void TurnGray()
        {
            int val = GetVal();
            for (int i = 0; i < IMG_HEIGHT; i++)
            {
                for (int j = 0; j < IMG_WIDTH; j++)
                {
                    Color color = pic.GetPixel(j, i);
                    int gray = (int)(0.39 * color.R + 0.50 * color.G + 0.11 * color.B);
                    if (gray > val)
                    {
                        pic.SetPixel(j, i, Color.FromArgb(255, 255, 255));
                    }
                    else
                    {
                        pic.SetPixel(j, i, Color.FromArgb(0, 0, 0));
                    }
                }
            }
        }
        /// <summary>
        /// 返回二值化后的图像
        /// </summary>
        /// <returns>图像</returns>
        public Bitmap RetrunPicture()
        {
            TurnGray();
            return pic;
        }

    }
}
