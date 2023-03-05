using DMTools.Config;
using DMTools.Control;
using DMTools.Forms;
using DMTools.FunList;
using NLog;
using System.Security.Policy;
using System.Text;
using System.Windows.Forms.VisualStyles;

namespace DMTools
{
    public partial class FormMain : Form
    {
        public readonly Logger log = LogManager.GetCurrentClassLogger();
        Dm.dmsoft objdm = new Dm.dmsoft();
        public FormMain()
        {
            InitializeComponent();
            LoadFile();
            KeyboardHook kh = new KeyboardHook();
            kh.OnKeyDownEvent += Kh_OnKeyDownEvent;
            kh.SetHook();

            //dt.Rows.Add("小键盘/", 111);
            //dt.Rows.Add("小键盘*", 106);
            //dt.Rows.Add("小键盘-", 109);
            //dt.Rows.Add("小键盘+", 107);

            //var d = new D3Main((int)this.Handle);
            //var t1 = new Config.D3Timers() { HotKey1 = Keys.LControlKey | Keys.Divide };
            //var p1 = d.NewD3Param(t1);
            //d.FunList.Add(new D3Fun(p1, EnumD3.一直按住移动键));
            //var p2 = d.NewD3Param(t1);
            //d.FunList.Add(new D3Fun(p2, EnumD3.按1234LRMS));
        }

        private void Kh_OnKeyDownEvent(object? sender, KeyEventArgs e)
        {
            e.Handled= this.ProcessKey(e.KeyData);
        }


        private void tsbAdd_Click(object sender, EventArgs e)
        {
            if (d3Config != null)
            {
                foreach (TabPage tp in this.tbfun.TabPages)
                {
                    var uf = tp.Controls[0] as UserFun;
                    if (uf != null)
                    {
                        uf.SaveData();
                    }
                }
                FrmAddFun f = new FrmAddFun(d3Config);
                if (f.ShowDialog() == DialogResult.OK)
                {
                    BindTabControl();
                }
            }
        }

        private void cmbfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbfile.SelectedItem == null)
                return;

