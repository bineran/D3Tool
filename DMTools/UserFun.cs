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
        public event Action<D3ConfigItem> RemoveFunEvent;
        public event Action<D3ConfigItem> ReNameFunEvent;
        public static DataTable dtkey= new DataTable();
        static UserFun() {
            dtkey = GetDT1();
        }
          

        public static DataTable GetDT1()
        {


            DataTable dt = new DataTable();
            dt.Columns.Add("KeyName");
            dt.Columns.Add("KeyCode",typeof(Keys));
            #region code
            dt.Rows.Add("向下滚", Keys.Control| Keys.PageDown);
            dt.Rows.Add("向上滚", Keys.Control | Keys.PageUp);
            dt.Rows.Add("~", 192);
            dt.Rows.Add("ctrl + / G1", Keys.Control | Keys.Divide);
            dt.Rows.Add("ctrl + * G2", Keys.Control | Keys.Multiply);
            dt.Rows.Add("ctrl + - G3", Keys.Control | Keys.Subtract);
            dt.Rows.Add("ctrl + + G4", Keys.Control | Keys.Add);
            dt.Rows.Add("小键盘/", 111);
            dt.Rows.Add("小键盘*", 106);
            dt.Rows.Add("小键盘-", 109);
            dt.Rows.Add("小键盘+", 107);
            dt.Rows.Add("1", 49);
            dt.Rows.Add("2", 50); dt.Rows.Add("3", 51

 ); dt.Rows.Add("4", 52

  ); dt.Rows.Add("5", 53

   ); dt.Rows.Add("6", 54

    ); dt.Rows.Add("7", 55

     ); dt.Rows.Add("8", 56

      ); dt.Rows.Add("9", 57

       ); dt.Rows.Add("0", 48

        );
            dt.Rows.Add("A", 65

             ); dt.Rows.Add("B", 66

              ); dt.Rows.Add("C", 67

               ); dt.Rows.Add("D", 68

                ); dt.Rows.Add("E", 69

                 ); dt.Rows.Add("F", 70

                  ); dt.Rows.Add("G", 71

                   ); dt.Rows.Add("H", 72

                    ); dt.Rows.Add("I", 73

                     ); dt.Rows.Add("J", 74

                      ); dt.Rows.Add("K", 75

                       ); dt.Rows.Add("L", 76

                        ); dt.Rows.Add("M", 77

                         ); dt.Rows.Add("N", 78

                          ); dt.Rows.Add("O", 79

                           ); dt.Rows.Add("P", 80

                            ); dt.Rows.Add("Q", 81

                             ); dt.Rows.Add("R", 82

                              ); dt.Rows.Add("S", 83

                               ); dt.Rows.Add("T", 84

                                ); dt.Rows.Add("U", 85

                                 ); dt.Rows.Add("V", 86

                                  ); dt.Rows.Add("W", 87

                                   ); dt.Rows.Add("X", 88

                                    ); dt.Rows.Add("Y", 89

                                     ); dt.Rows.Add("Z", 90



                                      );
            dt.Rows.Add("CTRL", 17);
            dt.Rows.Add("ALT", 18);
            dt.Rows.Add("SHIFT", 16);
            dt.Rows.Add("WIN", 91);
            dt.Rows.Add("SPACE", 32);
            dt.Rows.Add("CAP", 20);
            dt.Rows.Add("TAB", 9);

            dt.Rows.Add("UP", 38);
            dt.Rows.Add("DOWN", 40);
            dt.Rows.Add("LEFT", 37);
            dt.Rows.Add("RIGHT", 39);

            dt.Rows.Add("HOME", 36);
            dt.Rows.Add("END", 35);
            dt.Rows.Add("PGUP", 33);
            dt.Rows.Add("PGDN", 34);
            dt.Rows.Add("F1", 112);
            dt.Rows.Add("F2", 113);
            dt.Rows.Add("F3", 114);
            dt.Rows.Add("F4", 115);
            dt.Rows.Add("F5", 116);
            dt.Rows.Add("F6", 117);
            dt.Rows.Add("F7", 118);
            dt.Rows.Add("F8", 119);
            dt.Rows.Add("F9", 120);
            dt.Rows.Add("F10", 121);
            dt.Rows.Add("F11", 122);
            dt.Rows.Add("F12", 123);
            dt.Rows.Add("Num0", 96);
            dt.Rows.Add("Num1", 97);
            dt.Rows.Add("Num2", 98);
            dt.Rows.Add("Num3", 99);
            dt.Rows.Add("Num4", 100);
            dt.Rows.Add("Num5", 101);
            dt.Rows.Add("Num6", 102);
            dt.Rows.Add("Num7", 103);
            dt.Rows.Add("Num8", 104);
            dt.Rows.Add("Num9", 105);

            #endregion
            return dt;
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
                var ka = p.GetCustomAttribute(typeof(KeyNameAttribute)) as KeyNameAttribute;
                if (d3ConfigItem.d3TimeSettings != null)
                {
                    if (d3ConfigItem.d3TimeSettings.Any(r => r.KeyName == p.Name))
                    {

                        var s = d3ConfigItem.d3TimeSettings.FirstOrDefault(r => r.KeyName == p.Name);
                        if (s != null)
                        {
                            if (ka != null)
                            {
                                s.KeyInfo = ka.Name;
                            }
                            al.Add(s);
                            continue;
                        }
                 

                    }
                }
              
                if (ka == null)
                    continue;
                al.Add(new D3TimeSetting() { KeyName = p.Name,KeyInfo= ka.Name });
            }
            this.dataGridView1.DataSource = al;
            this.d3ConfigItem.d3TimeSettings = al;

            SetEnabled();


        }

        public  void SaveData()
        {
            if (this.d3ConfigItem != null)
            {
                List<string> alfn1 = new List<string>();
                foreach (var c in this.checkedListBox1.CheckedItems)
                {
                    alfn1.Add(c.ToString());
                }
                this.d3ConfigItem.strfunList = alfn1;

                if(this.comboBox1.SelectedValue!= null)
                d3ConfigItem.HotKey1 =(Keys) this.comboBox1.SelectedValue;
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
    }
}
