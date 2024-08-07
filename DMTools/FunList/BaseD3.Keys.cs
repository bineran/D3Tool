﻿using DMTools.libs;
using DMTools.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using NLog.Fluent;
using static System.Windows.Forms.DataFormats;
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
        public void AddKeyDownForTask( KeyTimeSetting ks, int sleepTime=100)
        {
            Keys keys = ks.KeyCode;
            //var objdm = this.CreateDM();
            int key = (int)keys;
            this.d3KeyState.SetState(keys, true);
            objdm.KeyDown (key);

            var action = () =>
            {

                if (this.d3KeyState.isPause)
                {
                    if (this.d3KeyState[key])
                    {
                        //log.Debug($"AddKeyDownForTask   {key}---KeyUp1");
                        objdm.KeyUp(key);
                    }
                    Sleep(20);
                    return;
                }
                if ((int)keys == this.d3Param.KeyCodes.KeyMove)
                {
                    if (this.d3KeyState[this.d3Param.KeyCodes.Key1] || this.d3KeyState[this.d3Param.KeyCodes.Key2] || this.d3KeyState[this.d3Param.KeyCodes.Key3] || this.d3KeyState[this.d3Param.KeyCodes.Key4])
                    {

                        if (this.d3KeyState[this.d3Param.KeyCodes.KeyMove])
                        {
                            //log.Debug($"AddKeyDownForTask   {key}---KeyUp2");
                            objdm.KeyUp(key);
                        }
                        Sleep(20);
                        return;
                    }
                }
                if(ks.KeyCode2>0)
                {
                    if (objdm.GetKeyState(Convert.ToInt32( ks.KeyCode2)) == 1)
                    {
                        if (this.d3KeyState[keys])
                        {
                            objdm.KeyUp(key);
                        }
                        Sleep(20);
                        return;
                    }
                }
                //return;
                if (!this.d3KeyState[keys])
                {
                    //log.Debug($"AddKeyDownForTask    {key}--- KeyDown");
                    objdm.KeyDown(key);
                }

            };
            var    sleepTime2 = ks.D2;
            if (ks.D2 > 0 && ks.D1>0)
            {
                sleepTime = ks.D1;
                action = () =>
                {

                    objdm.KeyDown(key);
                    Sleep(sleepTime2);
                    objdm.KeyUp(key);
                };
            }
            var checkhandle = true;
            if (this.d3Param.sysConfig.keypad!= "normal")
            {
                checkhandle= false; 


            }

            StartTaskList.Add(StartNewForTask(action, sleepTime, false, checkhandle));
            AddStopTaskKeysUp(key);

        }
        public void AddKeyPressForTask(KeyTimeSetting ts)
        {
        
            //正常先按再点击
            if (ts.Rank == 0 && ts.D1 > 0 && ts.keyClickType == KeyClickType.点击)
            {
         
                int key = (int)ts.KeyCode;
                //var objdm=CreateDM();
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
                var tmpd = ts.D1;
                if ( ts.D2>0 && ts.D2<100 && ( ts.KeyCode == Keys.D1 || ts.KeyCode == Keys.D2 || ts.KeyCode == Keys.D3 || ts.KeyCode == Keys.D4 || ts.KeyCode == Keys.D5))
                {
                    tmpd = Convert.ToInt32(ts.D1 * (1.0 + ts.D2 * 1.0 / 100));
                }
                var tmpd2 = ts.D3;
                if (ts.D2 > 0 && ts.D2 < 100 && (ts.KeyCode2 == Keys.D1 || ts.KeyCode2 == Keys.D2 || ts.KeyCode2 == Keys.D3 || ts.KeyCode2 == Keys.D4 || ts.KeyCode2 == Keys.D5))
                {
                    tmpd2 = Convert.ToInt32(ts.D3 * (1.0 + ts.D2 * 1.0 / 100));
                }
                var checkhandle = true;
                if (this.d3Param.sysConfig.keypad != "normal")
                {
                    checkhandle = false;
                }
                //交替按键
                if ((ts.KeyCode == Keys.D1 || ts.KeyCode == Keys.D2 || ts.KeyCode == Keys.D3 || ts.KeyCode == Keys.D4 || ts.KeyCode == Keys.D5) &&
                    (ts.KeyCode2 == Keys.D1 || ts.KeyCode2 == Keys.D2 || ts.KeyCode2 == Keys.D3 || ts.KeyCode2 == Keys.D4 || ts.KeyCode2 == Keys.D5)
                    && ts.KeyCode!=ts.KeyCode2 && ts.D3>0 
                    )
                {
                    action = () =>
                    {
                        objdm.KeyPress(key);
                        Sleep(tmpd);
                        objdm.KeyPress(Convert.ToInt32(ts.KeyCode2));
                        Sleep(tmpd2);
                    };
                    StartTaskList.Add(StartNewForTask(action, 1, true, checkhandle));
                }
                else
                {
                    StartTaskList.Add(StartNewForTask(action, tmpd,true, checkhandle));
                }
                
            }
            //先休眠正常先按再点击
            if (ts.Rank == 0 && ts.D1 <0 && ts.keyClickType == KeyClickType.点击)
            {

                int key = (int)ts.KeyCode;
                //var objdm=CreateDM();
                var isfirst = true;
                var action = () =>
                {
                 
                    if (isfirst = true)
                    {
                        SleepBefore(ts.D1);
                        isfirst = false;
                    }
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
                var tmpd = System.Math.Abs(ts.D1);
                if (ts.D2 > 0 && ts.D2 < 100 && (ts.KeyCode == Keys.D1 || ts.KeyCode == Keys.D2 || ts.KeyCode == Keys.D3 || ts.KeyCode == Keys.D4 || ts.KeyCode == Keys.D5))
                {
                    tmpd = System.Math.Abs(Convert.ToInt32(ts.D1 * (1.0 + ts.D2 * 1.0 / 100)));
                }
                var tmpd2 = ts.D3;
                if (ts.D2 > 0 && ts.D2 < 100 && (ts.KeyCode2 == Keys.D1 || ts.KeyCode2 == Keys.D2 || ts.KeyCode2 == Keys.D3 || ts.KeyCode2 == Keys.D4 || ts.KeyCode2 == Keys.D5))
                {
                    tmpd2 = Convert.ToInt32(ts.D3 * (1.0 + ts.D2 * 1.0 / 100));
                }
                var checkhandle = true;
                if (this.d3Param.sysConfig.keypad != "normal")
                {
                    checkhandle = false;


                }
                //交替按键
                if ((ts.KeyCode == Keys.D1 || ts.KeyCode == Keys.D2 || ts.KeyCode == Keys.D3 || ts.KeyCode == Keys.D4 || ts.KeyCode == Keys.D5) &&
                    (ts.KeyCode2 == Keys.D1 || ts.KeyCode2 == Keys.D2 || ts.KeyCode2 == Keys.D3 || ts.KeyCode2 == Keys.D4 || ts.KeyCode2 == Keys.D5)
                    && ts.KeyCode != ts.KeyCode2 && ts.D3 > 0
                    )
                {
                    action = () =>
                    {
                        objdm.KeyPress(key);
                        Sleep(tmpd);
                        objdm.KeyPress(Convert.ToInt32(ts.KeyCode2));
                        Sleep(tmpd2);
                    };
                    StartTaskList.Add(StartNewForTask(action, 1,true, checkhandle));
                }
                else
                {
                    StartTaskList.Add(StartNewForTask(action, tmpd,true, checkhandle));
                }

            }
        }
        public void AddPauseKeyPressForTask(KeyTimeSetting ts)
        {

            if (ts.Rank == 0 && ts.D1 > 0 && ts.keyClickType == KeyClickType.点击)
            {

                int key = (int)ts.KeyCode;
                //var objdm=CreateDM();
                var action = () =>
                {
                    if (this.d3KeyState.isPause)
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
                    }
                };
                StartTaskList.Add(StartNewForTask(action, ts.D1,false));
            }
        }
        public void AddLeftDownForTask(KeyTimeSetting ts, int sleep = 100)
        {
          
            if (ts.Rank == 0  && ts.KeyCode== ConvertKeys.MouseLeft && ts.keyClickType == KeyClickType.按下)
            {
                //var objdm = CreateDM();
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
                    if (this.d3KeyState.isRight)
                    {
                        if (this.d3KeyState.isLeft)
                        {
                            objdm.LeftUp();
                   
                        }
                        Sleep(30);
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
                //var objdm = CreateDM();
                objdm.RightDown();
           
                var action = () =>
                {
                    if (this.d3KeyState.isPause || !this.d3KeyState.isD3)
                    {
                        objdm.RightUp();
                        Sleep(sleep);
                        return;
                    }
                    if (this.d3KeyState.isLeft)
                    {
                        if (this.d3KeyState.isRight)
                        {
                            objdm.RightUp();
                        }
                        Sleep(30);
                        return;
                    }
                    if (ts.KeyCode2 > 0)
                    {
                        if (objdm.GetKeyState(Convert.ToInt32(ts.KeyCode2)) == 1)
                        {
                            if (this.d3KeyState.isRight)
                            {
                                objdm.RightUp();
                            }
                            Sleep(20);
                            return;
                        }
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
                //var objdm = CreateDM();
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
        public void AddShiftRightDownForTask(KeyTimeSetting ts, int sleep = 100)
        {

            if (ts.Rank == 0 && ts.KeyCode == ConvertKeys.MouseShiftRight && ts.keyClickType == KeyClickType.按下 && sleep > 0)
            {
                //var objdm = CreateDM();
                objdm.KeyDown(this.d3Param.KeyCodes.KeyStand);
                objdm.RightDown();
                var action = () =>
                {
                    if (this.d3KeyState.isPause || !this.d3KeyState.isD3)
                    {
                        objdm.KeyUp(this.d3Param.KeyCodes.KeyStand);
                        objdm.RightUp();
                        Sleep(sleep);
                        return;
                    }
                    if (!this.d3KeyState[this.d3Param.KeyCodes.KeyStand])
                    {
                        objdm.KeyDown(this.d3Param.KeyCodes.KeyStand);
                    }
                    if (!this.d3KeyState.isRight)
                    {
                        objdm.RightDown();
                    }
                };
                StartTaskList.Add(StartNewForTask(action, sleep, false, false));
                AddStopTaskRightUp();
                AddStopTaskKeysUpStand();
            }
        }
        public void AddShiftLeftClickForTask(KeyTimeSetting ts, int sleep = 100)
        {
         
            if (ts.Rank == 0 && ts.KeyCode == ConvertKeys.MouseShiftLeft && ts.keyClickType == KeyClickType.点击 && sleep>0)
            {
                //var objdm = this.CreateDM();
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
                //var objdm = this.CreateDM();
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
                //var objdm = this.CreateDM();
                var action = () =>
                {
                    objdm.RightClick();
                };
                StartTaskList.Add(StartNewForTask(action, ts.D1));
            }
        }
        public void AddWhereRightDownShiftForTask(KeyTimeSetting ts, int sleep = 100)
        {

            if (ts.Rank == 0 && ts.KeyCode == ConvertKeys.HotKeyRightWhereShiftDown && ts.keyClickType == KeyClickType.按下 && sleep > 0)
            {
                var keycode = 0;
                if (ts.Str1 != null && ts.Str1.Length == 1)
                {
                    keycode = Convert.ToInt32(ts.Str1.ToUpper()[0]);
                }
                else if (ts.Int1 > 0)
                {
                    keycode = ts.Int1;
                }
                else
                    return;
                //var objdm = this.CreateDM();
                var action = () =>
                {
                    if (this.d3Param.d3KeyState.isRight)
                    {
                        if (!this.d3Param.d3KeyState[keycode])
                        {
                            objdm.KeyDown(keycode);
                        }
                    }
                    else
                    {
                        if (this.d3Param.d3KeyState[keycode])
                        {
                           objdm.KeyUp(keycode);
                        }

                    }
                };
                StartTaskList.Add(StartNewForTask(action, sleep));
                AddStopTaskKeysUp(keycode);
            }
        }

        public void AddWhereLeftDownShiftForTask(KeyTimeSetting ts, int sleep = 100)
        {

            if (ts.Rank == 0 && ts.KeyCode == ConvertKeys.HotKeyLeftWhereShiftDown && ts.keyClickType == KeyClickType.按下 && sleep > 0)
            {
                var keycode = 0;
                if (ts.Str1 != null && ts.Str1.Length == 1)
                {
                    keycode = Convert.ToInt32(ts.Str1.ToUpper()[0]);
                }
                else if (ts.Int1 > 0)
                {
                    keycode = ts.Int1;
                }
                else
                    return;
                var action = () =>
                {
                    if (this.d3Param.d3KeyState.isLeft)
                    {
                        if (!this.d3Param.d3KeyState[keycode])
                        {
                            objdm.KeyDown(keycode);
                        }

                    }
                    else
                    {
                        if (this.d3Param.d3KeyState[keycode])
                        {
                            objdm.KeyUp(keycode);
                        }
                    }
                };
                StartTaskList.Add(StartNewForTask(action, sleep));

                AddStopTaskKeysUp(keycode);
            }
        }

    }
}
