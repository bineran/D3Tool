using DMTools.Config;
using DMTools.FunList;
using DMTools.libs;
using DMTools.Static;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DMTools
{
    public partial class UserFun : UserControl
    {
        public event Action<D3ConfigItem> RemoveFunEvent;
        public event Action<D3ConfigItem> ReNameFunEvent;
        public static DataTable dtkey = new DataTable();
        static UserFun()
        {
            dtkey = DTHelper.TableList[DataTableType.HotKey];
        }
        private class Funitem
        {
            public EnumD3 enumD3 { get; set; } = EnumD3.默认;
            public bool isSelect { get; set; } = false;
        }
        private List<Funitem> FunItems { get; set; }
        public UserFun(D3ConfigItem d3ConfigItem)
        {
            InitializeComponent();
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView2.AutoGenerateColumns = false;
            FunItems = new List<Funitem>();
            List<EnumD3> enumD3s = new List<EnumD3>();
            foreach (EnumD3 hs1 in Enum.GetValues(typeof(EnumD3)))
            {

                if (hs1 != EnumD3.默认)
                {
                    enumD3s.Add(hs1);
                }

            }

            Column1.DisplayMember = "enumD3";
            Column1.ValueMember = "enumD3";

            this.Column18.ValueType = typeof(EnumD3);

            this.Column18.DataSource = enumD3s.ToArray();



            Column2.DataSource = Enum.GetValues(typeof(KeyClickType));
            Column2.ValueType = typeof(KeyClickType);
            this.comboBox1.DataSource = dtkey.Copy();
            this.comboBox2.DataSource = dtkey.Copy();
            Column1.DisplayMember = "KeyName";
            Column1.ValueMember = "KeyCode";
            Column1.DataPropertyName = "KeyCode";
            this.Column1.DataSource = DTHelper.TableList[DataTableType.Key].Copy();
            this.d3ConfigItem = d3ConfigItem;
            BindData();
        }

        public D3ConfigItem d3ConfigItem;
        public List<D3ConfigFun> D3ConfigFuns = new List<D3ConfigFun>();
        private void BindData()
        {
            this.checkBox1.DataBindings.Add("Checked", d3ConfigItem, "StartBeforeStopOther");
            this.checkBox2.DataBindings.Add("Checked", d3ConfigItem, "OtherStopFlag");
            this.comboBox1.SelectedValue = d3ConfigItem.HotKey1;
            this.comboBox2.SelectedValue = d3ConfigItem.HotKey2;
            this.ckEnabled.DataBindings.Add("Checked", d3ConfigItem, "EnabledFlag");



            D3ConfigFuns = d3ConfigItem.d3ConfigFuns.OrderByDescending(r => r.EnableFlag).ThenBy(r => r.enumD3).ToList();
            this.dataGridView2.DataSource = new BindingList<D3ConfigFun>(D3ConfigFuns);






            SetEnabled();


        }

        public void SaveData()
        {
            if (this.d3ConfigItem != null)
            {
                for(int i=0;i<this.D3ConfigFuns.Count;i++)
                {
                    this.D3ConfigFuns[i].Times = this.D3ConfigFuns[i].Times.Where(r =>
 r.TSRemark.TrimLength() > 0 ||
   r.Rank > 0 ||
   r.Int1 > 0 || r.Int2 > 0 || r.Int3 > 0 || r.Int4 > 0 ||
   r.D1 > 0 || r.D2 > 0 || r.D3 > 0 || r.D4 > 0 ||
   r.Str1.TrimLength() > 0 || r.Str2.TrimLength() > 0 ||
   r.Str3.TrimLength() > 0 || r.Str4.TrimLength() > 0 || r.keyClickType != KeyClickType.不做操作)
     .ToList();
                }



                this.d3ConfigItem.d3ConfigFuns = this.D3ConfigFuns;


                //foreach (var s in alfn1)
                //{
                //    if (Enum.TryParse(s.Key, out EnumD3 e3))
                //    { 
                //        this.d3ConfigItem.d3ConfigFuns.Add(new D3ConfigFun() { enumD3 = e3, Times = s.Value, EnableFlag = true });
                //    }
                //}

                if (this.comboBox1.SelectedValue != null)
                    d3ConfigItem.HotKey1 = (Keys)this.comboBox1.SelectedValue;
                if (this.comboBox2.SelectedValue != null)
                    d3ConfigItem.HotKey2 = (Keys)this.comboBox2.SelectedValue;
                this.d3ConfigItem.EnabledFlag = this.ckEnabled.Checked;
                this.d3ConfigItem.OtherStopFlag = this.checkBox2.Checked;
                this.d3ConfigItem.StartBeforeStopOther = this.checkBox1.Checked;

            }

        }



        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                if (dataGridView1.Rows[e.RowIndex].IsNewRow) return;
                decimal dci;

                var pName = this.dataGridView1.Columns[e.ColumnIndex].DataPropertyName;
                if (pName == "D1" || pName == "D2" || pName == "D3" || pName == "D4")
                {
                    if (e.FormattedValue != null && e.FormattedValue.ToString().Length > 0)
                    {
                        if (!decimal.TryParse(e.FormattedValue.ToString(), out dci) || dci < 0)
                        {
                            e.Cancel = true;
                        }
                    }


                }
                if (pName == "Int1" || pName == "Int2" || pName == "Int3" || pName == "Int4")
                {
                    int d;
                    if (e.FormattedValue != null && e.FormattedValue.ToString().Length > 0)
                    {
                        if (!int.TryParse(e.FormattedValue.ToString(), out d) || d < 0)
                        {
                            e.Cancel = true;

                        }
                    }


                }
            }
            catch
            {
                e.Cancel = true;
            }


        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                if (e.CellStyle.Format != null && e.CellStyle.Format == "N2")
                {
                    var txt = e.Control as System.Windows.Forms.TextBox;
                    if (txt != null)
                    {

                        txt.KeyPress += Txt_KeyPress2;
                    }
                }
                if (e.CellStyle.Format != null && e.CellStyle.Format == "d")
                {
                    var txt = e.Control as System.Windows.Forms.TextBox;
                    if (txt != null)
                    {

                        txt.KeyPress += Txt_KeyPress;
                    }
                }
            }
            catch
            { }
        }
        private void Txt_KeyPress2(object? sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void Txt_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
            if (e.KeyChar == '.')
            {
                e.Handled = true;
            }
        }
        public void RestDebugDataGridView()
        {
            this.Invoke(() =>
            {
                this.dataGridView1.DataSource = new BindingList<KeyTimeSetting>(this.selectKTSList);
            });

        }
        private void btnRemoveFun_Click(object sender, EventArgs e)
        {
            if (RemoveFunEvent != null)
            {
                if (MessageBox.Show($"你确定要删除吗？", "删除功能", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    RemoveFunEvent(this.d3ConfigItem);
                }
            }
        }

        private void 重命名功能ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ReNameFunEvent != null)
            {

                ReNameFunEvent(this.d3ConfigItem);

            }

        }
        public void SetEnabled()
        {
            this.checkBox1.Enabled = ckEnabled.Checked;
            this.checkBox2.Enabled = ckEnabled.Checked;
            this.comboBox1.Enabled = ckEnabled.Checked;
            this.comboBox2.Enabled = ckEnabled.Checked;
            this.dataGridView1.Enabled = ckEnabled.Checked;
            this.dataGridView2.Enabled = ckEnabled.Checked;
        }
        private void ckEnabled_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }
        public List<KeyTimeSetting> selectKTSList = new List<KeyTimeSetting>();
        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private string GetFunInfo(EnumD3 enumD3)
        {

            var types = Assembly.GetAssembly(typeof(BaseD3)).GetTypes()
               .Where(r => r.BaseType == typeof(BaseD3) && !r.IsInterface
                                       && !r.IsAbstract);
            foreach (var t in types)
            {
                var field = t.GetField("enumD3Name");
                if (field == null) continue;
                var enumD3tmp = (EnumD3)field.GetRawConstantValue();
                if (enumD3tmp == enumD3)
                {

                    var ca = t.CustomAttributes.FirstOrDefault(r => r.AttributeType == typeof(KeyNameAttribute));
                    if (ca != null)
                    {
                        var ka = t.GetCustomAttribute<KeyNameAttribute>();
                        if (ka != null)
                            return ka.Name;
                    }

                    break;
                }
            }

            return "";
        }
        private void 查看功能说明ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (this.dataGridView2.CurrentRow != null)
            {
                var fun = this.dataGridView2.CurrentRow.DataBoundItem as Funitem;
                if (fun != null)
                {
                    var str1 = GetFunInfo(fun.enumD3);
                    if (str1.Length > 0)
                    {
                        MessageBox.Show(str1, Enum.GetName(typeof(EnumD3), fun.enumD3));
                    }

                }
            }
        }

        private void 粘贴调试的功能ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DMP dmp = new DMP();
            var str = dmp.DM.GetClipboard();
            if (str != null)
            {
                var al = str.FromJson<List<KeyTimeSetting>>();
                if (al != null && al.Count > 0)
                {
                    selectKTSList.AddRange(al.ToArray());
                    this.Invoke(() =>
                    {
                        this.dataGridView1.DataSource = new BindingList<KeyTimeSetting>(selectKTSList);
                    });
                    // dmp.DM.SetClipboard("");
                }
            }
        }

        private void 复制调试的功能ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<KeyTimeSetting> alkts = new List<KeyTimeSetting>();
            foreach (DataGridViewRow dr in this.dataGridView1.SelectedRows)
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

        private void 粘贴坐标和颜色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DMP dmp = new DMP();
            var str = dmp.DM.GetClipboard();
            if (str != null)
            {
                var al = str.FromJson<List<KeyTimeSetting>>();
                if (al != null && al.Count == 1 && this.dataGridView1.SelectedRows.Count == 1)
                {
                    var kt = this.dataGridView1.SelectedRows[0].DataBoundItem as KeyTimeSetting;
                    if (kt != null)
                    {
                        var ks = al[0];


                        kt.Int1 = ks.Int1;
                        kt.Int2 = ks.Int2;
                        kt.Int3 = ks.Int3;
                        kt.Int4 = ks.Int4;
                        kt.Str1 = ks.Str1;
                        kt.Str2 = ks.Str2;
                        kt.Str3 = ks.Str3;
                        kt.Str4 = ks.Str4;
                    }


                    this.Invoke(() =>
                    {
                        this.dataGridView1.DataSource = new BindingList<KeyTimeSetting>(selectKTSList);
                    });
                    // dmp.DM.SetClipboard("");
                }
            }
        }

        private void 粘贴坐标1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DMP dmp = new DMP();
            var str = dmp.DM.GetClipboard();
            if (str != null)
            {
                var al = str.FromJson<List<KeyTimeSetting>>();
                if (al != null && al.Count == 1 && this.dataGridView1.SelectedRows.Count == 1)
                {
                    var kt = this.dataGridView1.SelectedRows[0].DataBoundItem as KeyTimeSetting;
                    if (kt != null)
                    {
                        var ks = al[0];


                        kt.Int1 = ks.Int1;
                        kt.Int2 = ks.Int2;

                    }


                    this.Invoke(() =>
                    {
                        this.dataGridView1.DataSource = new BindingList<KeyTimeSetting>(selectKTSList);
                    });
                    // dmp.DM.SetClipboard("");
                }
            }
        }

        private void 粘贴坐标2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DMP dmp = new DMP();
            var str = dmp.DM.GetClipboard();
            if (str != null)
            {
                var al = str.FromJson<List<KeyTimeSetting>>();
                if (al != null && al.Count == 1 && this.dataGridView1.SelectedRows.Count == 1)
                {
                    var kt = this.dataGridView1.SelectedRows[0].DataBoundItem as KeyTimeSetting;
                    if (kt != null)
                    {
                        var ks = al[0];


                        kt.Int3 = ks.Int3;
                        kt.Int4 = ks.Int4;

                    }


                    this.Invoke(() =>
                    {
                        this.dataGridView1.DataSource = new BindingList<KeyTimeSetting>(selectKTSList);
                    });
                    // dmp.DM.SetClipboard("");
                }
            }
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if (this.dataGridView2.CurrentRow == null)
                return;

            var fun = dataGridView2.CurrentRow.DataBoundItem as D3ConfigFun;
            if (fun == null)
            {
                
                return;
            }


            selectKTSList = new List<KeyTimeSetting>();
            if (fun.Times != null && fun.Times.Count > 0)
            {
                selectKTSList = fun.Times;
            }
            else
            {
                selectKTSList.Add(new KeyTimeSetting() { KeyCode = Keys.D1 });
                selectKTSList.Add(new KeyTimeSetting() { KeyCode = Keys.D2 });
                selectKTSList.Add(new KeyTimeSetting() { KeyCode = Keys.D3 });
                selectKTSList.Add(new KeyTimeSetting() { KeyCode = Keys.D4 });
                selectKTSList.Add(new KeyTimeSetting() { KeyCode = Keys.W });
                selectKTSList.Add(new KeyTimeSetting() { KeyCode = Keys.Control | Keys.Left });
                selectKTSList.Add(new KeyTimeSetting() { KeyCode = Keys.Control | Keys.Right });
                selectKTSList.Add(new KeyTimeSetting() { KeyCode = Keys.Shift | Keys.Left });
            }
            this.dataGridView1.DataSource = new BindingList<KeyTimeSetting>(selectKTSList);
            fun.Times = selectKTSList;
        }

        private void dataGridView1_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
        }

        private void dataGridView2_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {



        }
    }
}
