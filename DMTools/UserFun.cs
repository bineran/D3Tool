using DMTools.Config;
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

namespace DMTools
{
    public partial class UserFun : UserControl
    {
        public UserFun()
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
        }
        public void BindData(TabPage tp, D3ConfigItem d3ConfigItem)
        {
            tp.Text = d3ConfigItem.ItemName.Trim();
            this.checkBox1.DataBindings.Add("Checked", d3ConfigItem, "StartBeforeStopOther");
            this.checkBox2.DataBindings.Add("Checked", d3ConfigItem, "OtherStopFlag");

            this.checkedListBox1.ClearSelected();
            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
            {
                //this.checkedListBox1.SetItemChecked(i, false);
                var cItem = this.checkedListBox1.Items[i];
                if (d3ConfigItem.strfunList != null && d3ConfigItem.strfunList.Contains(cItem.ToString()))
                {
                    this.checkedListBox1.SetItemChecked(i, true);
                }
            }
            var ps = typeof(D3Timers).GetProperties().Where(r => r.PropertyType == typeof(D3TimeSetting));
            List<D3TimeSetting> al = new List<D3TimeSetting>();
            foreach (var p in ps)
            {
                if (d3ConfigItem.d3TimeSettings != null)
                {
                    if (d3ConfigItem.d3TimeSettings.Any(r => r.Name == p.Name))
                    {
                        var s = d3ConfigItem.d3TimeSettings.FirstOrDefault(r => r.Name == p.Name);
                        if (s != null)
                        {
                            al.Add(s);
                            continue;
                        }
                 

                    }
                }
                al.Add(new D3TimeSetting() { Name = p.Name });
            }
            this.dataGridView1.DataSource = new BindingList<D3TimeSetting>(al);


        }


        public static void BindData(TabControl tc, D3Config d3Config)
        {
            tc.TabPages.Clear();
            foreach(var item in d3Config.d3ConfigItems)
            {
                TabPage tp = new TabPage();
                tp.Text = item.ItemName;
                UserFun u=new UserFun();
                u.BindData(tp, item);
                u.Dock= DockStyle.Fill; 
                tp.Controls.Add(u);
                tc.TabPages.Add(tp);
            }
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
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

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {

            if (e.CellStyle.Format != null && e.CellStyle.Format=="N2")
            {
                var txt = e.Control as System.Windows.Forms.TextBox;
                if (txt != null)
                {

                    txt.KeyPress += Txt_KeyPress2;
                }
            }
            if (e.CellStyle.Format != null && e.CellStyle.Format=="d")
            {
                var txt = e.Control as System.Windows.Forms.TextBox;
                if (txt != null)
                {

                    txt.KeyPress += Txt_KeyPress;
                }
            }
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
           if(!char.IsNumber(e.KeyChar)  && e.KeyChar!='\b') { 
                e.Handled= true;
            }
            if (e.KeyChar == '.')
            { 
                e.Handled=true;
            }
        }
    }
}
