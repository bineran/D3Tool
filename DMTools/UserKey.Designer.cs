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
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            groupBox1.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2 });
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
            dataGridView2.Columns.AddRange(new DataGridViewColumn[] { dataGridViewComboBoxColumn1, dataGridViewComboBoxColumn2, Column15, Column3, Column11, Column7, Column8, Column9, Column10, Column4, Column5, Column6, Column12, Column13, Column14 });
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
            // UserKey
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tabControl1);
            Controls.Add(groupBox1);
            Name = "UserKey";
            Size = new Size(847, 506);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
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
        private DataGridView dataGridView2;
        private DataGridViewComboBoxColumn dataGridViewComboBoxColumn1;
        private DataGridViewComboBoxColumn dataGridViewComboBoxColumn2;
        private DataGridViewTextBoxColumn Column15;
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
    }
}
