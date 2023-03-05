using DMTools.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMTools.Forms
{
    public partial class FrmAddFun : Form
    {
        D3Config d3Config;
        public FrmAddFun(D3Config d3Config)
        {
            InitializeComponent();
            this.d3Config = d3Config;
            SortedList<string, string> sl = new SortedList<string, string>();
            sl.Add("1", "一");
            sl.Add("2", "二");
            sl.Add("3", "三");
            sl.Add("4", "四");
            sl.Add("5", "五");
            sl.Add("6", "六");
            sl.Add("7", "七");
            sl.Add("8", "八");
            sl.Add("9", "九");
            sl.Add("10", "十");
            var funCount = this.d3Config.d3ConfigItems.Count + 1;
            if (funCount < 11)
            {
                this.textBox2.Text = "功能" + sl[funCount.ToString()];
            }
            else
                this.textBox2.Text = "功能";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox2.Text.Trim().Length == 0)
            { return; }



            d3Config.d3ConfigItems.Add(new D3ConfigItem() { ItemName = this.textBox2.Text.Trim() });
            this.DialogResult = DialogResult.OK;
            this.Close();

        }
    }
}
