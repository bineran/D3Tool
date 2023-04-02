using DMTools.libs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using DMTools.Config;
using DMTools.Control;
//using Microsoft.ML.OnnxRuntime;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Collections.Concurrent;
using Yolov5Net.Scorer.Models;
using Yolov5Net.Scorer;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.Fonts;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Drawing.Processing;
using Pen = SixLabors.ImageSharp.Drawing.Processing.Pen;
using SixLabors.ImageSharp;
using Microsoft.ML.OnnxRuntime;
using SixLabors.ImageSharp.Drawing;
using static DMTools.FunList.D3Test;
using PointF = SixLabors.ImageSharp.PointF;

//using Dm;


namespace DMTools.FunList
{

    [KeyName("测试")]
    public class D3Test : BaseD3
    {
        public const EnumD3 enumD3Name = EnumD3.测试;
        SixLabors.Fonts.Font font = new SixLabors.Fonts.Font(new FontCollection().Add("Assets/consola.ttf"), 16);

        string jpgName = "Assets\\test.jpg";
        string onnxName = "Assets\\Weights\\csgo.onnx";
        int LabelCount = 2;
        YoloScorer<YoloCocoP5Model> yoloScorer;
        private SortedList<int, BagPoint> bagPointList = new SortedList<int, BagPoint>();

        public D3Test(D3Param d3Param, EnumD3 enumD3) : base(d3Param, enumD3)
        {
            this.StartEvent += D3FJ_StartEvent;
            SessionOptions sessionOptions = new SessionOptions();
            sessionOptions.GraphOptimizationLevel = GraphOptimizationLevel.ORT_ENABLE_ALL;
            sessionOptions.AppendExecutionProvider_DML();
            yoloScorer = new YoloScorer<YoloCocoP5Model>(onnxName, LabelCount, sessionOptions);



        }
        /// <summary>
        /// 清空文件夹
        /// </summary>
        /// <param name="dir"></param>
        public void DeleteFolder(string dir)
        {
            foreach (string d in Directory.GetFileSystemEntries(dir))
            {
                if (File.Exists(d))
                {
                    FileInfo fi = new FileInfo(d);
                    if (fi.Attributes.ToString().IndexOf("ReadOnly") != -1)
                        fi.Attributes = FileAttributes.Normal;
                    File.Delete(d);//直接删除其中的文件 
                }
                else
                {
                    DirectoryInfo d1 = new DirectoryInfo(d);
                    if (d1.GetFiles().Length != 0)
                    {
                        DeleteFolder(d1.FullName);////递归删除子文件夹
                    }
                    Directory.Delete(d);
                }
            }
        }
        public struct StructCapture
        {
            public DateTime ImageTime { get; set; }
            public string ImageName { get; set; }
        }
        public struct StructImageAfter
        {
            public int x  { get; set; }
            public int y { get; set; }
  
        }
        public void FindLabel(System.Drawing.Image img)
        {


            var imgPath = Application.StartupPath + "Temp" + "\\" + Guid.NewGuid().ToString() + ".jpg";
            img.Save(imgPath, ImageFormat.Jpeg);
            var image = SixLabors.ImageSharp.Image.LoadAsync<Rgba32>(imgPath).Result;
            var predictions = yoloScorer.Predict(image, "person",0);
            foreach (var p in predictions)
            {
                var ractSize = 100;
                var (x1, y1) = (Convert.ToInt32(p.Rectangle.Left + (p.Rectangle.Right - p.Rectangle.Left) / 2),
                    Convert.ToInt32(p.Rectangle.Top + (p.Rectangle.Bottom - p.Rectangle.Top) / 2));
                if (this.D3W / 2 - ractSize <= x1 && x1 < this.D3W / 2 + ractSize
                    && this.D3H / 2 - ractSize <= y1 && y1 < this.D3H / 2 + ractSize)
                {
                    objdm.MoveTo(x1, y1);
                    objdm.LeftClick();
                }

            }
            Task.Run(() =>
             {
                 System.IO.File.Delete(imgPath);
             });

        }


