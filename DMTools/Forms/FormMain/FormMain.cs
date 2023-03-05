using DMTools.Config;
using DMTools.Control;
using DMTools.Forms;
using DMTools.FunList;
using NLog;
using System.Text;

namespace DMTools
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
        #region �˵��¼�
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

        private void ��������ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            FrmAddConfig f = new FrmAddConfig();
            if (f.ShowDialog() == DialogResult.OK)
            {

                LoadFile();

            }
        }

        private void ɾ������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.d3Config != null)
            {
                if (MessageBox.Show($"��ȷ��Ҫɾ�� ��{this.d3Config.ConfigName}��  ��", "ɾ������", MessageBoxButtons.OKCancel) == DialogResult.OK)
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

        private void ��淽��ToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void ���淽��ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveConfig();
        }
        #endregion

    }





}