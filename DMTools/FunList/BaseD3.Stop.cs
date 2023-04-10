using DMTools.Config;
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
        /// <summary>
        /// 执行一个TASK并将他加入到StartTaskList
        /// </summary>
        /// <param name="action"></param>
        public void AddStopTask(Action action)
        {
            StopTaskList.Add(CreateTaskNoCS(action));
        }
        public void AddStopTaskKeysUp(Keys key)
        {
          
            AddStopTaskKeysUp((int)key);

        }
        public void AddStopTaskKeysUp(int key)
        {
            
            StopTaskList.Add(CreateTaskNoCS(() => {
                //var objdm = CreateDM();
                objdm.KeyUp(key);
            }));
        }
        public void AddStopTaskKeysUpStand()
        {
            AddStopTaskKeysUp(this.d3Param.KeyCodes.KeyStand);

        }

        public void AddStopTaskLeftUp()
        {
         
            StopTaskList.Add(CreateTaskNoCS(() => {
                var objdm = CreateDM();
                objdm.LeftUp();
            }));
        }
        public void AddStopTaskRightUp()
        {
          
            StopTaskList.Add(CreateTaskNoCS(() => {
                var objdm = CreateDM();
                objdm.RightUp(); }));
        }


    }
}
