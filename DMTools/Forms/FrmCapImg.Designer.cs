namespace DMTools.Forms
{
    partial class FrmCapImg
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pictureBox1 = new PictureBox();
            panel1 = new Panel();
            pictureBox2 = new PictureBox();
            panel2 = new Panel();
            panel3 = new Panel();
            lblcolor = new Label();
            lblpoint = new Label();
            lblfbl = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(800, 450);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.DoubleClick += pictureBox1_DoubleClick;
            pictureBox1.MouseDown += pictureBox1_MouseDown;
            pictureBox1.MouseMove += pictureBox1_MouseMove;
            // 
            // panel1
            // 
            panel1.Controls.Add(pictureBox2);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new Size(200, 200);
            panel1.TabIndex = 1;
            // 
            // pictureBox2
            // 
            pictureBox2.Dock = DockStyle.Fill;
            pictureBox2.Location = new Point(0, 0);
            pictureBox2.Margin = new Padding(0);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(200, 200);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 0;
            pictureBox2.TabStop = false;
            // 
            // panel2
            // 
            panel2.Controls.Add(panel1);
            panel2.Controls.Add(panel3);
            panel2.Location = new Point(0, 0);
            panel2.Margin = new Padding(0);
            panel2.Name = "panel2";
            panel2.Size = new Size(200, 300);
            panel2.TabIndex = 2;
            panel2.Visible = false;
            // 
            // panel3
            // 
            panel3.Controls.Add(lblcolor);
            panel3.Controls.Add(lblpoint);
            panel3.Controls.Add(lblfbl);
            panel3.Dock = DockStyle.Bottom;
            panel3.Location = new Point(0, 200);
            panel3.Margin = new Padding(0);
            panel3.Name = "panel3";
            panel3.Size = new Size(200, 100);
            panel3.TabIndex = 2;
            // 
            // lblcolor
            // 
            lblcolor.AutoSize = true;
            lblcolor.ForeColor = Color.Black;
            lblcolor.Location = new Point(3, 64);
            lblcolor.Name = "lblcolor";
            lblcolor.Size = new Size(0, 17);
            lblcolor.TabIndex = 0;
            // 
            // lblpoint
            // 
            lblpoint.AutoSize = true;
            lblpoint.ForeColor = Color.FromArgb(0, 64, 0);
            lblpoint.Location = new Point(3, 34);
            lblpoint.Name = "lblpoint";
            lblpoint.Size = new Size(0, 17);
            lblpoint.TabIndex = 0;
            // 
            // lblfbl
            // 
            lblfbl.AutoSize = true;
            lblfbl.ForeColor = Color.Blue;
            lblfbl.Location = new Point(3, 3);
            lblfbl.Name = "lblfbl";
            lblfbl.Size = new Size(0, 17);
            lblfbl.TabIndex = 0;
            // 
            // FrmCapImg
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            ControlBox = false;
            Controls.Add(panel2);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmCapImg";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmCapImg";
            TopMost = true;
            WindowState = FormWindowState.Maximized;
            KeyPress += FrmCapImg_KeyPress;
            PreviewKeyDown += FrmCapImg_PreviewKeyDown;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            panel2.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
        private Panel panel1;
        private PictureBox pictureBox2;
        private Panel panel2;
        private Panel panel3;
        private Label lblcolor;
        private Label lblpoint;
        private Label lblfbl;
    }
}