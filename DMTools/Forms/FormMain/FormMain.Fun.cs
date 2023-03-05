using DMTools.Config;
using DMTools.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace DMTools
{
    public partial class FormMain
    {
        public object lockObject { get; set; } = new object();
        Dm.dmsoft objdm = new Dm.dmsoft();
        public SortedList<int, D3Main> sld3 = new SortedList<int, D3Main>();
        public List<Keys> GetALLHotKey(D3Config d3Config)
        {
            List<Keys> keys = new List<Keys>();
            foreach (var item in d3Config.d3ConfigItems)
            {
                if (!item.EnabledFlag) { continue; }
                if (!keys.Contains(item.HotKey1))
                    keys.Add(item.HotKey1);
                if (!keys.Contains(item.HotKey2))
                    keys.Add(item.HotKey2);

            }
            var cks = d3Config.ConfigKeys.Where(r => UserKey.HotKeys.Contains(r.KeyName));
            foreach (var ck in cks)
            {
                if (!keys.Contains(ck.KeyCode))
                    keys.Add(ck.KeyCode);
            }
            return keys;
        }

        public bool ProcessKey(Keys key)
        {
            try
            {
                if (this.d3Config == null)
                    return false;
                if (!d3Config.ALLHotKeys.Contains(key))
                    return false;
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
            }
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

        public void RestD3Main(D3Config d3Config, int hd)
        {

            if (sld3.ContainsKey(hd))
            {
                sld3[hd].StopAll();
            }

            var d3Main = D3Main.BuildD3Main(d3Config, hd);
            if (d3Main != null)
            {
                lock (lockObject)
                {
                    if (!sld3.ContainsKey(hd))
                    {

                        sld3.Add(hd, d3Main);
                    }
                    else
                    {
                        sld3[hd] = d3Main;
                    }
                }
            }


        }
        public void RestD3Main(D3Config d3Config)
        {
            try
            {
                for (int i = 0; i < sld3.Count; i++)
                {
                    RestD3Main(d3Config, sld3.Keys[i]);
                }
            }
            catch (Exception ex) { log.Error(ex); }
        }

        Tuple<int, bool, string> HDINFO()
        {
            var hd = objdm.GetMousePointWindow();
            var isbl = sld3.ContainsKey(hd);
            var str = objdm.GetWindowClass(hd);
            return new Tuple<int, bool, string>(hd, isbl, str);
        }
        public void SetMouseInfo()
        {

            var items = HDINFO();
            var hd = items.Item1;
            var isbl = items.Item2;
            var strClass = items.Item3;


            object x = new object();
            object y = new object();
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

    }
}
