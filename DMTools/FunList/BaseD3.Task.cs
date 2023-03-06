using DMTools.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMTools.FunList
{
    public abstract partial class BaseD3 
    {
        private Task StartNewTask(Action action)
        {

            var t= Task.Factory.StartNew(() =>
            {
                try
                {
                    action();
                }
                catch (OperationCanceledException ex)
                {
                    //不记录 取消的日志
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                }
            }, cs.Token);
            return t;

        }
        private Task StartNewForTask(Action action,int sleep, bool checkPause = true,bool checkHandle=true)
        {

            var t = Task.Factory.StartNew(() =>
            {
                try
                {
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
                      
                        action();
                        Sleep(sleep);
                    }
                }
                catch (OperationCanceledException ex)
                {
                    //不记录 取消的日志
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                }
            }, cs.Token);
            return t;

        }
        /// <summary>
        /// 执行一个TASK并将他加入到StartTaskList
        /// </summary>
        /// <param name="action"></param>
        public void AddStartTask(Action action)
        {
            StartTaskList.Add(StartNewTask(action));
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
        /// <summary>
        /// 执行一个TASK并将他加入到StartTaskList
        /// </summary>
        /// <param name="action"></param>
        public void AddStopTask(Action action)
        {
            StopTaskList.Add(CreateTask(action));
        }
        public void AddStopTaskKeysUp(Keys key)
        {
            AddStopTaskKeysUp((int)key);
           
        }
        public void AddStopTaskKeysUp(int key)
        {
            StopTaskList.Add(CreateTask(() => { objdm.KeyUp(key); }));
        }
        public void AddStopTaskKeysUpStand()
        {
            AddStopTaskKeysUp(this.d3Param.KeyCodes.KeyStand);
 
        }
        
        public void AddStopTaskLeftUp()
        {
            StopTaskList.Add(CreateTask(() => { objdm.LeftUp(); }));
        }
        public void AddStopTaskRightUp()
        {
            StopTaskList.Add(CreateTask(() => { objdm.RightUp(); }));
        }


        private Task CreateTask(Action action)
        {
            return new Task(() =>
            {
                try
                { action(); }
                catch (Exception ex) { log.Error(ex); }
            });

        }
        private Task CreateForTask(Action action,int sleepTime,bool checkHandle=true)
        {
            return new Task(() =>
            {
                try
                {
                    while (true)
                    { 
                        if(checkHandle && !this.IsHandle)
                        {
                            Sleep(50);
                            continue;
                        }
                        action();
                        Sleep(sleepTime);
                    }
                  }
                catch (Exception ex) { log.Error(ex); }
            });

        }
        public void StartKeyDown()
        {
            var list = this.Times.Where(r => r.keyClickType == Config.KeyClickType.按下
             && r.KeyCode > 0 && r.Rank == 0).ToList();
            foreach (var kt in list)
            {
                if (NoMouseKey(kt.KeyCode))
                    AddKeyDownForTask(kt.KeyCode);
                else if (kt.KeyCode == BaseD3.MouseLeft)
                {
                    AddLeftDownForTask(kt);
                }
                else if (kt.KeyCode == BaseD3.MouseRight)
                {
                    AddRightDownForTask(kt);
                }
                else if (kt.KeyCode == BaseD3.MouseShiftLeft)
                {
                    AddShiftLeftDownForTask(kt);
                }
            }
        }
        public void StartKeyPress()
        {
            var list = this.Times.Where(r => r.keyClickType == Config.KeyClickType.点击
             && r.KeyCode > 0 && r.Rank == 0 && NoMouseKey(r.KeyCode)).ToList();
            foreach (var kt in list)
            {
                if (NoMouseKey(kt.KeyCode))
                    AddKeyPressForTask(kt);
                else if (kt.KeyCode == BaseD3.MouseLeft)
                {
                    AddLeftClickForTask(kt);
                }
                else if (kt.KeyCode == BaseD3.MouseRight)
                {
                    AddRightClickForTask(kt);
                }
                else if (kt.KeyCode == BaseD3.MouseShiftLeft)
                {
                    AddShiftLeftClickForTask(kt);
                }
            }
        }

        public void StartKeyRank()
        {

            var list = this.Times.Where(r => r.keyClickType != Config.KeyClickType.不做操作
             && r.KeyCode > 0 && r.Rank > 0).OrderBy(r => r.Rank);
            bool addStand = false;
            bool addLeft = false;
            bool addRight = false;
            foreach (var ts in list)
            {
                if (NoMouseKey(ts.KeyCode))
                {
                    switch (ts.keyClickType)
                    {
                        case Config.KeyClickType.点击:
                            KeyPress(ts);
                            break;
                        case Config.KeyClickType.按下:
                            KeyDown(ts);
                            break;
                        case Config.KeyClickType.弹起:
                            KeyUp(ts);
                            break;
                    }
                }
                else
                {
                    if (ts.KeyCode == BaseD3.MouseLeft)
                    {
                        switch (ts.keyClickType)
                        {
                            case Config.KeyClickType.点击:
                                objdm.LeftClick(); Sleep(ts.D1);
                                break;
                            case Config.KeyClickType.按下:
                                addLeft = true;
                                objdm.LeftDown(); Sleep(ts.D1);
                                break;
                            case Config.KeyClickType.弹起:
                                objdm.LeftUp(); Sleep(ts.D1);
                                break;
                        }
                    }
                    else if (ts.KeyCode == BaseD3.MouseRight)
                    {
                        switch (ts.keyClickType)
                        {
                            case Config.KeyClickType.点击:
                                objdm.RightClick(); Sleep(ts.D1);
                                break;
                            case Config.KeyClickType.按下:
                                addRight = true;
                                objdm.RightDown(); Sleep(ts.D1);
                                break;
                            case Config.KeyClickType.弹起:
                                objdm.RightUp(); Sleep(ts.D1);
                                break;
                        }
                    }
                    else if (ts.KeyCode == BaseD3.MouseShiftLeft)
                    {
                        switch (ts.keyClickType)
                        {
                            case Config.KeyClickType.点击:
                                addStand = true;
                                objdm.KeyDown(this.d3Param.KeyCodes.KeyStand);
                                objdm.LeftClick();
                                objdm.KeyUp(this.d3Param.KeyCodes.KeyStand);
                                Sleep(ts.D1);
                                break;
                            case Config.KeyClickType.按下:
                                addLeft = true;
                                addStand = true;
                                objdm.KeyDown(this.d3Param.KeyCodes.KeyStand);
                                objdm.LeftDown(); Sleep(ts.D1);
                                break;
                            case Config.KeyClickType.弹起:
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
            Action action = () =>
            {
                if (this.d3KeyState.isPause)
                {
                    objdm.LeftClick();
                }
            };
           StartTaskList.Add(StartNewForTask(action,50,false));
        }


    }
}
