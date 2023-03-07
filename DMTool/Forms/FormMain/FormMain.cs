using DMTool.Config;
using DMTool.Control;
using DMTool.Forms;
using DMTool.FunList;
using NLog;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace DMTool
{
    public partial class FormMain : Form
    {
        public readonly Logger log = LogManager.GetCurrentClassLogger();

        public FormMain()
        {
            InitializeComponent();
            LoadFile();
            InitKeyHook();
            InitMouseHook();
        }
        #region 菜单事件
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
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (this.tbfun.TabPages.Count > 0)
            {
                var userFun = this.tbfun.SelectedTab.Controls[0] as UserFun;
                if (userFun != null)
                {
                    FrmCopyFun f = new FrmCopyFun(userFun.d3ConfigItem);
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        this.d3Config.d3ConfigItems.Add(f.d3ConfigItem);
                        SaveConfig();
                    }
                
                }
            }
        }
        #endregion


    }





}