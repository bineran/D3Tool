namespace DMTools
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            tbfun = new MyTabControl();
            menuStrip1 = new MenuStrip();
            方案ToolStripMenuItem = new ToolStripMenuItem();
            新增方案ToolStripMenuItem = new ToolStripMenuItem();
            删除方案ToolStripMenuItem = new ToolStripMenuItem();
            另存方案ToolStripMenuItem = new ToolStripMenuItem();
            功能ToolStripMenuItem = new ToolStripMenuItem();
            添加功能ToolStripMenuItem = new ToolStripMenuItem();
            复制功能ToolStripMenuItem = new ToolStripMenuItem();
            显示所有功能ToolStripMenuItem = new ToolStripMenuItem();
            cmbfile = new ToolStripComboBox();
            保存方案ToolStripMenuItem = new ToolStripMenuItem();
            隐藏ToolStripMenuItem = new ToolStripMenuItem();
            notifyIcon1 = new NotifyIcon(components);
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // tbfun
            // 
            tbfun.AllowDrop = true;
            tbfun.Dock = DockStyle.Fill;
            tbfun.DrawMode = TabDrawMode.OwnerDrawFixed;
            tbfun.Location = new Point(0, 29);
            tbfun.Name = "tbfun";
            tbfun.SelectedIndex = 0;
            tbfun.Size = new Size(898, 499);
            tbfun.TabIndex = 2;
            tbfun.DrawItem += tbfun_DrawItem;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { 方案ToolStripMenuItem, 功能ToolStripMenuItem, cmbfile, 保存方案ToolStripMenuItem, 隐藏ToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(898, 29);
            menuStrip1.TabIndex = 3;
            menuStrip1.Text = "menuStrip1";
            // 
            // 方案ToolStripMenuItem
            // 
            方案ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { 新增方案ToolStripMenuItem, 删除方案ToolStripMenuItem, 另存方案ToolStripMenuItem });
            方案ToolStripMenuItem.Name = "方案ToolStripMenuItem";
            方案ToolStripMenuItem.Size = new Size(44, 25);
            方案ToolStripMenuItem.Text = "方案";
            // 
            // 新增方案ToolStripMenuItem
            // 
            新增方案ToolStripMenuItem.Name = "新增方案ToolStripMenuItem";
            新增方案ToolStripMenuItem.Size = new Size(100, 22);
            新增方案ToolStripMenuItem.Text = "新增";
            新增方案ToolStripMenuItem.Click += 新增方案ToolStripMenuItem_Click;
            // 
            // 删除方案ToolStripMenuItem
            // 
            删除方案ToolStripMenuItem.Name = "删除方案ToolStripMenuItem";
            删除方案ToolStripMenuItem.Size = new Size(100, 22);
            删除方案ToolStripMenuItem.Text = "删除";
            删除方案ToolStripMenuItem.Click += 删除方案ToolStripMenuItem_Click;
            // 
            // 另存方案ToolStripMenuItem
            // 
            另存方案ToolStripMenuItem.Name = "另存方案ToolStripMenuItem";
            另存方案ToolStripMenuItem.Size = new Size(100, 22);
            另存方案ToolStripMenuItem.Text = "另存";
            另存方案ToolStripMenuItem.Click += 另存方案ToolStripMenuItem_Click;
            // 
            // 功能ToolStripMenuItem
            // 
            功能ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { 添加功能ToolStripMenuItem, 复制功能ToolStripMenuItem, 显示所有功能ToolStripMenuItem });
            功能ToolStripMenuItem.Name = "功能ToolStripMenuItem";
            功能ToolStripMenuItem.Size = new Size(44, 25);
            功能ToolStripMenuItem.Text = "功能";
            // 
            // 添加功能ToolStripMenuItem
            // 
            添加功能ToolStripMenuItem.Name = "添加功能ToolStripMenuItem";
            添加功能ToolStripMenuItem.Size = new Size(153, 22);
            添加功能ToolStripMenuItem.Text = "添加功能";
            添加功能ToolStripMenuItem.Click += tsbAdd_Click;
            // 
            // 复制功能ToolStripMenuItem
            // 
            复制功能ToolStripMenuItem.Name = "复制功能ToolStripMenuItem";
            复制功能ToolStripMenuItem.Size = new Size(153, 22);
            复制功能ToolStripMenuItem.Text = "复制功能";
            复制功能ToolStripMenuItem.Click += toolStripButton1_Click;
            // 
            // 显示所有功能ToolStripMenuItem
            // 
            显示所有功能ToolStripMenuItem.Name = "显示所有功能ToolStripMenuItem";
            显示所有功能ToolStripMenuItem.Size = new Size(153, 22);
            显示所有功能ToolStripMenuItem.Text = "显示/隐藏功能";
            显示所有功能ToolStripMenuItem.Click += 显示所有功能ToolStripMenuItem_Click;
            // 
            // cmbfile
            // 
            cmbfile.DropDownHeight = 100;
            cmbfile.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbfile.DropDownWidth = 240;
            cmbfile.IntegralHeight = false;
            cmbfile.Name = "cmbfile";
            cmbfile.Size = new Size(250, 25);
            cmbfile.SelectedIndexChanged += cmbfile_SelectedIndexChanged;
            // 
            // 保存方案ToolStripMenuItem
            // 
            保存方案ToolStripMenuItem.Name = "保存方案ToolStripMenuItem";
            保存方案ToolStripMenuItem.Size = new Size(68, 25);
            保存方案ToolStripMenuItem.Text = "保存方案";
            保存方案ToolStripMenuItem.Click += 保存方案ToolStripMenuItem_Click;
            // 
            // 隐藏ToolStripMenuItem
            // 
            隐藏ToolStripMenuItem.Alignment = ToolStripItemAlignment.Right;
            隐藏ToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
            隐藏ToolStripMenuItem.Image = Properties.Resources.appbmp;
            隐藏ToolStripMenuItem.Name = "隐藏ToolStripMenuItem";
            隐藏ToolStripMenuItem.Size = new Size(28, 25);
            隐藏ToolStripMenuItem.Text = "隐藏";
            隐藏ToolStripMenuItem.Click += tsbhide_Click;
            // 
            // notifyIcon1
            // 
            notifyIcon1.Icon = (Icon)resources.GetObject("notifyIcon1.Icon");
            notifyIcon1.Text = "BineranDM";
            notifyIcon1.MouseClick += notifyIcon1_MouseClick;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(898, 528);
            Controls.Add(tbfun);
            Controls.Add(menuStrip1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Name = "FormMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Bineran-DM";
            WindowState = FormWindowState.Maximized;
            FormClosed += FormMain_FormClosed;
            SizeChanged += FormMain_SizeChanged;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private MyTabControl tbfun;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem 方案ToolStripMenuItem;
        private ToolStripMenuItem 新增方案ToolStripMenuItem;
        private ToolStripMenuItem 删除方案ToolStripMenuItem;
        private ToolStripMenuItem 另存方案ToolStripMenuItem;
        private ToolStripComboBox cmbfile;
        private ToolStripMenuItem 保存方案ToolStripMenuItem;
        private NotifyIcon notifyIcon1;
        private ToolStripMenuItem 功能ToolStripMenuItem;
        private ToolStripMenuItem 添加功能ToolStripMenuItem;
        private ToolStripMenuItem 复制功能ToolStripMenuItem;
        private ToolStripMenuItem 隐藏ToolStripMenuItem;
        private ToolStripMenuItem 显示所有功能ToolStripMenuItem;
    }
}