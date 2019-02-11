using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace D3Tool
{
    public partial class Frm_Plan : Form
    {
        public Frm_Plan()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text.Trim() .Length> 0)
            {
                var name = this.textBox1.Text.Trim();
                string file = D3Config.PlanPath + "\\" + name + ".txt";
                System.IO.File.WriteAllText(file, "");
                D3Config.PLAN.Path = file;
                D3Config.PLAN.Name = name;
                System.IO.File.WriteAllText(file, D3Config.PLAN.ToJson(),Encoding.GetEncoding("gb2312"));
                System.IO.File.WriteAllText(D3Config.DefaultPath, file, Encoding.GetEncoding("gb2312"));
                this.DialogResult = System.Windows.Forms.DialogResult.OK;

            }
        }
    }
}
