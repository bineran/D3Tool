
using DMTools.Control;
using Microsoft.ML.OnnxRuntime;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Collections.Concurrent;
using Yolov5Net.Scorer.Models;
using Yolov5Net.Scorer;
using SixLabors.ImageSharp.PixelFormats;

using SixLabors.ImageSharp.Processing;


using SixLabors.ImageSharp;
using Microsoft.ML.OnnxRuntime;

using static DMTools.FunList.D3Test;
using PointF = SixLabors.ImageSharp.PointF;

//using Dm;


namespace DMTools.FunList
{

    [KeyName("测试")]
    public class D3Test : BaseD3
    {
        public const EnumD3 enumD3Name = EnumD3.测试;
       // SixLabors.Fonts.Font font = new SixLabors.Fonts.Font(new FontCollection().Add("Assets/consola.ttf"), 16);

        string jpgName = "Assets\\test.jpg";
        string onnxName = "Assets\\Weights\\csgo.onnx";
        int LabelCount = 2;
        int YoloScorerCount = 5;
        YoloScorer<YoloCocoP5Model> yoloScorer;
        private SortedList<int, BagPoint> bagPointList = new SortedList<int, BagPoint>();

        public D3Test(D3Param d3Param, EnumD3 enumD3) : base(d3Param, enumD3)
        {
            this.StartEvent += D3FJ_StartEvent;
            SessionOptions sessionOptions = new SessionOptions();
            sessionOptions.GraphOptimizationLevel = GraphOptimizationLevel.ORT_ENABLE_ALL;
            sessionOptions.AppendExecutionProvider_DML();
            yoloScorer = new YoloScorer<YoloCocoP5Model>(onnxName, LabelCount, YoloScorerCount, sessionOptions);
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


        public void FindLabel(string imagePath,int index)
        {

            using var image = SixLabors.ImageSharp.Image.LoadAsync<Rgba32>(imagePath).Result;
            var predictions = yoloScorer.Predict(image, "person",0);
            deleteFiles.Enqueue(imagePath);
            Interlocked.Increment(ref PredictCount);
            //log.Info($"==========单次=========推理：{this.stopwatch.ElapsedMilliseconds}毫秒");
            var rectSize = 300;
            foreach (var p in predictions)
            {
                var (x1, y1) = (Convert.ToInt32(p.Rectangle.Left + (p.Rectangle.Right - p.Rectangle.Left) / 2),
                    Convert.ToInt32(p.Rectangle.Top + (p.Rectangle.Bottom - p.Rectangle.Top) / 2));
                if (this.D3W / 2 - rectSize <= x1 && x1 < this.D3W / 2 + rectSize
                    && this.D3H / 2 - rectSize <= y1 && y1 < this.D3H / 2 + rectSize)
                {
                    findAfter.Push((x1, y1));
                    return;
               }
            }


        }

        ConcurrentStack<string> strings = new ConcurrentStack<string>();
        ConcurrentStack<(int,int)> findAfter = new ConcurrentStack<(int, int)>();
        ConcurrentQueue<string> deleteFiles = new ConcurrentQueue<string>();
        int FileCount = 0;
        int PredictCount = 0;
        private void D3FJ_StartEvent()
        {
            // FindLabel(new StructCapture() { ImageName = Application.StartupPath  + jpgName, ImageTime = DateTime.Now });
            var TempPath = Application.StartupPath + "Temp";
            if (!Directory.Exists(TempPath))
            {
                Directory.CreateDirectory(TempPath);
            }
            DeleteFolder(TempPath);
            FileCount = 0;
            PredictCount = 0;

            log.Info("=================截图开始================");

        
        
        
            
            Stopwatch sw = Stopwatch.StartNew();
            Parallel.For(0, 1, i =>
            {
                StartForThreadToList(() =>
                {
                    var imgPath = TempPath + "\\" +DateTime.Now.Ticks.ToString()+ ".bmp";
                    if (objdm.Capture(0, 0, this.D3W, this.D3H, imgPath) > 0)
                    {
                        strings.Push(imgPath);
                        Interlocked.Increment(ref FileCount);
                    }
                    System.Threading.Thread.Sleep(5);
                });
            });
            // yolo识别
            StartForThreadToList(() =>
            {
                string[] strs = new string[1000];
                var itemCount = strings.TryPopRange(strs);
                if (itemCount == 0)
                {
                    System.Threading.Thread.Sleep(2);
                    return;
                }
                //取最后三张图片
                Parallel.For(0, YoloScorerCount, i =>
                {
                    if (i < itemCount)
                    {
                        FindLabel(strs[i],i);
                    }
                });
                //删除多余的文件
                if (itemCount > YoloScorerCount)
                {
                    foreach (var str in strs.Skip(YoloScorerCount))
                    {
                        deleteFiles.Enqueue(str);
                    }
                }

            });

            //删除生成的图片
            StartForThreadToList(() =>
            {
                string str;
                if (deleteFiles.TryDequeue(out str))
                {
                    if (!string.IsNullOrEmpty(str))
                    {
                        File.Delete(str);
                    }
               
                }
                else { 
                    Thread.Sleep(2);
                }
                

            }
            );

            AddStopTask(() =>
            {
                sw.Stop();
                log.Info($"==========截图结束=========执行时间：" +
                    $"{sw.ElapsedMilliseconds}毫秒,生成文件数量：" +
                    $"{FileCount}张，平均每张耗时:" +
                    $"{Math.Round(sw.ElapsedMilliseconds * 1.0 / FileCount, 2)}毫秒,"+
                    $"同时AI处理{YoloScorerCount}张,AI共识图:{PredictCount}张,平均每张AI识图:{Math.Round(sw.ElapsedMilliseconds * 1.0 / PredictCount, 2)}毫秒");
            });
        }

    }
}