            string str = this.cmbfile.SelectedItem.ToString();
            if (str != null && sl.ContainsKey(str))
            {
                this.d3Config = sl[str];

                RestD3Main(d3Config);

                BindTabControl();
            }

        }

        private void 新增方案ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            FrmAddConfig f = new FrmAddConfig();
            if (f.ShowDialog() == DialogResult.OK)
            {

                LoadFile();

            }
        }

        private void 删除方案ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.d3Config != null)
            {
                if (MessageBox.Show($"你确定要删除 “{this.d3Config.ConfigName}”  吗？", "删除方案", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    var path = d3Config.FilePath;
                    if (File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                    LoadFile();
                }
            }
        }

        private void 另存方案ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.d3Config != null)
            {

                FrmSaveAsConfig f = new FrmSaveAsConfig(this.d3Config);
                if (f.ShowDialog() == DialogResult.OK)
                {

                    LoadFile();

                }
            }
        }

        private void 保存方案ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveConfig();
        }


        /// <summary>
        /// 加载下面的TabPage
        /// </summary>
        public void BindTabControl()
        {
            this.tbfun.TabPages.Clear();
            if (d3Config == null)
                return;
            foreach (var item in d3Config.d3ConfigItems)
            {
                TabPage tp = new TabPage();
                tp.Tag = item;
                tp.Text = item.ItemName.Trim();
                UserFun u = new UserFun(item);
                u.RemoveFunEvent += U_RemoveFunEvent;
                u.ReNameFunEvent += U_ReNameFunEvent;
                u.Dock = DockStyle.Fill;
                tp.Controls.Add(u);
                this.tbfun.TabPages.Add(tp);
            }
            if (d3Config.ConfigKeys != null)
            {
                TabPage tp = new TabPage();
                tp.Text = "按钮设置";
                UserKey u = new UserKey(d3Config);
                u.Dock = DockStyle.Fill;
                tp.Controls.Add(u);
                this.tbfun.TabPages.Add(tp);
            }


        }

        private void U_ReNameFunEvent(D3ConfigItem obj)
        {
            FrmEditFun f = new FrmEditFun(obj);
            if (f.ShowDialog() == DialogResult.OK)
            {
                BindTabControl();
            }
        }
        private void U_RemoveFunEvent(D3ConfigItem obj)
        {
            this.d3Config.d3ConfigItems.Remove(obj);
            BindTabControl();
        }

        D3Config d3Config;

        SortedList<string, D3Config> sl = new SortedList<string, D3Config>();
        public void LoadFile()
        {
            foreach (var hm in sld3)
            {
                if (hm.Value.RunState)
                { 
                    hm.Value.StopAll();
                }
            }
            var path = Application.StartupPath + "Config";
            this.cmbfile.Items.Clear();
            this.tbfun.TabPages.Clear();
            this.d3Config = null;
            
            sl = new SortedList<string, D3Config>();
            if (Directory.Exists(path))
            {
                var fs = Directory.GetFiles(path);
                var tmp = DateTime.Now.AddYears(-10);
                FileInfo lastFI = null;
                D3Config LastConfig = null;
                foreach (var f in fs)
                {
                    var jsonBody = File.ReadAllText(f, Encoding.UTF8);


                    var d3Config = jsonBody.FromJson<D3Config>();
                    if (d3Config != null)
                    {
                        FileInfo fi = new FileInfo(f);
                        if (lastFI != null)
                        {

                            if (lastFI.LastWriteTime < fi.LastWriteTime)
                            {
                                lastFI = fi;
                                LastConfig = d3Config;
                            }

                        }
                        else
                        {
                            LastConfig = d3Config;
                            lastFI = fi;
                        }
                        d3Config.FilePath = f;
                        d3Config.ALLHotKeys = GetALLHotKey(d3Config);
                        sl.Add(d3Config.ConfigName, d3Config);
                    }

                }
                this.cmbfile.Items.Clear();
                foreach (var s in sl)
                {
                    this.cmbfile.Items.Add(s.Key);
                }
                if (LastConfig != null)
                    this.cmbfile.SelectedItem = LastConfig.ConfigName;
                else
                {
                    if (this.cmbfile.Items.Count > 0)
                    {
                        this.cmbfile.SelectedIndex = 0;
                    }
                }
            }


        }
        public void SaveConfig()
        {
            if (this.d3Config == null)
                return;
            foreach (TabPage tp in this.tbfun.TabPages)
            {
                var uf = tp.Controls[0] as UserFun;
                if (uf != null)
                {
                    uf.SaveData();
                }
            }
            if (d3Config != null && d3Config.d3ConfigItems.Count > 0)
            {
                if (FrmAddConfig.SaveConfig(d3Config))
                {
                    LoadFile();
                }
            }
        }
        public List<Keys> GetALLHotKey(D3Config d3Config)
        {
            List<Keys> keys = new List<Keys>();
            foreach (var item in d3Config.d3ConfigItems)
            {
                if(!item.EnabledFlag) { continue; }
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
                var ck = d3Config.ConfigKeys.Where(r => UserKey.HotKeys.Contains(r.KeyName) && r.KeyCode == key).FirstOrDefault();
                if (ck != null)
                {
                    ProcessSysKey(ck);
                    
                    return true;
                }

                ProcessFunKey(d3Config, key);
                return true;
            }
            catch(Exception ex)
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
                if ( d3Config.WindowClass == null)
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
            catch(Exception ex) { 
                log.Error(ex); }
        }
        object lockObject=new object();
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
                for(int i=0;i<sld3.Count;i++) 
                {
                    RestD3Main(d3Config, sld3.Keys[i]);
                }
            }
            catch (Exception ex) { log.Error(ex); }
        }
        public SortedList<int,D3Main> sld3=new SortedList<int,D3Main>();
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