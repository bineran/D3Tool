using Dm;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading;
using System.Text;
using System.Threading.Tasks;

namespace D3Tool
{




    public class T_DiabloProcess
    {
        public object o=3;

        public void 监视文件按4(List<Thread> alth, T_Time tt, List<Thread> althstop)
        {
            alth.Clear();
            var th = new Thread(new ThreadStart(delegate()
            {
                FileSystemWatcher watcher = new FileSystemWatcher();
                watcher.Path = @"D:\Files";
                watcher.NotifyFilter = NotifyFilters.LastWrite;
                watcher.Filter = "ShadowPower.txt";
                DateTime t = DateTime.Now;
                watcher.Changed += (object sender, FileSystemEventArgs e) =>
                {
                    try
                    {
                        lock (o)
                        {
                            if (DateTime.Now > t.AddSeconds(30))
                            {
                                objdm.KeyDown(D3Config.KEYS.Key4);
                               
                                Sleep(Convert.ToInt32(tt.Key4*1000));

                                objdm.KeyUp(D3Config.KEYS.Key4);
                                t = DateTime.Now;
                                System.IO.File.AppendAllText(@"D:\Files\ShadowPower_result.txt", DateTime.Now.ToString("     yyyy-MM-dd HH:mm:ss    "));

                            }
                        }
                    }
                    catch
                    {
                    }
                };
                watcher.EnableRaisingEvents = true;

            }));
            th.IsBackground = true;
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            alth.Add(th);
            althstop.Clear();
        }
         



        public void FN(List<Thread> alth, T_Time tt, List<Thread> althstop)
        {
 
            tt.FNType = 0;
            tt.FNMax = 1;
            switch (tt.fmode)
            {

                case EnumD3.流放原地第一技能:
                    ting(alth, tt, althstop);

                    CreateThread(this.流放原地第一技能, alth, tt, althstop);
                     break;
                case EnumD3.流放一键吃药:


                    CreateThread(this.流放一键吃药, alth, tt, althstop);
                     break;
              case EnumD3.先停:
         

                     ting(alth, tt, althstop);
                     break;
                case EnumD3.天谴加马:
                    tt.FNType = 1;
                    tt.FNMax = 2;

                     ting(alth, tt, althstop);
                     CreateThread(this.天谴加马, alth, tt, althstop);
                    break;

                case EnumD3.奥陨宏:
                     ting(alth, tt, althstop);
                    CreateThread(this.奥陨宏, alth, tt, althstop);
                    break;
                case EnumD3.奥陨宏2:
                    ting(alth, tt, althstop);
                    CreateThread(this.奥陨宏2, alth, tt, althstop);
                    break;
                case EnumD3.一直按Q定时按1234:
                    ting(alth, tt, althstop);
                    CreateThread(this.一直按Q定时按1234, alth, tt, althstop);
                    break;
                case EnumD3.蛮子先停再3:
                      ting(alth, tt, althstop);
                      CreateThread(this.蛮子先停再3, alth, tt, althstop, 100);
                      open(alth, tt, althstop, Convert.ToInt32(tt.KeyR * 1000));
                    break;

                case EnumD3.监视文件按4:
                    监视文件按4(alth, tt, althstop);
                    break;
                case EnumD3.不做操作:
                    break;
                case EnumD3.原地左键定时按_1_2_3_4_右键_空格暂停:
                    原地左键定时按_1_2_3_4_右键_空格暂停(alth, tt, althstop);
                    break;
                case EnumD3.原地右键定时按_1_2_3_4_左键_空格暂停:
                    原地右键定时按_1_2_3_4_左键_空格暂停(alth, tt, althstop);
                    break;
                case EnumD3.先停_原地右键定时按_1_2_3_4_左键_空格暂停:
                    ting(alth, tt, althstop);
                    原地右键定时按_1_2_3_4_左键_空格暂停(alth, tt, althstop);
                    break;
                case EnumD3.先停再_原地依次按_1_2_3_4:
                    ting(alth, tt, althstop);
                    先停再_原地依次按_1_2_3_4(alth, tt, althstop);
                    break;
               
                    

                case EnumD3.一直按住强制移动定时按_1_2_3_4_左键_右键:
                    一直按住强制移动定时按_1_2_3_4_左键_右键(alth, tt, althstop);
                    break;
                case EnumD3.先停_定时按_1_2_3_4_左键_右键_神龙猴子:
                    ting(alth, tt, althstop);
                    定时按_1_2_3_4_左键_右键_神龙猴子(alth, tt, althstop);
                    break;
                case EnumD3.先停_按住强制移动定时按_2_3_4_左键_右键_金钟破:
                    ting(alth, tt, althstop);
                    先停_按住强制移动定时按_2_3_4_左键_右键_金钟破(alth, tt, althstop);
                    break;
                case EnumD3.先停_按住强制移动定时按_1_2_3_4_左键_右键:
                    ting(alth, tt, althstop);
                         CreateThread(  一直按住强制移动定时按_1_2_3_4_左键_右键,alth, tt, althstop);
                    break;
                case EnumD3.不停一直按_1_2_3_4_左键_右键:
                    CreateThread(不停一直按_1_2_3_4_左键_右键, alth, tt, althstop);
                    break;
                    
                case EnumD3.定时按_1_2_3_4_左键_右键:
                    定时按_1_2_3_4_左键_右键(alth, tt, althstop);
                    break;
                case EnumD3.先停_定时按_1_2_3_4_左键_右键:
                    ting(alth, tt, althstop);
                    CreateThread(  定时按_1_2_3_4_左键_右键,alth, tt, althstop);
                    break;
                case EnumD3.地震:
                    ting(alth, tt, althstop);
                    CreateThread(地震, alth, tt, althstop);
                    break;
                    
                case EnumD3.原地_依次按_1_2_3_4_左键_右键:
                    原地1次_依次按_1_2_3_4_左键_右键(alth, tt, althstop);
                    break;
                case EnumD3.先停_左键循环:
                    ting(alth, tt, althstop);
                    CreateThread(this.左键循环, alth, tt, althstop, 100);
                    break;
                case EnumD3.持续点击左键一段时间:
                    持续点击左键一段时间(alth, tt, althstop);
                    break;
                    
                case EnumD3.单次_按3_1_2:
                    单次_按3_1_2(alth, tt, althstop);
                    break;
                case EnumD3.单次_按2_1:
                    单次_按2_1(alth, tt, althstop);
                    break;
                case EnumD3.定时按_1_2_3_4_右键_空格不暂停:
                    定时按_1_2_3_4_右键_空格不暂停(alth, tt, althstop);
                    break;
                case EnumD3.先停功能_单次_按3_1_2:
                    ting(alth, tt, althstop);
                    单次_按3_1_2(alth, tt, althstop);
                    break;
                case EnumD3.原地2次_依次按_1_2_3_4_左键_右键:
                    this.原地2次_依次按_1_2_3_4_左键_右键(alth, tt, althstop);
                    break;
                case EnumD3.先停再_原地左键定时按_1_2_3_4_右键_空格暂停:
                    ting(alth, tt, althstop);
                 
                  CreateThread(  this.先停再_原地左键定时按_1_2_3_4_右键_空格暂停,alth, tt, althstop);
                    break;

                                    case EnumD3.先停再_原地左键定时按_1_2_3_4_右键_空格连点:
                    ting(alth, tt, althstop);
                    先停再_原地左键定时按_1_2_3_4_右键_空格连点(alth, tt, althstop);
                    break;


                case EnumD3.火蝠转转转:
                    ting(alth, tt, althstop);

                    CreateThread(this.火蝠转转转, alth, tt, althstop);
                    break;
                case EnumD3.魂弹:
                    ting(alth, tt, althstop);

                    CreateThread(this.魂弹, alth, tt, althstop);
                    break;
                case EnumD3.魂弹自动:
                    ting(alth, tt, althstop);

                    CreateThread(this.魂弹自动, alth, tt, althstop);
                    break;
                    
                case EnumD3.先停_按住1_定时按2_3_4_左键_右键:
                    ting(alth, tt, althstop);

                    CreateThread(this.先停_按住1_定时按2_3_4_左键_右键, alth, tt, althstop);
                    break;

                case EnumD3.三刀飞刀_左键:
                    ting(alth, tt, althstop);

                    CreateThread(this.三刀飞刀_左键, alth, tt, althstop);
                    break;

                case EnumD3.先停_按住右键_定时按_1_2_3_4_按住空格连点:
                         ting(alth, tt, althstop);

                         CreateThread(this.先停_按住右键_定时按_1_2_3_4_按住空格连点, alth, tt, althstop);
                    break;
                case EnumD3.三刀飞刀:
                    ting(alth, tt, althstop);

                    CreateThread(this.三刀飞刀, alth, tt, althstop);
                    break;
                    
                case EnumD3.先停_原地按住左键_然后按住右键定时按1_2_3_4:
                    ting(alth, tt, althstop);

                    CreateThread(this.先停_原地按住左键_然后按住右键定时按1_2_3_4, alth, tt, althstop);
                    break;
                case EnumD3.先停_原地按右键_然后按住左键定时按1_2_3_4:
                    ting(alth, tt, althstop);

                    CreateThread(this.先停_原地按右键_然后按住左键定时按1_2_3_4, alth, tt, althstop);
                    break;
                    
                    
                    
                case EnumD3.三秒火_按住2键_奥术洪流_和强制移动:
                    this.三秒火_按住2键_奥术洪流_和强制移动(alth, tt, althstop);
                    break;
                case EnumD3.检查1功能再_单次_按3_1_2:
                    if (alth == alth2)
                    {
                        if (alth1.Count == 0)
                        {
                            FN1(D3Config.PLAN);
                        }
                    }
                    else if (alth == alth1)
                    {
                        if (alth2.Count == 0)
                        {
                            FN2(D3Config.PLAN);
                        }
                    }
                    this.单次_按3_1_2(alth, tt, althstop);
                    break;
                case EnumD3.先停_原地和强制移动_定时按_1_2_3_4_左键_右键:
                    ting(alth, tt, althstop);
                  CreateThread(  this.原地和强制移动_定时按_1_2_3_4_检测左键_定时右键,alth, tt, althstop);
                    break;
                case EnumD3.P闪_再用技能1_2_3_4_非指向:
                    this.P闪_再用技能1_2_3_4_非指向(alth, tt, althstop);
                    break;
                case EnumD3.P闪_再用技能1_2_3_4_指向:
                    this.P闪_再用技能1_2_3_4_指向(alth, tt, althstop);
                    break;
                case EnumD3.先停原地再1左键1_选择右键:
                    ting(alth, tt, althstop);
                    this.先停原地再1左键1_选择右键(alth, tt, althstop);
                    break;
                case EnumD3.先停_原地依次按_1_2_3_4_左键_4_休眠右键的时间_再开一:
                    ting(alth, tt, althstop);
                    this.原地1次_依次按_1_2_3_4_左键_4(alth, tt, althstop);
           
                  open(alth, tt, althstop, Convert.ToInt32(tt.KeyR * 1000));
                 
                    break;
                case EnumD3.先停_原地依次按_1_2_3_4_右键_休眠左键的时间_再开一:
       
                    ting(alth, tt, althstop);
                    this.Sleep(Convert.ToInt32(tt.KeyL * 1000));
                   
                    this.原地依次按_1_2_3_4_按住右键(alth, tt, althstop);
               
                    open(alth, tt, althstop, Convert.ToInt32(tt.KeyL * 1000));
               

                    break;
                case EnumD3.分解传奇:
                    分解传奇(alth, tt, althstop);
                    break;
                case EnumD3.三刀翻滚:
                    ting(alth, tt, althstop);
                    三刀翻滚(alth, tt, althstop);
                    break;
                case EnumD3.死灵放大:
                    ting(alth, tt, althstop);
                    死灵放大(alth, tt, althstop);
                    break;

                case EnumD3.先停_按住最大数量_定时按1_2_3_4_左键_右键:
                    ting(alth, tt, althstop);
                    CreateThread(this.先停_按住最大数量_定时按1_2_3_4_左键_右键, alth, tt, althstop, 100);
                    break;
                case EnumD3.先停_按住最大数量原地_定时按1_2_3_4_左键_右键:
                    ting(alth, tt, althstop);
                    CreateThread(this.先停_按住最大数量原地_定时按1_2_3_4_左键_右键, alth, tt, althstop, 100);
                    break;
                    
            }
        }
        public void CreateThread(Action<List<Thread>, T_Time, List<Thread>> a, List<Thread> alth, T_Time tt, List<Thread> althstop,int sleeptime=100)
        {
            Thread thtmp = new Thread(new ThreadStart(() =>
                  {
                      this.Sleep(sleeptime);
                      a(alth, tt, althstop);
                  }));
            thtmp.Start();
        }
        int lastindex = 0;
        public void ting(List<Thread> alth, T_Time tt, List<Thread> althstop)
        {
            Frm_Main.right_time = DateTime.Now.AddDays(-1);
            Frm_Main.left_time = DateTime.Now.AddDays(-1);
            if (alth == alth1)
            {

                Stop2();
                Stop3();
                Stop4();
            }
            else if (alth == alth2)
            {
                Stop1();
                Stop3();
                Stop4();
            }
            else if (alth == alth3)
            {
                Stop1();
                Stop2();
                Stop4();
            }
            else if (alth == alth4)
            {
                Stop1();
                Stop2();
                Stop3();
 
            }
       
        }
        public void startLast()
        {
 
        }
        public void Suspend_Resume(List<Thread> alth, decimal centertime)
        {
            if (alth == alth1)
            {
          
                Suspend_Resume(2, centertime);
                Suspend_Resume(3, centertime);
                Suspend_Resume(4, centertime);
            }
            else if (alth == alth2)
            {
                Suspend_Resume(1, centertime);
       
                Suspend_Resume(3, centertime);
                Suspend_Resume(4, centertime);
            }
            else if (alth == alth3)
            {
                Suspend_Resume(1, centertime);
                Suspend_Resume(2, centertime);
        
                Suspend_Resume(4, centertime);
            }
            else if (alth == alth4)
            {
                Suspend_Resume(1, centertime);
                Suspend_Resume(2, centertime);
                Suspend_Resume(3, centertime);
           

            }
        }
        public void open(List<Thread> alth, T_Time tt, List<Thread> althstop, int centerTime)
        {
            Thread th = new Thread(new ThreadStart(() =>
              {
                  this.Sleep(centerTime);
                  if (alth == alth2)
                  {

                      FN1(D3Config.PLAN);
                  }
                  else if (alth == alth1)
                  {
                      FN2(D3Config.PLAN);
                  }
                  else
                  {
                      FN1(D3Config.PLAN);
                  }
              }));
            th.Start();
        }
        public void P闪_再用技能1_2_3_4_非指向(List<Thread> alth, T_Time tt, List<Thread> althstop)
        {
            alth.Clear();
            Thread th = new Thread(new ThreadStart(() =>
            {
    
                objdm.KeyPressChar("p");
                this.Sleep(50);
                objdm.LeftClick();
                this.Sleep(Convert.ToInt32(tt.KeyL * 1000));
                for (int i = 0; i < 1; i++)
                {
                    if (tt.Key1 > 0)
                    {
                        this.Sleep(Convert.ToInt32(tt.Key1 * 1000));
                        objdm.KeyPress(D3Config.KEYS.Key1);
                    }
                    if (tt.Key2 > 0)
                    {
                        this.Sleep(Convert.ToInt32(tt.Key2 * 1000));
                        objdm.KeyPress(D3Config.KEYS.Key2);

                    }
                    if (tt.Key3 > 0)
                    {
                        this.Sleep(Convert.ToInt32(tt.Key3 * 1000));
                        objdm.KeyPress(D3Config.KEYS.Key3);

                    }
                    if (tt.Key4 > 0)
                    {
                        this.Sleep(Convert.ToInt32(tt.Key4 * 1000));
                        objdm.KeyPress(D3Config.KEYS.Key4);

                    }
         
                    //if (tt.KeyL > 0)
                    //{
                    //    this.Sleep(Convert.ToInt32(tt.KeyL * 1000));
                    //    objdm.LeftClick();
                    //}
                    //if (tt.KeyR > 0)
                    //{
                    //    this.Sleep(Convert.ToInt32(tt.KeyR * 1000));
                    //    objdm.RightClick();
                    //}
                }
            }));
            th.IsBackground = true;
            th.SetApartmentState(ApartmentState.STA);
            th.Start();

        }
        public void 先停原地再1左键1_选择右键(List<Thread> alth, T_Time tt, List<Thread> althstop)
        {
            var th = new Thread(new ThreadStart(delegate()
            {
                objdm.KeyDown(D3Config.KEYS.Key_Stand);
                this.Sleep(50);
               
                objdm.KeyPress(D3Config.KEYS.Key1);

                this.Sleep(50);
                if (tt.KeyR > 0)
                {

                    // this.Sleep(Convert.ToInt32(tt.KeyR * 1000));
                    objdm.RightClick();
                    this.Sleep(50);
                }
                objdm.LeftClick();
                this.Sleep(150);
                objdm.KeyPress(D3Config.KEYS.Key1);
                this.Sleep(50);

                objdm.KeyUp(D3Config.KEYS.Key_Stand);
               
                if(tt.KeyL>0)
                {
                    this.Sleep(200);
                    objdm.KeyDown(D3Config.KEYS.Key_Move);
                    this.Sleep(Convert.ToInt32(tt.KeyL * 1000));
                    objdm.KeyUp(D3Config.KEYS.Key_Move);
                }
          
            }));
            th.IsBackground = true;
            th.SetApartmentState(ApartmentState.STA);
            th.Start();

        }
        public void P闪_再用技能1_2_3_4_指向(List<Thread> alth, T_Time tt, List<Thread> althstop)
        {
            alth.Clear();
            Thread th = new Thread(new ThreadStart(() =>
            {
                objdm.KeyPress(112);
                objdm.KeyPressChar("p");
                this.Sleep(Convert.ToInt32(tt.KeyL * 1000));
                objdm.LeftClick();

                for (int i = 0; i < 1; i++)
                {
                    if (tt.Key1 > 0)
                    {
                        this.Sleep(Convert.ToInt32(tt.Key1 * 1000));
                        objdm.KeyPress(D3Config.KEYS.Key1);
                        this.Sleep(Convert.ToInt32(tt.KeyL * 1000));
                        objdm.LeftClick();
                    }
                    if (tt.Key2 > 0)
                    {
                        this.Sleep(Convert.ToInt32(tt.Key2 * 1000));
                        objdm.KeyPress(D3Config.KEYS.Key2);
                        this.Sleep(Convert.ToInt32(tt.KeyL * 1000));
                        objdm.LeftClick();

                    }
                    if (tt.Key3 > 0)
                    {
                        this.Sleep(Convert.ToInt32(tt.Key3 * 1000));
                        objdm.KeyPress(D3Config.KEYS.Key3);
                        this.Sleep(Convert.ToInt32(tt.KeyL * 1000));
                        objdm.LeftClick();

                    }
                    if (tt.Key4 > 0)
                    {
                        this.Sleep(Convert.ToInt32(tt.Key4 * 1000));
                        objdm.KeyPress(D3Config.KEYS.Key4);
                        this.Sleep(Convert.ToInt32(tt.KeyL * 1000));
                        objdm.LeftClick();

                    }
                    objdm.KeyPress(112);
                    //if (tt.KeyL > 0)
                    //{
                    //    this.Sleep(Convert.ToInt32(tt.KeyL * 1000));
                    //    objdm.LeftClick();
                    //}
                    //if (tt.KeyR > 0)
                    //{
                    //    this.Sleep(Convert.ToInt32(tt.KeyR * 1000));
                    //    objdm.RightClick();
                    //}
                }
            }));
            th.IsBackground = true;
            th.SetApartmentState(ApartmentState.STA);
            th.Start();

        }
        public void 左键循环(List<Thread> alth, T_Time tt, List<Thread> althstop)
        {
            var th = new Thread(new ThreadStart(delegate()
            {
                var t = Convert.ToInt32(tt.KeyL * 1000);
                while (true)
                {
                    if (!isD3)
                    {
                        System.Threading.Thread.Sleep(300);
                        continue;
                    }
                    objdm.LeftClick();
                    this.Sleep(t);
                }
            }));
            th.IsBackground = true;
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            alth.Add(th);
        }
        public void 持续点击左键一段时间(List<Thread> alth, T_Time tt, List<Thread> althstop)
        {
            var th = new Thread(new ThreadStart(delegate()
            {
                var t = DateTime.Now.AddMilliseconds(Convert.ToInt32(tt.KeyL * 1000));
                while (true)
                {
                    if (!isD3)
                    {
                        System.Threading.Thread.Sleep(300);
                        continue;
                    }
                    objdm.LeftClick();
                    this.Sleep(20);
                    if (DateTime.Now > t)
                    { break; }
                }
            }));
            th.IsBackground = true;
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            alth.Add(th);
        }

