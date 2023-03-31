using DMTools.Config;
using DMTools.libs;
using DMTools.Static;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DMTools
{
    public partial class UserKey : UserControl
    {


        public void RestDebugDataGridView()
        {
            this.Invoke(() =>
            {
                foreach (var t in this.d3Config.DebugTimes)
                {
                    try
                    {
                        t.ImageFile = t.Str1.ImageFromDm_Bmp_Path();
                    }
                    catch
                    { }
                }
                this.dataGridView2.DataSource = new BindingList<KeyTimeSetting>(this.d3Config.DebugTimes);
            });

        }
        public D3Config d3Config { get; set; }
        public UserKey(D3Config d3Config)
        {
            InitializeComponent();
            this.txtClass.DataBindings.Add("Text", d3Config, "WindowClass");
            this.dataGridView1.AutoGenerateColumns = false;
            this.Column2.DisplayMember = "KeyName";
            this.Column2.ValueMember = "KeyCode";
            this.Column2.DataSource = UserFun.dtkey.Copy();
            this.d3Config = d3Config;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView2.AutoGenerateColumns = false;

            dataGridViewComboBoxColumn2.DataSource = Enum.GetValues(typeof(KeyClickType));
            dataGridViewComboBoxColumn2.ValueType = typeof(KeyClickType);

            dataGridViewComboBoxColumn1.DisplayMember = "KeyName";
            dataGridViewComboBoxColumn1.ValueMember = "KeyCode";
            dataGridViewComboBoxColumn1.DataPropertyName = "KeyCode";
            this.dataGridViewComboBoxColumn1.DataSource = DTHelper.TableList[DataTableType.Key].Copy();

            if (this.d3Config.sysConfig == null)
            {
                this.d3Config.sysConfig = new SysConfig();
            }
            this.textBox2.DataBindings.Add("Text", this.d3Config.sysConfig, "delta_color");
            this.numericUpDown1.DataBindings.Add("Value", this.d3Config.sysConfig, "sim");
            this.numericUpDown2.DataBindings.Add("Value", this.d3Config.sysConfig, "image_size");
            this.numericUpDown3.DataBindings.Add("Value", this.d3Config.sysConfig, "color_sim");

            DataTable listDisplay = new DataTable();
            listDisplay.Columns.Add("name");
            listDisplay.Rows.Add("normal");
            listDisplay.Rows.Add("gdi");
            listDisplay.Rows.Add("gdi2");
            listDisplay.Rows.Add("dx2");
            listDisplay.Rows.Add("dx3");
            listDisplay.Rows.Add("dx");

            DataTable listMouse = new DataTable();
            listMouse.Columns.Add("name");

            listMouse.Rows.Add("normal");
            listMouse.Rows.Add("windows");
            listMouse.Rows.Add("windows2");
            listMouse.Rows.Add("windows3");
            listMouse.Rows.Add("dx");
            listMouse.Rows.Add("dx2");
            DataTable listKey = new DataTable();
            listKey.Columns.Add("name");

            listKey.Rows.Add("normal");
            listKey.Rows.Add("windows");
            listKey.Rows.Add("dx");


            var tmpConfig = this.d3Config.sysConfig.JsonCopy();

            this.cmbdx.DataSource = listDisplay;
            this.cmbmouse.DataSource = listMouse;
            this.cmbkey.DataSource = listKey;
            this.cmbdx.SelectedValue = tmpConfig.display;
            this.cmbmouse.SelectedValue = tmpConfig.mouse;
            this.cmbkey.SelectedValue = tmpConfig.keypad;
            BindData();

        }


        public void SetDebugText(string text)
        {
            this.Invoke(new Action(() =>
            {
                this.textBox1.Text = text;
            }));

        }

        public void BindData()
        {
            var cks = Hotkey.ConfigKeys.JsonCopy();
            foreach (var ck in cks)
            {
                if (this.d3Config.ConfigKeys != null)
                {
                    var tmpck = this.d3Config.ConfigKeys.FirstOrDefault(r => r.KeyName == ck.KeyName);
                    if (tmpck != null)
                    {
                        ck.KeyCode = tmpck.KeyCode;
                    }
                }
            }


            this.d3Config.ConfigKeys = cks;
            this.dataGridView1.DataSource = this.d3Config.ConfigKeys;
            this.dataGridView2.DataSource = new BindingList<KeyTimeSetting>(this.d3Config.DebugTimes);
        }

        private void dataGridView2_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<KeyTimeSetting> alkts = new List<KeyTimeSetting>();
            foreach (DataGridViewRow dr in this.dataGridView2.SelectedRows)
            {
                var kts = dr.DataBoundItem as KeyTimeSetting;
                alkts.Add(kts);

            }
            alkts = alkts.JsonCopy();
            alkts.ForEach(r => { r.ImageFile = null; });
            if (alkts.Count > 0)
            {
                DMP dMP = new DMP();
                dMP.DM.SetClipboard(alkts.ToJson());
            }
        }

        private void cmbdx_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbdx.SelectedValue != null)
                this.d3Config.sysConfig.display = this.cmbdx.SelectedValue.ToString();
            if (this.cmbmouse.SelectedValue != null)
                this.d3Config.sysConfig.mouse = this.cmbmouse.SelectedValue.ToString();
            if (this.cmbkey.SelectedValue != null)
                this.d3Config.sysConfig.keypad = this.cmbkey.SelectedValue.ToString();

            this.label5.ForeColor = Color.Black;
            this.label6.ForeColor = Color.Black;
            this.label7.ForeColor = Color.Black;
            if (FormMain.LastHandle > 0)
            {
                DMP obj = new DMP();
                var ret = obj.DM.BindWindow(FormMain.LastHandle, d3Config.sysConfig.display, d3Config.sysConfig.mouse, d3Config.sysConfig.keypad, d3Config.sysConfig.mode);
                if (ret == 1)
                {

                    this.label5.ForeColor = Color.Green;
                    this.label6.ForeColor = Color.Green;
                    this.label7.ForeColor = Color.Green;
                    obj.DM.UnBindWindow();
                }
                else
                {
                    this.label5.ForeColor = Color.Red;
                    this.label6.ForeColor = Color.Red;
                    this.label7.ForeColor = Color.Red;
                }

            }


        }
    }
}
