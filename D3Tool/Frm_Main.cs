using Dm;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace D3Tool
{
    public partial class Frm_Main : Form
    {
        bool run_state = true;
        dmsoftClass objdm = new dmsoftClass();

        KeyboardHook kh;
        MouseHook mouseHook;
        public int GetMousePointWindowHandle
        {
            get { return objdm.GetMousePointWindow(); }
        }
        public SortedList<int, T_DiabloProcess> slhd = new SortedList<int, T_DiabloProcess>();
        public Frm_Main()
        {
            InitializeComponent();
            var hwnds = this.objdm.EnumWindow(0, "Grim", "", 1 + 4 + 8 + 16);

  
            InitData();
            BindCMB();
            this.WindowState = FormWindowState.Minimized;

            kh = new KeyboardHook();
            kh.SetHook();
            kh.OnKeyDownEvent += kh_OnKeyDownEvent;
            mouseHook = new MouseHook();
         
            mouseHook.MouseWheel += mouseHook_MouseWheel;
            mouseHook.MouseUp+=mouseHook_MouseUp;
            mouseHook.MouseDown += mouseHook_MouseDown;
            mouseHook.Start();
        }

        void mouseHook_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                isRight = false;
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                isleft = false;
            }
            return;
           // MessageBox.Show("mouseHook_MouseUp");
        }
      public static bool isleft = false;
      public static bool isRight = false;

              public static DateTime right_time =Convert.ToDateTime("2000-01-01");
              public static DateTime left_time = Convert.ToDateTime("2000-01-01");
        void mouseHook_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
             
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
               
                    right_time = DateTime.Now;
                    isRight = true;
                }
                else if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    left_time = DateTime.Now;
                    isleft = true;
                }
         
                if (!this.run_state)
                {
                    return;
                }

             
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    if (!D3Config.IsRight)
                        return;
                }
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    if (!D3Config.IsLeft)
                        return;
                }
                if (D_Time != null && (DateTime.Now - D_Time).TotalSeconds < 0.6)
                {
                    return;
                }
                D_Time = DateTime.Now;
                var hd = this.GetMousePointWindowHandle;

                var isbl = slhd.ContainsKey(hd);
                string str = objdm.GetWindowClass(hd);
                if (!D3Config.D3Class.ToLower().Contains(str.ToLower()))
                {
                    return;
                }
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    if (!D3Config.IsRight)
                        return;
                    D3Config.PLAN.t1.fmode = (EnumD3)Enum.Parse(typeof(EnumD3), this.comboBox2.SelectedValue.ToString());
                    D3Config.PLAN.t2.fmode = (EnumD3)Enum.Parse(typeof(EnumD3), this.comboBox3.SelectedValue.ToString());
                    D3Config.PLAN.t3.fmode = (EnumD3)Enum.Parse(typeof(EnumD3), this.comboBox4.SelectedValue.ToString());
                    D3Config.PLAN.t4.fmode = (EnumD3)Enum.Parse(typeof(EnumD3), this.comboBox5.SelectedValue.ToString());
                    if (D3Config.PLAN == null)
                        return;
                    if (!isbl)
                        CreateForm(hd);
                    var td = slhd[hd];
                    if (D3Config.KEYS.Key_FN1 == -3 || -3 == D3Config.KEYS.Key_FN10)
                    {
                        td.FN1(D3Config.PLAN);
                    }
                    else if (D3Config.KEYS.Key_FN2 == -3 || -3 == D3Config.KEYS.Key_FN20)
                    {
                        td.FN2(D3Config.PLAN);
                    }
                    else if (D3Config.KEYS.Key_FN3 == -3 || -3 == D3Config.KEYS.Key_FN30)
                    {
                        td.FN3(D3Config.PLAN);
                    }
                    else if (D3Config.KEYS.Key_FN4 == -3 || -3 == D3Config.KEYS.Key_FN40)
                    {
                        td.FN4(D3Config.PLAN);
                    }
                    mouseHook.isCallback = true;
                }
                else if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    D3Config.PLAN.t1.fmode = (EnumD3)Enum.Parse(typeof(EnumD3), this.comboBox2.SelectedValue.ToString());
                    D3Config.PLAN.t2.fmode = (EnumD3)Enum.Parse(typeof(EnumD3), this.comboBox3.SelectedValue.ToString());
                    D3Config.PLAN.t3.fmode = (EnumD3)Enum.Parse(typeof(EnumD3), this.comboBox4.SelectedValue.ToString());
                    D3Config.PLAN.t4.fmode = (EnumD3)Enum.Parse(typeof(EnumD3), this.comboBox5.SelectedValue.ToString());
                    if (D3Config.PLAN == null)
                        return;
                    if (!isbl)
                        CreateForm(hd);
                    var td = slhd[hd];
                    if (D3Config.KEYS.Key_FN1 == -4 || -4 == D3Config.KEYS.Key_FN10)
                    {
                        td.FN1(D3Config.PLAN);
                    }
                    else if (D3Config.KEYS.Key_FN2 == -4 || -4 == D3Config.KEYS.Key_FN20)
                    {
                        td.FN2(D3Config.PLAN);
                    }
                    else if (D3Config.KEYS.Key_FN3 == -4 || -4 == D3Config.KEYS.Key_FN30)
                    {
                        td.FN3(D3Config.PLAN);
                    }
                    else if (D3Config.KEYS.Key_FN4 == -4 || -4 == D3Config.KEYS.Key_FN40)
                    {
                        td.FN4(D3Config.PLAN);
                    }
                    mouseHook.isCallback = true;
                }
 
            }
            catch
            { }
  
           // MessageBox.Show("mouseHook_MouseDown");
        }
        DateTime S_Time, X_Time, D_Time;

        void mouseHook_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                if (!this.run_state)
                {
                    return;
                }
                if (!D3Config.IsMouseWheel)
                {

                    return;
                }


                var hd = this.GetMousePointWindowHandle;

                var isbl = slhd.ContainsKey(hd);
                string str = objdm.GetWindowClass(hd);
                if (!D3Config.D3Class.ToLower().Contains(str.ToLower()))
                {
                    return;
                }
                D3Config.PLAN.t1.fmode = (EnumD3)Enum.Parse(typeof(EnumD3), this.comboBox2.SelectedValue.ToString());
                D3Config.PLAN.t2.fmode = (EnumD3)Enum.Parse(typeof(EnumD3), this.comboBox3.SelectedValue.ToString());
                D3Config.PLAN.t3.fmode = (EnumD3)Enum.Parse(typeof(EnumD3), this.comboBox4.SelectedValue.ToString());
                D3Config.PLAN.t4.fmode = (EnumD3)Enum.Parse(typeof(EnumD3), this.comboBox5.SelectedValue.ToString());
                if (D3Config.PLAN == null)
                    return;
                if (!isbl)
                    CreateForm(hd);
                var td = slhd[hd];


                if (e.Delta == 120)
                {
                    //向上
                    if (S_Time == null || (DateTime.Now - S_Time).TotalSeconds > 0.6)
                    {
                        S_Time = DateTime.Now;
                        if (D3Config.KEYS.Key_FN1 == -2 || -2 == D3Config.KEYS.Key_FN10)
                        {
                            td.FN1(D3Config.PLAN);
                        }
                        else if (D3Config.KEYS.Key_FN2 == -2 || -2 == D3Config.KEYS.Key_FN20)
                        {
                            td.FN2(D3Config.PLAN);
                        }
                        else if (D3Config.KEYS.Key_FN3 == -2 || -2 == D3Config.KEYS.Key_FN30)
                        {
                            td.FN3(D3Config.PLAN);
                        }
                        else if (D3Config.KEYS.Key_FN4 == -2 || -2 == D3Config.KEYS.Key_FN40)
                        {
                            td.FN4(D3Config.PLAN);
                        }

                    }
                }
                else
                {
                    //向下
                    if (X_Time == null || (DateTime.Now - X_Time).TotalSeconds > 0.6)
                    {
                        X_Time = DateTime.Now;

                        if (D3Config.KEYS.Key_FN1 == -1 || -1 == D3Config.KEYS.Key_FN10)
                        {
                            td.FN1(D3Config.PLAN);
                        }
                        else if (D3Config.KEYS.Key_FN2 == -1 || -1 == D3Config.KEYS.Key_FN20)
                        {
                            td.FN2(D3Config.PLAN);
                        }
                        else if (D3Config.KEYS.Key_FN3 == -1 || -1 == D3Config.KEYS.Key_FN30)
                        {
                            td.FN3(D3Config.PLAN);
                        }
                        else if (D3Config.KEYS.Key_FN4 == -1 || -1 == D3Config.KEYS.Key_FN40)
                        {
                            td.FN4(D3Config.PLAN);
                        }
                    }
                }
            }
            catch
            { }

        }

        void kh_OnKeyDownEvent(object sender, KeyEventArgs e)
        {
            try
            {
           
                var hd = this.GetMousePointWindowHandle;
                int tmpkey = e.KeyValue;
                var isbl = slhd.ContainsKey(hd);
                string str = objdm.GetWindowClass(hd);
                if (tmpkey == D3Config.KEYS.Key_Start)
                {
                    this.run_state = !this.run_state;
                    if (!this.run_state)
                    {
                        if (slhd.ContainsKey(hd))
                        {
                            slhd[hd].Stop();
                            slhd.Remove(hd);
                        }
                    }
                    return;
                }
                if (e.Control )
                {
                    if (tmpkey == 111)
                    {
                        tmpkey = 120 * 10;
                    }
                    else if (tmpkey == 106)
                    {
                        tmpkey = 121 * 10;
                    }
                    else if (tmpkey == 109)
                    {
                        tmpkey = 122 * 10;
                    }
                    else if (tmpkey == 107)
                    {
                        tmpkey = 123 * 10;
                    }
                }
                
                if (tmpkey == D3Config.KEYS.Key_Stop)
                {
                    if (slhd.ContainsKey(hd))
                    {
                        slhd[hd].Stop();
                        slhd.Remove(hd);
                    }
                    else
                    {
                        if (!D3Config.KEYS.GameClass.Contains(str))
                        {
                            D3Config.KEYS.GameClass += " " + str;
                        }
                        this.Text = str;
                    }
                    
                    return;
                }
                //if (str !=  D3Config.D3Class )
                //{
                //    return;
                //}
               
                if (D3Config.ALFN.Contains(tmpkey))
                {

                    D3Config.PLAN.t1.fmode = (EnumD3)Enum.Parse(typeof(EnumD3), this.comboBox2.SelectedValue.ToString());
                    D3Config.PLAN.t2.fmode = (EnumD3)Enum.Parse(typeof(EnumD3), this.comboBox3.SelectedValue.ToString());
                    D3Config.PLAN.t3.fmode = (EnumD3)Enum.Parse(typeof(EnumD3), this.comboBox4.SelectedValue.ToString());
                    D3Config.PLAN.t4.fmode = (EnumD3)Enum.Parse(typeof(EnumD3), this.comboBox5.SelectedValue.ToString());
                    if (D3Config.PLAN == null)
                        return;
                    if (!isbl)
                        CreateForm(hd);
                    var td = slhd[hd];
                    if (tmpkey == D3Config.KEYS.Key_FN1 || tmpkey==D3Config.KEYS.Key_FN10)
                    {
                       td.FN1(D3Config.PLAN);
                    }
                    else if (tmpkey == D3Config.KEYS.Key_FN2 || tmpkey == D3Config.KEYS.Key_FN20)
                    {
                        td.FN2(D3Config.PLAN);
                    }
                    else if (tmpkey == D3Config.KEYS.Key_FN3 || tmpkey == D3Config.KEYS.Key_FN30)
                    {
                        td.FN3(D3Config.PLAN);
                    }
                    else if (tmpkey == D3Config.KEYS.Key_FN4 || tmpkey == D3Config.KEYS.Key_FN40)
                    {
                        td.FN4(D3Config.PLAN);
                    }
                }


            }
            catch { }
        }

        
        private void InitData()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("name");
            foreach (string s in Enum.GetNames(typeof(EnumD3)))
            {
                dt.Rows.Add(s);
            }
            this.comboBox2.ValueMember = "name";
            this.comboBox2.DisplayMember = "name";
            this.comboBox2.DataSource = dt.Copy();

            this.comboBox3.ValueMember = "name";
            this.comboBox3.DisplayMember = "name";
            this.comboBox3.DataSource = dt.Copy();

            this.comboBox4.ValueMember = "name";
            this.comboBox4.DisplayMember = "name";
            this.comboBox4.DataSource = dt.Copy();

            this.comboBox5.ValueMember = "name";
            this.comboBox5.DisplayMember = "name";
            this.comboBox5.DataSource = dt.Copy();

       
        }
        private void BindCMB()
        {
            if(D3Config.PLAN.t3==null)
            {
                D3Config.PLAN.t3 = new T_Time() { 
                 fmode= EnumD3.不做操作
                };
            }
            if (D3Config.PLAN.t4 == null)
            {
                D3Config.PLAN.t4 = new T_Time()
                {
                    fmode = EnumD3.不做操作
                };
            }

            this.comboBox2.SelectedValue = D3Config.PLAN.t1.fmode.ToString();

            this.comboBox3.SelectedValue = D3Config.PLAN.t2.fmode.ToString();

            this.comboBox4.SelectedValue = D3Config.PLAN.t3.fmode.ToString();

            this.comboBox5.SelectedValue = D3Config.PLAN.t4.fmode.ToString();

            var tmppath = D3Config.PLAN.Path;

            this.comboBox1.DataSource = null;
            Application.DoEvents();
            this.comboBox1.DisplayMember = "Name";
            this.comboBox1.ValueMember = "Path";
            this.comboBox1.DataSource = D3Config.alplan;
            this.comboBox1.SelectedValue = tmppath;
          
        }
        public void AddItem(T_Time t, List<string> alstr, DataTable dt)
        {
            if (t.Memo1 != null && t.Memo1.Trim().Length > 0 && !alstr.Contains(t.Memo1.Trim()))
            {
                alstr.Add(t.Memo1.Trim());
                dt.Columns.Add(t.Memo1.Trim());
            }
            if (t.Memo2 != null && t.Memo2.Trim().Length > 0 && !alstr.Contains(t.Memo2.Trim()))
            {
                alstr.Add(t.Memo2.Trim());
                dt.Columns.Add(t.Memo2.Trim());
            }
            if (t.Memo3 != null && t.Memo3.Trim().Length > 0 && !alstr.Contains(t.Memo3.Trim()))
            {
                alstr.Add(t.Memo3.Trim());
                dt.Columns.Add(t.Memo3.Trim());
            }
            if (t.Memo4 != null && t.Memo4.Trim().Length > 0 && !alstr.Contains(t.Memo4.Trim()))
            {
                alstr.Add(t.Memo4.Trim());
                dt.Columns.Add(t.Memo4.Trim());
            }
        }
        public void BindJNCMB(params TextBox[] alcmb)
        {
            if (alcmb != null)
            {
                foreach (var cmb in alcmb)
                {
                    cmb.DataBindings.Clear();
                    cmb.ImeMode = System.Windows.Forms.ImeMode.On;
                }

            }
        }
        private void BindData()
        {
            if (D3Config.PLAN.t3 == null)
            {
                D3Config.PLAN.t3 = new T_Time()
                {
                    fmode = EnumD3.不做操作
                };
            }
            if (D3Config.PLAN.t4 == null)
            {
                D3Config.PLAN.t4 = new T_Time()
                {
                    fmode = EnumD3.不做操作
                };
            }
            BindJNCMB(
                this.cmbjn11,
                this.cmbjn12,
                this.cmbjn13,
                this.cmbjn14,
                this.cmbjn21,
                this.cmbjn22,
                this.cmbjn23,
                this.cmbjn24,
                this.cmbjn31,
                this.cmbjn32,
                this.cmbjn33,
                this.cmbjn34,
                this.cmbjn41,
                this.cmbjn42,
                this.cmbjn43,
                this.cmbjn44,
                this.cmb1l,
                this.cmb1r,
                this.cmb2l,
                this.cmb2r,
                this.cmb3l,
                this.cmb3r,
                this.cmb4l,
                this.cmb4r
                );
            numericUpDown1.DataBindings.Clear();
            numericUpDown2.DataBindings.Clear();
            numericUpDown3.DataBindings.Clear();
            numericUpDown4.DataBindings.Clear();
            numericUpDown5.DataBindings.Clear();
            numericUpDown6.DataBindings.Clear();
            numericUpDown11.DataBindings.Clear();
            numericUpDown12.DataBindings.Clear();
            numericUpDown13.DataBindings.Clear();
            numericUpDown14.DataBindings.Clear();
            numericUpDown15.DataBindings.Clear();
            numericUpDown16.DataBindings.Clear();

            numericUpDown10.DataBindings.Clear();
            numericUpDown9.DataBindings.Clear();
            numericUpDown18.DataBindings.Clear();
            numericUpDown17.DataBindings.Clear();
            numericUpDown8.DataBindings.Clear();
            numericUpDown7.DataBindings.Clear();

            this.numericUpDown22.DataBindings.Clear();
            this.numericUpDown21.DataBindings.Clear();
            this.numericUpDown24.DataBindings.Clear();
            this.numericUpDown23.DataBindings.Clear();
            this.numericUpDown20.DataBindings.Clear();
            this.numericUpDown19.DataBindings.Clear();

          var  SelectPlan = D3Config.PLAN;

            this.numericUpDown1.DataBindings.Add("Value", SelectPlan.t1, "Key1");
            this.numericUpDown2.DataBindings.Add("Value", SelectPlan.t1, "Key2");
            this.numericUpDown3.DataBindings.Add("Value", SelectPlan.t1, "Key3");
            this.numericUpDown4.DataBindings.Add("Value", SelectPlan.t1, "Key4");
            this.numericUpDown5.DataBindings.Add("Value", SelectPlan.t1, "KeyL");
            this.numericUpDown6.DataBindings.Add("Value", SelectPlan.t1, "KeyR");

            this.cmbjn11.DataBindings.Add("Text", SelectPlan.t1, "Memo1");
            this.cmbjn12.DataBindings.Add("Text", SelectPlan.t1, "Memo2");
            this.cmbjn13.DataBindings.Add("Text", SelectPlan.t1, "Memo3");
            this.cmbjn14.DataBindings.Add("Text", SelectPlan.t1, "Memo4");
            this.cmb1l.DataBindings.Add("Text", SelectPlan.t1, "MemoL");
            this.cmb1r.DataBindings.Add("Text", SelectPlan.t1, "MemoR");



            
            this.numericUpDown11.DataBindings.Add("Value", SelectPlan.t2, "Key1");
            this.numericUpDown12.DataBindings.Add("Value", SelectPlan.t2, "Key2");
            this.numericUpDown13.DataBindings.Add("Value", SelectPlan.t2, "Key3");
            this.numericUpDown14.DataBindings.Add("Value", SelectPlan.t2, "Key4");
            this.numericUpDown15.DataBindings.Add("Value", SelectPlan.t2, "KeyL");
            this.numericUpDown16.DataBindings.Add("Value", SelectPlan.t2, "KeyR");


            this.cmbjn21.DataBindings.Add("Text", SelectPlan.t2, "Memo1");
            this.cmbjn22.DataBindings.Add("Text", SelectPlan.t2, "Memo2");
            this.cmbjn23.DataBindings.Add("Text", SelectPlan.t2, "Memo3");
            this.cmbjn24.DataBindings.Add("Text", SelectPlan.t2, "Memo4");
            this.cmb2l.DataBindings.Add("Text", SelectPlan.t2, "MemoL");
            this.cmb2r.DataBindings.Add("Text", SelectPlan.t2, "MemoR");
           

            this.comboBox2.SelectedValue = SelectPlan.t1.fmode.ToString();
            this.comboBox3.SelectedValue = SelectPlan.t2.fmode.ToString();
   


            this.numericUpDown10.DataBindings.Add("Value", SelectPlan.t3, "Key1");
            this.numericUpDown9.DataBindings.Add("Value", SelectPlan.t3, "Key2");
            this.numericUpDown18.DataBindings.Add("Value", SelectPlan.t3, "Key3");
            this.numericUpDown17.DataBindings.Add("Value", SelectPlan.t3, "Key4");
            this.numericUpDown8.DataBindings.Add("Value", SelectPlan.t3, "KeyL");
            this.numericUpDown7.DataBindings.Add("Value", SelectPlan.t3, "KeyR");

            this.cmbjn31.DataBindings.Add("Text", SelectPlan.t3, "Memo1");
            this.cmbjn32.DataBindings.Add("Text", SelectPlan.t3, "Memo2");
            this.cmbjn33.DataBindings.Add("Text", SelectPlan.t3, "Memo3");
            this.cmbjn34.DataBindings.Add("Text", SelectPlan.t3, "Memo4");
            this.cmb3l.DataBindings.Add("Text", SelectPlan.t3, "MemoL");
            this.cmb3r.DataBindings.Add("Text", SelectPlan.t3, "MemoR");
           



            this.numericUpDown22.DataBindings.Add("Value", SelectPlan.t4, "Key1");
            this.numericUpDown21.DataBindings.Add("Value", SelectPlan.t4, "Key2");
            this.numericUpDown24.DataBindings.Add("Value", SelectPlan.t4, "Key3");
            this.numericUpDown23.DataBindings.Add("Value", SelectPlan.t4, "Key4");
            this.numericUpDown20.DataBindings.Add("Value", SelectPlan.t4, "KeyL");
            this.numericUpDown19.DataBindings.Add("Value", SelectPlan.t4, "KeyR");

            this.cmbjn41.DataBindings.Add("Text", SelectPlan.t4, "Memo1");
            this.cmbjn42.DataBindings.Add("Text", SelectPlan.t4, "Memo2");
            this.cmbjn43.DataBindings.Add("Text", SelectPlan.t4, "Memo3");
            this.cmbjn44.DataBindings.Add("Text", SelectPlan.t4, "Memo4");
            this.cmb4l.DataBindings.Add("Text", SelectPlan.t4, "MemoL");
            this.cmb4r.DataBindings.Add("Text", SelectPlan.t4, "MemoR");
           


            this.comboBox4.SelectedValue = SelectPlan.t3.fmode.ToString();
            this.comboBox5.SelectedValue = SelectPlan.t4.fmode.ToString();


        }

        void keyboardHook_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }
        private void CreateForm(int hwdn)
        {
            try
            {
                if (hwdn > 0)
                {

                    if (!slhd.ContainsKey(hwdn))
                    {
                        string str = objdm.GetWindowClass(hwdn);
                        if (D3Config.D3Class.ToLower().Contains(str.ToLower()))
                        {
                            slhd.Add(hwdn, new T_DiabloProcess(hwdn));
                        }
                    }
                }
            }
            catch
            {
          
            }
        }

        private void mi_set_Click(object sender, EventArgs e)
        {

        }

        private void mi_plan_Click(object sender, EventArgs e)
        {
            Frm_Plan f = new Frm_Plan();
            f.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Frm_Set f = new Frm_Set();
            f.ShowDialog();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
                return;
            SaveInfo();
            T_Plan tmp = null;
            foreach (var p in D3Config.alplan)
            {
                if (p.Path == this.comboBox1.SelectedValue.ToString())
                {
                    tmp = p;
                    break;
                }

            }
            D3Config.PLAN = tmp;
            if (D3Config.PLAN.Keys!=null)
            {
                D3Config.KEYS = D3Config.PLAN.Keys.ToJson().FromJson<T_Key>();
            }
            BindData();
            SaveInfo();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Frm_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            kh.UnHook();
            mouseHook.Stop();
            foreach (var th in slhd)
            {
                th.Value.Stop();
            }
            Application.ExitThread();
        }
        private void SaveInfo()
        {
            foreach (var th in slhd)
            {
                th.Value.Stop();
            }
            if (D3Config.PLAN.t3 == null)
            {
                D3Config.PLAN.t3 = new T_Time()
                {
                    fmode = EnumD3.不做操作
                };
            }
            if (D3Config.PLAN.t4 == null)
            {
                D3Config.PLAN.t4 = new T_Time()
                {
                    fmode = EnumD3.不做操作
                };
            }
            D3Config.PLAN.t1.fmode = (EnumD3)Enum.Parse(typeof(EnumD3), this.comboBox2.SelectedValue.ToString());
            D3Config.PLAN.t2.fmode = (EnumD3)Enum.Parse(typeof(EnumD3), this.comboBox3.SelectedValue.ToString());
            D3Config.PLAN.t3.fmode = (EnumD3)Enum.Parse(typeof(EnumD3), this.comboBox4.SelectedValue.ToString());
            D3Config.PLAN.t4.fmode = (EnumD3)Enum.Parse(typeof(EnumD3), this.comboBox5.SelectedValue.ToString());
            if (D3Config.PLAN != null
                && D3Config.PLAN.Path != null && D3Config.PLAN.Path.Length > 0
                  && D3Config.PLAN.Name != null && D3Config.PLAN.Name.Length > 0)
            {
                var str = D3Config.PLAN.ToJson();
                System.IO.File.WriteAllText(D3Config.PLAN.Path, str, Encoding.GetEncoding("gb2312"));
                System.IO.File.WriteAllText(D3Config.DefaultPath, D3Config.PLAN.Path, Encoding.GetEncoding("gb2312"));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Frm_Plan f = new Frm_Plan();
            f.ShowDialog();
            if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
            {

                D3Config.LoadPlanList();
                BindCMB();
           
            }
         
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (D3Config.PLAN.Path.ToUpper().Contains("DEFAULT.TXT"))
            {
                MessageBox.Show("默认的不能删除");
                return;
            }
            System.IO.File.Delete(D3Config.PLAN.Path);

            System.IO.File.WriteAllText(D3Config.DefaultPath, D3Config.PlanPath + "\\Default.txt", Encoding.GetEncoding("gb2312"));

            D3Config.LoadPlanList();
            BindCMB();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            SaveInfo();
            for (int i = 0; i < slhd.Count; i++)
            {
                var hd = slhd.Keys[i];
                slhd[hd].Stop();
                slhd.Remove(hd);
                i--;
            }
        }

        private void Frm_Main_Load(object sender, EventArgs e)
        {

        }

        private void Frm_Main_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

     


    }
}
