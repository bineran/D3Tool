using DMTools.Config;
using DMTools.FunList;
using DMTools.libs;
using DMTools.Static;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Idmsoft = DMTools.libs.Idmsoft;
namespace DMTools
{
    public partial class FormMain
    {
        public SortedList<Keys, bool> slKeys = new SortedList<Keys, bool>();
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

                if (!slKeys.ContainsKey(key))
                {
                    slKeys.Add(key, false);
                }
                if (slKeys[key] == true)
                {
                    return false;
                }
                slKeys[key] = true;



                log.Info(key);
                ReplayProcessKey(key);
                if ((DateTime.Now - D_Time).TotalSeconds < 0.05)
                {
                    return false;
                }
                D_Time = DateTime.Now;
                var ck = d3Config.ConfigKeys.Where(r => Hotkey.HotKeys.Contains(r.KeyName) && r.KeyCode == key).FirstOrDefault();
                if (ck != null)
                {
                    ProcessSysKey(ck);
                    slKeys[key] = false;
                    return true;
                }

                ProcessFunKey(d3Config, key);
                slKeys[key] = false;
                return true;
            }
            catch (Exception ex)
            {
                slKeys[key] = false;
                log.Error(ex);
            }
            return false;
        }

        /// <summary>
        /// 最后一次的Handle
        /// </summary>
        public static int LastHandle { get; private set; }
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
                case Hotkey.HotKeyMouse:
                    SetMouseInfo();
                    break;
                case Hotkey.HotKeyStopAll:
                    StopAll();
                    break;
                case Hotkey.HotKeyReplay:
                    Replay();
                    break;
                case Hotkey.HotKeyPtrScr:
                    PrScrnHelper.PrScrn();
                    break;
                case Hotkey.HotKeyTestBind:
                    TestBind();
                    break;
            }
        }
        public void TestBind()
        {
            List<string> listDisplay = new List<string>();
            listDisplay.Add("normal");
            listDisplay.Add("gdi");
            listDisplay.Add("gdi2");
            listDisplay.Add("dx2");
            listDisplay.Add("dx3");
            listDisplay.Add("dx");


            List<string> listMouse = new List<string>();
            listMouse.Add("normal");
            listMouse.Add("windows");
            listMouse.Add("windows2");
            listMouse.Add("windows3");
            listMouse.Add("dx");
            listMouse.Add("dx2");
            List<string> listKey = new List<string>();
            listKey.Add("normal");
            listKey.Add("windows");
            listKey.Add("dx");
            var hd = objdm.GetMousePointWindow();
            log.Debug("BindWindow Begin=======");
            List<string> list = new List<string>();
            List<string> listd = new List<string>();
            List<string> listm = new List<string>();
            List<string> listk = new List<string>();

            foreach (var d in listDisplay)
            {
                foreach (var k in listKey)
                {
                    foreach (var m in listMouse)
                    {
                        DMP dMP = new DMP();
                        var ret = dMP.DM.BindWindow(hd, d, m, k, 0);
                     
                        if (ret == 1)
                        {
                            if (!listd.Contains(d))
                            {
                                listd.Add(d);
                            }
                            if (!listm.Contains(m))
                            {
                                listm.Add(m);
                            }
                            if (!listk.Contains(k))
                            {
                                listk.Add(k);
                            }

                           // var str = $"显示：{d.PadRight(8)}、鼠标：{m.PadRight(8)}、键盘：{k.PadRight(8)}";
                           // list.Add(str);
                            dMP.DM.UnBindWindow();
                        }
                      
                    }
                }
            }
            list.Add("大漠绑定支持的模式");
            list.Add($"显示:  " + string.Join("、", listd.ToArray()));
            list.Add($"鼠标:  " + string.Join("、", listm.ToArray()));
            list.Add($"键盘:  " + string.Join("、", listk.ToArray()));
            list.Add("性能从高到低");
            if (this.tbfun.TabPages.Count > 1)
            {
                foreach(TabPage tb in this.tbfun.TabPages)
                {
                    var userKey = tb.Controls[0] as UserKey;
                    if (userKey != null)
                    {
                        userKey.SetDebugText(string.Join("\r\n", list));
                    }
                }
               
            }
        
            //log.Debug("BindWindow End=======");
        }
        public  Idmsoft BindForm(Idmsoft objdm, int handle, SysConfig sysConfig)
        {
            try
            {
                //if (display == "normal" && display == "normal" && display == "normal" && mode == 0)
                //{
                //    return;
                //}
                if (objdm.IsBind(handle) == 1)
                {
                    objdm.UnBindWindow();
                }
                objdm.delay(50);

                var c = objdm.BindWindow(handle, sysConfig.display, sysConfig.mouse, sysConfig.keypad, sysConfig.mode);


            }
            catch (Exception e)
            {

            }
            return objdm;

        }
        public void SetMouseInfo()
        {

            var items = HDINFO();
            var hd = items.Item1;
            var isbl = items.Item2;
            var strClass = items.Item3;
            this.BindForm(objdm, hd,new SysConfig());

            var (tsPointColor,x,y) = BaseD3.GetKTSPointColor(objdm);

            objdm.MoveTo(0, 0);
            Task.Delay(300).Wait();
            var tsPointColor2 = BaseD3.GetPointColor(objdm, x, y);
            objdm.MoveTo(x, y);
            Task.Delay(100).Wait();
            if (!this.d3Config.WindowClass.ToLower().Contains(strClass.ToLower()))
            {
                if (this.d3Config.WindowClass != null && this.d3Config.WindowClass.Length > 0)
                {
                    this.d3Config.WindowClass += ",";
                }
                this.d3Config.WindowClass += strClass;
                SaveConfig();
            }
            KeyTimeSetting tsImage= new KeyTimeSetting(); 
            foreach (TabPage tp in this.tbfun.TabPages)
            {
                var uk = tp.Controls[0] as UserKey;
                if (uk != null)
                {
                   // uk.d3Config.DebugTimes.Add(tsPointColor);
                    tsImage = BaseD3.SavePointImage(this.objdm, uk.d3Config.sysConfig);
                   // uk.d3Config.DebugTimes.Add(tsImage);
                   // uk.RestDebugDataGridView();
                }
            }
            if (this.tbfun.SelectedTab != null)
            {
                var uf = this.tbfun.SelectedTab.Controls[0] as UserFun;
                if(uf != null )
                {
                    uf.selectKTSList.Add(tsPointColor);
                    if (tsPointColor.Str1 != tsPointColor2.Str1)
                    {
                        uf.selectKTSList.Add(tsPointColor2);
                    }
                    
                    uf.selectKTSList.Add(tsImage);
                    uf.RestDebugDataGridView();
                }
            }
            if (objdm.IsBind(hd) == 1)
            {
                objdm.UnBindWindow();
            }
        }
        public void RestDebugDataGridBind()
        {
           
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
        public List<KeyTimeSetting> ReplayList { get; set; }
        public DateTime ReplayLastTime{ get; set; }
        public void Replay()
        {
            ReplayFlag = !ReplayFlag;
            if (ReplayFlag)
            {
                ReplayLastTime = DateTime.Now;
                ReplayList = new List<KeyTimeSetting>();

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
                ReplayProcessKey(ConvertKeys.MouseRight);
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                ReplayProcessKey(ConvertKeys.MouseLeft);
            }

          
        }
        public void ReplayProcessKey(Keys key)
        {
            if (!ReplayFlag)
            {
                return;
            }
            DateTime dateTime = ReplayLastTime;
            KeyTimeSetting ds = new KeyTimeSetting();
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
