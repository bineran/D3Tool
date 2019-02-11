using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace D3Tool
{
    public partial class Frm_Set : Form
    {
        public Frm_Set()
        {
            InitializeComponent();
            var dt = GetDT1();

            this.comboBox1.DataSource = dt.Copy();
            this.comboBox2.DataSource = dt.Copy();
            this.comboBox3.DataSource = dt.Copy();
            this.comboBox4.DataSource = dt.Copy();
            this.comboBox6.DataSource = dt.Copy();
            this.comboBox5.DataSource = dt.Copy();
            this.comboBox8.DataSource = dt.Copy();
            this.comboBox7.DataSource = dt.Copy();
            this.comboBox10.DataSource = dt.Copy();
            this.comboBox9.DataSource = dt.Copy();
            this.comboBox12.DataSource = dt.Copy();
            this.comboBox11.DataSource = dt.Copy();
            this.comboBox100.DataSource = dt.Copy();
            this.comboBox90.DataSource = dt.Copy();
            this.comboBox120.DataSource = dt.Copy();
            this.comboBox110.DataSource = dt.Copy();

            this.comboBox1.DataBindings.Add("SelectedValue", D3Config.KEYS, "Key1");
            this.comboBox2.DataBindings.Add("SelectedValue", D3Config.KEYS, "Key2");
            this.comboBox3.DataBindings.Add("SelectedValue", D3Config.KEYS, "Key3");
            this.comboBox4.DataBindings.Add("SelectedValue", D3Config.KEYS, "Key4");
            this.comboBox6.DataBindings.Add("SelectedValue", D3Config.KEYS, "Key_Move");
            this.comboBox5.DataBindings.Add("SelectedValue", D3Config.KEYS, "Key_Stand");
            this.comboBox8.DataBindings.Add("SelectedValue", D3Config.KEYS, "Key_Start");
            this.comboBox7.DataBindings.Add("SelectedValue", D3Config.KEYS, "Key_Stop");
            this.comboBox10.DataBindings.Add("SelectedValue", D3Config.KEYS, "Key_FN1");
            this.comboBox9.DataBindings.Add("SelectedValue", D3Config.KEYS, "Key_FN2");
            this.comboBox12.DataBindings.Add("SelectedValue", D3Config.KEYS, "Key_FN3");
            this.comboBox11.DataBindings.Add("SelectedValue", D3Config.KEYS, "Key_FN4");
            if (D3Config.KEYS.Key_FN10 == 0)
            {
                D3Config.KEYS.Key_FN10 = 1200;
            }
            if (D3Config.KEYS.Key_FN20 == 0)
            {
                D3Config.KEYS.Key_FN20 = 1210;
            }
            if (D3Config.KEYS.Key_FN30 == 0)
            {
                D3Config.KEYS.Key_FN30 = 1220;
            }
            if (D3Config.KEYS.Key_FN40 == 0)
            {
                D3Config.KEYS.Key_FN40 = 1230;
            }
            this.comboBox100.DataBindings.Add("SelectedValue", D3Config.KEYS, "Key_FN10");
            this.comboBox90.DataBindings.Add("SelectedValue", D3Config.KEYS, "Key_FN20");
            this.comboBox120.DataBindings.Add("SelectedValue", D3Config.KEYS, "Key_FN30");
            this.comboBox110.DataBindings.Add("SelectedValue", D3Config.KEYS, "Key_FN40");
            if (D3Config.KEYS.GameClass == null || D3Config.KEYS.GameClass.Length == 0)
            { 
               D3Config.KEYS.GameClass="D3 Main Window Class VVideoClass";
            }
            this.txtclass.DataBindings.Add("Text", D3Config.KEYS, "GameClass");
        }
        public DataTable GetDT1()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("KeyName");
            dt.Columns.Add("KeyCode");
            #region code
            dt.Rows.Add("向下滚", -1);
            dt.Rows.Add("向上滚", -2);
            dt.Rows.Add("左键", -4);
            dt.Rows.Add("右键", -3);
            dt.Rows.Add("~", 192);
            dt.Rows.Add("G1", 1200);
            dt.Rows.Add("G2", 1210);
            dt.Rows.Add("G3", 1220);
            dt.Rows.Add("G4", 1230);
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
            #endregion
            return dt;
        }

        private void Frm_Set_FormClosing(object sender, FormClosingEventArgs e)
        {

            
            this.textBox1.Focus();
            var str = D3Config.KEYS.ToJson();
            if (D3Config.PLAN != null)
            {
                D3Config.PLAN.Keys = D3Config.KEYS.ToJson().FromJson<T_Key>();
                var str2 = D3Config.PLAN.ToJson();
                System.IO.File.WriteAllText(D3Config.PLAN.Path, str2, Encoding.GetEncoding("gb2312"));
                System.IO.File.WriteAllText(D3Config.DefaultPath, D3Config.PLAN.Path, Encoding.GetEncoding("gb2312"));
            }
            System.IO.File.WriteAllText(D3Config.KeyPath, str, Encoding.GetEncoding("gb2312"));

         
        }
    }

}
