using DMTools.libs;
using DMTools.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.CompilerServices.RuntimeHelpers;
//using Dm;

namespace DMTools.FunList
{
    public abstract partial class BaseD3 
    {

        /// <summary>
        /// 验证是否为上下滚轮
        /// </summary>
        /// <param name="k"></param>
        /// <returns></returns>
        public static bool IsHotKeyMouseUpDown(Keys k)
        { 
            if(k== ConvertKeys.HotKeyMouseUp) return true;
            else if(k== ConvertKeys.HotKeyMouseDown) return true;
            return false;
        }

  
        /// <summary>
        /// 添加一直接住的键的TASK
        /// </summary>
        /// <param name="action"></param>
        public void AddKeyDownForTask(Keys keys, int sleepTime=100)
        {
            var objdm = this.CreateDM();
            int key = (int)keys;
            objdm.KeyDown(key);
            var action = () =>
            {
                if(this.d3KeyState.isPause || !this.d3KeyState.isD3)
                {
                    objdm.KeyUp(key);
                    Sleep(sleepTime);
                    return;
                }
                //return;
                if (!this.d3KeyState[keys])
                {
                    objdm.KeyDown(key);
                }
            };
            StartTaskList.Add(StartNewForTask(action, sleepTime, false,false));
            AddStopTaskKeysUp(key);
        }
        public void AddKeyPressForTask(KeyTimeSetting ts)
        {
        
            if (ts.Rank == 0 && ts.D1 > 0 && ts.keyClickType == KeyClickType.点击)
            {
         
                int key = (int)ts.KeyCode;
                var objdm=CreateDM();
                var action = () =>
                {
             
                    switch (ts.KeyCode)
                    {
                        case ConvertKeys.MouseLeft:
                            objdm.LeftClick();
                            break;
                        case ConvertKeys.MouseRight:
                            objdm.RightClick();
                            break;
                        case ConvertKeys.MouseShiftLeft:
                            objdm.KeyDown(this.d3Param.KeyCodes.KeyStand);
                            objdm.LeftClick();
                            objdm.KeyUp(this.d3Param.KeyCodes.KeyStand);
                            break;
                        default:
                            objdm.KeyPress(key);
                            break;
                    }
                };
                StartTaskList.Add(StartNewForTask(action, ts.D1));
            }
        }

        public void AddLeftDownForTask(KeyTimeSetting ts, int sleep = 100)
        {
          
            if (ts.Rank == 0  && ts.KeyCode== ConvertKeys.MouseLeft && ts.keyClickType == KeyClickType.按下)
            {
                var objdm = CreateDM();
                // DateTime tmp = DateTime.Now.AddSeconds(2);
                objdm.LeftDown();
                var action = () =>
                {
                    if (this.d3KeyState.isPause || !this.d3KeyState.isD3)
                    {
                        objdm.LeftUp();
                        Sleep(sleep);
                        return;
                    }
                    //if (tmp < DateTime.Now)
                    //{
                    //    objdm.LeftDown();
                    //    tmp = DateTime.Now.AddSeconds(2);
                    //}
                    if (!this.d3KeyState.isLeft)
                    {
                        objdm.LeftDown();
                    }
                };
                StartTaskList.Add(StartNewForTask(action, sleep,false, false));
                AddStopTaskLeftUp();
            }
        }
        public void AddRightDownForTask(KeyTimeSetting ts, int sleep = 100)
        {
         
            if (ts.Rank == 0 && ts.KeyCode == ConvertKeys.MouseRight && ts.keyClickType == KeyClickType.按下 && sleep>0)
            {
                var objdm = CreateDM();
                objdm.RightDown();
           
                var action = () =>
                {
                    if (this.d3KeyState.isPause || !this.d3KeyState.isD3)
                    {
                        objdm.RightUp();
                        Sleep(sleep);
                        return;
                    }
                    if (!this.d3KeyState.isRight)
                    {
                        objdm.RightDown();
                    }
                };
                StartTaskList.Add(StartNewForTask(action, sleep, false, false));
                AddStopTaskRightUp();
            }
        }

        public void AddShiftLeftDownForTask(KeyTimeSetting ts, int sleep = 100)
        {
          
            if (ts.Rank == 0 && ts.KeyCode == ConvertKeys.MouseShiftLeft && ts.keyClickType == KeyClickType.按下  && sleep > 0)
            {
                var objdm = CreateDM();
                objdm.KeyDown(this.d3Param.KeyCodes.KeyStand);
                objdm.LeftDown();
                var action = () =>
                {
                    if (this.d3KeyState.isPause || !this.d3KeyState.isD3)
                    {
                        objdm.KeyUp(this.d3Param.KeyCodes.KeyStand);
                        objdm.LeftUp();
                        Sleep(sleep);
                        return;
                    }
                    if (!this.d3KeyState[this.d3Param.KeyCodes.KeyStand])
                    {
                        objdm.KeyDown(this.d3Param.KeyCodes.KeyStand);
                    }
                    if (!this.d3KeyState.isLeft)
                    {
                        objdm.LeftDown();
                    }
                };
                StartTaskList.Add(StartNewForTask(action, sleep, false, false));
                AddStopTaskLeftUp();
                AddStopTaskKeysUpStand();
            }
        }
        public void AddShiftLeftClickForTask(KeyTimeSetting ts, int sleep = 100)
        {
         
            if (ts.Rank == 0 && ts.KeyCode == ConvertKeys.MouseShiftLeft && ts.keyClickType == KeyClickType.点击 && sleep>0)
            {
                var objdm = this.CreateDM();
                var action = () =>
                {
                    objdm.KeyDown(this.d3Param.KeyCodes.KeyStand);
                    objdm.LeftClick();
                    objdm.KeyUp(this.d3Param.KeyCodes.KeyStand);
                };
                StartTaskList.Add(StartNewForTask(action, sleep));
                AddStopTaskKeysUpStand();
            }
        }

        public void AddLeftClickForTask(KeyTimeSetting ts)
        {
            if (ts.Rank == 0 && ts.D1 > 0 && ts.keyClickType == KeyClickType.点击)
            {
                var objdm = this.CreateDM();
                var action = () =>
                {
                    objdm.LeftClick();
                };
                StartTaskList.Add(StartNewForTask(action, ts.D1));
            }
        }
        public void AddRightClickForTask(KeyTimeSetting ts)
        {
            if (ts.Rank == 0 && ts.D1 > 0 && ts.keyClickType == KeyClickType.点击)
            {
                var objdm = this.CreateDM();
                var action = () =>
                {
                    objdm.RightClick();
                };
                StartTaskList.Add(StartNewForTask(action, ts.D1));
            }
        }
      
    }
}
