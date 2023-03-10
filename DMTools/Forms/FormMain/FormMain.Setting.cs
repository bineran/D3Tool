using DMTools.Config;
using DMTools.Control;
using DMTools.Forms;
using DMTools.FunList;
using NLog;
using System.Text;


namespace DMTools
{
    public partial class FormMain
    {
        SortedList<string, D3Config> sl = new SortedList<string, D3Config>();
        D3Config d3Config;
        /// <summary>
        /// 加载下面的TabPage
        /// </summary>
        public void BindTabControl()
        {
            this.tbfun.TabPages.Clear();
            if (d3Config == null)
                return;
            var al=d3Config.d3ConfigItems.OrderByDescending(r=>r.EnabledFlag)
                .ThenBy(r=>r.ItemRank)
                .ToList(); 

            foreach (var item in al)
            {
                TabPage tp = new TabPage();
                tp.Tag = item;
                tp.Text = item.ItemName.Trim();
                UserFun u = new UserFun(item);
                u.RemoveFunEvent += U_RemoveFunEvent;
                u.ReNameFunEvent += U_ReNameFunEvent;
                u.Dock = DockStyle.Fill;
                tp.AllowDrop = item.EnabledFlag;

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
                var fs = Directory.GetFiles(path,"*.config");
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
            var index = 0;
            foreach (TabPage tp in this.tbfun.TabPages)
            {
                var uf = tp.Controls[0] as UserFun;
                if (uf != null)
                {
                    uf.d3ConfigItem.ItemRank= index++;
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

    }
}
