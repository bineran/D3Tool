using DMTools.libs;
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

        public static bool NoMouseKey(Keys k)
        {
            if (k == ConvertKeys.MouseLeft)
                return false;
            else if (k == ConvertKeys.MouseRight)
                return false;
            else if (k == ConvertKeys.MouseShiftLeft)
                return false;
            return true;
        }
        /// <summary>
        /// 添加一直接住的键的TASK
        /// </summary>
        /// <param name="action"></param>
        public void AddKeyDownForTask(Keys keys, int sleep = 100)
        {
            var objdm = CreateAndBindDm();
            int key = (int)keys;
            objdm.KeyDown(key);
            //DateTime tmp = DateTime.Now.AddSeconds(2);
            var action = () =>
            {
                if(this.d3KeyState.isPause || !this.d3KeyState.isD3)
                {
                    objdm.KeyUp(key);
                    Sleep(sleep);
                    return;
                }
              
                //if (tmp < DateTime.Now)
                //{
                //   // objdm.KeyDown(key);
                //    tmp = DateTime.Now.AddSeconds(2);
                //}
                if (!this.d3KeyState[keys])
                {
                    objdm.KeyDown(key);
                }
            };
            StartTaskList.Add(StartNewForTask(action, sleep,false,false));
            AddStopTaskKeysUp(objdm,key);
        }
        public void AddKeyPressForTask(KeyTimeSetting ts)
        {
        
            if (ts.Rank == 0 && ts.D1 > 0 && ts.keyClickType == KeyClickType.点击)
            {
                var  objdm = CreateAndBindDm();
                int key = (int)ts.KeyCode;
                var action = () =>
                {
                    objdm.KeyPress(key);
                };
                StartTaskList.Add(StartNewForTask(action, ts.D1));
            }
        }

        public void AddLeftDownForTask(KeyTimeSetting ts, int sleep = 100)
        {
          
            if (ts.Rank == 0  && ts.KeyCode== ConvertKeys.MouseLeft && ts.keyClickType == KeyClickType.按下)
            {
                var objdm = CreateAndBindDm();
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
                AddStopTaskLeftUp(objdm);
            }
        }
        public void AddRightDownForTask(KeyTimeSetting ts, int sleep = 100)
        {
         
            if (ts.Rank == 0 && ts.KeyCode == ConvertKeys.MouseRight && ts.keyClickType == KeyClickType.按下)
            {
                var objdm = CreateAndBindDm();
                //DateTime tmp = DateTime.Now.AddSeconds(2);
                objdm.RightDown();
                var action = () =>
                {
                    if (this.d3KeyState.isPause || !this.d3KeyState.isD3)
                    {
                        objdm.RightUp();
                        Sleep(sleep);
                        return;
                    }
                    //if (tmp < DateTime.Now)
                    //{
                    //    objdm.RightDown();
                    //    tmp = DateTime.Now.AddSeconds(2);
                    //}
                    if (!this.d3KeyState.isRight)
                    {
                        objdm.RightDown();
                    }
                };
                StartTaskList.Add(StartNewForTask(action, sleep, false, false));
                AddStopTaskRightUp(objdm);
            }
        }

        public void AddShiftLeftDownForTask(KeyTimeSetting ts, int sleep = 100)
        {
          
            if (ts.Rank == 0 && ts.KeyCode == ConvertKeys.MouseShiftLeft && ts.keyClickType == KeyClickType.按下)
            {
                var objdm = CreateAndBindDm();
                // DateTime tmp = DateTime.Now.AddSeconds(2);
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
                    //if (tmp < DateTime.Now)
                    //{
                    //    objdm.KeyDown(this.d3Param.KeyCodes.KeyStand);
                    //    objdm.LeftDown();
                    //    tmp = DateTime.Now.AddSeconds(2);
                    //}
                    if (!this.d3KeyState.isLeft)
                    {
                        objdm.LeftDown();
                    }
                };
                StartTaskList.Add(StartNewForTask(action, sleep, false, false));
                AddStopTaskLeftUp(objdm);
                AddStopTaskKeysUpStand(objdm);
            }
        }
        public void AddShiftLeftClickForTask(KeyTimeSetting ts, int sleep = 100)
        {
         
            if (ts.Rank == 0 && ts.KeyCode == ConvertKeys.MouseShiftLeft && ts.keyClickType == KeyClickType.点击)
            {
                var objdm = CreateAndBindDm();
                var action = () =>
                {
                    objdm.KeyDown(this.d3Param.KeyCodes.KeyStand);
                    objdm.LeftClick();
                    objdm.KeyUp(this.d3Param.KeyCodes.KeyStand);
                };
                StartTaskList.Add(StartNewForTask(action, sleep));
                AddStopTaskKeysUpStand(objdm);
            }
        }

        public void AddLeftClickForTask(KeyTimeSetting ts)
        {
            var objdm = CreateAndBindDm();
            if (ts.Rank == 0 && ts.D1 > 0 && ts.keyClickType == KeyClickType.点击)
            {
                var action = () =>
                {
                    objdm.LeftClick();
                };
                StartTaskList.Add(StartNewForTask(action, ts.D1));
            }
        }
        public void AddRightClickForTask(KeyTimeSetting ts)
        {
            var objdm = CreateAndBindDm();
            if (ts.Rank == 0 && ts.D1 > 0 && ts.keyClickType == KeyClickType.点击)
            {
                var action = () =>
                {
                    objdm.RightClick();
                };
                StartTaskList.Add(StartNewForTask(action, ts.D1));
            }
        }
        public void KeyPress (KeyTimeSetting ts,Idmsoft objdm)
        {
            objdm.KeyPress((int)ts.KeyCode);
            Sleep(ts.D1);
        }
        public void KeyDown(KeyTimeSetting ts, Idmsoft objdm)
        {
            objdm.KeyDown((int)ts.KeyCode);
            Sleep(ts.D1);
        }
        public void KeyUp(KeyTimeSetting ts, Idmsoft objdm)
        {
            objdm.KeyUp((int)ts.KeyCode);
            Sleep(ts.D1);
        }
    }
}
