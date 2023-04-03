using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tesseract;

namespace Test
{
    public class OCR
    {
        public static string TesseractOCR(string imagePath)
        {


            //Tesseract.Page    chi_sim为中文训练数据包  
            Page page = new TesseractEngine(Application.StartupPath + @"Assets", "chi_sim", EngineMode.Default).Process(Pix.LoadFromFile(imagePath));

            //释放程序对图片的占用
       

            //打印识别率
            Console.WriteLine(String.Format("{0:P}", page.GetMeanConfidence()));

            //打印识别文本 //替换'/n'为'(空)'//替换'(空格)'为'(空)'
            string s = page.GetText().Replace("\n", "").Replace(" ", "");
            Console.WriteLine(s);
            return s;
        }

    }
}