        public void 先停再_原地左键定时按_1_2_3_4_右键_空格连点(List<Thread> alth, T_Time tt, List<Thread> althstop)
        {

            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key1, tt.Key1));
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key2, tt.Key2));
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key3, tt.Key3));
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key4, tt.Key4));
            alth.Add(CreateMouseRightPressThread(tt.KeyR));


            var th = new Thread(new ThreadStart(delegate()
            {

                while (true)
                {
                    if (!isD3)
                    {
                        System.Threading.Thread.Sleep(100);
                        continue;
                    }
                    if (isspack)
                    {
                   
                            objdm.KeyUp(D3Config.KEYS.Key_Stand);
                        
                      
                        
                        objdm.LeftClick();
                        System.Threading.Thread.Sleep(50);
                        continue;
                    }
                    else
                    {
                        if (!isleft)
                        {
                            objdm.LeftDown();
                        }
                        objdm.KeyDown(D3Config.KEYS.Key_Stand);
                        System.Threading.Thread.Sleep(200);
                    }
                }
            }));
            th.IsBackground = true;
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            alth.Add(th);
            althstop.Clear();
            var tmpth = new Thread(new ThreadStart(delegate()
            {
                objdm.LeftUp();
                objdm.RightUp();
                objdm.KeyUp(D3Config.KEYS.Key_Stand);

            }));
            tmpth.IsBackground = true;
            tmpth.SetApartmentState(ApartmentState.STA);
            althstop.Add(tmpth);

        }

      
        public void 先停再_原地左键定时按_1_2_3_4_右键_空格暂停(List<Thread> alth, T_Time tt, List<Thread> althstop)
        {

            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key1, tt.Key1));
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key2, tt.Key2));
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key3, tt.Key3));
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key4, tt.Key4));
            alth.Add(CreateMouseRightPressThread(tt.KeyR));


            var th = new Thread(new ThreadStart(delegate()
            {
            
                objdm.KeyDown(D3Config.KEYS.Key_Stand);
                while (true)
                {
                    if (!isD3)
                    {
                        System.Threading.Thread.Sleep(100);
                        continue;
                    }
                    if (isspack)
                    {
                        objdm.KeyUp(D3Config.KEYS.Key_Stand);
                        objdm.LeftUp();
                        System.Threading.Thread.Sleep(200);
                        continue;
                    }
                    else
                    {
                        if (!isleft)
                        {
                            objdm.LeftDown();
                        }
                        objdm.KeyDown(D3Config.KEYS.Key_Stand);
                        System.Threading.Thread.Sleep(200);
                    }
                }
            }));
            th.IsBackground = true;
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            alth.Add(th);
            althstop.Clear();
            var tmpth = new Thread(new ThreadStart(delegate()
            {
                objdm.LeftUp();
                objdm.RightUp();
                objdm.KeyUp(D3Config.KEYS.Key_Stand);

            }));
            tmpth.IsBackground = true;
            tmpth.SetApartmentState(ApartmentState.STA);
            althstop.Add(tmpth);

        }

      
        public void 先停_按住1_定时按2_3_4_左键_右键(List<Thread> alth, T_Time tt, List<Thread> althstop)
        {


            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key2, tt.Key2));
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key3, tt.Key3));
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key4, tt.Key4));
            alth.Add(CreateMouseLeftPressThread(tt.KeyL));
            alth.Add(CreateMouseRightPressThread(tt.KeyR));


            var th = new Thread(new ThreadStart(delegate()
            {

          
                //objdm.KeyDown(D3Config.KEYS.Key_Stand);
                 var t1 = Convert.ToInt32(tt.Key1 * 1000);

                while (true)
                {
                    if (!isD3)
                    {
                        System.Threading.Thread.Sleep(100);
                        continue;
                    }
                    if (isRight)
                    {
                        objdm.KeyUp(D3Config.KEYS.Key1);
                        System.Threading.Thread.Sleep(200);
                        continue;
                    }
                    if (isspack)
                    {
                        objdm.KeyUp(D3Config.KEYS.Key1);
                        System.Threading.Thread.Sleep(50);
                        continue;
                    }
                    else
                    {
                        this.Sleep(5);
                        if(!iskey1)
                            objdm.KeyDown(D3Config.KEYS.Key1);
                    }
                }
            }));
            th.IsBackground = true;
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            alth.Add(th);
            althstop.Clear();
            var tmpth = new Thread(new ThreadStart(delegate()
            {
                objdm.LeftUp();
                objdm.RightUp();
                objdm.KeyUp(D3Config.KEYS.Key1);
                objdm.KeyUp(D3Config.KEYS.Key_Stand);
       

            }));
            tmpth.IsBackground = true;
            tmpth.SetApartmentState(ApartmentState.STA);
            althstop.Add(tmpth);

        }

        public void 先停_按住最大数量_定时按1_2_3_4_左键_右键(List<Thread> alth, T_Time tt, List<Thread> althstop)
        {
            var keymax = D3Config.KEYS.Key1;
            decimal ttmax = tt.Key1;

            if (tt.Key2 > ttmax)
            {
                keymax = D3Config.KEYS.Key2;
                ttmax = tt.Key2;
            }
            if (tt.Key3 > ttmax)
            {
                keymax = D3Config.KEYS.Key3;
                ttmax = tt.Key3;
            }
            if (tt.Key4 > ttmax)
            {
                keymax = D3Config.KEYS.Key4;
                ttmax = tt.Key4;
            }

            if (keymax != D3Config.KEYS.Key1)
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key1, tt.Key1));
            if (keymax != D3Config.KEYS.Key2)
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key2, tt.Key2));
            if (keymax != D3Config.KEYS.Key3)
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key3, tt.Key3));
            if (keymax != D3Config.KEYS.Key4)
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key4, tt.Key4));

            alth.Add(CreateMouseLeftPressThread(tt.KeyL));
            alth.Add(CreateMouseRightPressThread(tt.KeyR));


            var th = new Thread(new ThreadStart(delegate()
            {
                while (true)
                {
                    if (!isD3)
                    {
                        System.Threading.Thread.Sleep(100);
                        continue;
                    }

                    if (isspack)
                    {
                        objdm.KeyUp(keymax);

                        System.Threading.Thread.Sleep(50);
                        if(ttmax%2==0)
                        {
                            objdm.LeftClick();
                        }

                        continue;
                    }
                    else
                    {
                        this.Sleep(5);
                        if ((keymax == D3Config.KEYS.Key1 && !iskey1) ||
                            (keymax == D3Config.KEYS.Key2 && !iskey2) ||
                            (keymax == D3Config.KEYS.Key3 && !iskey3) ||
                            (keymax == D3Config.KEYS.Key4 && !iskey4) )
                            objdm.KeyDown(keymax);
                    }
                }
            }));
            th.IsBackground = true;
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            alth.Add(th);
            althstop.Clear();
            var tmpth = new Thread(new ThreadStart(delegate()
            {
                objdm.LeftUp();
                objdm.RightUp();
                objdm.KeyUp(keymax);



            }));
            tmpth.IsBackground = true;
            tmpth.SetApartmentState(ApartmentState.STA);
            althstop.Add(tmpth);

        }
        public void 先停_按住最大数量原地_定时按1_2_3_4_左键_右键(List<Thread> alth, T_Time tt, List<Thread> althstop)
        {
            var keymax = D3Config.KEYS.Key1;
            decimal ttmax = tt.Key1;

            if (tt.Key2 > ttmax)
            {
                keymax = D3Config.KEYS.Key2;
                ttmax = tt.Key2;
            }
            if (tt.Key3 > ttmax)
            {
                keymax = D3Config.KEYS.Key3;
                ttmax = tt.Key3;
            }
            if (tt.Key4 > ttmax)
            {
                keymax = D3Config.KEYS.Key4;
                ttmax = tt.Key4;
            }
            if (keymax != D3Config.KEYS.Key1)
                alth.Add(CreateKeyPressThread(D3Config.KEYS.Key1, tt.Key1));
            if (keymax != D3Config.KEYS.Key2)
                alth.Add(CreateKeyPressThread(D3Config.KEYS.Key2, tt.Key2));
            if (keymax != D3Config.KEYS.Key3)
                alth.Add(CreateKeyPressThread(D3Config.KEYS.Key3, tt.Key3));
            if (keymax != D3Config.KEYS.Key4)
                alth.Add(CreateKeyPressThread(D3Config.KEYS.Key4, tt.Key4));

            alth.Add(CreateMouseLeftPressThread(tt.KeyL));
            alth.Add(CreateMouseRightPressThread(tt.KeyR));
            alth.Add(KeyDownStand());

            var th = new Thread(new ThreadStart(delegate()
            {
                while (true)
                {
                    if (!isD3)
                    {
                        System.Threading.Thread.Sleep(100);
                        continue;
                    }

                    if (isspack)
                    {
                        objdm.KeyUp(keymax);
                        objdm.LeftClick();
                        System.Threading.Thread.Sleep(50);
                        continue;
                    }
                    else
                    {
                        this.Sleep(5);
                        if ((keymax == D3Config.KEYS.Key1 && !iskey1) ||
                            (keymax == D3Config.KEYS.Key2 && !iskey2) ||
                            (keymax == D3Config.KEYS.Key3 && !iskey3) ||
                            (keymax == D3Config.KEYS.Key4 && !iskey4))
                            objdm.KeyDown(keymax);
                    }
                }
            }));
            th.IsBackground = true;
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            alth.Add(th);
            althstop.Clear();
            var tmpth = new Thread(new ThreadStart(delegate()
            {
                objdm.LeftUp();
                objdm.RightUp();
                objdm.KeyUp(keymax);
                objdm.KeyUp(D3Config.KEYS.Key_Stand);


            }));
            tmpth.IsBackground = true;
            tmpth.SetApartmentState(ApartmentState.STA);
            althstop.Add(tmpth);

        }


        /// <summary>
        ///  1噬魂,2爆狗,3亡者,4收割,左键虫群,右键魂弹
        /// </summary>
        /// <param name="alth"></param>
        /// <param name="tt"></param>
        /// <param name="althstop"></param>
        public void 魂弹(List<Thread> alth, T_Time tt, List<Thread> althstop)
        {
            alth.Add(KeyDownStand());
            var th = new Thread(new ThreadStart(delegate()
            {


                //objdm.KeyPress(D3Config.KEYS.Key3);
                //this.Sleep(50);
                var tmpi = 0;
                var t2= Convert.ToInt32(tt.Key2 * 1000);
                var t1 = Convert.ToInt32(tt.Key1 * 1000);
                if (t2 > 0)
                {
                    objdm.KeyPress(D3Config.KEYS.Key2);
                }
                if (t1 > 0)
                {
                    
                    objdm.KeyDown(D3Config.KEYS.Key1);
                    System.Threading.Thread.Sleep(t1);
                    objdm.KeyUp(D3Config.KEYS.Key1);
                }
                while (true)
                {
                    if (!isD3)
                    {
                        System.Threading.Thread.Sleep(100);
                        continue;
                    }
                    if (this.iskey1 || this.iskey2)
                    {
                        // objdm.KeyUp(D3Config.KEYS.Key1);
                        System.Threading.Thread.Sleep(50);
                        continue;
                    }
                    if (isspack)
                    {
                        System.Threading.Thread.Sleep(50);
                        continue;
                    }
                    objdm.LeftDown();
                    objdm.KeyPress(D3Config.KEYS.Key3);
                    objdm.RightClick();
                    Sleep(100);
                    objdm.RightClick();
                    Sleep(100);
                    objdm.RightClick();
                    Sleep(100);
                    tmpi = 0;
                    for (tmpi = 0; tmpi < 7; tmpi++)
                    {
                        objdm.KeyPress(D3Config.KEYS.Key4);
                        Sleep(100);
                    }
                    objdm.LeftUp();

                }
            }));
            th.IsBackground = true;
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            alth.Add(th);
            althstop.Clear();
            var tmpth = new Thread(new ThreadStart(delegate()
            {
                objdm.LeftUp();
                objdm.RightUp();
                objdm.KeyUp(D3Config.KEYS.Key_Stand);
                objdm.KeyUp(D3Config.KEYS.Key1);

            }));
            tmpth.IsBackground = true;
            tmpth.SetApartmentState(ApartmentState.STA);
            althstop.Add(tmpth);
        }


        public void 魂弹备份(List<Thread> alth, T_Time tt, List<Thread> althstop)
        {
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key4, tt.Key4));
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key3, tt.Key3));
            alth.Add(CreateMouseRightPressThread(tt.KeyR));
            alth.Add(KeyDownStand());
            var th = new Thread(new ThreadStart(delegate()
            {
                this.Sleep(200);
                objdm.KeyDown(D3Config.KEYS.Key2);
                this.Sleep(500);
                objdm.KeyUp(D3Config.KEYS.Key2);
             
                //objdm.KeyPress(D3Config.KEYS.Key3);
                //this.Sleep(50);
       
                while (true)
                {
                    if (!isD3)
                    {
                        System.Threading.Thread.Sleep(100);
                        continue;
                    }
                    if (this.iskey1 || this.iskey2)
                    {
                        // objdm.KeyUp(D3Config.KEYS.Key1);

                        System.Threading.Thread.Sleep(50);
                        continue;
                    }
                    if (isspack)
                    {
       
                        // objdm.KeyUp(D3Config.KEYS.Key1);
                        objdm.LeftUp();
                        System.Threading.Thread.Sleep(50);
                        continue;
                    }
                    if (!this.isleft)
                    {
                        objdm.LeftDown();
                    }
                    System.Threading.Thread.Sleep(100);

                }
            }));
            th.IsBackground = true;
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            alth.Add(th);
            althstop.Clear();
            var tmpth = new Thread(new ThreadStart(delegate()
            {
                objdm.LeftUp();
                objdm.RightUp();
                objdm.KeyUp(D3Config.KEYS.Key_Stand);


            }));
            tmpth.IsBackground = true;
            tmpth.SetApartmentState(ApartmentState.STA);
            althstop.Add(tmpth);

        }

        public void 魂弹自动(List<Thread> alth, T_Time tt, List<Thread> althstop)
        {
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key4, tt.Key4));
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key3, tt.Key3));
            alth.Add(CreateMouseRightPressThread(tt.KeyR));
            alth.Add(KeyDownStand());
            var th = new Thread(new ThreadStart(delegate()
            {
    
                bool isfirst = true;
                var t1 = Convert.ToInt32(tt.Key1 * 1000);
                var t2 = Convert.ToInt32(tt.Key2 * 1000);
                var tL = Convert.ToInt32(tt.KeyL * 1000);

                objdm.KeyDown(D3Config.KEYS.Key2);
                this.Sleep(t2);
                objdm.KeyUp(D3Config.KEYS.Key2);
                //objdm.KeyPress(D3Config.KEYS.Key3);
                //this.Sleep(50);

                while (true)
                {
                    if (!isD3)
                    {
                        System.Threading.Thread.Sleep(100);
                        continue;
                    }
                    if (isspack)
                    {

                        // objdm.KeyUp(D3Config.KEYS.Key1);
                        objdm.LeftUp();
                        System.Threading.Thread.Sleep(50);
                        continue;
                    }
                    if (isfirst)
                    {
                        isfirst = false;
                        objdm.LeftDown();
                        System.Threading.Thread.Sleep(tL);
                        objdm.KeyDown(D3Config.KEYS.Key1);
                        objdm.LeftUp();
                        System.Threading.Thread.Sleep(t1);
                        objdm.KeyUp(D3Config.KEYS.Key1);
                    }
                    if (this.iskey1)
                    {
                        // objdm.KeyUp(D3Config.KEYS.Key1);
                        objdm.LeftUp();
                        System.Threading.Thread.Sleep(50);
                        continue;
                    }



                    if (!this.isleft)
                    {
                        objdm.LeftDown();
                    }
                    System.Threading.Thread.Sleep(100);

                }
            }));
            th.IsBackground = true;
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            alth.Add(th);
            althstop.Clear();
            var tmpth = new Thread(new ThreadStart(delegate()
            {
                objdm.KeyUp(D3Config.KEYS.Key1);
                objdm.LeftUp();
                objdm.RightUp();
                objdm.KeyUp(D3Config.KEYS.Key2);
                objdm.KeyUp(D3Config.KEYS.Key_Stand);


            }));
            tmpth.IsBackground = true;
            tmpth.SetApartmentState(ApartmentState.STA);
            althstop.Add(tmpth);

        }


        public void 火蝠转转转(List<Thread> alth, T_Time tt, List<Thread> althstop)
        {
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key4, tt.Key4));
            alth.Add(KeyDownStand());
            var th = new Thread(new ThreadStart(delegate()
            {

                    objdm.KeyPress(D3Config.KEYS.Key2);
                    this.Sleep(50);
                    objdm.KeyPress(D3Config.KEYS.Key3);
                    this.Sleep(50);
                    var KEY3 = false;
                    while (true)
                    {
                        if (!isD3)
                        {
                            System.Threading.Thread.Sleep(100);
                            continue;
                        }


                        if (isspack || this.iskey1)
                        {
                            KEY3 = true;
                            // objdm.KeyUp(D3Config.KEYS.Key1);
                            objdm.LeftUp();
                            System.Threading.Thread.Sleep(50);
                            continue;
                        }
 

                        if (KEY3)
                        {
                            objdm.KeyPress(D3Config.KEYS.Key3);
                            System.Threading.Thread.Sleep(150);
                            KEY3 = false;
                        }

                        if (!this.isleft)
                        {
                            objdm.LeftDown();
                        }
                        System.Threading.Thread.Sleep(100);

                    }
            }));
            th.IsBackground = true;
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            alth.Add(th);
            althstop.Clear();
            var tmpth = new Thread(new ThreadStart(delegate()
            {
                objdm.LeftUp();
                objdm.RightUp();
                objdm.KeyUp(D3Config.KEYS.Key_Stand);


            }));
            tmpth.IsBackground = true;
            tmpth.SetApartmentState(ApartmentState.STA);
            althstop.Add(tmpth);

        }

        public void 先停_原地按右键_然后按住左键定时按1_2_3_4(List<Thread> alth, T_Time tt, List<Thread> althstop)
        {

            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key1, tt.Key1));
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key2, tt.Key2));
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key3, tt.Key3));
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key4, tt.Key4));
            alth.Add(KeyDownStand());

            var th = new Thread(new ThreadStart(delegate()
            {

                 var tR = Convert.ToInt32(tt.KeyR * 1000);
                 var tL = Convert.ToInt32(tt.KeyL * 1000);
                while (true)
                {
                    if (!isD3)
                    {
                        System.Threading.Thread.Sleep(100);
                        continue;
                    }
                    if (isspack)
                    {
                        objdm.LeftUp();
                        objdm.RightUp();
                        System.Threading.Thread.Sleep(50);
                        continue;
                    }
                    else
                    {
                        this.Sleep(5);
                        objdm.LeftUp();
                        //objdm.KeyDown(D3Config.KEYS.Key_Stand);
                        objdm.RightDown();
                        this.Sleep(tR); 
                       objdm.RightUp();
                       objdm.LeftDown();
                        this.Sleep(tL);

                        System.Threading.Thread.Sleep(5);
                    }
                }
            }));
            th.IsBackground = true;
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            alth.Add(th);
            althstop.Clear();
            var tmpth = new Thread(new ThreadStart(delegate()
            {
                objdm.LeftUp();
                objdm.RightUp();
                objdm.KeyUp(D3Config.KEYS.Key_Stand);
       

            }));
            tmpth.IsBackground = true;
            tmpth.SetApartmentState(ApartmentState.STA);
            althstop.Add(tmpth);

        }
        public void 先停_原地按住左键_然后按住右键定时按1_2_3_4(List<Thread> alth, T_Time tt, List<Thread> althstop)
        {

            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key1, tt.Key1));
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key2, tt.Key2));
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key3, tt.Key3));
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key4, tt.Key4));
            alth.Add(KeyDownStand());

            var th = new Thread(new ThreadStart(delegate()
            {

                 var tR = Convert.ToInt32(tt.KeyR * 1000);
                 var tL = Convert.ToInt32(tt.KeyL * 1000);
                while (true)
                {
                    if (!isD3)
                    {
                        System.Threading.Thread.Sleep(100);
                        continue;
                    }
                    if (isspack)
                    {
                        //objdm.KeyUp(D3Config.KEYS.Key_Stand);
                        objdm.KeyUp(D3Config.KEYS.Key1);
                        objdm.LeftUp();
                        System.Threading.Thread.Sleep(50);
                        continue;
                    }
                    else
                    {
                        this.Sleep(5);
                        objdm.RightUp();
                        //objdm.KeyDown(D3Config.KEYS.Key_Stand);
                        objdm.LeftDown();
                       this.Sleep(tL);
                       objdm.LeftUp();
                        objdm.RightDown();
                        this.Sleep(tR);

                        System.Threading.Thread.Sleep(5);
                    }
                }
            }));
            th.IsBackground = true;
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            alth.Add(th);
            althstop.Clear();
            var tmpth = new Thread(new ThreadStart(delegate()
            {
                objdm.LeftUp();
                objdm.RightUp();
                objdm.KeyUp(D3Config.KEYS.Key_Stand);
       

            }));
            tmpth.IsBackground = true;
            tmpth.SetApartmentState(ApartmentState.STA);
            althstop.Add(tmpth);

        }

        public void 分解传奇(List<Thread> alth, T_Time tt, List<Thread> althstop)
        {

            alth.Add(CreateKeyPressThread(13, 0.3m));


            var th = new Thread(new ThreadStart(delegate()
            {
                while (true)
                {
                    if (!isD3)
                    {
                        System.Threading.Thread.Sleep(100);
                        continue;
                    }
                    objdm.LeftClick();
                    this.Sleep(400);
                }
            }));
            th.IsBackground = true;
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            alth.Add(th);
            althstop.Clear();
          

        }

        public void 原地左键定时按_1_2_3_4_右键_空格暂停(List<Thread> alth, T_Time tt, List<Thread> althstop)
        {

            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key1, tt.Key1));
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key2, tt.Key2));
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key3, tt.Key3));
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key4, tt.Key4));
            alth.Add(CreateMouseRightPressThread(tt.KeyR));


            var th = new Thread(new ThreadStart(delegate()
            {
                objdm.LeftDown();
                objdm.KeyDown(D3Config.KEYS.Key_Stand);
                while (true)
                {
                    if (!isD3)
                    {
                        System.Threading.Thread.Sleep(100);
                        continue;
                    }
                    if (isspack)
                    {
                        objdm.KeyUp(D3Config.KEYS.Key_Stand);
                        objdm.LeftUp();

                        System.Threading.Thread.Sleep(200);
                        continue;
                    }
                    else
                    {
           
                            objdm.LeftDown();
                            objdm.KeyDown(D3Config.KEYS.Key_Stand);
                        

                        // objdm.LeftDown();
              
                        System.Threading.Thread.Sleep(200);
                    }
                }
            }));
            th.IsBackground = true;
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            alth.Add(th);
            althstop.Clear();
            var tmpth = new Thread(new ThreadStart(delegate()
            {
                objdm.LeftUp();
                objdm.RightUp();
                objdm.KeyUp(D3Config.KEYS.Key_Stand);

            }));
            tmpth.IsBackground = true;
            tmpth.SetApartmentState(ApartmentState.STA);
            althstop.Add(tmpth);

        }
        public void 三秒火_按住2键_奥术洪流_和强制移动(List<Thread> alth, T_Time tt, List<Thread> althstop)
        {

            var th = new Thread(new ThreadStart(delegate()
            {
                objdm.KeyDown(D3Config.KEYS.Key_Move);
                //objdm.KeyDown(D3Config.KEYS.Key_Stand);
                objdm.KeyDown(D3Config.KEYS.Key2);
                while (true)
                {
                    if (!isD3)
                    {
                        System.Threading.Thread.Sleep(100);
                        continue;
                    }
                    if (isspack)
                    {
                        objdm.KeyUp(D3Config.KEYS.Key2);
                        //objdm.KeyUp(D3Config.KEYS.Key_Stand);
                        objdm.KeyUp(D3Config.KEYS.Key_Move);
            
                        System.Threading.Thread.Sleep(200);
                        continue;
                    }
                    else
                    {

                            objdm.KeyDown(D3Config.KEYS.Key_Move);
                            //objdm.KeyDown(D3Config.KEYS.Key_Stand);
                            objdm.KeyDown(D3Config.KEYS.Key2);
                        
                    
                        System.Threading.Thread.Sleep(100);
                    }
                }
            }));
            th.IsBackground = true;
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            alth.Add(th);
            althstop.Clear();
            var tmpth = new Thread(new ThreadStart(delegate()
            {
                objdm.KeyUp(D3Config.KEYS.Key_Move);
                objdm.KeyUp(D3Config.KEYS.Key2);
                objdm.KeyUp(D3Config.KEYS.Key_Stand);

            }));
            tmpth.IsBackground = true;
            tmpth.SetApartmentState(ApartmentState.STA);
            althstop.Add(tmpth);

        }
        public void 原地右键定时按_1_2_3_4_左键_空格暂停(List<Thread> alth, T_Time tt, List<Thread> althstop)
        {

            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key1, tt.Key1));
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key2, tt.Key2));
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key3, tt.Key3));
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key4, tt.Key4));
            alth.Add(CreateMouseLeftPressThread(tt.KeyL));
            alth.Add(KeyDownStand());

            var th = new Thread(new ThreadStart(delegate()
            {

                objdm.RightDown();
                while (true)
                {
                    if (!isD3)
                    {
                        System.Threading.Thread.Sleep(100);
                        continue;
                    }
                   
                    if (isspack)
                    {
                        if (isRight)
                        {
                            objdm.RightUp();
                        }
                        objdm.LeftClick();
                        System.Threading.Thread.Sleep(50);
                        continue;
                    }
                    else
                    {
                        if(!isRight)
                        {
                            objdm.RightDown();
                        }
               
                        System.Threading.Thread.Sleep(200);
                    }
                }
            }));
            th.IsBackground = true;
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            alth.Add(th);
            althstop.Clear();
            var tmpth = new Thread(new ThreadStart(delegate()
            {
                objdm.LeftUp();
                objdm.RightUp();
                objdm.KeyUp(D3Config.KEYS.Key_Stand);

            }));
            tmpth.IsBackground = true;
            tmpth.SetApartmentState(ApartmentState.STA);
            althstop.Add(tmpth);

        }
        public void 原地和强制移动_定时按_1_2_3_4_检测左键_定时右键(List<Thread> alth, T_Time tt, List<Thread> althstop)
        {

            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key1, tt.Key1));
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key2, tt.Key2));
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key3, tt.Key3));
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key4, tt.Key4));
            alth.Add(KeyDownStand());
            alth.Add(CreateMouseRightPressThread(tt.KeyR));
            alth.Add(CreateMouseLeftPressThread(tt.KeyL));


            var th = new Thread(new ThreadStart(delegate()
            {
                objdm.KeyDown(D3Config.KEYS.Key_Move);
                objdm.KeyDown(D3Config.KEYS.Key_Stand);

                while (true)
                {
                    if (!isD3)
                    {
                        System.Threading.Thread.Sleep(100);
                        continue;
                    }
                    if (isleft)
                    {
                        objdm.KeyUp(D3Config.KEYS.Key_Move);

                    }
                    else
                    {
                        objdm.KeyDown(D3Config.KEYS.Key_Move);
                    }

                    System.Threading.Thread.Sleep(100);
                }
            }));
            th.IsBackground = true;
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            alth.Add(th);
            althstop.Clear();
            var tmpth = new Thread(new ThreadStart(delegate()
            {
                objdm.KeyUp(D3Config.KEYS.Key_Move);

                objdm.KeyUp(D3Config.KEYS.Key_Stand);
                objdm.RightUp();
                objdm.LeftUp();
                isstand = false;

            }));
            tmpth.IsBackground = true;
            tmpth.SetApartmentState(ApartmentState.STA);
            althstop.Add(tmpth);






        }
                    public void 原地和强制移动_定时按_1_2_3_4_左键_右键(List<Thread> alth, T_Time tt, List<Thread> althstop)
        {

            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key1, tt.Key1));
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key2, tt.Key2));
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key3, tt.Key3));
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key4, tt.Key4));
            alth.Add(CreateMouseRightPressThread(tt.KeyR));
            alth.Add(CreateMouseLeftPressThread(tt.KeyL));


            var th = new Thread(new ThreadStart(delegate()
            {
                objdm.KeyDown(D3Config.KEYS.Key_Move);
                objdm.KeyDown(D3Config.KEYS.Key_Stand);

                while (true)
                {
                    if (!isD3)
                    {
                        System.Threading.Thread.Sleep(100);
                        continue;
                    }
                    if (isspack)
                    {
        
                        objdm.KeyUp(D3Config.KEYS.Key_Stand);
                        objdm.KeyUp(D3Config.KEYS.Key_Move);
 
                        System.Threading.Thread.Sleep(200);
                        continue;
                    }
                    else
                    {
                      
                            objdm.KeyDown(D3Config.KEYS.Key_Move);
                           objdm.KeyDown(D3Config.KEYS.Key_Stand);
                    
          
                        System.Threading.Thread.Sleep(100);
                    }
                }
            }));
            th.IsBackground = true;
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            alth.Add(th);
            althstop.Clear();
            var tmpth = new Thread(new ThreadStart(delegate()
            {
                objdm.KeyUp(D3Config.KEYS.Key_Move);
          
                objdm.KeyUp(D3Config.KEYS.Key_Stand);
                objdm.RightUp();
                objdm.LeftUp();

            }));
            tmpth.IsBackground = true;
            tmpth.SetApartmentState(ApartmentState.STA);
            althstop.Add(tmpth);






        }
                    public void 地震(List<Thread> alth, T_Time tt, List<Thread> althstop)
        {
            alth.Clear();

            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key2, tt.Key2));
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key3, tt.Key3));
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key4, tt.Key4));
            alth.Add(CreateMouseLeftPressThread(tt.KeyL));
    
            var th = new Thread(new ThreadStart(delegate()
            {
                objdm.KeyDown(D3Config.KEYS.Key_Move);
                while (true)
                {
                    if (!isD3)
                    {
                        System.Threading.Thread.Sleep(100);
                        continue;
                    }
                    
                    if (isspack)
                    {
                        objdm.RightClick();
                        objdm.KeyDown(D3Config.KEYS.Key_Move);
                        objdm.KeyUp(D3Config.KEYS.Key1);
                        objdm.LeftClick();
                        Sleep(100);
                            continue;
                    }
                    if(!iskey1)
                        objdm.KeyDown(D3Config.KEYS.Key1);

                    
        
                  
                }
            }));
            th.IsBackground = true;
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            alth.Add(th);


           
            althstop.Clear();
            var tmpth = new Thread(new ThreadStart(delegate()
            {
                objdm.KeyUp(D3Config.KEYS.Key_Move);
                objdm.KeyUp(D3Config.KEYS.Key1);
                objdm.RightUp();
                objdm.LeftUp();

            }));
            tmpth.IsBackground = true;
            tmpth.SetApartmentState(ApartmentState.STA);
            althstop.Add(tmpth);


        }
        public void 奥陨宏(List<Thread> alth, T_Time tt, List<Thread> althstop)
        {
            //1奥术洪流 2 电刑 3冰霜新星（大于0才放） 4原力之波 左键风暴护甲  右键 陨石术
            var th = new Thread(new ThreadStart(delegate()
            {
                while (true)
                {
                    if (!isD3)
                    {
                        continue;
                    }
                    if (isspack)
                    {
                        continue;
                    }
                    if (tt.Key3 > 0)
                    {
                        objdm.KeyPress(D3Config.KEYS.Key3);
                    }
                        objdm.KeyDown(D3Config.KEYS.Key4);
                    Sleep(50);
                       objdm.KeyUp(D3Config.KEYS.Key4);
                      Sleep(50);
                       objdm.KeyDown(D3Config.KEYS.Key2);
                        Sleep(1580);
                      objdm.KeyUp(D3Config.KEYS.Key2);

                    objdm.RightDown();
                     Sleep(50);
                    objdm.RightUp();
                       Sleep(50);
                       objdm.KeyDown(D3Config.KEYS.Key2);
                         Sleep(990);
                       objdm.KeyUp(D3Config.KEYS.Key2);
                        objdm.KeyDown(D3Config.KEYS.Key1);
                       Sleep(370);
                      objdm.KeyUp(D3Config.KEYS.Key1);
                }
            }));
            th.IsBackground = true;
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            alth.Add(th);

                        althstop.Clear();
            var tmpth = new Thread(new ThreadStart(delegate()
            {
                objdm.RightUp();
                objdm.KeyUp(D3Config.KEYS.Key1);
                    objdm.KeyUp(D3Config.KEYS.Key2);
                    objdm.KeyUp(D3Config.KEYS.Key4);

            }));
            tmpth.IsBackground = true;
            tmpth.SetApartmentState(ApartmentState.STA);
            althstop.Add(tmpth);


        }

        public void 奥陨宏2(List<Thread> alth, T_Time tt, List<Thread> althstop)
        {
            D3Config.PLAN.CursorPosTime = DateTime.Now.AddDays(-1);
            //1奥术洪流 2 电刑 3冰霜新星（大于0才放） 4原力之波 左键风暴护甲  右键 陨石术
            var th = new Thread(new ThreadStart(delegate ()
            {
                while (true)
                {
                    if (!isD3)
                    {
                         continue;
                    }
                    if (isspack)
                    {
                        continue;
                    }
               
                    objdm.KeyDown(D3Config.KEYS.Key4);
                    Sleep(tt.Key4 * 1000);
                    objdm.KeyUp(D3Config.KEYS.Key4);
                    if (tt.Key3 > 0 && (DateTime.Now - D3Config.PLAN.CursorPosTime).TotalMilliseconds < 4000)
                    {
                        D3Config.PLAN.CursorPosTime = DateTime.Now.AddDays(-1);
                        objdm.MoveTo((int)D3Config.PLAN.CursorPosX, (int)D3Config.PLAN.CursorPosY);
                        objdm.KeyPress(D3Config.KEYS.Key2);
                    }
                    objdm.KeyDown(D3Config.KEYS.Key2);
                    Sleep(tt.Key2 * 1000);
                    objdm.KeyUp(D3Config.KEYS.Key2);
            


                    objdm.RightDown();
                    Sleep(tt.KeyR*1000);
                    objdm.RightUp();

                    objdm.KeyDown(D3Config.KEYS.Key2);
                    Sleep(tt.KeyL * 1000);
                    objdm.KeyUp(D3Config.KEYS.Key2);

                    objdm.KeyDown(D3Config.KEYS.Key1);
                    Sleep(tt.Key1 * 1000);
                    objdm.KeyUp(D3Config.KEYS.Key1);
                  
                }
            }));
            th.IsBackground = true;
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            alth.Add(th);

            althstop.Clear();
            var tmpth = new Thread(new ThreadStart(delegate ()
            {
                objdm.RightUp();
                objdm.KeyUp(D3Config.KEYS.Key1);
                objdm.KeyUp(D3Config.KEYS.Key2);
                objdm.KeyUp(D3Config.KEYS.Key4);

            }));
            tmpth.IsBackground = true;
            tmpth.SetApartmentState(ApartmentState.STA);
            althstop.Add(tmpth);


        }

        public void 流放原地第一技能(List<Thread> alth, T_Time tt, List<Thread> althstop)
        {
            var th = new Thread(new ThreadStart(delegate()
            {
                objdm.KeyDown(D3Config.KEYS.Key1);
                objdm.KeyDown(D3Config.KEYS.Key_Stand);
                while (true)
                {
                    if (!isD3)
                    {
                        Sleep(200);
                        continue;
                    }
                    if (isspack)
                    {
                        Sleep(200);
                        continue;
                    }


                    if (!isstand)
                    {
                        objdm.KeyDown(D3Config.KEYS.Key_Stand);
                    }
                    if (!iskey1)
                    {
                        objdm.KeyDown(D3Config.KEYS.Key1);
                    }
                    Sleep(200);
                }
            }));
            th.IsBackground = true;
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            alth.Add(th);

            althstop.Clear();
            var tmpth = new Thread(new ThreadStart(delegate()
            {
                objdm.KeyUp(D3Config.KEYS.Key_Stand);
                objdm.KeyUp(D3Config.KEYS.Key1);


            }));
            tmpth.IsBackground = true;
            tmpth.SetApartmentState(ApartmentState.STA);
            althstop.Add(tmpth);
  
        }
        public void 流放一键吃药(List<Thread> alth, T_Time tt, List<Thread> althstop)
        {
            if (tt.Key1 > 0)
            {
                objdm.KeyPressChar("1");
                Sleep(tt.Key1);
            }
            if (tt.Key2 > 0)
            {
                objdm.KeyPressChar("2");
                Sleep(tt.Key2);
            }
            if (tt.Key3 > 0)
            {
                objdm.KeyPressChar("3");
                Sleep(tt.Key3);
            }
            if (tt.Key4 > 0)
            {
                objdm.KeyPressChar("4");
                Sleep(tt.Key4);
            }
            if (tt.KeyL > 0)
            {
                objdm.KeyPressChar("5");
                Sleep(tt.KeyL);
            }
  
        }

        public void 先停再_原地依次按_1_2_3_4(List<Thread> alth, T_Time tt, List<Thread> althstop)
        {
            objdm.KeyDown(D3Config.KEYS.Key_Stand);
            if (tt.Key1 > 0)
            {
                objdm.KeyPress(D3Config.KEYS.Key1);
                Sleep(tt.Key1);
            }
            if (tt.Key2 > 0)
            {
                objdm.KeyPress(D3Config.KEYS.Key2);
                Sleep(tt.Key2);
            }
            if (tt.Key3 > 0)
            {
                objdm.KeyPress(D3Config.KEYS.Key3);
                Sleep(tt.Key3);
            }
            if (tt.Key4 > 0)
            {
                objdm.KeyPress(D3Config.KEYS.Key4);
                Sleep(tt.Key4);
            }
            objdm.KeyUp(D3Config.KEYS.Key_Stand);
        }

        


        public void 一直按Q定时按1234(List<Thread> alth, T_Time tt, List<Thread> althstop)
        {
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key1, tt.Key1));
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key2, tt.Key2));
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key3, tt.Key3));
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key4, tt.Key4));

            var th = new Thread(new ThreadStart(delegate()
            {
                while (true)
                {
                    if (!isD3)
                    {
                        objdm.KeyUpChar("q");
                        continue;
                    }
                    if (isspack)
                    {
                        objdm.KeyUpChar("q");
                        System.Threading.Thread.Sleep(100);
                        continue;
                    }
                    else
                    {
                        objdm.KeyDownChar("q");
                        System.Threading.Thread.Sleep(100);
                    }
                }
            }));
            th.IsBackground = true;
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            alth.Add(th);



            objdm.KeyDownChar("q");
            althstop.Clear();
            var tmpth = new Thread(new ThreadStart(delegate()
            {
                iskey1 = false;
                iskey2 = false;
                iskey3 = false;
                iskey4 = false;

                objdm.KeyUp(D3Config.KEYS.Key1);
                objdm.KeyUp(D3Config.KEYS.Key2);
                objdm.KeyUp(D3Config.KEYS.Key3);
                objdm.KeyUp(D3Config.KEYS.Key4);
                objdm.KeyUpChar("q");
            }));
            tmpth.IsBackground = true;
            tmpth.SetApartmentState(ApartmentState.STA);
            althstop.Add(tmpth);

        }
        
                    int ssss3333 = 1;
        public void 蛮子先停再3(List<Thread> alth, T_Time tt, List<Thread> althstop)
        {

            objdm.KeyPress(13);
     

         objdm.SendString2(this.Handle, "/p Zou Le 333---" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            objdm.KeyPress(13);

            althstop.Clear();
            var tmpth = new Thread(new ThreadStart(delegate()
            {
           
                objdm.KeyUp(13);
            }));
            tmpth.IsBackground = true;
            tmpth.SetApartmentState(ApartmentState.STA);
            althstop.Add(tmpth);
        }
         public void 定时按_1_2_3_4_左键_右键(List<Thread> alth, T_Time tt, List<Thread> althstop)
        {

            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key1, tt.Key1));
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key2, tt.Key2));
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key3, tt.Key3));
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key4, tt.Key4));
            alth.Add(CreateMouseLeftPressThread(tt.KeyL));
            alth.Add(CreateMouseRightPressThread(tt.KeyR));


           
            althstop.Clear();
            var tmpth = new Thread(new ThreadStart(delegate()
            {
                objdm.RightUp();
                objdm.LeftUp();

            }));
            tmpth.IsBackground = true;
            tmpth.SetApartmentState(ApartmentState.STA);
            althstop.Add(tmpth);


        }
        public void 定时按_1_2_3_4_右键_空格不暂停(List<Thread> alth, T_Time tt, List<Thread> althstop)
        {

            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key1, tt.Key1,false));
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key2, tt.Key2, false));
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key3, tt.Key3, false));
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key4, tt.Key4, false));
            alth.Add(CreateMouseRightPressThread(tt.KeyR));



            althstop.Clear();
            var tmpth = new Thread(new ThreadStart(delegate()
            {
                objdm.RightUp();


            }));
            tmpth.IsBackground = true;
            tmpth.SetApartmentState(ApartmentState.STA);
            althstop.Add(tmpth);


        }
        public void 原地2次_依次按_1_2_3_4_左键_右键(List<Thread> alth, T_Time tt, List<Thread> althstop)
        {
            alth.Clear();
            Thread th = new Thread(new ThreadStart(()=>{

                for (int i = 0; i < 2; i++)
                {


                    objdm.KeyDown(D3Config.KEYS.Key_Stand);
                    if (tt.Key1 > 0)
                    {
                        this.Sleep(Convert.ToInt32(tt.Key1 * 1000));
                        objdm.KeyPress(D3Config.KEYS.Key1);
                    }
                    if (tt.Key2 > 0)
                    {
                        this.Sleep(Convert.ToInt32(tt.Key2 * 1000));
                        objdm.KeyPress(D3Config.KEYS.Key2);
                    }
                    if (tt.Key3 > 0)
                    {
                        this.Sleep(Convert.ToInt32(tt.Key3 * 1000));
                        objdm.KeyPress(D3Config.KEYS.Key3);
                    }
                    if (tt.Key4 > 0)
                    {
                        this.Sleep(Convert.ToInt32(tt.Key4 * 1000));
                        objdm.KeyPress(D3Config.KEYS.Key4);
                    }
                    if (tt.KeyL > 0)
                    {
                        this.Sleep(Convert.ToInt32(tt.KeyL * 1000));
                        objdm.LeftClick();
                    }
                    if (tt.KeyR > 0)
                    {
                        this.Sleep(Convert.ToInt32(tt.KeyR * 1000));
                        objdm.RightClick();
                    }
                    objdm.KeyUp(D3Config.KEYS.Key_Stand);
                    System.Threading.Thread.Sleep(300);

                }
             }));
            th.IsBackground = true;
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
           
        }

        public void 原地1次_依次按_1_2_3_4_左键_右键(List<Thread> alth, T_Time tt, List<Thread> althstop)
        {
            alth.Clear();
            Thread th = new Thread(new ThreadStart(() =>
            {

                for (int i = 0; i < 1; i++)
                {


                    objdm.KeyDown(D3Config.KEYS.Key_Stand);
                    if (tt.Key1 > 0)
                    {
                        this.Sleep(Convert.ToInt32(tt.Key1 * 1000));
                        objdm.KeyPress(D3Config.KEYS.Key1);
                    }
                    if (tt.Key2 > 0)
                    {
                        this.Sleep(Convert.ToInt32(tt.Key2 * 1000));
                        objdm.KeyPress(D3Config.KEYS.Key2);
                    }
                    if (tt.Key3 > 0)
                    {
                        this.Sleep(Convert.ToInt32(tt.Key3 * 1000));
                        objdm.KeyPress(D3Config.KEYS.Key3);
                    }
                    if (tt.Key4 > 0)
                    {
                        this.Sleep(Convert.ToInt32(tt.Key4 * 1000));
                        objdm.KeyPress(D3Config.KEYS.Key4);
                    }
                    if (tt.KeyL > 0)
                    {
                        this.Sleep(Convert.ToInt32(tt.KeyL * 1000));
                        objdm.LeftClick();
                    }
                    if (tt.KeyR > 0)
                    {
                        this.Sleep(Convert.ToInt32(tt.KeyR * 1000));
                        objdm.RightClick();
                    }
                    objdm.KeyUp(D3Config.KEYS.Key_Stand);
                }
            }));
            th.IsBackground = true;
            th.SetApartmentState(ApartmentState.STA);
            th.Start();

        }

        public void 原地1次_依次按_1_2_3_4_左键(List<Thread> alth, T_Time tt, List<Thread> althstop)
        {
            alth.Clear();
            Thread th = new Thread(new ThreadStart(() =>
            {

                for (int i = 0; i < 1; i++)
                {


                    objdm.KeyDown(D3Config.KEYS.Key_Stand);
                    if (tt.Key1 > 0)
                    {
                        this.Sleep(Convert.ToInt32(tt.Key1 * 1000));
                        objdm.KeyPress(D3Config.KEYS.Key1);
                    }
                    if (tt.Key2 > 0)
                    {
                        this.Sleep(Convert.ToInt32(tt.Key2 * 1000));
                        objdm.KeyPress(D3Config.KEYS.Key2);
                    }
                    if (tt.Key3 > 0)
                    {
                        this.Sleep(Convert.ToInt32(tt.Key3 * 1000));
                        objdm.KeyPress(D3Config.KEYS.Key3);
                    }
                    if (tt.Key4 > 0)
                    {
                        this.Sleep(Convert.ToInt32(tt.Key4 * 1000));
                        objdm.KeyPress(D3Config.KEYS.Key4);
                    }
                    if (tt.KeyL > 0)
                    {
                        this.Sleep(Convert.ToInt32(tt.KeyL * 1000));
                        objdm.LeftClick();
                    }
                    objdm.KeyUp(D3Config.KEYS.Key_Stand);
                }
            }));
            th.IsBackground = true;
            th.SetApartmentState(ApartmentState.STA);
            th.Start();

        }
        public void 原地1次_依次按_1_2_3_4_左键_4(List<Thread> alth, T_Time tt, List<Thread> althstop)
        {
            alth.Clear();
            Thread th = new Thread(new ThreadStart(() =>
            {

                for (int i = 0; i < 1; i++)
                {


                    objdm.KeyDown(D3Config.KEYS.Key_Stand);
                    if (tt.Key1 > 0)
                    {
                        this.Sleep(Convert.ToInt32(tt.Key1 * 1000));
                        objdm.KeyPress(D3Config.KEYS.Key1);
                    }
                    if (tt.Key2 > 0)
                    {
                        this.Sleep(Convert.ToInt32(tt.Key2 * 1000));
                        objdm.KeyPress(D3Config.KEYS.Key2);
                    }
                    if (tt.Key3 > 0)
                    {
                        this.Sleep(Convert.ToInt32(tt.Key3 * 1000));
                        objdm.KeyPress(D3Config.KEYS.Key3);
                    }
                    if (tt.Key4 > 0)
                    {
                        this.Sleep(Convert.ToInt32(tt.Key4 * 1000));
                        objdm.KeyPress(D3Config.KEYS.Key4);
                    }
                    this.Sleep(250);
                    objdm.LeftClick();
                    if (tt.KeyL > 0.2m)
                    {
                        objdm.KeyDown(D3Config.KEYS.Key_Move);
                        this.Sleep(200);
                        objdm.KeyUp(D3Config.KEYS.Key_Move);
                        this.Sleep(Convert.ToInt32(tt.KeyL * 1000)-200);
                        objdm.KeyPress(D3Config.KEYS.Key4);
                    }
                    objdm.KeyUp(D3Config.KEYS.Key_Stand);
                }
            }));
            th.IsBackground = true;
            th.SetApartmentState(ApartmentState.STA);
            th.Start();

        }
        /// <summary>
        /// 1飞刀（0.2）右键翻滚（0.35）
        /// </summary>
        /// <param name="alth"></param>
        /// <param name="tt"></param>
        /// <param name="althstop"></param>
        public void 三刀翻滚(List<Thread> alth, T_Time tt, List<Thread> althstop)
        {
            alth.Clear();

            var th = new Thread(new ThreadStart(delegate()
            {


                var rc = Convert.ToInt32(tt.KeyL);
                if(rc==0)
                {
                    rc = 4;
                }
                int rs = Convert.ToInt32(tt.KeyR*1000);
                if (rs == 0)
                {
                    rs = 350;
                }
                while (true)
                {
     
        
                 
                    if (!isD3)
                    {
                        System.Threading.Thread.Sleep(100);
                        continue;
                    }
                    if (isspack)
                    {
                 
                        System.Threading.Thread.Sleep(50);
                        continue;
                    }
                    objdm.KeyPress(D3Config.KEYS.Key1);
                    Sleep(200);
                    //if (!ismove)
                    //{
                    //    objdm.KeyDown(D3Config.KEYS.Key_Move);
                    //}
                    if (!iskey3 && tt.Key3>0)
                    {
                        objdm.KeyDown(D3Config.KEYS.Key3);
                    }
                    if (!iskey4 && tt.Key4 > 0)
                    {
                        objdm.KeyDown(D3Config.KEYS.Key4);
                    }

                    for (int i = 0; i < rc; i++)
                    {
                        objdm.RightClick();
                        Sleep(rs);
                    }
                      
                 ;
                }
            }));
            th.IsBackground = true;
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            alth.Add(th);
            althstop.Clear();
            var tmpth = new Thread(new ThreadStart(delegate()
            {
                iskey3 = false;
                iskey4 = false;
               ismove = false;
                objdm.KeyUp(D3Config.KEYS.Key3);
                objdm.KeyUp(D3Config.KEYS.Key4);
                objdm.KeyUp(D3Config.KEYS.Key_Move);

            }));
            tmpth.IsBackground = true;
            tmpth.SetApartmentState(ApartmentState.STA);
            althstop.Add(tmpth);

        }

        public void 三刀飞刀_左键(List<Thread> alth, T_Time tt, List<Thread> althstop)
        {

            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key1, tt.Key1));
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key2, tt.Key2));
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key3, tt.Key3));
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key4, tt.Key4));
            alth.Add(CreateMouseLeftPressThread(tt.KeyL));

            var th = new Thread(new ThreadStart(delegate()
            {



                while (true)
                {
                    if (!isD3)
                    {
                        System.Threading.Thread.Sleep(100);
                        continue;
                    }
                    objdm.KeyDown(D3Config.KEYS.Key_Stand);
                    System.Threading.Thread.Sleep(300);
                    objdm.KeyUp(D3Config.KEYS.Key_Stand);
                    System.Threading.Thread.Sleep(1650);
                }
            }));
            th.IsBackground = true;
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            alth.Add(th);


            althstop.Clear();
            var tmpth = new Thread(new ThreadStart(delegate()
            {
                objdm.RightUp();
                objdm.LeftUp();
                objdm.KeyUp(D3Config.KEYS.Key_Stand);
            }));
            tmpth.IsBackground = true;
            tmpth.SetApartmentState(ApartmentState.STA);
            althstop.Add(tmpth);

        }

        public void 先停_按住右键_定时按_1_2_3_4_按住空格连点(List<Thread> alth, T_Time tt, List<Thread> althstop)
        {

            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key1, tt.Key1));
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key2, tt.Key2));
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key3, tt.Key3));
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key4, tt.Key4));
   
      


            var th = new Thread(new ThreadStart(delegate()
            {
                while (true)
                {
                    if (!isD3)
                    {
                        System.Threading.Thread.Sleep(100);
                        continue;
                    }
                    if (isspack)
                    {
                        if (isRight)
                        {
                            objdm.RightUp();
                        }
                        objdm.LeftClick();
                        System.Threading.Thread.Sleep(50);
                        continue;
                    }
                   
                    if (!isRight)
                    {
                        objdm.RightDown();
                    }
                    System.Threading.Thread.Sleep(50);
                }
            }));
            th.IsBackground = true;
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            alth.Add(th);
            althstop.Clear();
            var tmpth = new Thread(new ThreadStart(delegate()
            {
                objdm.LeftUp();
                objdm.RightUp();

            }));
            tmpth.IsBackground = true;
            tmpth.SetApartmentState(ApartmentState.STA);
            althstop.Add(tmpth);

        }
        public void 三刀飞刀(List<Thread> alth, T_Time tt, List<Thread> althstop)
        {


            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key2, tt.Key2));
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key3, tt.Key3));
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key4, tt.Key4));
            alth.Add(CreateMouseLeftPressThread2(tt.KeyL));
      


            var th = new Thread(new ThreadStart(delegate()
            {


                //objdm.KeyDown(D3Config.KEYS.Key_Stand);
                var t1 = Convert.ToInt32(tt.Key1 * 1000);
                var tmp1 = DateTime.Now;

                while (true)
                {
                    if (!isD3)
                    {
                        System.Threading.Thread.Sleep(100);
                        continue;
                    }
                    if (isspack)
                    {
                        objdm.LeftClick();
                   
                            objdm.KeyUp(D3Config.KEYS.Key1);
                        
                        System.Threading.Thread.Sleep(50);
                        continue;
                    }
                    if (isRight)
                    {
                        if (DateTime.Now >tmp1.AddMilliseconds(1900))
                        {

                            objdm.KeyDown(D3Config.KEYS.Key1);
                            Sleep(350);
                            tmp1 = DateTime.Now;
                        }
                        objdm.KeyUp(D3Config.KEYS.Key1);
                        System.Threading.Thread.Sleep(50);
                        continue;
                    }
                    else
                    {
                        tmp1 = DateTime.Now;
                    }



                    this.Sleep(5);
                    if (!iskey1)
                    {
                        objdm.KeyDown(D3Config.KEYS.Key1);
    
                    }


                }
            }));
            th.IsBackground = true;
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            alth.Add(th);
            althstop.Clear();
            var tmpth = new Thread(new ThreadStart(delegate()
            {
                objdm.LeftUp();
                objdm.RightUp();
                objdm.KeyUp(D3Config.KEYS.Key1);
                objdm.KeyUp(D3Config.KEYS.Key_Stand);


            }));
            tmpth.IsBackground = true;
            tmpth.SetApartmentState(ApartmentState.STA);
            althstop.Add(tmpth);

        }
        /// <summary>
        /// 1飞刀（0.2）右键翻滚（0.35）
        /// </summary>
        /// <param name="alth"></param>
        /// <param name="tt"></param>
        /// <param name="althstop"></param>
        public void 先停_三刀翻滚_按住1_定时234(List<Thread> alth, T_Time tt, List<Thread> althstop)
        {
            alth.Clear();
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key2, tt.Key2));
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key3, tt.Key3));
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key4, tt.Key4));

            var th = new Thread(new ThreadStart(delegate()
            {



                while (true)
                {
                    if (!isD3)
                    {
                        System.Threading.Thread.Sleep(100);
                        continue;
                    }

                    //if (!ismove)
                    //{
                    //    objdm.KeyDown(D3Config.KEYS.Key_Move);
                    //}
                    if (!iskey1 )
                    {
                        objdm.KeyDown(D3Config.KEYS.Key1);
                    }



                    ;
                }
            }));
            th.IsBackground = true;
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            alth.Add(th);
            althstop.Clear();
            var tmpth = new Thread(new ThreadStart(delegate()
            {
                objdm.KeyUp(D3Config.KEYS.Key3);
                objdm.KeyUp(D3Config.KEYS.Key4);
                objdm.KeyUp(D3Config.KEYS.Key1);
                iskey3 = false;
                iskey4 = false;
                iskey1 = false;

            }));
            tmpth.IsBackground = true;
            tmpth.SetApartmentState(ApartmentState.STA);
            althstop.Add(tmpth);

        }
        public void 天谴加马(List<Thread> alth, T_Time tt, List<Thread> althstop)
        {
            alth.Clear();
            Thread th = new Thread(new ThreadStart(() =>
            {
                var t1 = Convert.ToInt32(tt.Key1 * 1000);
                var t2 = Convert.ToInt32(tt.Key2 * 1000);
                var t3 = Convert.ToInt32(tt.Key3 * 1000);
                var t4 = Convert.ToInt32(tt.Key4 * 1000);
                var tr = Convert.ToInt32(tt.KeyR * 1000);
                var tl = Convert.ToInt32(tt.KeyL * 1000);
                var time1 = DateTime.Now;
                var time2 = DateTime.Now;
                var time3 = DateTime.Now;
                var time4 = DateTime.Now;
                var timer = DateTime.Now;
                var timel = DateTime.Now;
      
                var times = DateTime.Now;

                int isfn1 = 1;
                while (true)
                {
                    if (!isD3)
                    {
                        Sleep(100);
                        continue;
                    }

              
                    if (tt.FNType == 1)
                    {
                  
                        isfn1 = 1;
                        if (DateTime.Now > time1.AddMilliseconds(t1) && t1 > 0)
                        {
                            time1 = DateTime.Now;
                            objdm.KeyPress(D3Config.KEYS.Key1);
                        }
                        else if (DateTime.Now > time2.AddMilliseconds(t2) && t2 > 0)
                        {
                            time2 = DateTime.Now;
                            objdm.KeyPress(D3Config.KEYS.Key2);
                        }
                        else if (DateTime.Now > time3.AddMilliseconds(t3) && t3 > 0)
                        {
                            time3 = DateTime.Now;
                            objdm.KeyPress(D3Config.KEYS.Key3);
                        }
                        else if (DateTime.Now > time4.AddMilliseconds(t4) && t4 > 0)
                        {
                            time4 = DateTime.Now;
                            objdm.KeyPress(D3Config.KEYS.Key4);
                        }
                        else if (DateTime.Now > timer.AddMilliseconds(tr) && tr > 0)
                        {
                            timer = DateTime.Now;
                            objdm.RightClick();
                        }
                        else if (DateTime.Now > timel.AddMilliseconds(tl) && tl > 0)
                        {
                            timel = DateTime.Now;
                            objdm.LeftClick();
                        }
                        Sleep(10);
                        
                    }
                    else
                    {
                        if (!ismove)
                        {
                            objdm.KeyDown(D3Config.KEYS.Key_Move);
                        }
                        objdm.RightClick();
                        Sleep(50);
                    }
                    

                }
            }));
            th.IsBackground = true;
            th.SetApartmentState(ApartmentState.STA);
            th.Start();

            alth.Add(th);
            althstop.Clear();
            var tmpth = new Thread(new ThreadStart(delegate()
            {
                objdm.KeyUp(D3Config.KEYS.Key1);
                objdm.KeyUp(D3Config.KEYS.Key2);
                objdm.KeyUp(D3Config.KEYS.Key3);
                objdm.KeyUp(D3Config.KEYS.Key4);
                objdm.LeftUp();
                objdm.RightUp();
                objdm.KeyUp(D3Config.KEYS.Key_Move);
                objdm.KeyUp(D3Config.KEYS.Key_Stand);

            }));
            tmpth.IsBackground = true;
            tmpth.SetApartmentState(ApartmentState.STA);
            althstop.Add(tmpth);

        }


        public void 死灵放大(List<Thread> alth, T_Time tt, List<Thread> althstop)
        {
            alth.Clear();
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key4, tt.Key4));
            Thread th = new Thread(new ThreadStart(() =>
            {
                
                if (tt.Key2 > 0)
                {
                    this.Sleep(Convert.ToInt32(100));
                    objdm.KeyPress(D3Config.KEYS.Key2);
                    this.Sleep(Convert.ToInt32(tt.Key2 * 1000));
                }
                if (tt.Key3 > 0)
                {
                 
                    objdm.KeyPress(D3Config.KEYS.Key3);
                    this.Sleep(Convert.ToInt32(tt.Key3 * 1000));
                }
                if (tt.Key1 > 0)
                {
                    objdm.KeyDown(D3Config.KEYS.Key1);
                    this.Sleep(Convert.ToInt32(D3Config.KEYS.Key3 * 1000));
                    objdm.KeyUp(D3Config.KEYS.Key1);
                }
            }));
            th.IsBackground = true;
            th.SetApartmentState(ApartmentState.STA);
            th.Start();

            alth.Add(th);
            althstop.Clear();
            var tmpth = new Thread(new ThreadStart(delegate()
            {
                objdm.KeyUp(D3Config.KEYS.Key1);
                objdm.KeyUp(D3Config.KEYS.Key2);
                objdm.KeyUp(D3Config.KEYS.Key3);
                objdm.KeyUp(D3Config.KEYS.Key4);
         
                iskey2 = false;
                iskey3 = false;
                iskey4 = false;
                iskey1 = false;

            }));
            tmpth.IsBackground = true;
            tmpth.SetApartmentState(ApartmentState.STA);
            althstop.Add(tmpth);

        }
        public void 原地依次按_1_2_3_4_按住右键(List<Thread> alth, T_Time tt, List<Thread> althstop)
        {
            alth.Clear();
            Thread th = new Thread(new ThreadStart(() =>
            {
                objdm.KeyDown(D3Config.KEYS.Key_Stand);
                if (tt.Key1 > 0)
                {
                    this.Sleep(Convert.ToInt32(tt.Key1 * 1000));
                    objdm.KeyPress(D3Config.KEYS.Key1);
                }
                if (tt.Key2 > 0)
                {
                    this.Sleep(Convert.ToInt32(tt.Key2 * 1000));
                    objdm.KeyPress(D3Config.KEYS.Key2);
                }
                if (tt.Key3 > 0)
                {
                    this.Sleep(Convert.ToInt32(tt.Key3 * 1000));
                    objdm.KeyPress(D3Config.KEYS.Key3);
                }
                if (tt.Key4 > 0)
                {
                    this.Sleep(Convert.ToInt32(tt.Key4 * 1000));
                    objdm.KeyPress(D3Config.KEYS.Key4);
                }

                if (tt.KeyR > 0)
                {
                    objdm.RightDown();
                    this.Sleep(Convert.ToInt32(tt.KeyR * 1000));
                    objdm.RightUp();
                   
                 
                }
                objdm.KeyUp(D3Config.KEYS.Key_Stand);
                
            }));
            th.IsBackground = true;
            th.SetApartmentState(ApartmentState.STA);
            th.Start();

        }
   
        public void 单次_按3_1_2(List<Thread> alth, T_Time tt, List<Thread> althstop)
        {
            alth.Clear();
            Thread th = new Thread(new ThreadStart(() =>
            {
                if (tt.Key3 > 0)
                {
                    this.Sleep(Convert.ToInt32(tt.Key3 * 1000));
                    objdm.KeyPress(D3Config.KEYS.Key3);
                }
                if (tt.Key2 > 0)
                {
                    this.Sleep(Convert.ToInt32(tt.Key2 * 1000));
                    objdm.KeyPress(D3Config.KEYS.Key2);
                }
                if (tt.Key1 > 0)
                {
                    this.Sleep(Convert.ToInt32(tt.Key1 * 1000));
                    objdm.KeyPress(D3Config.KEYS.Key1);
                }


            }));
            th.IsBackground = true;
            th.SetApartmentState(ApartmentState.STA);
            th.Start();

        }
        public void 单次_按2_1(List<Thread> alth, T_Time tt, List<Thread> althstop)
        {
            alth.Clear();
            Thread th = new Thread(new ThreadStart(() =>
            {

                if (tt.Key2 > 0)
                {
                    this.Sleep(Convert.ToInt32(tt.Key2 * 1000));
                    objdm.KeyPress(D3Config.KEYS.Key2);
                }
                if (tt.Key1 > 0)
                {
                    this.Sleep(Convert.ToInt32(tt.Key1 * 1000));
                    objdm.KeyPress(D3Config.KEYS.Key1);
                }


            }));
            th.IsBackground = true;
            th.SetApartmentState(ApartmentState.STA);
            th.Start();

        }

        public void 不停一直按_1_2_3_4_左键_右键(List<Thread> alth, T_Time tt, List<Thread> althstop)
        {
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key1, tt.Key1));
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key2, tt.Key2));
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key3, tt.Key3));
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key4, tt.Key4));
            alth.Add(CreateMouseRightPressThread(tt.KeyR));
          
            althstop.Clear();
            var tmpth=new Thread(new ThreadStart(delegate()
            {

                objdm.RightUp();

            }));
            tmpth.IsBackground = true;
            tmpth.SetApartmentState(ApartmentState.STA);
            althstop.Add(tmpth);
        }
        public void 一直按住强制移动定时按_1_2_3_4_左键_右键(List<Thread> alth, T_Time tt, List<Thread> althstop)
        {
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key1, tt.Key1));
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key2, tt.Key2));
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key3, tt.Key3));
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key4, tt.Key4));
            alth.Add(CreateMouseLeftPressThread(tt.KeyL));
            alth.Add(CreateMouseRightPressThread(tt.KeyR));
            var th = new Thread(new ThreadStart(delegate()
            {
                while (true)
                {
                    if (!isD3)
                    {
                        System.Threading.Thread.Sleep(100);
                        continue;
                    }


                    if(!ismove)
                    {
                        objdm.KeyDown(D3Config.KEYS.Key_Move);
                    }
                  
                    System.Threading.Thread.Sleep(200);
                }
            }));
            th.IsBackground = true;
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            alth.Add(th);
            althstop.Clear();
            var tmpth=new Thread(new ThreadStart(delegate()
            {
                objdm.KeyUp(D3Config.KEYS.Key_Move);
                objdm.RightUp();

            }));
            tmpth.IsBackground = true;
            tmpth.SetApartmentState(ApartmentState.STA);
            althstop.Add(tmpth);
        }


        public void 先停_按住强制移动定时按_2_3_4_左键_右键_金钟破(List<Thread> alth, T_Time tt, List<Thread> althstop)
        {
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key2, tt.Key2, false));
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key3, tt.Key3,false));
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key4, tt.Key4,false));
            alth.Add(CreateMouseLeftPressThreadStand(tt.KeyL, () => { return false; }));
            alth.Add(CreateMouseRightPressThread(tt.KeyR, () => { return this.iskey1; }));


            var th = new Thread(new ThreadStart(delegate()
            {
                while (true)
                {
                    if (!isD3)
                    {
                        System.Threading.Thread.Sleep(100);
                        continue;
                    }


                    if (iskey1)
                    {

                        if (!isstand)
                            objdm.KeyDown(D3Config.KEYS.Key_Stand);
                        if (ismove)
                            objdm.KeyUp(D3Config.KEYS.Key_Move);
                        objdm.KeyPress(D3Config.KEYS.Key1);
                        this.Sleep(50);
                        continue;

                    }
                    else
                    {
                        objdm.KeyUp(D3Config.KEYS.Key_Stand);
                    }

                    
                    if (!ismove)
                        objdm.KeyDown(D3Config.KEYS.Key_Move);
                    System.Threading.Thread.Sleep(50);
                }
            }));
            th.IsBackground = true;
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            alth.Add(th);
            althstop.Clear();
            var tmpth=new Thread(new ThreadStart(delegate()
            {
                objdm.RightUp();
                objdm.LeftUp();
                objdm.KeyUp(D3Config.KEYS.Key_Move);
                objdm.KeyUp(D3Config.KEYS.Key_Stand);
            }));
            tmpth.IsBackground = true;
            tmpth.SetApartmentState(ApartmentState.STA);
            althstop.Add(tmpth);
        }
          public void 定时按_1_2_3_4_左键_右键_神龙猴子(List<Thread> alth, T_Time tt, List<Thread> althstop)
        {
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key1, tt.Key1));
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key2, tt.Key2, false));
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key3, tt.Key3,false));
            alth.Add(CreateKeyPressThread(D3Config.KEYS.Key4, tt.Key4,false));
            alth.Add(CreateMouseLeftPressThread(tt.KeyL, () => { return this.iskey1; }));
            alth.Add(CreateMouseRightPressThread(tt.KeyR, () => { return this.iskey1; }));

            var th = new Thread(new ThreadStart(delegate()
            {
                while (true)
                {
                    if (!isD3)
                    {
                        System.Threading.Thread.Sleep(100);
                        continue;
                    }
                 

                    if(iskey1)
                    {
                        objdm.KeyPress(D3Config.KEYS.Key1);
                        this.Sleep(100);
                        continue;
                   
                    }

                  
                    System.Threading.Thread.Sleep(200);
                }
            }));
            th.IsBackground = true;
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            alth.Add(th);
            althstop.Clear();
            var tmpth=new Thread(new ThreadStart(delegate()
            {
                objdm.RightUp();
                objdm.LeftUp();
                objdm.KeyUp(D3Config.KEYS.Key_Stand);

            }));
            tmpth.IsBackground = true;
            tmpth.SetApartmentState(ApartmentState.STA);
            althstop.Add(tmpth);
        }

        #region
        dmsoftClass objdm = new dmsoftClass();
        public int Handle
        {
            get;
            private set;
        }
        public bool IsMain
        {
            get;
            set;
        }
        public T_DiabloProcess(int hd)
        {
            this.Handle = hd;
            CreateCheckD3();
            BindForm();
        }
        public void BindForm()
        {
            if (objdm.IsBind(this.Handle) == 1)
            {
                objdm.UnBindWindow();
            }
            objdm.delay(50);
            var c = objdm.BindWindow(this.Handle, "normal", "normal", "windows", 1);
        }
        public List<Thread> alth1 = new List<Thread>();
        public List<Thread> alth2 = new List<Thread>();
        public List<Thread> althstop1 = new List<Thread>();
        public List<Thread> althstop2 = new List<Thread>();

        public List<Thread> alth3 = new List<Thread>();
        public List<Thread> alth4 = new List<Thread>();
        public List<Thread> althstop3 = new List<Thread>();
        public List<Thread> althstop4 = new List<Thread>();
        public Thread thcheckd3 = null;
        public void Stop()
        {
            thcheckd3.Suspend();
            Stop1();
            Stop2(); Stop3(); Stop4();
        }
        private void Stop1(bool isstop = true)
        {
            if (!isstop && NoStop(D3Config.PLAN.t1.fmode))
            {
                return;
            }
            Thread thtmp = new Thread(new ThreadStart(() =>
            {
                try
                {
                    foreach (var th in alth1)
                    {
                        th.Abort();
                    }
                    alth1.Clear();
                    foreach (var th in althstop1)
                    {
                        try
                        {
                            th.Start();
                        }
                        catch
                        { }
                    }
                }
                catch
                { }
            }));
            thtmp.Start();

        }
        private bool NoStop(EnumD3 e)
        {
            if (e== EnumD3.不停一直按_1_2_3_4_左键_右键)
            {
                return true;
            }
            return false;
        }
        private void Stop2(bool isstop = true)
        {
            if (!isstop && NoStop(D3Config.PLAN.t2.fmode))
            {
                return;
            }
            Thread thtmp = new Thread(new ThreadStart(() =>
                { try
                {
                    foreach (var th in alth2)
                    {
                        th.Abort();
                    }
                    alth2.Clear();
                    foreach (var th in althstop2)
                    {
                        try
                        {
                            th.Start();
                        }
                        catch
                        { }
                    }
                }
                catch
                { }
                }));
            thtmp.Start();
        }
        private void Suspend_Resume(int i, decimal sleeptime)
        {
            List<Thread> tmp_alth = null;
            if (i == 1)
                tmp_alth = alth1;
            else if (i == 2)
                tmp_alth = alth2;
            else if (i == 3)
                tmp_alth = alth3;
            else
                tmp_alth = alth4;
            if (tmp_alth != null)
            {
                Thread thtmp = new Thread(new ThreadStart(() =>
                {
                    foreach (var th in tmp_alth)
                    {
                        th.Suspend();
                    }
                    this.Sleep(Convert.ToInt32(sleeptime * 1000));
                    foreach (var th in tmp_alth)
                    {
                        th.Resume();
                    }
                }));
                thtmp.Start();
            }
        }

        private void Stop3(bool isstop = true)
        {
            if (!isstop && NoStop(D3Config.PLAN.t3.fmode))
            {
                return;
            }
            Thread thtmp = new Thread(new ThreadStart(() =>
                {
                    try{
            foreach (var th in alth3)
            {
                th.Abort();
            }
            alth3.Clear();
            foreach (var th in althstop3)
            {
                try
                {
                    th.Start();
                }
                catch
                { }
            }
                            }
                catch
                { }
                       }));
            thtmp.Start();
        }
        private void Stop4(bool isstop = true)
        {
            if (!isstop && NoStop(D3Config.PLAN.t4.fmode))
            {
                return;
            }
            Thread thtmp = new Thread(new ThreadStart(() =>
            {
                try{
                foreach (var th in alth4)
                {
                    th.Abort();
                }
                alth4.Clear();
                foreach (var th in althstop4)
                {
                    try
                    {
                        th.Start();
                    }
                    catch
                    { }
                }
                        }
                catch
                { }
            }));
            thtmp.Start();
        }
        public void FN0(T_Plan tp,int type)
        {
            try
            {
                if (type == 1)
                {
                    try
                    {
                    
                        if (alth1.Count > 0)
                        {
                            Stop1();
                            return;
                        }
                        if (thcheckd3 != null && thcheckd3.ThreadState == ThreadState.Suspended)
                        {
                            thcheckd3.Resume();
                        }

                        //alth1.Add(CreateCheckD3());
                        if (thcheckd3 != null)
                            FN(alth1, tp.t1, althstop1);
                    }
                    catch
                    { }
                }
                if (type ==2)
                {
                    try
                    {

                        if (alth2.Count > 0)
                        {
                            Stop2();
                            return;
                        }
                        if (thcheckd3 != null && thcheckd3.ThreadState == ThreadState.Suspended)
                        {
                            thcheckd3.Resume();
                        }
                        //alth2.Add(CreateCheckD3());
                        FN(alth2, tp.t2, althstop2);
                    }
                    catch
                    { }
                }
                if (type ==3)
                {
                    try
                    {
                        if (alth3.Count > 0)
                        {
                            Stop3();
                            return;
                        }
                        if (thcheckd3 != null && thcheckd3.ThreadState == ThreadState.Suspended)
                        {
                            thcheckd3.Resume();
                        }
                        // alth3.Add(CreateCheckD3());
                        FN(alth3, tp.t3, althstop3);
                    }
                    catch
                    { }
                }
                if (type == 4)
                {
                    try
                    {
                        if (alth4.Count > 0)
                        {
                            Stop4();
                            return;
                        }
                        if (thcheckd3 != null && thcheckd3.ThreadState == ThreadState.Suspended)
                        {
                            thcheckd3.Resume();
                        }
                        //alth4.Add(CreateCheckD3());
                        FN(alth4, tp.t4, althstop4);
                    }
                    catch
                    { }
                }

            }
            catch
            { }

        }

        public void FN1(T_Plan tp)
        {
            try
            {

                    if (alth1.Count > 0)
                    {
                        if (tp.t1.FNMax == 1)
                        {
                            Stop1(true);
                        }
                        else
                        {
                            tp.t1.FNType = tp.t1.FNType == tp.t1.FNMax ? 1 : tp.t1.FNType + 1;
                        }
                        return;
                    }
                    if (thcheckd3 != null && thcheckd3.ThreadState == ThreadState.Suspended)
                    {
                        thcheckd3.Resume();
                    }

                    //alth1.Add(CreateCheckD3());
                    if (thcheckd3 != null)
                        FN(alth1, tp.t1, althstop1);

            }
            catch
            { }
           
        }


        public void FN2(T_Plan tp)
        { try
            {

            if (alth2.Count > 0)
            {
                Stop2(true);
                return;
            }
            if (thcheckd3 != null && thcheckd3.ThreadState == ThreadState.Suspended)
            {
                thcheckd3.Resume();
            }
            //alth2.Add(CreateCheckD3());
            FN(alth2, tp.t2, althstop2);
                     }
            catch
            { }
        }

        public void FN3(T_Plan tp)
        {
            try
            {
                if (alth3.Count > 0)
                {
                    Stop3(true);
                    return;
                }
                if (thcheckd3 != null && thcheckd3.ThreadState == ThreadState.Suspended)
                {
                    thcheckd3.Resume();
                }
                // alth3.Add(CreateCheckD3());
                FN(alth3, tp.t3, althstop3);
            }
            catch
            { }

        }


        public void FN4(T_Plan tp)
        {
            try
            {
                if (alth4.Count > 0)
                {
                    Stop4(true);
                    return;
                }
                if (thcheckd3 != null && thcheckd3.ThreadState == ThreadState.Suspended)
                {
                    thcheckd3.Resume();
                }
                //alth4.Add(CreateCheckD3());
                FN(alth4, tp.t4, althstop4);
            }
            catch
            { }
        }

        bool isspack = false;
        bool iskey1 { get; set; }
        bool iskey2 { get; set; }
        bool iskey3 { get; set; }
        bool iskey4 { get; set; }
        bool isD3 = true;
        bool isleft = false;
        bool isRight = false;
        public void Sleep(int tt)
        {
            System.Threading.Thread.Sleep(tt);
        }
        public void Sleep(decimal decmail)
        {
            System.Threading.Thread.Sleep(Convert.ToInt32(decmail));
        }
        private Thread CreateKeyPressThread(int key, decimal keytime,bool isstop=true)
        {
            var th1 = new Thread(new ThreadStart(delegate()
            {
                if (keytime > 0)
                {
                    var t = Convert.ToInt32(keytime * 1000);
                    while (true)
                    {
                        if (isspack && isstop)
                        {
                            System.Threading.Thread.Sleep(100);
                            continue;
                        }
                        if (!isD3)
                        {
                            System.Threading.Thread.Sleep(100);
                            continue;
                        }
                        objdm.KeyPress(key);
                        System.Threading.Thread.Sleep(t);
                    }
                }
            }));
            th1.IsBackground = true;
            th1.SetApartmentState( ApartmentState.STA);
            th1.Start();
            return th1;
        }
        private Thread CreateKey1PressThread(int key, decimal keytime, bool isstop = true)
        {
            var th1 = new Thread(new ThreadStart(delegate()
            {
                if (keytime > 0)
                {
                    var t = Convert.ToInt32(keytime * 1000);
                    while (true)
                    {
                        if (iskey1 && isstop)
                        {
                            System.Threading.Thread.Sleep(100);
                            continue;
                        }
                        if (!isD3)
                        {
                            System.Threading.Thread.Sleep(100);
                            continue;
                        }
                        objdm.KeyPress(key);
                        System.Threading.Thread.Sleep(t);
                    }
                }
            }));
            th1.IsBackground = true;
            th1.SetApartmentState(ApartmentState.STA);
            th1.Start();
            return th1;
        }
        /// <summary>
        /// 按住原地不动
        /// </summary>
        /// <param name="isstop"></param>
        /// <returns></returns>
        private Thread KeyDownStand( bool isstop = true)
        {
            var th1 = new Thread(new ThreadStart(delegate()
            {
                objdm.KeyDown(D3Config.PLAN.Keys.Key_Stand);
                    while (true)
                    {

                       
                       
                            if (isspack && isstop)
                            {
                                objdm.KeyUp(D3Config.PLAN.Keys.Key_Stand);
                                System.Threading.Thread.Sleep(100);
                                continue;
                            }
                            if (!isD3)
                            {
                                objdm.KeyUp(D3Config.PLAN.Keys.Key_Stand);
                                System.Threading.Thread.Sleep(100);
                                continue;
                            }
                            if (!isstand)
                            {
                                objdm.KeyDown(D3Config.PLAN.Keys.Key_Stand);
                            }
                        this.Sleep(50);

               
                     
                    }
                
            }));
            th1.IsBackground = true;
            th1.SetApartmentState(ApartmentState.STA);
            th1.Start();
            return th1;
        }
        public bool isstand { get; set; }
        public bool ismove { get; set; }
      
                /// <summary>
        /// 检查D3
        /// </summary>
        /// <returns></returns>
        private void CreateCheckD3()
        {
            thcheckd3 = new Thread(new ThreadStart(delegate()
            {
                try
                {
                    while (true)
                    {
                        try
                        {
                            var hd = objdm.GetMousePointWindow();
                            if (hd != this.Handle)
                            {
                                isD3 = false;
                            }
                            else
                            {
                                isD3 = true;
                            }
                            if (objdm.GetKeyState(32) == 1)
                            {
                                isspack = true;
                            }
                            else
                            {
                              isspack=false ;
                            }
                            if (objdm.GetKeyState(D3Config.PLAN.Keys.Key_Move) == 1)
                            {
                                ismove = true;
                            }
                            else
                            {
                                ismove = false;
                            }
                            if (objdm.GetKeyState(D3Config.PLAN.Keys.Key_Stand) == 1)
                            {
                                isstand = true;
                            }
                            else
                            {
                                isstand = false;
                            }
                            if (objdm.GetKeyState(D3Config.PLAN.Keys.Key1) == 1)
                            {
                                iskey1 = true;
                            }
                            else
                            {
                                iskey1 = false;
                            }
                            if (objdm.GetKeyState(D3Config.PLAN.Keys.Key2) == 1)
                            {
                                iskey2 = true;
                            }
                            else
                            {
                                iskey2 = false;
                            }
                            if (objdm.GetKeyState(D3Config.PLAN.Keys.Key3) == 1)
                            {
                                iskey3 = true;
                            }
                            else
                            {
                                iskey3 = false;
                            }
                            if (objdm.GetKeyState(D3Config.PLAN.Keys.Key4) == 1)
                            {
                                iskey4 = true;
                            }
                            else
                            {
                                iskey4 = false;
                            }
                            isleft = Frm_Main.isleft;
                            isRight = Frm_Main.isRight;
                        }
                        catch
                        { }
                        System.Threading.Thread.Sleep(80);
                    }
                }
                catch
                { 
                }
            }));
            thcheckd3.IsBackground = true;
            thcheckd3.SetApartmentState(ApartmentState.STA);
            thcheckd3.Start();
        }
        /// <summary>
        /// 原地左键
        /// </summary>
        /// <param name="keytime"></param>
        /// <returns></returns>
        private Thread CreateMouseLeftPressThread(decimal keytime,bool isstop=true)
        {
            var th1 = new Thread(new ThreadStart(delegate()
            {
                if (keytime > 0)
                {
                    var t = Convert.ToInt32(keytime * 1000);
                    while (true)
                    {

                        if (isspack && isstop)
                        {
                            System.Threading.Thread.Sleep(100);
                            continue;
                        }
                        if (!isD3)
                        {
                            System.Threading.Thread.Sleep(100);
                            continue;
                        }
                        if (keytime > 3)
                        {
                            if (Frm_Main.left_time.AddSeconds(Convert.ToDouble(keytime)) > DateTime.Now)
                            {
                                this.Sleep(50);
                                continue;
                            }
                        }
       
                        objdm.LeftClick();
                        System.Threading.Thread.Sleep(t);
                    }
                }
            }));
            th1.IsBackground = true;
            th1.SetApartmentState(ApartmentState.STA);
            th1.Start();
            return th1;
        }
        /// <summary>
        /// 原地左键
        /// </summary>
        /// <param name="keytime"></param>
        /// <returns></returns>
        private Thread CreateMouseLeftPressThread2(decimal keytime, bool isstop = true)
        {
            var th1 = new Thread(new ThreadStart(delegate()
            {
                if (keytime > 0)
                {
                    var t = Convert.ToInt32(keytime * 1000);
                    while (true)
                    {

                        if (isspack && isstop)
                        {
                            System.Threading.Thread.Sleep(100);
                            continue;
                        }
                        if (!isD3)
                        {
                            System.Threading.Thread.Sleep(100);
                            continue;
                        }
                        objdm.KeyDown(D3Config.KEYS.Key_Stand);
                        objdm.LeftClick();
                        objdm.KeyUp(D3Config.KEYS.Key_Stand);
                        System.Threading.Thread.Sleep(t);
                    }
                }
            }));
            th1.IsBackground = true;
            th1.SetApartmentState(ApartmentState.STA);
            th1.Start();
            return th1;
        }
        /// <summary>
        /// 原地左键
        /// </summary>
        /// <param name="keytime"></param>
        /// <returns></returns>
        private Thread CreateMouseLeftPressThread(decimal keytime,Func<bool> f, bool isstop = true)
        {
            var th1 = new Thread(new ThreadStart(delegate()
            {
                if (keytime > 0)
                {
                    var t = Convert.ToInt32(keytime * 1000);
                    while (true)
                    {

                        if (isspack && isstop)
                        {
                            System.Threading.Thread.Sleep(100);
                            continue;
                        }
                        if (f())
                        {
                            System.Threading.Thread.Sleep(100);
                            continue;
                        }
                        if (!isD3)
                        {
                            System.Threading.Thread.Sleep(100);
                            continue;
                        }
                        if (keytime > 3)
                        {
                            if (Frm_Main.left_time.AddSeconds(Convert.ToDouble(keytime)) > DateTime.Now)
                            {
                                this.Sleep(50);
                                continue;
                            }
                        }
                        objdm.LeftClick();
                        System.Threading.Thread.Sleep(t);
                    }
                }
            }));
            th1.IsBackground = true;
            th1.SetApartmentState(ApartmentState.STA);
            th1.Start();
            return th1;
        }

        private Thread CreateMouseLeftPressThreadStand(decimal keytime, Func<bool> f, bool isstop = true)
        {
            var th1 = new Thread(new ThreadStart(delegate()
            {
                if (keytime > 0)
                {
                    var t = Convert.ToInt32(keytime * 1000);
                    var tmpdate = DateTime.Now.AddSeconds(-3);
                    while (true)
                    {

                        if (isspack && isstop)
                        {
                            System.Threading.Thread.Sleep(100);
                            continue;
                        }
                        if (f())
                        {
                            System.Threading.Thread.Sleep(100);
                            continue;
                        }
                        if (!isD3)
                        {
                            System.Threading.Thread.Sleep(100);
                            continue;
                        }
                        if (keytime > 3)
                        {
                            if (Frm_Main.left_time.AddSeconds(Convert.ToDouble(keytime)) > DateTime.Now)
                            {
                                this.Sleep(50);
                                continue;
                            }
                        }
                        if ((DateTime.Now - tmpdate).TotalSeconds > 2)
                        {
                            objdm.KeyDown(D3Config.KEYS.Key_Stand);
                            objdm.LeftClick();
                            objdm.KeyUp(D3Config.KEYS.Key_Stand);
                            tmpdate = DateTime.Now;
                        }
                        else
                        {
                            objdm.LeftClick();
                        }
                        System.Threading.Thread.Sleep(t);
                    }
                }
            }));
            th1.IsBackground = true;
            th1.SetApartmentState(ApartmentState.STA);
            th1.Start();
            return th1;
        }
        /// <summary>
        /// 原地右键 
        /// </summary>
        /// <param name="keytime"></param>
        /// <returns></returns>
        private Thread CreateMouseRightPressThread(decimal keytime,bool isstop=true)
        {
            var th1 = new Thread(new ThreadStart(delegate()
            {
                if (keytime > 0)
                {
                    var t = Convert.ToInt32(keytime * 1000);
                    while (true)
                    {
                        if (isspack && isstop)
                        {
                            System.Threading.Thread.Sleep(50);
                            continue;
                        }
                        if (!isD3)
                        {
                            System.Threading.Thread.Sleep(50);
                            continue;
                        }

                        if (Frm_Main.right_time.AddSeconds(Convert.ToDouble(keytime)) > DateTime.Now)
                        {
                            this.Sleep(50);
                            continue;
                        }
                        else
                        {
                            objdm.RightClick();
                            Frm_Main.right_time = DateTime.Now;
                            this.Sleep(10);
                        }

                    }
                }
            }));
            th1.IsBackground = true;
            th1.SetApartmentState(ApartmentState.STA);
            th1.Start();
            return th1;
        }
        /// <summary>
        /// 原地右键 
        /// </summary>
        /// <param name="keytime"></param>
        /// <returns></returns>
        private Thread CreateMouseRightPressThread(decimal keytime,Func<bool> f, bool isstop = true)
        {
            var th1 = new Thread(new ThreadStart(delegate()
            {
                if (keytime > 0)
                {
                    var t = Convert.ToInt32(keytime * 1000);
                    while (true)
                    {
                        if (isspack && isstop)
                        {
                            System.Threading.Thread.Sleep(50);
                            continue;
                        }
                        if(f())
                        {
                            System.Threading.Thread.Sleep(50);
                            continue;
                        }
                        if (!isD3)
                        {
                            System.Threading.Thread.Sleep(50);
                            continue;
                        }

                        if (Frm_Main.right_time.AddSeconds(Convert.ToDouble(keytime)) > DateTime.Now)
                        {
                            this.Sleep(50);
                            continue;
                        }
                        else
                        {
                            objdm.RightClick();
                            Frm_Main.right_time = DateTime.Now;
                            this.Sleep(10);
                        }

                    }
                }
            }));
            th1.IsBackground = true;
            th1.SetApartmentState(ApartmentState.STA);
            th1.Start();
            return th1;
        }


        #endregion
    }
}
