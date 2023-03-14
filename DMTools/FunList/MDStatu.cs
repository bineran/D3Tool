using DMTools.Config;
using DMTools.Control;
using DMTools.libs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static DMTools.Stats;

namespace DMTools.FunList
{
    [KeyName(@"
魔电自动喝药：
类型：需要配置一条任意按键的点击
四屏药的方框范围   Int1,INT2,Int3,Int4 对应 x1,y1,x2,y2
D1  1-100 低于多少血的百分比
D2  喝药的间隔
Str1 药的图片，不要太大
会优先从第四瓶药水喝起，需要设置 对应的1234的按键
")]
    public class MDStatu : BaseD3
    {
        public const EnumD3 enumD3Name = EnumD3.魔电血量;

        private SortedList<int, BagPoint> bagPointList = new SortedList<int, BagPoint>();
        MainWindow mainWindow;
        public MDStatu(D3Param d3Param, EnumD3 enumD3) : base(d3Param, enumD3)
        {
            this.StartEvent += MDStatu_StartEvent;
            //mainWindow;= new MainWindow();
        }

        public List<T> GetMemoryData<T>(byte[] bytes, int structSize)
        {

            T[] structure = new T[bytes.Length/structSize];

            byte[] buffer = new byte[structSize];
            int structureIndex = 0;
            for (int i = 0; i < bytes.Length; i += structSize)
            {
                Buffer.BlockCopy(bytes, i, buffer, 0, structSize);

                GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                structure[structureIndex] = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
                handle.Free();

                structureIndex++;
            }
           
            return new List<T>(structure);
        }
      
        private Tuple<int,int> ConvertCache(List<TagStat> al)
        {
             int[] cacheStats = new int[8];
            for (int i = 0; i < al.Count; i++)
            {
                int wStatIndex = (int)al[i].wStatIndex;
                if (wStatIndex < 1024)
                {
                    int dwStatValue = al[i].dwStatValue;
                    int num2 = wStatIndex;
                    if (num2 >= 6 && num2 <= 11)
                    {
                        cacheStats[ wStatIndex] += dwStatValue / 256;
                    }
                    else
                    {
                        cacheStats[ wStatIndex] += dwStatValue;
                    }
                }
            }
            return new Tuple<int, int>(cacheStats[6], cacheStats[7]);
        }
        private void MDStatu_StartEvent()
        {
            var objdm = this.CreateDM();

            //0x6fab0000

            var address1 = "[[[<D2Client.dll>+0011BBFC]+5C]+48]";
  
            //var sss = objdm.GetModuleBaseAddr(this.Handle, "D2Client.dll");
            //var sssdfsdfs=new IntPtr(sss);
            //var num1 = objdm.READ(this.Handle, address1, 0);
            //var num2 = objdm.ReadData(this.Handle, address1, 0);

            //log.Info($"生命:{num1}/{num2}");


            var ts = this.Times.FirstOrDefault(r =>
            r.keyClickType== KeyClickType.点击 &&
            r.D1<=100 &&
            r.Int1>0 && r.Int2>0 &&
            r.Int3>0 && r.Int4>0 && r.D1>0 && r.D2>0
            && r.Str1.TrimLength()>0
            );
            if(ts!=null )
            {
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


                while (true)
                {
                    var byteStr = objdm.ReadData(this.Handle, address1, 8 * 8);

                    var bytes = byteStr.HexStringToByteArray();
                    var tagList = GetMemoryData<TagStat>(bytes, 8);

                    var items = ConvertCache(tagList);


                    //var items = mainWindow.GetLifeStatu();
                    if (items.Item1 * 100.0 / items.Item2 <= ts.D1)
                    {
                        FindeYS(ts, objdm, allpic);
                        continue;
                    }
                    Sleep(500); 
                }
            }
  
        }
        private void FindeYS(KeyTimeSetting ts, Idmsoft objdm,string allpic)
        {

            var x1 = ts.Int1;
            var y1 = ts.Int2;
            var x2 = ts.Int3;
            var y2 = ts.Int4;
            object ox;
            object oy;
            var ret = objdm.FindPic(x1, y1, x2, y2, allpic, this.d3Param.sysConfig.delta_color, this.d3Param.sysConfig.sim, 2, out ox, out oy);
            if (ret > -1)
            {
                int x, y;
                x = Convert.ToInt32(ox);
                y = Convert.ToInt32(oy);
                var w = (x2 * 1.0 - x1) / 4;
                var tmpcode = 1;
                for (int i = 1; i < 5; i++)
                {
                    if (x < x1 + i * w)
                    {
                        tmpcode = i;
                        break;
                    }

                }
                var p=typeof(D3KeyCodes).GetProperty("Key" + tmpcode);
                if (p != null)
                {
                    var keyCode = Convert.ToInt32(p.GetValue(this.d3Param.KeyCodes));
                    objdm.KeyPress(keyCode);
                    Sleep(ts.D2);
                }
            }
            
        }
    }
}
