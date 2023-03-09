using DMTools.Config;
using DMTools.FunList;
using DMTools.Static;
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
        public void ProcessSysKey(D3ConfigKey keys)
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


            int x;
            int y;
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
            if (!ReplayFlag)
            {
                var ss = ReplayList.ToJson();
            }
            else {
                ReplayLastTime = DateTime.Now;
                ReplayList=new List<D3TimeSetting>();
            }
        }
        public void ReplayMouseEventArgs(MouseEventArgs e)
        {
            if (!ReplayFlag)
            {
                return;
            }
            DateTime dateTime = ReplayLastTime;
             D3TimeSetting ds= new D3TimeSetting();
            ds.keyClickType = KeyClickType.点击;
            ds.Int1=e.X; ds.Int2=e.Y;
            ReplayLastTime = DateTime.Now;
            var d1 = Convert.ToInt32((DateTime.Now - dateTime).TotalMilliseconds);
            if (d1 == 0)
            { 
                
            }
            ds.D1 = d1;
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                ds.KeyCode = BaseD3.MouseRight;
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                ds.KeyCode = BaseD3.MouseLeft;
            }
            ReplayList.Add(ds);
        }
        public void ReplayKeyEventArgs(Keys key)
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
            if (d1 == 0)
            {

            }
            ds.D1 = d1;
            ds.KeyCode = key;
            ReplayList.Add(ds);
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Hotkey.UnRegist(this.Handle);
        }

        public void RegistHotKey()
        {
            //Hotkey.Regist(this.Handle, HotkeyModifiers.MOD_ALT, Keys.F1, ScreenCapture);
        }
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            Hotkey.ProcessHotKey(m);
        }
    }
}
