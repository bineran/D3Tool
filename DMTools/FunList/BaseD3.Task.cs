using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// <summary>
        /// 执行一个TASK并将他加入到StartTaskList
        /// </summary>
        /// <param name="action"></param>
        public void AddStartTask(Action action)
        {
            StartTaskList.Add(StartNewTask(action));
        }
        public   void Sleep(int Sleep)
        {
             Task.Delay(Sleep).Wait(cs.Token);
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


        private Task CreateTask(Action action)
        {
            return new Task(() =>
            {
                try
                { action(); }
                catch (Exception ex) { log.Error(ex); }
            });

        }


    }
}
