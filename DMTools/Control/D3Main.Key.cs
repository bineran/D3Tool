﻿using DMTools.Config;
using DMTools.FunList;
using DMTools.libs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMTools.Control
{
    public partial class D3Main
    {
        public Keys StopAllKey { get; set; }
        public Keys MouseKey { get; set; }
        CancellationTokenSource cs = new CancellationTokenSource();
        public List<Task> TaskBackList { get; set; }=new List<Task>();
        object loackobj = new object();
 
        /// <summary>
        /// 开启后台监控按钮状态
        /// </summary>
        public void StartBackgroundTask()
        {
           List<int> alkey = new List<int>();
            alkey.Add(this.d3KeySetting.KeyPause);
            alkey.Add(this.d3KeySetting.KeyStand);
            alkey.Add(this.d3KeySetting.KeyMove);
            alkey.AddRange(new[] { this.d3KeySetting.Key1, this.d3KeySetting.Key2, this.d3KeySetting.Key3, this.d3KeySetting.Key4 });


            var funlist=this.FunList.Where(r => r.EnabledFlag);
            foreach (var fun in funlist)
            {
                foreach (var times in fun.d3Param.SLTimes.Values)
                {
                    var ts = times.Where(r => r.keyClickType == KeyClickType.按下
                     && r.KeyCode > 0 && ConvertKeys.NoMouseKey(r.KeyCode));
                    foreach (var t in ts)
                    {
                        if (!alkey.Contains((int)t.KeyCode))
                        {
                            alkey.Add((int)t.KeyCode);
                        }
                    }
                }
            }

           

            cs.Cancel();
            if (TaskBackList.Count > 0)
            {
                Task.WaitAll(TaskBackList.ToArray());
            }
          
            TaskBackList = new List<Task>();
            cs =new CancellationTokenSource();

            //alkey.Clear();
            foreach (var key in alkey)
            {

                TaskBackList.Add(StartTask(() =>
                {
                    try
                    {
                        var objdm = DMP.CreateDM();
                        while (true)
                        {


                            if (key == this.d3KeySetting.KeyPause)
                            {
                                this.d3KeyState.SetPauseState((Keys)key, objdm.GetKeyState(key) == 1);
                            }
                            else
                            {
                                this.d3KeyState.SetState((Keys)key, objdm.GetKeyState(key) == 1);
                            }


                            Task.Delay(50).Wait(cs.Token);
                        }
                    }
                    catch
                    {
                        //不记录 取消的日志
                    }
                }));
            }
            TaskBackList.Add(StartTask(() =>
            {
                try
                {
                    var objdm = DMP.CreateDM();
                    while (true)
                    {
                        if (cs.IsCancellationRequested)
                        { break; }
                        var hd = objdm.GetMousePointWindow();
                        if (hd != this.handle)
                        {
                            this.d3KeyState.isD3 = false;
                        }
                        else
                        {
                            this.d3KeyState.isD3 = true;
                        }

                        Task.Delay(50).Wait(cs.Token);
                    }
                }
                catch
                {
                }
            }));

           

        }
        public Task StartTask(Action action)
        {
            Task task= new Task(action, this.cs.Token);
            task.Start();
            return task;
    }
    }

}
