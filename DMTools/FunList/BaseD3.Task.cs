﻿//using Dm;
using DMTools.Config;
using DMTools.Control;
using DMTools.libs;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMTools.FunList
{
    public abstract partial class BaseD3 
    {
       

     
        /// <summary>
        /// 是否需要初始化大漠
        /// </summary>
        /// <param name="action"></param>
        /// <param name="isInitDM"></param>
        /// <returns></returns>
        private Task CreateTask(Action action,bool isInitDM=false)
        {
            return new Task(() =>
            {
                try
                {
                    if (isInitDM)
                    {
                        this.Init();
                    }
                    action(); }
                catch (OperationCanceledException ex)
                {
                    //不记录 取消的日志
                }
                catch (Exception ex) { log.Error(ex); }
            }, cs.Token);


        }
        private Task CreateTaskNoCS(Action action)
        {
            return new Task(() =>
            {
                try
                { action(); }
                catch (Exception ex) { log.Error(ex); }
            });
        }
        public Task StartNewTask(Action action, bool isInitDM = false)
        {
            var task = CreateTask(action,  isInitDM);
            task.Start();
            return task;
        }
        public void StartNewTaskToList(Action action, bool isInitDM = false)
        {
            this.StartTaskList.Add(StartNewTask(action, isInitDM));

        }
        /// <summary>
        /// 开启一个根据cs.IsCancellationRequested 的Thread
        /// </summary>
        /// <param name="action"></param>
        public void StartForThreadToList(Action action)
        {

            Thread th = new Thread(() =>
            {
                while (!cs.IsCancellationRequested)
                {
                    action();
                }
            });
            th.IsBackground = true;
            // th.SetApartmentState(ApartmentState.STA);
            th.Start();
            this.StartThreadList.Add(th);

        }

        public Task StartNewForTask(Action action, int sleep, bool checkPause = true, bool checkHandle = true)
        {
            var actionFor = () =>
            {
                if (sleep < 1)
                {
                    return;
                }
                while (true)
                {
                    if (checkHandle && !this.d3KeyState.isD3)
                    {
                        Sleep(50);
                        continue;
                    }
                    if (checkPause && this.d3KeyState.isPause)
                    {
                        Sleep(50);
                        continue;
                    }
                    try
                    {
                        action();
                    }
                    catch (Exception ex)
                    { log.Error(ex); }
                    Sleep(sleep);
                }
            };
            return StartNewTask(actionFor);
        }
       

        public void Sleep(int Sleep)
        {
            if (Sleep <1)
            {
                return;
            }
            if (cs != null)
            {
                Task.Delay(Sleep).Wait(cs.Token);
            }
            if (cs.IsCancellationRequested)
            {
                cs.Token.ThrowIfCancellationRequested();
            }

        }
       


        public void StartKeyDown()
        {
            var list = this.Times.Where(r => r.keyClickType == KeyClickType.按下
             && r.KeyCode > 0 && r.Rank == 0).ToList();
            foreach (var kt in list)
            {
                if (ConvertKeys.NoMouseKey(kt.KeyCode))
                    AddKeyDownForTask(kt.KeyCode);
                else if (kt.KeyCode == ConvertKeys.MouseLeft)
                {
                    AddLeftDownForTask(kt);
                }
                else if (kt.KeyCode == ConvertKeys.MouseRight)
                {
                    AddRightDownForTask(kt);
                }
                else if (kt.KeyCode == ConvertKeys.MouseShiftLeft)
                {
                    AddShiftLeftDownForTask(kt);
                }
                else if (kt.KeyCode == ConvertKeys.MouseShiftLeft)
                {
                    AddShiftLeftDownForTask(kt);
                }
                else if (kt.KeyCode == ConvertKeys.MouseShiftLeft)
                {
                    AddShiftLeftDownForTask(kt);
                }
                else if (kt.KeyCode == ConvertKeys.HotKeyRightWhereShiftDown)
                {
                    AddWhereRightDownShiftForTask(kt,25);
                }
                else if (kt.KeyCode == ConvertKeys.HotKeyLeftWhereShiftDown)
                {
                    AddWhereLeftDownShiftForTask(kt,25);
                }
            }
        }
        public void StartKeyPress()
        {
            var list = this.Times.Where(r => r.keyClickType == KeyClickType.点击
             && r.KeyCode > 0 && r.Rank == 0).ToList();
            foreach (var kt in list)
            {
                AddKeyPressForTask(kt);
            }
        }
        public void MoveMouse(KeyTimeSetting k)
        {
            if (k.Int1 > 0 && k.Int2 > 0)
            { 
                objdm.MoveTo(k.Int1, k.Int2);
              
            }
            Sleep(k.D1);
        }
        /// <summary>
        /// 验证两个点的颜色,找到为真,否则为false
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public bool ValidatePointColor(KeyTimeSetting ts)
        {
            var x = ts.Int1;
            var y = ts.Int2;
            var tagColor = ts.Str1.ToColor();
            var tagColor2 = ts.Str2.ToColor();
            var x1 = ts.Int3;
            var y1 = ts.Int4;



            var ret = objdm.CmpColor(x, y, tagColor, this.d3Param.sysConfig.color_sim);
            var ret2 = -1;
            if (x1 > 0 && y1 > 0 && tagColor2 != null)
            {
                ret2 = objdm.CmpColor(x1, y1, tagColor2, this.d3Param.sysConfig.color_sim);
            }
            if (ret == 0 && (ret2 == -1 || ret2 == 0))
            {
                return true;
            }
            return false;
        }
        public bool ValidateImage(KeyTimeSetting ts)
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
                return false;
            }
            object x;
            object y;
            var ret = objdm.FindPic(ts.Int1, ts.Int2, ts.Int3, ts.Int4, allpic, this.d3Param.sysConfig.delta_color, this.d3Param.sysConfig.sim, 0, out x, out y);
            return (ret >= 0);
        }
        public void StartKeyRank()
        {
            //var objdm = this.CreateDM();
             var list = this.Times.Where(r => r.keyClickType != KeyClickType.不做操作
             && r.KeyCode > 0 && r.Rank > 0).OrderBy(r => r.Rank).ToList();
            bool addStand = false;
            bool addLeft = false;
            bool addRight = false;
            this.Sleep(50);
            if(list.Count > 0)
            {
                var ts = list[0];
                if (ts.keyClickType == KeyClickType.颜色不匹配点击 && ValidatePointColor(ts) )
                    return;
                if (ts.keyClickType == KeyClickType.颜色匹配点击 && !ValidatePointColor(ts))
                    return;
                if (ts.keyClickType == KeyClickType.图片未找到点击 && ValidateImage(ts))
                    return;
                if (ts.keyClickType == KeyClickType.图片找到点击 && !ValidateImage(ts))
                    return;
            }
            foreach (var ts in list)
            {
                if (ConvertKeys.NoMouseKey(ts.KeyCode))
                {
                    switch (ts.keyClickType)
                    {
                        case KeyClickType.点击:
                            objdm.KeyPress((int)ts.KeyCode);

                            break;
                        case KeyClickType.按下:
                            objdm.KeyDown((int)ts.KeyCode);
 
                            break;
                        case KeyClickType.弹起:
                            objdm.KeyUp((int)ts.KeyCode);
                            break;
                    }
                    Sleep(ts.D1);
                }
                else
                {
                    if (ts.KeyCode == ConvertKeys.MouseLeft)
                    {
                        MoveMouse(ts);
                        switch (ts.keyClickType)
                        {
                            case KeyClickType.点击:
                                objdm.LeftClick(); 
                                break;
                            case KeyClickType.按下:
                                addLeft = true;
                                objdm.LeftDown(); 
                                break;
                            case KeyClickType.弹起:
                                objdm.LeftUp(); 
                                break;
                        }
                    }
                    else if (ts.KeyCode == ConvertKeys.MouseRight)
                    {
                        MoveMouse(ts);
                        switch (ts.keyClickType)
                        {
                            case KeyClickType.点击:
                                objdm.RightClick(); 
                                break;
                            case KeyClickType.按下:
                                addRight = true;
                                objdm.RightDown(); 
                                break;
                            case KeyClickType.弹起:
                                objdm.RightUp(); 
                                break;
                        }
                    }
                    else if (ts.KeyCode == ConvertKeys.MouseShiftLeft)
                    {
                        MoveMouse(ts);
                        switch (ts.keyClickType)
                        {
                            case KeyClickType.点击:
                                addStand = true;
                                objdm.KeyDown(this.d3Param.KeyCodes.KeyStand);
                                objdm.LeftClick();
                                objdm.KeyUp(this.d3Param.KeyCodes.KeyStand);
     
                                break;
                            case KeyClickType.按下:
                                addLeft = true;
                                addStand = true;
                                objdm.KeyDown(this.d3Param.KeyCodes.KeyStand);
                                objdm.LeftDown(); 
                                break;
                            case KeyClickType.弹起:
                                objdm.LeftUp();
                                objdm.KeyUp(this.d3Param.KeyCodes.KeyStand);

                                break;
                        }
                    }
                }
            }
            if (addStand)
            {
                AddStopTask(() => {
                    objdm.KeyUp(this.d3Param.KeyCodes.KeyStand);
                });
            }
            if (addLeft)
            {
                AddStopTask(() => {
                    objdm.LeftUp();
                });
            }
            if (addRight)
            {
                AddStopTask(() => {
                    objdm.RightUp();
                });
            }

        }
        public bool IsHandle{ get { return this.d3KeyState.isD3; } }
        public void AddPauseClick()
        {
            var list = this.Times.Where(r => r.keyClickType == KeyClickType.点击
           && r.KeyCode > 0 && r.Rank == 0).ToList();
            foreach (var kt in list)
            {
                AddPauseKeyPressForTask(kt);
            }
           
        }



    }
}
