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
                this.d3Config.sysConfig=new SysConfig();
            }
            this.textBox2.DataBindings.Add("Text", this.d3Config.sysConfig, "delta_color");
            this.numericUpDown1.DataBindings.Add("Value", this.d3Config.sysConfig, "sim");
            this.numericUpDown2.DataBindings.Add("Value", this.d3Config.sysConfig, "image_size");
            this.numericUpDown3.DataBindings.Add("Value", this.d3Config.sysConfig, "color_sim");



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
                var kts=dr.DataBoundItem as KeyTimeSetting;
                alkts.Add(kts);

            }
            alkts= alkts.JsonCopy();
            alkts.ForEach(r => { r.ImageFile = null; });
            if (alkts.Count > 0)
            {
                DMP dMP = new DMP();
                dMP.DM.SetClipboard(alkts .ToJson());
            }
        }
    }
}
