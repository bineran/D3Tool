using DMTool.Config;
using DMTool.FunList;
using DMTool.Static;

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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DMTool
{
    public partial class UserFun : UserControl
    {
        public event Action<D3ConfigItem> RemoveFunEvent;
        public event Action<D3ConfigItem> ReNameFunEvent;
        public static DataTable dtkey= new DataTable();
        static UserFun() {
            dtkey = DTHelper.TableList[DataTableType.HotKey];
        }
         
        public UserFun(D3ConfigItem d3ConfigItem)
        {
            InitializeComponent();
            this.dataGridView1.AutoGenerateColumns = false;
            this.checkedListBox1.Items.Clear();
            foreach (EnumD3 hs1 in Enum.GetValues(typeof(EnumD3)))
            {
                if (hs1 != EnumD3.默认)
                    this.checkedListBox1.Items.Add(hs1.ToString());
            }
            Column2.DataSource = Enum.GetValues(typeof(KeyClickType));
            Column2.ValueType = typeof(KeyClickType);
            this.comboBox1.DataSource = dtkey.Copy();
            this.comboBox2.DataSource = dtkey.Copy();
            Column1.DisplayMember = "KeyName";
            Column1.ValueMember= "KeyCode";
            Column1.DataPropertyName = "KeyCode";
            this.Column1.DataSource = DTHelper.TableList[DataTableType.Key].Copy();
            this.d3ConfigItem = d3ConfigItem;
            BindData();
        }

        public  D3ConfigItem d3ConfigItem;
        private void BindData( )
        {
            this.checkBox1.DataBindings.Add("Checked", d3ConfigItem, "StartBeforeStopOther");
            this.checkBox2.DataBindings.Add("Checked", d3ConfigItem, "OtherStopFlag");
            this.comboBox1.SelectedValue = d3ConfigItem.HotKey1;
            this.comboBox2.SelectedValue = d3ConfigItem.HotKey2;
            this.ckEnabled.DataBindings.Add("Checked", d3ConfigItem, "EnabledFlag");

            this.checkedListBox1.ClearSelected();
            bool issetSelect = false;
            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
            {
                this.checkedListBox1.SetItemChecked(i, false);
                var cItem = this.checkedListBox1.Items[i];
                Enum.TryParse(cItem.ToString(), out EnumD3 cenumd3);
                if (d3ConfigItem.d3ConfigFuns != null && d3ConfigItem.d3ConfigFuns.Any(r=>r.enumD3== cenumd3 && r.EnableFlag))
                {
                    if (!issetSelect)
                    {
                        this.checkedListBox1.SelectedIndex = i;
                        issetSelect = true;
                    }

                    this.checkedListBox1.SetItemChecked(i, true);
                }
            }
            if (this.checkedListBox1.SelectedIndex < 0)
                this.checkedListBox1.SelectedIndex = 0;






            SetEnabled();


        }

        public  void SaveData()
        {
            if (this.d3ConfigItem != null)
            {
                SortedList<string, List<D3TimeSetting>> alfn1 = new SortedList<string, List<D3TimeSetting>>();
                foreach (var c in this.checkedListBox1.Items)
                {
                    var key = c.ToString();
                    Enum.TryParse(key, out EnumD3 cenumd3);
                   var tmpFun= this.d3ConfigItem.d3ConfigFuns.FirstOrDefault(r => r.enumD3 == cenumd3);
                    if (tmpFun != null)
                    {
                        var alkey = tmpFun.Times.Where(r =>
                          r.Rank > 0 ||
                          r.Int1 > 0 || r.Int2 > 0 || r.Int3 > 0 || r.Int4 > 0 ||
                          r.D1 > 0 || r.D2 > 0 || r.D3 > 0 || r.D4 > 0 ||
                          r.Str1.TrimLength() > 0 || r.Str2.TrimLength() > 0 ||
                          r.Str3.TrimLength() > 0 || r.Str4.TrimLength() > 0 || r.keyClickType != KeyClickType.不做操作)
                            .ToList();
                        tmpFun.Times = alkey;
                        tmpFun.EnableFlag = this.checkedListBox1.CheckedItems.Contains(c);
                    }
                  
                    
                
                  
                }

                //foreach (var s in alfn1)
                //{
                //    if (Enum.TryParse(s.Key, out EnumD3 e3))
                //    { 
                //        this.d3ConfigItem.d3ConfigFuns.Add(new D3ConfigFun() { enumD3 = e3, Times = s.Value, EnableFlag = true });
                //    }
                //}

                if(this.comboBox1.SelectedValue!= null)
                d3ConfigItem.HotKey1 =(Keys) this.comboBox1.SelectedValue;
                if (this.comboBox2.SelectedValue != null)
                    d3ConfigItem.HotKey2 = (Keys)this.comboBox2.SelectedValue;
                this.d3ConfigItem.EnabledFlag = this.ckEnabled.Checked;
                this.d3ConfigItem.OtherStopFlag = this.checkBox2.Checked;
                this.d3ConfigItem.StartBeforeStopOther = this.checkBox1.Checked;
           
            }
            
        }

        public void AddXYColor(int x,int y,string color,string strClass)
        {
  
            if(this.checkedListBox1.SelectedItem!= null)
            {
                var key=this.checkedListBox1.SelectedItem.ToString();
                if (Enum.TryParse(key, out EnumD3 enumD3))
                { 
                    D3TimeSetting ts = new D3TimeSetting();
                    ts.Int1 = x; ts.Int2 = y;
                    ts.Str1 = color;
                    ts.keyClickType = KeyClickType.不做操作;
                    ts.KeyCode = Keys.NumPad0;
                    ts.Str4 = strClass;
                    var fs= this.d3ConfigItem.d3ConfigFuns.FirstOrDefault(r => r.enumD3 == enumD3);
                    if (fs != null)
                    {
                        fs.Times.Add(ts);
                        Application.DoEvents();
                        
                    }
                }
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
            catch {
            e.Cancel=true;
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
        private void Txt_KeyPress2(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void Txt_KeyPress(object sender, KeyPressEventArgs e)
        {
           if(!char.IsNumber(e.KeyChar)  && e.KeyChar!='\b') { 
                e.Handled= true;
            }
            if (e.KeyChar == '.')
            { 
                e.Handled=true;
            }
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
            this.checkedListBox1.Enabled = ckEnabled.Checked;
        }
        private void ckEnabled_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled();


        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string key = checkedListBox1.SelectedItem.ToString() ?? "";

            if (key.TrimLength() == 0) { return; }
            Enum.TryParse(key, out EnumD3 enumD3);
            if (!this.d3ConfigItem.d3ConfigFuns.Any(r => r.enumD3 == enumD3))
            {
                this.d3ConfigItem.d3ConfigFuns.Add(new D3ConfigFun() { EnableFlag = false, enumD3 = enumD3 });
            }
            var tmpFun = this.d3ConfigItem.d3ConfigFuns.FirstOrDefault(r => r.enumD3 == enumD3);
            if (tmpFun == null) { return; }

            List<D3TimeSetting> al = new List<D3TimeSetting>();
            if (tmpFun.Times != null && tmpFun.Times.Count > 0)
            {
                al = tmpFun.Times;
            }
            else
            {
                al.Add(new D3TimeSetting() { KeyCode = Keys.D1 });
                al.Add(new D3TimeSetting() { KeyCode = Keys.D2 });
                al.Add(new D3TimeSetting() { KeyCode = Keys.D3 });
                al.Add(new D3TimeSetting() { KeyCode = Keys.D4 });
                al.Add(new D3TimeSetting() { KeyCode = Keys.W });
                al.Add(new D3TimeSetting() { KeyCode = Keys.Control | Keys.Left });
                al.Add(new D3TimeSetting() { KeyCode = Keys.Control | Keys.Right });
                al.Add(new D3TimeSetting() { KeyCode = Keys.Shift | Keys.Left });
            }
            this.dataGridView1.DataSource = new BindingList<D3TimeSetting>(al);
            tmpFun.Times = al;

            this.toolTip1.Show(GetFunInfo(key), this.lbltools, 5000);
        }

        private string GetFunInfo(string str)
        {
            if (Enum.TryParse(str, out EnumD3 enumD3))
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
            }
            return "";
        }
        private void 查看功能说明ToolStripMenuItem_Click(object sender, EventArgs e)
        {
    
            if(this.checkedListBox1.SelectedItem!=null)
            {
                var str=this.checkedListBox1.SelectedItem.ToString();
                if(str!=null)
                {
                    var str1 = GetFunInfo(str);
                    if (str1.Length > 0)
                    {
                        MessageBox.Show(str1, str);
                    }

                }
            }
        }
    }
}
