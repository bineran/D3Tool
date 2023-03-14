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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.tbfun = new DMTools.MyTabControl();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.方案ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新增方案ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除方案ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.另存方案ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.功能ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.添加功能ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.复制功能ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmbfile = new System.Windows.Forms.ToolStripComboBox();
            this.保存方案ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.隐藏ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbfun
            // 
            this.tbfun.AllowDrop = true;
            this.tbfun.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbfun.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tbfun.Location = new System.Drawing.Point(0, 29);
            this.tbfun.Name = "tbfun";
            this.tbfun.SelectedIndex = 0;
            this.tbfun.Size = new System.Drawing.Size(898, 499);
            this.tbfun.TabIndex = 2;
            this.tbfun.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tbfun_DrawItem);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.方案ToolStripMenuItem,
            this.功能ToolStripMenuItem,
            this.cmbfile,
            this.保存方案ToolStripMenuItem,
            this.隐藏ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(898, 29);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 方案ToolStripMenuItem
            // 
            this.方案ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新增方案ToolStripMenuItem,
            this.删除方案ToolStripMenuItem,
            this.另存方案ToolStripMenuItem});
            this.方案ToolStripMenuItem.Name = "方案ToolStripMenuItem";
            this.方案ToolStripMenuItem.Size = new System.Drawing.Size(44, 25);
            this.方案ToolStripMenuItem.Text = "方案";
            // 
            // 新增方案ToolStripMenuItem
            // 
            this.新增方案ToolStripMenuItem.Name = "新增方案ToolStripMenuItem";
            this.新增方案ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.新增方案ToolStripMenuItem.Text = "新增";
            this.新增方案ToolStripMenuItem.Click += new System.EventHandler(this.新增方案ToolStripMenuItem_Click);
            // 
            // 删除方案ToolStripMenuItem
            // 
            this.删除方案ToolStripMenuItem.Name = "删除方案ToolStripMenuItem";
            this.删除方案ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.删除方案ToolStripMenuItem.Text = "删除";
            this.删除方案ToolStripMenuItem.Click += new System.EventHandler(this.删除方案ToolStripMenuItem_Click);
            // 
            // 另存方案ToolStripMenuItem
            // 
            this.另存方案ToolStripMenuItem.Name = "另存方案ToolStripMenuItem";
            this.另存方案ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.另存方案ToolStripMenuItem.Text = "另存";
            this.另存方案ToolStripMenuItem.Click += new System.EventHandler(this.另存方案ToolStripMenuItem_Click);
            // 
            // 功能ToolStripMenuItem
            // 
            this.功能ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.添加功能ToolStripMenuItem,
            this.复制功能ToolStripMenuItem});
            this.功能ToolStripMenuItem.Name = "功能ToolStripMenuItem";
            this.功能ToolStripMenuItem.Size = new System.Drawing.Size(44, 25);
            this.功能ToolStripMenuItem.Text = "功能";
            // 
            // 添加功能ToolStripMenuItem
            // 
            this.添加功能ToolStripMenuItem.Name = "添加功能ToolStripMenuItem";
            this.添加功能ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.添加功能ToolStripMenuItem.Text = "添加功能";
            this.添加功能ToolStripMenuItem.Click += new System.EventHandler(this.tsbAdd_Click);
            // 
            // 复制功能ToolStripMenuItem
            // 
            this.复制功能ToolStripMenuItem.Name = "复制功能ToolStripMenuItem";
            this.复制功能ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.复制功能ToolStripMenuItem.Text = "复制功能";
            this.复制功能ToolStripMenuItem.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // cmbfile
            // 
            this.cmbfile.DropDownHeight = 100;
            this.cmbfile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbfile.DropDownWidth = 240;
            this.cmbfile.IntegralHeight = false;
            this.cmbfile.Name = "cmbfile";
            this.cmbfile.Size = new System.Drawing.Size(250, 25);
            this.cmbfile.SelectedIndexChanged += new System.EventHandler(this.cmbfile_SelectedIndexChanged);
            // 
            // 保存方案ToolStripMenuItem
            // 
            this.保存方案ToolStripMenuItem.Name = "保存方案ToolStripMenuItem";
            this.保存方案ToolStripMenuItem.Size = new System.Drawing.Size(68, 25);
            this.保存方案ToolStripMenuItem.Text = "保存方案";
            this.保存方案ToolStripMenuItem.Click += new System.EventHandler(this.保存方案ToolStripMenuItem_Click);
            // 
            // 隐藏ToolStripMenuItem
            // 
            this.隐藏ToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.隐藏ToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.隐藏ToolStripMenuItem.Image = global::DMTools.Properties.Resources._22;
            this.隐藏ToolStripMenuItem.Name = "隐藏ToolStripMenuItem";
            this.隐藏ToolStripMenuItem.Size = new System.Drawing.Size(28, 25);
            this.隐藏ToolStripMenuItem.Text = "隐藏";
            this.隐藏ToolStripMenuItem.Click += new System.EventHandler(this.tsbhide_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "BineranDM";
            this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(898, 528);
            this.Controls.Add(this.tbfun);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bineran-DM";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.SizeChanged += new System.EventHandler(this.FormMain_SizeChanged);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}