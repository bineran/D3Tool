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

        public FormMain(string[] args)
        {
            
            InitializeComponent();
            if(args != null && args.Length>0 )
            {
                this.ckload.Checked = false;
               
            }
            else { this.ckload.Checked = true; }
            SetStyle(
                ControlStyles.ResizeRedraw |
            ControlStyles.AllPaintingInWmPaint |  //全部在窗口绘制消息中绘图
            ControlStyles.OptimizedDoubleBuffer, true //使用双缓冲
            );
            LoadFile();
            InitKeyHook();
            InitMouseHook();
        }



        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
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
            isshoall = false;
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

        private void tsbhide_Click(object sender, EventArgs e)
        {

            this.notifyIcon1.Visible = true;
            this.Visible = false;
            this.ShowInTaskbar = true;

        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (!this.Visible)
            {

                this.Visible = true;
                this.notifyIcon1.Visible = false;
                this.WindowState = lastState;
            }
        }
        FormWindowState lastState = FormWindowState.Maximized;

        private void FormMain_SizeChanged(object sender, EventArgs e)
        {
            lastState = this.WindowState;
        }
        private bool isshoall = false;
        private void 显示所有功能ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isshoall = !isshoall;
            BindTabControl(isshoall);
        }
    }





}