namespace DMTools
{
    partial class UserFun
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
            groupBox2 = new GroupBox();
            dataGridView1 = new DataGridView();
            Column1 = new DataGridViewComboBoxColumn();
            Column2 = new DataGridViewComboBoxColumn();
            Column15 = new DataGridViewTextBoxColumn();
            Column16 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
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
            contextMenuStrip3 = new ContextMenuStrip(components);
            复制调试的功能ToolStripMenuItem = new ToolStripMenuItem();
            粘贴调试的功能ToolStripMenuItem = new ToolStripMenuItem();
            粘贴坐标和颜色ToolStripMenuItem = new ToolStripMenuItem();
            粘贴坐标1ToolStripMenuItem = new ToolStripMenuItem();
            粘贴坐标2ToolStripMenuItem = new ToolStripMenuItem();
            panel1 = new Panel();
            dataGridView2 = new DataGridView();
            Column17 = new DataGridViewCheckBoxColumn();
            Column18 = new DataGridViewComboBoxColumn();
            contextMenuStrip2 = new ContextMenuStrip(components);
            查看功能说明ToolStripMenuItem = new ToolStripMenuItem();
            panel2 = new Panel();
            lbltools = new Label();
            groupBox1 = new GroupBox();
            ckEnabled = new CheckBox();
            btnRemoveFun = new Button();
            contextMenuStrip1 = new ContextMenuStrip(components);
            重命名功能ToolStripMenuItem = new ToolStripMenuItem();
            comboBox2 = new ComboBox();
            label2 = new Label();
            comboBox1 = new ComboBox();
            label1 = new Label();
            checkBox2 = new CheckBox();
            checkBox1 = new CheckBox();
            toolTip1 = new ToolTip(components);
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            contextMenuStrip3.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            contextMenuStrip2.SuspendLayout();
            panel2.SuspendLayout();
            groupBox1.SuspendLayout();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(dataGridView1);
            groupBox2.Controls.Add(panel1);
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Location = new Point(0, 54);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(847, 452);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "功能列表";
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2, Column15, Column16, Column3, Column11, Column7, Column8, Column9, Column10, Column4, Column5, Column6, Column12, Column13, Column14 });
            dataGridView1.ContextMenuStrip = contextMenuStrip3;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(203, 19);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(641, 430);
            dataGridView1.TabIndex = 1;
            dataGridView1.CellValidating += dataGridView1_CellValidating;
            dataGridView1.ColumnHeaderMouseDoubleClick += dataGridView1_ColumnHeaderMouseDoubleClick;
            dataGridView1.DataError += dataGridView1_DataError;
            // 
            // Column1
            // 
            Column1.DataPropertyName = "KeyCode";
            Column1.Frozen = true;
            Column1.HeaderText = "键";
            Column1.Name = "Column1";
            Column1.Resizable = DataGridViewTriState.True;
            Column1.SortMode = DataGridViewColumnSortMode.Automatic;
            // 
            // Column2
            // 
            Column2.DataPropertyName = "keyClickType";
            Column2.Frozen = true;
            Column2.HeaderText = "类型";
            Column2.Items.AddRange(new object[] { "不做操作", "CD好了就按", "定时按", "按住", "按顺序" });
            Column2.Name = "Column2";
            Column2.Width = 120;
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
            // Column16
            // 
            Column16.DataPropertyName = "TSRemark";
            Column16.Frozen = true;
            Column16.HeaderText = "备注";
            Column16.Name = "Column16";
            // 
            // Column3
            // 
            Column3.DataPropertyName = "D1";
            dataGridViewCellStyle2.Format = "N0";
            dataGridViewCellStyle2.NullValue = null;
            Column3.DefaultCellStyle = dataGridViewCellStyle2;
            Column3.HeaderText = "时间1";
            Column3.Name = "Column3";
            Column3.Width = 75;
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
            // contextMenuStrip3
            // 
            contextMenuStrip3.Items.AddRange(new ToolStripItem[] { 复制调试的功能ToolStripMenuItem, 粘贴调试的功能ToolStripMenuItem, 粘贴坐标和颜色ToolStripMenuItem, 粘贴坐标1ToolStripMenuItem, 粘贴坐标2ToolStripMenuItem });
            contextMenuStrip3.Name = "contextMenuStrip3";
            contextMenuStrip3.Size = new Size(161, 114);
            // 
            // 复制调试的功能ToolStripMenuItem
            // 
            复制调试的功能ToolStripMenuItem.Name = "复制调试的功能ToolStripMenuItem";
            复制调试的功能ToolStripMenuItem.Size = new Size(160, 22);
            复制调试的功能ToolStripMenuItem.Text = "复制";
            复制调试的功能ToolStripMenuItem.Click += 复制调试的功能ToolStripMenuItem_Click;
            // 
            // 粘贴调试的功能ToolStripMenuItem
            // 
            粘贴调试的功能ToolStripMenuItem.Name = "粘贴调试的功能ToolStripMenuItem";
            粘贴调试的功能ToolStripMenuItem.Size = new Size(160, 22);
            粘贴调试的功能ToolStripMenuItem.Text = "粘贴";
            粘贴调试的功能ToolStripMenuItem.Click += 粘贴调试的功能ToolStripMenuItem_Click;
            // 
            // 粘贴坐标和颜色ToolStripMenuItem
            // 
            粘贴坐标和颜色ToolStripMenuItem.Name = "粘贴坐标和颜色ToolStripMenuItem";
            粘贴坐标和颜色ToolStripMenuItem.Size = new Size(160, 22);
            粘贴坐标和颜色ToolStripMenuItem.Text = "粘贴坐标和颜色";
            粘贴坐标和颜色ToolStripMenuItem.Click += 粘贴坐标和颜色ToolStripMenuItem_Click;
            // 
            // 粘贴坐标1ToolStripMenuItem
            // 
            粘贴坐标1ToolStripMenuItem.Name = "粘贴坐标1ToolStripMenuItem";
            粘贴坐标1ToolStripMenuItem.Size = new Size(160, 22);
            粘贴坐标1ToolStripMenuItem.Text = "粘贴坐标1";
            粘贴坐标1ToolStripMenuItem.Click += 粘贴坐标1ToolStripMenuItem_Click;
            // 
            // 粘贴坐标2ToolStripMenuItem
            // 
            粘贴坐标2ToolStripMenuItem.Name = "粘贴坐标2ToolStripMenuItem";
            粘贴坐标2ToolStripMenuItem.Size = new Size(160, 22);
            粘贴坐标2ToolStripMenuItem.Text = "粘贴坐标2";
            粘贴坐标2ToolStripMenuItem.Click += 粘贴坐标2ToolStripMenuItem_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(dataGridView2);
            panel1.Controls.Add(panel2);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(3, 19);
            panel1.Name = "panel1";
            panel1.Size = new Size(200, 430);
            panel1.TabIndex = 2;
            // 
            // dataGridView2
            // 
            dataGridView2.AllowUserToResizeRows = false;
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView2.ColumnHeadersVisible = false;
            dataGridView2.Columns.AddRange(new DataGridViewColumn[] { Column17, Column18 });
            dataGridView2.ContextMenuStrip = contextMenuStrip2;
            dataGridView2.Dock = DockStyle.Fill;
            dataGridView2.Location = new Point(0, 0);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.RowHeadersVisible = false;
            dataGridView2.RowTemplate.Height = 25;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.Size = new Size(200, 396);
            dataGridView2.TabIndex = 4;
            dataGridView2.DataError += dataGridView2_DataError;
            dataGridView2.SelectionChanged += dataGridView2_SelectionChanged;
            // 
            // Column17
            // 
            Column17.DataPropertyName = "EnableFlag";
            Column17.HeaderText = "";
            Column17.Name = "Column17";
            Column17.Width = 35;
            // 
            // Column18
            // 
            Column18.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Column18.DataPropertyName = "enumD3";
            Column18.HeaderText = "功能列表";
            Column18.Name = "Column18";
            Column18.Resizable = DataGridViewTriState.True;
            Column18.SortMode = DataGridViewColumnSortMode.Automatic;
            // 
            // contextMenuStrip2
            // 
            contextMenuStrip2.Items.AddRange(new ToolStripItem[] { 查看功能说明ToolStripMenuItem });
            contextMenuStrip2.Name = "contextMenuStrip2";
            contextMenuStrip2.Size = new Size(149, 26);
            // 
            // 查看功能说明ToolStripMenuItem
            // 
            查看功能说明ToolStripMenuItem.Name = "查看功能说明ToolStripMenuItem";
            查看功能说明ToolStripMenuItem.Size = new Size(148, 22);
            查看功能说明ToolStripMenuItem.Text = "查看功能说明";
            查看功能说明ToolStripMenuItem.Click += 查看功能说明ToolStripMenuItem_Click;
            // 
            // panel2
            // 
            panel2.Controls.Add(lbltools);
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(0, 396);
            panel2.Name = "panel2";
            panel2.Size = new Size(200, 34);
            panel2.TabIndex = 3;
            // 
            // lbltools
            // 
            lbltools.AutoSize = true;
            lbltools.Location = new Point(3, 9);
            lbltools.Name = "lbltools";
            lbltools.Size = new Size(0, 17);
            lbltools.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(ckEnabled);
            groupBox1.Controls.Add(btnRemoveFun);
            groupBox1.Controls.Add(comboBox2);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(comboBox1);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(checkBox2);
            groupBox1.Controls.Add(checkBox1);
            groupBox1.Dock = DockStyle.Top;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(847, 54);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "启动设置";
            // 
            // ckEnabled
            // 
            ckEnabled.AutoSize = true;
            ckEnabled.Location = new Point(676, 22);
            ckEnabled.Name = "ckEnabled";
            ckEnabled.Size = new Size(51, 21);
            ckEnabled.TabIndex = 5;
            ckEnabled.Text = "启用";
            ckEnabled.UseVisualStyleBackColor = true;
            ckEnabled.CheckedChanged += ckEnabled_CheckedChanged;
            // 
            // btnRemoveFun
            // 
            btnRemoveFun.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRemoveFun.BackColor = Color.Linen;
            btnRemoveFun.ContextMenuStrip = contextMenuStrip1;
            btnRemoveFun.Location = new Point(813, 20);
            btnRemoveFun.Name = "btnRemoveFun";
            btnRemoveFun.Size = new Size(28, 23);
            btnRemoveFun.TabIndex = 4;
            btnRemoveFun.Text = "X";
            btnRemoveFun.UseVisualStyleBackColor = false;
            btnRemoveFun.Click += btnRemoveFun_Click;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { 重命名功能ToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(137, 26);
            // 
            // 重命名功能ToolStripMenuItem
            // 
            重命名功能ToolStripMenuItem.Name = "重命名功能ToolStripMenuItem";
            重命名功能ToolStripMenuItem.Size = new Size(136, 22);
            重命名功能ToolStripMenuItem.Text = "重命名功能";
            重命名功能ToolStripMenuItem.Click += 重命名功能ToolStripMenuItem_Click;
            // 
            // comboBox2
            // 
            comboBox2.DisplayMember = "KeyName";
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(549, 20);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(121, 25);
            comboBox2.TabIndex = 3;
            comboBox2.ValueMember = "KeyCode";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(504, 24);
            label2.Name = "label2";
            label2.Size = new Size(39, 17);
            label2.TabIndex = 1;
            label2.Text = "热键2";
            // 
            // comboBox1
            // 
            comboBox1.DisplayMember = "KeyName";
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(370, 20);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(121, 25);
            comboBox1.TabIndex = 2;
            comboBox1.ValueMember = "KeyCode";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(325, 23);
            label1.Name = "label1";
            label1.Size = new Size(39, 17);
            label1.TabIndex = 1;
            label1.Text = "热键1";
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(147, 22);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(159, 21);
            checkBox2.TabIndex = 0;
            checkBox2.Text = "运行时阻止其它功能停止";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(6, 22);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(135, 21);
            checkBox1.TabIndex = 0;
            checkBox1.Text = "启动时先停其它功能";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // UserFun
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "UserFun";
            Size = new Size(847, 506);
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            contextMenuStrip3.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            contextMenuStrip2.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private GroupBox groupBox2;
        private DataGridView dataGridView1;
        private GroupBox groupBox1;
        private CheckBox checkBox2;
        private CheckBox checkBox1;
        private Label label2;
        private ComboBox comboBox1;
        private Label label1;
        private ComboBox comboBox2;
        private Button btnRemoveFun;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem 重命名功能ToolStripMenuItem;
        private CheckBox ckEnabled;
        private ContextMenuStrip contextMenuStrip2;
        private ToolStripMenuItem 查看功能说明ToolStripMenuItem;
        private ToolTip toolTip1;
        private Panel panel1;
        private Panel panel2;
        private Label lbltools;
        private ContextMenuStrip contextMenuStrip3;
        private ToolStripMenuItem 粘贴调试的功能ToolStripMenuItem;
        private ToolStripMenuItem 复制调试的功能ToolStripMenuItem;
        private ToolStripMenuItem 粘贴坐标和颜色ToolStripMenuItem;
        private DataGridViewComboBoxColumn Column1;
        private DataGridViewComboBoxColumn Column2;
        private DataGridViewTextBoxColumn Column15;
        private DataGridViewTextBoxColumn Column16;
        private DataGridViewTextBoxColumn Column3;
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
        private ToolStripMenuItem 粘贴坐标1ToolStripMenuItem;
        private ToolStripMenuItem 粘贴坐标2ToolStripMenuItem;
        private DataGridView dataGridView2;
        private DataGridViewCheckBoxColumn Column17;
        private DataGridViewComboBoxColumn Column18;
    }
}
