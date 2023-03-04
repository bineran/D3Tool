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
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.cmbfile = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbAdd = new System.Windows.Forms.ToolStripButton();
            this.tsbremove = new System.Windows.Forms.ToolStripButton();
            this.tsbfremove = new System.Windows.Forms.ToolStripButton();
            this.tsbfadd = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbsave = new System.Windows.Forms.ToolStripButton();
            this.tsbhide = new System.Windows.Forms.ToolStripButton();
            this.tbfun = new System.Windows.Forms.TabControl();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.cmbfile,
            this.tsbfadd,
            this.tsbfremove,
            this.toolStripSeparator2,
            this.toolStripSeparator1,
            this.tsbAdd,
            this.tsbremove,
            this.tsbsave,
            this.tsbhide});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(56, 22);
            this.toolStripLabel1.Text = "方案选择";
            // 
            // cmbfile
            // 
            this.cmbfile.Name = "cmbfile";
            this.cmbfile.Size = new System.Drawing.Size(121, 25);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbAdd
            // 
            this.tsbAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAdd.Image = ((System.Drawing.Image)(resources.GetObject("tsbAdd.Image")));
            this.tsbAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAdd.Name = "tsbAdd";
            this.tsbAdd.Size = new System.Drawing.Size(23, 22);
            this.tsbAdd.Text = "添加功能";
            this.tsbAdd.Click += new System.EventHandler(this.tsbAdd_Click);
            // 
            // tsbremove
            // 
            this.tsbremove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbremove.Image = ((System.Drawing.Image)(resources.GetObject("tsbremove.Image")));
            this.tsbremove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbremove.Name = "tsbremove";
            this.tsbremove.Size = new System.Drawing.Size(23, 22);
            this.tsbremove.Text = "删除功能";
            // 
            // tsbfremove
            // 
            this.tsbfremove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbfremove.Image = ((System.Drawing.Image)(resources.GetObject("tsbfremove.Image")));
            this.tsbfremove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbfremove.Name = "tsbfremove";
            this.tsbfremove.Size = new System.Drawing.Size(23, 22);
            this.tsbfremove.Text = "toolStripButton1";
            // 
            // tsbfadd
            // 
            this.tsbfadd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbfadd.Image = ((System.Drawing.Image)(resources.GetObject("tsbfadd.Image")));
            this.tsbfadd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbfadd.Name = "tsbfadd";
            this.tsbfadd.Size = new System.Drawing.Size(23, 22);
            this.tsbfadd.Text = "toolStripButton2";
            this.tsbfadd.Click += new System.EventHandler(this.tsbfadd_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbsave
            // 
            this.tsbsave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbsave.Image = ((System.Drawing.Image)(resources.GetObject("tsbsave.Image")));
            this.tsbsave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbsave.Name = "tsbsave";
            this.tsbsave.Size = new System.Drawing.Size(23, 22);
            this.tsbsave.Text = "toolStripButton1";
            // 
            // tsbhide
            // 
            this.tsbhide.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbhide.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbhide.Image = ((System.Drawing.Image)(resources.GetObject("tsbhide.Image")));
            this.tsbhide.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbhide.Name = "tsbhide";
            this.tsbhide.Size = new System.Drawing.Size(23, 22);
            this.tsbhide.Text = "toolStripButton2";
            // 
            // tbfun
            // 
            this.tbfun.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbfun.Location = new System.Drawing.Point(0, 25);
            this.tbfun.Name = "tbfun";
            this.tbfun.SelectedIndex = 0;
            this.tbfun.Size = new System.Drawing.Size(800, 425);
            this.tbfun.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tbfun);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ToolStrip toolStrip1;
        private ToolStripLabel toolStripLabel1;
        private ToolStripComboBox cmbfile;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton tsbAdd;
        private ToolStripButton tsbremove;
        private ToolStripButton tsbfadd;
        private ToolStripButton tsbfremove;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton tsbsave;
        private ToolStripButton tsbhide;
        private TabControl tbfun;
    }
}