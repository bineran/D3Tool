//using Dm;
using DMTools.Config;
using DMTools.Control;
using DMTools.libs;
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
        public  Task BuildTask(Action<Idmsoft> action, bool runStart = true)
        {
            DMP dMP= new DMP();
            Idmsoft objdm = dMP.DM;
            D3Main.BindForm(objdm, funTaskParam.Handle);
            Task task;
            if (funTaskParam.cancellationTokenSource != null)
            {
                task = new Task(() =>
                {
                    action(objdm);
                }, funTaskParam.cancellationTokenSource.Token);
            }
            else
            {
                task = new Task(() =>
                {
                    action(objdm);
                });
            }

            if (runStart)
            {
                task.Start();
            }
            return task;
        }

     
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
                            Sleep(ts.D1);
                            break;
                        case KeyClickType.按下:
                            objdm.KeyDown((int)ts.KeyCode);
                            Sleep(ts.D1);
                            break;
                        case KeyClickType.弹起:
                            objdm.KeyUp((int)ts.KeyCode);
                            Sleep(ts.D1);
                            break;
                    }
                }
                else
                {
                    if (ts.KeyCode == ConvertKeys.MouseLeft)
                    {
                        switch (ts.keyClickType)
                        {
                            case KeyClickType.点击:
                                objdm.LeftClick(); Sleep(ts.D1);
                                break;
                            case KeyClickType.按下:
                                addLeft = true;
                                objdm.LeftDown(); Sleep(ts.D1);
                                break;
                            case KeyClickType.弹起:
                                objdm.LeftUp(); Sleep(ts.D1);
                                break;
                        }
                    }
                    else if (ts.KeyCode == ConvertKeys.MouseRight)
                    {
                        switch (ts.keyClickType)
                        {
                            case KeyClickType.点击:
                                objdm.RightClick(); Sleep(ts.D1);
                                break;
                            case KeyClickType.按下:
                                addRight = true;
                                objdm.RightDown(); Sleep(ts.D1);
                                break;
                            case KeyClickType.弹起:
                                objdm.RightUp(); Sleep(ts.D1);
                                break;
                        }
                    }
                    else if (ts.KeyCode == ConvertKeys.MouseShiftLeft)
                    {
                        switch (ts.keyClickType)
                        {
                            case KeyClickType.点击:
                                addStand = true;
                                objdm.KeyDown(this.d3Param.KeyCodes.KeyStand);
                                objdm.LeftClick();
                                objdm.KeyUp(this.d3Param.KeyCodes.KeyStand);
                                Sleep(ts.D1);
                                break;
                            case KeyClickType.按下:
                                addLeft = true;
                                addStand = true;
                                objdm.KeyDown(this.d3Param.KeyCodes.KeyStand);
                                objdm.LeftDown(); Sleep(ts.D1);
                                break;
                            case KeyClickType.弹起:
                                objdm.LeftUp();
                                objdm.KeyUp(this.d3Param.KeyCodes.KeyStand);
                                Sleep(ts.D1);
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