        public void FindLabel(string imagePath,bool isSave=false)
        {

            using var image = SixLabors.ImageSharp.Image.LoadAsync<Rgba32>(imagePath).Result;
            var predictions = yoloScorer.Predict(image, "person",0);
            //log.Info($"==========单次=========推理：{this.stopwatch.ElapsedMilliseconds}毫秒");
            var rectSize = 300;
            foreach (var p in predictions)
            {
                var (x1, y1) = (Convert.ToInt32(p.Rectangle.Left + (p.Rectangle.Right - p.Rectangle.Left) / 2),
                    Convert.ToInt32(p.Rectangle.Top + (p.Rectangle.Bottom - p.Rectangle.Top) / 2));
                if (this.D3W / 2 - rectSize <= x1 && x1 < this.D3W / 2 + rectSize
                    && this.D3H / 2 - rectSize <= y1 && y1 < this.D3H / 2 + rectSize)
                {
                    objdm.MoveTo(x1, y1);
                    objdm.LeftClick();
               }
            }
            if (isSave)
            {


                foreach (var prediction in predictions) // draw predictions
                {
                    var score = Math.Round(prediction.Score, 2);

                    var (x, y) = (prediction.Rectangle.Left - 3, prediction.Rectangle.Top - 23);

                    image.Mutate(a => a.DrawPolygon(new Pen(prediction.Label.Color, 1),
                        new PointF(prediction.Rectangle.Left, prediction.Rectangle.Top),
                        new PointF(prediction.Rectangle.Right, prediction.Rectangle.Top),
                        new PointF(prediction.Rectangle.Right, prediction.Rectangle.Bottom),
                        new PointF(prediction.Rectangle.Left, prediction.Rectangle.Bottom)
                    ));

                    image.Mutate(a => a.DrawText($"{prediction.Label.Name} ({score})",
                        font, prediction.Label.Color, new PointF(x, y)));
                }

                image.Save(imagePath);

            }


        }
        ConcurrentQueue<StructImageAfter> structImageAfters = new ConcurrentQueue<StructImageAfter>();
        private void D3FJ_StartEvent()
        {
            // FindLabel(new StructCapture() { ImageName = Application.StartupPath  + jpgName, ImageTime = DateTime.Now });
            var TempPath = Application.StartupPath + "Temp";
            if (!Directory.Exists(TempPath))
            {
                Directory.CreateDirectory(TempPath);
            }
            DeleteFolder(TempPath);


            log.Info("=================截图开始================");

            ConcurrentStack<string> strings = new ConcurrentStack<string>();
            int fileCount = 0;
        
            
            Stopwatch sw = Stopwatch.StartNew();
            Parallel.For(0, 2, i =>
            {
                StartForThreadToList(() =>
                {
                    var imgPath = TempPath + "\\" + Guid.NewGuid().ToString() + ".bmp";
                    if (objdm.Capture(0, 0, this.D3W, this.D3H, imgPath) > 0)
                    {
                        //FindLabel(imgPath);
                        strings.Push(imgPath);
                        Interlocked.Increment(ref fileCount);
                    }
                });
            });

            StartForThreadToList(() =>
            {
                string[] strs=new string[1000];
                var itemCount=strings.TryPopRange(strs);
                if (itemCount == 0)
                    return;
                
                FindLabel(strs[0],true);
                Parallel.For(1, itemCount, i => {
                    File.Delete(strs[i]);
                });
   
                
            });
            




            //StartNewTaskToList(() =>
            //{


            //        //stopwatch.Restart();
            //        var imgPath = TempPath + "\\" + Guid.NewGuid().ToString() + ".bmp";
            //        if (objdm.Capture(0, 0, this.D3W, this.D3H, imgPath) > 0)
            //        {
            //            //FindLabel(imgPath);
            //            strings.Push(imgPath);
            //            Interlocked.Increment(ref fileCount);
            //        }

            //});
            AddStopTask(() =>
            {
                sw.Stop();
                log.Info($"==========截图结束=========执行时间：{sw.ElapsedMilliseconds}毫秒,生成文件数量：{fileCount}张，平均每张耗时:{Math.Round(sw.ElapsedMilliseconds * 1.0 / fileCount, 2)}毫秒");
            });
        }

    }
}
