using DMTools.Config;
using DMTools.Control;
using DMTools.Forms;
using DMTools.FunList;
using DMTools.Static;
using NLog;
using System.Text;
using System.Windows.Forms;

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

        private void tbfun_DrawItem(object sender, DrawItemEventArgs e)
        {
           e.DrawBackground();
            try
            {
                Color c = Color.White;

                var uf = this.tbfun.TabPages[e.Index].Controls[0] as UserFun;
                if (uf != null)
                {
                    if (!uf.d3ConfigItem.EnabledFlag)
                    {
                        c = Color.LightGray;
                    }
                }

                using (Brush br = new SolidBrush(c))
                {
                    e.Graphics.FillRectangle(br, e.Bounds);
                    SizeF sz = e.Graphics.MeasureString(tbfun.TabPages[e.Index].Text, e.Font);
                    e.Graphics.DrawString(tbfun.TabPages[e.Index].Text, e.Font,
                        Brushes.Black,
                        e.Bounds.Left + (e.Bounds.Width - sz.Width) / 2,
                        e.Bounds.Top + (e.Bounds.Height - sz.Height) / 2 + 1);
                    Rectangle rect = e.Bounds;
                    rect.Offset(0, 1);
                    rect.Inflate(0, -1);
                    e.Graphics.DrawRectangle(Pens.DarkGray, rect);
                    e.DrawFocusRectangle();
                }
            }
            catch
            { }
        }


            #endregion


        }





}