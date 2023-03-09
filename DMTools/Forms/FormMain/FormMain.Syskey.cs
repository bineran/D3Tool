using DMTools.Config;
using DMTools.FunList;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMTools
{
    public partial class FormMain
    {
        public bool ProcessKey(Keys key)
        {
            try
            {
                if (d3Config == null)
                {
                    return false;
                }
 
                if (!d3Config.ALLHotKeys.Contains(key))
                    return false;
                ReplayProcessKey(key);
                if ((DateTime.Now - D_Time).TotalSeconds < 0.2)
                {
                    return false;
                }
                D_Time = DateTime.Now;
                var ck = d3Config.ConfigKeys.Where(r => UserKey.HotKeys.Contains(r.KeyName) && r.KeyCode == key).FirstOrDefault();
                if (ck != null)
                {
                    ProcessSysKey(ck);

                    return true;
                }

                ProcessFunKey(d3Config, key);
                return true;
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            return false;
        }


        void ProcessFunKey(D3Config d3Config, Keys keys)
        {
            try
            {
                if (d3Config.WindowClass == null)
                    return;

                var items = HDINFO();
                var hd = items.Item1;
                var isbl = items.Item2;
                var strClass = items.Item3;
                if (!d3Config.WindowClass.ToLower().Contains(strClass.ToLower()))
                {
                    return;
                }

                if (!isbl)
                {
                    RestD3Main(d3Config, hd);
                }
                if (sld3.ContainsKey(hd))
                    sld3[hd].ProcessKeys(keys);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        void ProcessSysKey(D3ConfigKey keys)
        {
            switch (keys.KeyName)
            {
                case UserKey.HotKeyMouse:
                    SetMouseInfo();
                    break;
                case UserKey.HotKeyStopAll:
                    StopAll();
                    break;
                case UserKey.ReplayKey:
                    Replay();
                    break;
            }
        }


        public void SetMouseInfo()
        {

            var items = HDINFO();
            var hd = items.Item1;
            var isbl = items.Item2;
            var strClass = items.Item3;


            object x;
            object y;
            objdm.GetCursorPos(out x, out y);
            var color = objdm.GetColor(Convert.ToInt32(x), Convert.ToInt32(y));
            if (!this.d3Config.WindowClass.ToLower().Contains(strClass.ToLower()))
            {
                if (this.d3Config.WindowClass != null && this.d3Config.WindowClass.Length > 0)
                {
                    this.d3Config.WindowClass += ",";
                }
                this.d3Config.WindowClass += strClass;
                SaveConfig();
            }
            var r = Convert.ToInt32(color.Substring(0, 2), 16);
            var g = Convert.ToInt32(color.Substring(2, 2), 16);
            var b = Convert.ToInt32(color.Substring(4, 2), 16);
            var objinfo = new
            {
                x = x.ToString(),
                y = y.ToString(),
                color = new
                {
                    color,
                    r,
                    g,
                    b
                },
                Handle = hd,
                windowClass = strClass
            };
            log.Info(objinfo.ToJson());
            if (this.tbfun.TabPages.Count > 1)
            {
                var userFun = this.tbfun.TabPages[0].Controls[0] as UserFun;
                if (userFun != null)
                {
                    userFun.AddXYColor((int)x, (int)y, color, strClass);
                }
            }

        }
        public void StopAll()
        {


            int hd;
            bool isbl;
            string strClass;
            var items = HDINFO();
            hd = items.Item1;
            isbl = items.Item2;
            strClass = items.Item3;
            if (isbl)
            {
                sld3[hd].StopAll();
            }

        }

        public bool ReplayFlag { get; set; } = false;
        public List<D3TimeSetting> ReplayList { get; set; }
        public DateTime ReplayLastTime{ get; set; }
        public void Replay()
        {
            ReplayFlag = !ReplayFlag;
            if (ReplayFlag)
            {
                ReplayLastTime = DateTime.Now;
                ReplayList = new List<D3TimeSetting>();

            }
            else
            {
                if (this.d3Config != null && ReplayList.Count>0)
                {
                    this.d3Config.DebugTimes.AddRange(ReplayList.ToArray());
                }
            }
        }
        public void ReplayMouseEventArgs(MouseEventArgs e)
        {
            if (!ReplayFlag)
            {
                return;
            }
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                ReplayProcessKey(BaseD3.MouseRight);
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                ReplayProcessKey(BaseD3.MouseLeft);
            }

          
        }
        public void ReplayProcessKey(Keys key)
        {
            if (!ReplayFlag)
            {
                return;
            }
            DateTime dateTime = ReplayLastTime;
            D3TimeSetting ds = new D3TimeSetting();
            ds.keyClickType = KeyClickType.点击;
            ReplayLastTime = DateTime.Now;
            var d1 = Convert.ToInt32((DateTime.Now - dateTime).TotalMilliseconds);

            object x;
            object y;
            objdm.GetCursorPos(out x, out y);
            ds.Int1 = Convert.ToInt32(x); ds.Int2 = Convert.ToInt32(y);
            var color = objdm.GetColor(ds.Int1, ds.Int2);
            ds.Str1 = color;

            ds.D1 = d1;
            ds.KeyCode = key;
            ds.Str4 = $"new PointCheckColor({x},{y}, \"{color}\")";
            ReplayList.Add(ds);
        }
    }
}
