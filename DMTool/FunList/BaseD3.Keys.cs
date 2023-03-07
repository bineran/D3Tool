using DMTool.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMTool.FunList
{
    public abstract partial class BaseD3 
    {
        public const Keys MouseLeft= Keys.Control | Keys.Left;
        public const Keys MouseRight = Keys.Control | Keys.Right;
        public const Keys MouseShiftLeft = Keys.Shift | Keys.Left;
        public static bool NoMouseKey(Keys k)
        {
            if (k == MouseLeft)
                return false;
            else if (k == MouseRight)
                return false;
            else if (k == MouseShiftLeft)
                return false;
            return true;
        }
        /// <summary>
        /// 添加一直接住的键的TASK
        /// </summary>
        /// <param name="action"></param>
        public void AddKeyDownForTask(Keys keys, int sleep = 100)
        {
            int key = (int)keys;
            objdm.KeyDown(key);
            DateTime tmp = DateTime.Now.AddSeconds(2);
            var action = new Action(() =>
            {
                if (this.d3KeyState.isPause || !this.d3KeyState.isD3)
                {
                    objdm.KeyUp(key);
                    Sleep(sleep);
                    return;
                }

                if (tmp < DateTime.Now)
                {
                    objdm.KeyDown(key);
                    tmp = DateTime.Now.AddSeconds(2);
                }
                if (!this.d3KeyState[keys])
                {
                    objdm.KeyDown(key);
                }
            });
            StartTaskList.Add(StartNewForTask(action, sleep,false,false));
            AddStopTaskKeysUp(key);
        }
        public void AddKeyPressForTask(D3TimeSetting ts)
        {
            if (ts.Rank == 0 && ts.D1 > 0 && ts.keyClickType == KeyClickType.点击)
            {
                int key = (int)ts.KeyCode;
                var action = new Action(() =>
                {
                    objdm.KeyPress(key);
                });
                StartTaskList.Add(StartNewForTask(action, ts.D1));
            }
        }

        public void AddLeftDownForTask(D3TimeSetting ts, int sleep = 100)
        {
            if (ts.Rank == 0  && ts.KeyCode== BaseD3.MouseLeft && ts.keyClickType == KeyClickType.按下)
            {
                DateTime tmp = DateTime.Now.AddSeconds(2);
                objdm.LeftDown();
                var action = new Action(() =>
                {
                    if (this.d3KeyState.isPause || !this.d3KeyState.isD3)
                    {
                        objdm.LeftUp();
                        Sleep(sleep);
                        return;
                    }
                    if (tmp < DateTime.Now)
                    {
                        objdm.LeftDown();
                        tmp = DateTime.Now.AddSeconds(2);
                    }
                    if (!this.d3KeyState.isLeft)
                    {
                        objdm.LeftDown();
                    }
                });
                StartTaskList.Add(StartNewForTask(action, sleep,false, false));
                AddStopTaskLeftUp();
            }
        }
        public void AddRightDownForTask(D3TimeSetting ts, int sleep = 100)
        {
            if (ts.Rank == 0 && ts.KeyCode == BaseD3.MouseRight && ts.keyClickType == KeyClickType.按下)
            {
                DateTime tmp = DateTime.Now.AddSeconds(2);
                objdm.RightDown();
                var action = new Action(() =>
                {
                    if (this.d3KeyState.isPause || !this.d3KeyState.isD3)
                    {
                        objdm.RightUp();
                        Sleep(sleep);
                        return;
                    }
                    if (tmp < DateTime.Now)
                    {
                        objdm.RightDown();
                        tmp = DateTime.Now.AddSeconds(2);
                    }
                    if (!this.d3KeyState.isRight)
                    {
                        objdm.RightDown();
                    }
                });
                StartTaskList.Add(StartNewForTask(action, sleep, false, false));
                AddStopTaskRightUp();
            }
        }

        public void AddShiftLeftDownForTask(D3TimeSetting ts, int sleep = 100)
        {
            if (ts.Rank == 0 && ts.KeyCode == BaseD3.MouseShiftLeft && ts.keyClickType == KeyClickType.按下)
            {
                DateTime tmp = DateTime.Now.AddSeconds(2);
                objdm.KeyDown(this.d3Param.KeyCodes.KeyStand);
                objdm.LeftDown();
                var action = new Action(() =>
                {
                    if (this.d3KeyState.isPause || !this.d3KeyState.isD3)
                    {
                        objdm.KeyUp(this.d3Param.KeyCodes.KeyStand);
                        objdm.LeftUp();
                        Sleep(sleep);
                        return;
                    }
                    if (tmp < DateTime.Now)
                    {
                        objdm.KeyDown(this.d3Param.KeyCodes.KeyStand);
                        objdm.LeftDown();
                        tmp = DateTime.Now.AddSeconds(2);
                    }
                    if (!this.d3KeyState.isLeft)
                    {
                        objdm.LeftDown();
                    }
                });
                StartTaskList.Add(StartNewForTask(action, sleep, false, false));
                AddStopTaskLeftUp();
                AddStopTaskKeysUpStand();
            }
        }
        public void AddShiftLeftClickForTask(D3TimeSetting ts, int sleep = 100)
        {
            if (ts.Rank == 0 && ts.KeyCode == BaseD3.MouseShiftLeft && ts.keyClickType == KeyClickType.点击)
            {

                var action = new Action(() =>
                {
                    objdm.KeyDown(this.d3Param.KeyCodes.KeyStand);
                    objdm.LeftClick();
                    objdm.KeyUp(this.d3Param.KeyCodes.KeyStand);
                });
                StartTaskList.Add(StartNewForTask(action, sleep));
                AddStopTaskKeysUpStand();
            }
        }

        public void AddLeftClickForTask(D3TimeSetting ts)
        {
            if (ts.Rank == 0 && ts.D1 > 0 && ts.keyClickType == KeyClickType.点击)
            {
                var action = new Action(() =>
                {
                    objdm.LeftClick();
                });
                StartTaskList.Add(StartNewForTask(action, ts.D1));
            }
        }
        public void AddRightClickForTask(D3TimeSetting ts)
        {
            if (ts.Rank == 0 && ts.D1 > 0 && ts.keyClickType == KeyClickType.点击)
            {
                var action = new Action(() =>
                {
                    objdm.RightClick();
                });
                StartTaskList.Add(StartNewForTask(action, ts.D1));
            }
        }
        public void KeyPress(D3TimeSetting ts)
        {
            objdm.KeyPress((int)ts.KeyCode);
            Sleep(ts.D1);
        }
        public void KeyDown(D3TimeSetting ts)
        {
            objdm.KeyDown((int)ts.KeyCode);
            Sleep(ts.D1);
        }
        public void KeyUp(D3TimeSetting ts)
        {
            objdm.KeyUp((int)ts.KeyCode);
            Sleep(ts.D1);
        }
    }
}
