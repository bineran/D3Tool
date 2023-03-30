using DMTools.libs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using DMTools.Config;
using DMTools.Control;
using Microsoft.ML.OnnxRuntime;
using System.Diagnostics;
using System.Drawing.Imaging;
//using Dm;

namespace DMTools.FunList
{

    [KeyName("测试")]
    public class D3Test : BaseD3
    {
        public const EnumD3 enumD3Name = EnumD3.测试;

        private SortedList<int, BagPoint> bagPointList = new SortedList<int, BagPoint>();

        public D3Test(D3Param d3Param, EnumD3 enumD3) : base(d3Param, enumD3)
        {
            this.StartEvent += D3FJ_StartEvent;

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
        private void D3FJ_StartEvent()
        {
            var TempPath = Application.StartupPath + "\\Temp";
            DeleteFolder(TempPath);

            //SessionOptions sessionOptions = new SessionOptions();
            //sessionOptions.GraphOptimizationLevel = GraphOptimizationLevel.ORT_ENABLE_ALL;
            //sessionOptions.AppendExecutionProvider_DML(1);
            log.Info("=================截图开始================" );
            int count = 100;
            int retOK = 0;
            int retBlack = 0;
           var st= new Stopwatch();
            //st.Start();
            //for (int i=0; i<count; i++) {
            //    //object obj1=new object();
            //    //object obj2=new object();
            //    //retOK+= objdm.Capture(0, 0, 1920, 1080, Application.StartupPath+"Temp\\"+ i.ToString()+".bmp");

            //    Bitmap image = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            //    Graphics imgGraphics = Graphics.FromImage(image);
            //    //设置截屏区域 柯乐义
            //    imgGraphics.CopyFromScreen(0,0 ,1920 ,1080 , new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height));
            //    image.Save(Application.StartupPath + "Temp\\" + i.ToString() + ".bmp");

            //}
            //st.Stop();
            //log.Info($"==========截图结束========={st.ElapsedMilliseconds}毫秒,成功数量：{retOK}张，平均一张:{st.ElapsedMilliseconds*1.0/ count}毫秒");
            DirectXScreenCapturer dx=new DirectXScreenCapturer();
            List <Task> tasks = new List<Task>();
            st.Restart();
            retOK = 0;
            retBlack = 0;


            for (int i = 0; i < count; i++)
            {

                //object obj1=new object();
                //object obj2=new object();
                //retOK+= objdm.Capture(0, 0, 1920, 1080, Application.StartupPath+"Temp\\"+ i.ToString()+".bmp");
                tasks.Add(Task.Run(() =>
                {
                   objdm.Capture(0, 0, 1920, 1080, TempPath + "\\" + i.ToString() + ".bmp");
                    //var (result, isBlackFrame, image) = dx.GetFrameImage(10);
                    //if (result.Success )
                    //{
                    //    retOK += 1;
                    //    //image.Save($@"{TempPath}\{DateTime.Now.Ticks}.jpg", ImageFormat.Jpeg);
                    //}
                    //if(isBlackFrame)
                    //{
                    //    retBlack += 1;
                    //}
                    //image?.Dispose();
                }));
                Task.Delay(10).Wait();
            }
            Task.WaitAll(tasks.ToArray());
            st.Stop();
            log.Info($"==========DX截图结束========={st.ElapsedMilliseconds}毫秒,成功:{retOK}张、黑屏:{retBlack}张、总数：{count}张，平均一张:{st.ElapsedMilliseconds * 1.0 / count}毫秒");

        }

    }
}
