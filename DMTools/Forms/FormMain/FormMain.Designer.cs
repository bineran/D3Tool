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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbAdd = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbhide = new System.Windows.Forms.ToolStripButton();
            this.tbfun = new DMTools.MyTabControl();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.方案ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新增方案ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除方案ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.另存方案ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmbfile = new System.Windows.Forms.ToolStripComboBox();
            this.保存方案ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAdd,
            this.toolStripSeparator2,
            this.toolStripButton1,
            this.toolStripSeparator1,
            this.tsbhide});
            this.toolStrip1.Location = new System.Drawing.Point(0, 29);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(898, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbAdd
            // 
            this.tsbAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbAdd.Image = ((System.Drawing.Image)(resources.GetObject("tsbAdd.Image")));
            this.tsbAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAdd.Name = "tsbAdd";
            this.tsbAdd.Size = new System.Drawing.Size(60, 22);
            this.tsbAdd.Text = "添加功能";
            this.tsbAdd.Click += new System.EventHandler(this.tsbAdd_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(60, 22);
            this.toolStripButton1.Text = "复制功能";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbhide
            // 
            this.tsbhide.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbhide.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbhide.Image = ((System.Drawing.Image)(resources.GetObject("tsbhide.Image")));
            this.tsbhide.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbhide.Name = "tsbhide";
            this.tsbhide.Size = new System.Drawing.Size(72, 22);
            this.tsbhide.Text = "隐蒧主界面";
            this.tsbhide.Visible = false;
            // 
            // tbfun
            // 
            this.tbfun.AllowDrop = true;
            this.tbfun.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbfun.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tbfun.Location = new System.Drawing.Point(0, 54);
            this.tbfun.Name = "tbfun";
            this.tbfun.SelectedIndex = 0;
            this.tbfun.Size = new System.Drawing.Size(898, 474);
            this.tbfun.TabIndex = 2;
            this.tbfun.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tbfun_DrawItem);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.方案ToolStripMenuItem,
            this.cmbfile,
            this.保存方案ToolStripMenuItem});
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
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(898, 528);
            this.Controls.Add(this.tbfun);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bineran-DM";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ToolStrip toolStrip1;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton tsbAdd;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton tsbhide;
        private MyTabControl tbfun;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem 方案ToolStripMenuItem;
        private ToolStripMenuItem 新增方案ToolStripMenuItem;
        private ToolStripMenuItem 删除方案ToolStripMenuItem;
        private ToolStripMenuItem 另存方案ToolStripMenuItem;
        private ToolStripComboBox cmbfile;
        private ToolStripMenuItem 保存方案ToolStripMenuItem;
        private ToolStripButton toolStripButton1;
    }
}