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
    public partial class FrmAddConfig : Form
    {
        D3Config d3Config;
        public FrmAddConfig(D3Config d3Config)
        {
            InitializeComponent();
            this.d3Config = d3Config;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(this.textBox1.Text.Trim().Length == 0 || this.textBox2.Text.Trim().Length == 0)
            { return; }
      
            d3Config.ConfigName = this.textBox1.Text.Trim();
            d3Config.d3ConfigItems = new List<D3ConfigItem>();
            d3Config.d3ConfigItems.Add(new D3ConfigItem() { ItemName=this.textBox2.Text.Trim() });
            this.DialogResult= DialogResult.OK;
            this.Close();

        }
    }
}
