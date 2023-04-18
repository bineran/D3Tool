//using Dm;
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
        public void StartKeyRank()
        {
            //var objdm = this.CreateDM();
             var list = this.Times.Where(r => r.keyClickType != KeyClickType.不做操作
             && r.KeyCode > 0 && r.Rank > 0).OrderBy(r => r.Rank).ToList();
            bool addStand = false;
            bool addLeft = false;
            bool addRight = false;
 
            Sleep(150);
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
