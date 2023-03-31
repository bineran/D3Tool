namespace DMTools
{
    partial class UserKey
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
            dataGridView1 = new DataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewComboBoxColumn();
            contextMenuStrip1 = new ContextMenuStrip(components);
            复制ToolStripMenuItem = new ToolStripMenuItem();
            groupBox1 = new GroupBox();
            txtClass = new TextBox();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            dataGridView2 = new DataGridView();
            dataGridViewComboBoxColumn1 = new DataGridViewComboBoxColumn();
            dataGridViewComboBoxColumn2 = new DataGridViewComboBoxColumn();
            Column15 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            Column16 = new DataGridViewImageColumn();
            Column11 = new DataGridViewTextBoxColumn();
            Column7 = new DataGridViewTextBoxColumn();
            Column8 = new DataGridViewTextBoxColumn();
            Column9 = new DataGridViewTextBoxColumn();
            Column10 = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            Column5 = new DataGridViewTextBoxColumn();
            Column6 = new DataGridViewTextBoxColumn();
            Column12 = new DataGridViewTextBoxColumn();
            Column13 = new DataGridViewTextBoxColumn();
            Column14 = new DataGridViewTextBoxColumn();
            tabPage3 = new TabPage();
            textBox1 = new TextBox();
            tabPage4 = new TabPage();
            cmbkey = new ComboBox();
            cmbmouse = new ComboBox();
            cmbdx = new ComboBox();
            label3 = new Label();
            label4 = new Label();
            label2 = new Label();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label1 = new Label();
            numericUpDown2 = new NumericUpDown();
            numericUpDown3 = new NumericUpDown();
            numericUpDown1 = new NumericUpDown();
            textBox2 = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            contextMenuStrip1.SuspendLayout();
            groupBox1.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            tabPage3.SuspendLayout();
            tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2 });
            dataGridView1.ContextMenuStrip = contextMenuStrip1;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(3, 3);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(554, 470);
            dataGridView1.TabIndex = 0;
            // 
            // Column1
            // 
            Column1.DataPropertyName = "KeyInfo";
            Column1.Frozen = true;
            Column1.HeaderText = "名称";
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            Column1.Width = 200;
            // 
            // Column2
            // 
            Column2.DataPropertyName = "KeyCode";
            Column2.HeaderText = "热键";
            Column2.Name = "Column2";
            Column2.Width = 200;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { 复制ToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(101, 26);
            // 
            // 复制ToolStripMenuItem
            // 
            复制ToolStripMenuItem.Name = "复制ToolStripMenuItem";
            复制ToolStripMenuItem.Size = new Size(100, 22);
            复制ToolStripMenuItem.Text = "复制";
            复制ToolStripMenuItem.Click += 复制ToolStripMenuItem_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txtClass);
            groupBox1.Dock = DockStyle.Right;
            groupBox1.Location = new Point(568, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(279, 506);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "其它设置";
            // 
            // txtClass
            // 
            txtClass.Dock = DockStyle.Top;
            txtClass.Location = new Point(3, 19);
            txtClass.Multiline = true;
            txtClass.Name = "txtClass";
            txtClass.Size = new Size(273, 136);
            txtClass.TabIndex = 0;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Controls.Add(tabPage4);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(568, 506);
            tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(dataGridView1);
            tabPage1.Location = new Point(4, 26);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(560, 476);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "执键设置";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(dataGridView2);
            tabPage2.Location = new Point(4, 26);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(560, 476);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "调试信息一";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridView2
            // 
            dataGridView2.AllowUserToResizeRows = false;
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView2.Columns.AddRange(new DataGridViewColumn[] { dataGridViewComboBoxColumn1, dataGridViewComboBoxColumn2, Column15, Column3, Column16, Column11, Column7, Column8, Column9, Column10, Column4, Column5, Column6, Column12, Column13, Column14 });
            dataGridView2.ContextMenuStrip = contextMenuStrip1;
            dataGridView2.Dock = DockStyle.Fill;
            dataGridView2.Location = new Point(3, 3);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.RowTemplate.Height = 25;
            dataGridView2.Size = new Size(554, 470);
            dataGridView2.TabIndex = 2;
            dataGridView2.DataError += dataGridView2_DataError;
            // 
            // dataGridViewComboBoxColumn1
            // 
            dataGridViewComboBoxColumn1.DataPropertyName = "KeyCode";
            dataGridViewComboBoxColumn1.Frozen = true;
            dataGridViewComboBoxColumn1.HeaderText = "键";
            dataGridViewComboBoxColumn1.Name = "dataGridViewComboBoxColumn1";
            dataGridViewComboBoxColumn1.Resizable = DataGridViewTriState.True;
            dataGridViewComboBoxColumn1.SortMode = DataGridViewColumnSortMode.Automatic;
            // 
            // dataGridViewComboBoxColumn2
            // 
            dataGridViewComboBoxColumn2.DataPropertyName = "keyClickType";
            dataGridViewComboBoxColumn2.Frozen = true;
            dataGridViewComboBoxColumn2.HeaderText = "类型";
            dataGridViewComboBoxColumn2.Items.AddRange(new object[] { "不做操作", "CD好了就按", "定时按", "按住", "按顺序" });
            dataGridViewComboBoxColumn2.Name = "dataGridViewComboBoxColumn2";
            // 
            // Column15
            // 
            Column15.DataPropertyName = "Rank";
            dataGridViewCellStyle1.Format = "N0";
            dataGridViewCellStyle1.NullValue = null;
            Column15.DefaultCellStyle = dataGridViewCellStyle1;
            Column15.Frozen = true;
            Column15.HeaderText = "顺序";
            Column15.Name = "Column15";
            Column15.Resizable = DataGridViewTriState.True;
            Column15.SortMode = DataGridViewColumnSortMode.NotSortable;
            Column15.Width = 60;
            // 
            // Column3
            // 
            Column3.DataPropertyName = "D1";
            dataGridViewCellStyle2.Format = "N0";
            dataGridViewCellStyle2.NullValue = null;
            Column3.DefaultCellStyle = dataGridViewCellStyle2;
            Column3.Frozen = true;
            Column3.HeaderText = "时间1";
            Column3.Name = "Column3";
            Column3.Width = 75;
            // 
            // Column16
            // 
            Column16.DataPropertyName = "ImageFile";
            Column16.Frozen = true;
            Column16.HeaderText = "图片";
            Column16.Name = "Column16";
            Column16.ReadOnly = true;
            // 
            // Column11
            // 
            Column11.DataPropertyName = "Str1";
            Column11.HeaderText = "文本1";
            Column11.Name = "Column11";
            // 
            // Column7
            // 
            Column7.DataPropertyName = "Int1";
            dataGridViewCellStyle3.Format = "d";
            dataGridViewCellStyle3.NullValue = null;
            Column7.DefaultCellStyle = dataGridViewCellStyle3;
            Column7.HeaderText = "整数1";
            Column7.Name = "Column7";
            Column7.Width = 75;
            // 
            // Column8
            // 
            Column8.DataPropertyName = "Int2";
            dataGridViewCellStyle4.Format = "d";
            Column8.DefaultCellStyle = dataGridViewCellStyle4;
            Column8.HeaderText = "整数2";
            Column8.Name = "Column8";
            Column8.Width = 75;
            // 
            // Column9
            // 
            Column9.DataPropertyName = "Int3";
            dataGridViewCellStyle5.Format = "d";
            Column9.DefaultCellStyle = dataGridViewCellStyle5;
            Column9.HeaderText = "整数3";
            Column9.Name = "Column9";
            Column9.Width = 75;
            // 
            // Column10
            // 
            Column10.DataPropertyName = "Int4";
            dataGridViewCellStyle6.Format = "d";
            Column10.DefaultCellStyle = dataGridViewCellStyle6;
            Column10.HeaderText = "整数4";
            Column10.Name = "Column10";
            Column10.Width = 75;
            // 
            // Column4
            // 
            Column4.DataPropertyName = "D2";
            dataGridViewCellStyle7.Format = "N0";
            Column4.DefaultCellStyle = dataGridViewCellStyle7;
            Column4.HeaderText = "时间2";
            Column4.Name = "Column4";
            Column4.Width = 75;
            // 
            // Column5
            // 
            Column5.DataPropertyName = "D3";
            dataGridViewCellStyle8.Format = "N0";
            Column5.DefaultCellStyle = dataGridViewCellStyle8;
            Column5.HeaderText = "时间3";
            Column5.Name = "Column5";
            Column5.Width = 75;
            // 
            // Column6
            // 
            Column6.DataPropertyName = "D4";
            dataGridViewCellStyle9.Format = "N0";
            Column6.DefaultCellStyle = dataGridViewCellStyle9;
            Column6.HeaderText = "时间4";
            Column6.Name = "Column6";
            Column6.Width = 75;
            // 
            // Column12
            // 
            Column12.DataPropertyName = "Str2";
            Column12.HeaderText = "文本2";
            Column12.Name = "Column12";
            // 
            // Column13
            // 
            Column13.DataPropertyName = "Str3";
            Column13.HeaderText = "文本3";
            Column13.Name = "Column13";
            // 
            // Column14
            // 
            Column14.DataPropertyName = "Str4";
            Column14.HeaderText = "文本4";
            Column14.Name = "Column14";
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(textBox1);
            tabPage3.Location = new Point(4, 26);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(560, 476);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "绑定测试";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Dock = DockStyle.Fill;
            textBox1.Location = new Point(3, 3);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(554, 470);
            textBox1.TabIndex = 0;
            // 
            // tabPage4
            // 
            tabPage4.Controls.Add(cmbkey);
            tabPage4.Controls.Add(cmbmouse);
            tabPage4.Controls.Add(cmbdx);
            tabPage4.Controls.Add(label3);
            tabPage4.Controls.Add(label4);
            tabPage4.Controls.Add(label2);
            tabPage4.Controls.Add(label7);
            tabPage4.Controls.Add(label6);
            tabPage4.Controls.Add(label5);
            tabPage4.Controls.Add(label1);
            tabPage4.Controls.Add(numericUpDown2);
            tabPage4.Controls.Add(numericUpDown3);
            tabPage4.Controls.Add(numericUpDown1);
            tabPage4.Controls.Add(textBox2);
            tabPage4.Location = new Point(4, 26);
            tabPage4.Name = "tabPage4";
            tabPage4.Padding = new Padding(3);
            tabPage4.Size = new Size(560, 476);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "系统设置";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // cmbkey
            // 
            cmbkey.DisplayMember = "name";
            cmbkey.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbkey.FormattingEnabled = true;
            cmbkey.Location = new Point(145, 272);
            cmbkey.Name = "cmbkey";
            cmbkey.Size = new Size(121, 25);
            cmbkey.TabIndex = 4;
            cmbkey.ValueMember = "name";
            cmbkey.SelectedIndexChanged += cmbdx_SelectedIndexChanged;
            // 
            // cmbmouse
            // 
            cmbmouse.DisplayMember = "name";
            cmbmouse.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbmouse.FormattingEnabled = true;
            cmbmouse.Location = new Point(146, 229);
            cmbmouse.Name = "cmbmouse";
            cmbmouse.Size = new Size(121, 25);
            cmbmouse.TabIndex = 4;
            cmbmouse.ValueMember = "name";
            cmbmouse.SelectedIndexChanged += cmbdx_SelectedIndexChanged;
            // 
            // cmbdx
            // 
            cmbdx.DisplayMember = "name";
            cmbdx.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbdx.FormattingEnabled = true;
            cmbdx.Location = new Point(146, 188);
            cmbdx.Name = "cmbdx";
            cmbdx.Size = new Size(121, 25);
            cmbdx.TabIndex = 4;
            cmbdx.ValueMember = "name";
            cmbdx.SelectedIndexChanged += cmbdx_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(35, 108);
            label3.Name = "label3";
            label3.Size = new Size(56, 17);
            label3.TabIndex = 3;
            label3.Text = "截图大小";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(35, 149);
            label4.Name = "label4";
            label4.Size = new Size(68, 17);
            label4.TabIndex = 3;
            label4.Text = "颜色相似度";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(35, 71);
            label2.Name = "label2";
            label2.Size = new Size(44, 17);
            label2.TabIndex = 3;
            label2.Text = "相似度";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(35, 280);
            label7.Name = "label7";
            label7.Size = new Size(32, 17);
            label7.TabIndex = 3;
            label7.Text = "键盘";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(35, 232);
            label6.Name = "label6";
            label6.Size = new Size(32, 17);
            label6.TabIndex = 3;
            label6.Text = "鼠标";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(35, 191);
            label5.Name = "label5";
            label5.Size = new Size(32, 17);
            label5.TabIndex = 3;
            label5.Text = "显示";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(35, 32);
            label1.Name = "label1";
            label1.Size = new Size(56, 17);
            label1.TabIndex = 3;
            label1.Text = "图片偏色";
            // 
            // numericUpDown2
            // 
            numericUpDown2.ImeMode = ImeMode.Off;
            numericUpDown2.Location = new Point(146, 106);
            numericUpDown2.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
            numericUpDown2.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            numericUpDown2.Name = "numericUpDown2";
            numericUpDown2.Size = new Size(120, 23);
            numericUpDown2.TabIndex = 2;
            numericUpDown2.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // numericUpDown3
            // 
            numericUpDown3.DecimalPlaces = 2;
            numericUpDown3.ImeMode = ImeMode.Off;
            numericUpDown3.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            numericUpDown3.Location = new Point(146, 147);
            numericUpDown3.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown3.Minimum = new decimal(new int[] { 5, 0, 0, 65536 });
            numericUpDown3.Name = "numericUpDown3";
            numericUpDown3.Size = new Size(120, 23);
            numericUpDown3.TabIndex = 2;
            numericUpDown3.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // numericUpDown1
            // 
            numericUpDown1.DecimalPlaces = 2;
            numericUpDown1.ImeMode = ImeMode.Off;
            numericUpDown1.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            numericUpDown1.Location = new Point(146, 69);
            numericUpDown1.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown1.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(120, 23);
            numericUpDown1.TabIndex = 2;
            numericUpDown1.Value = new decimal(new int[] { 5, 0, 0, 65536 });
            // 
            // textBox2
            // 
            textBox2.Location = new Point(146, 29);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(120, 23);
            textBox2.TabIndex = 0;
            // 
            // UserKey
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tabControl1);
            Controls.Add(groupBox1);
            Name = "UserKey";
            Size = new Size(847, 506);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            contextMenuStrip1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
            tabPage4.ResumeLayout(false);
            tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewComboBoxColumn Column2;
        private GroupBox groupBox1;
        private TextBox txtClass;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TextBox textBox1;
        public DataGridView dataGridView2;
        private DataGridViewComboBoxColumn dataGridViewComboBoxColumn1;
        private DataGridViewComboBoxColumn dataGridViewComboBoxColumn2;
        private DataGridViewTextBoxColumn Column15;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewImageColumn Column16;
        private DataGridViewTextBoxColumn Column11;
        private DataGridViewTextBoxColumn Column7;
        private DataGridViewTextBoxColumn Column8;
        private DataGridViewTextBoxColumn Column9;
        private DataGridViewTextBoxColumn Column10;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewTextBoxColumn Column12;
        private DataGridViewTextBoxColumn Column13;
        private DataGridViewTextBoxColumn Column14;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem 复制ToolStripMenuItem;
        private TabPage tabPage4;
        private Label label2;
        private Label label1;
        private NumericUpDown numericUpDown1;
        private TextBox textBox2;
        private Label label3;
        private NumericUpDown numericUpDown2;
        private Label label4;
        private NumericUpDown numericUpDown3;
        private ComboBox cmbkey;
        private ComboBox cmbmouse;
        private ComboBox cmbdx;
        private Label label7;
        private Label label6;
        private Label label5;
    }
}
