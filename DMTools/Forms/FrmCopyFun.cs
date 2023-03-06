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
    public partial class FrmCopyFun : Form
    {
       public D3ConfigItem d3ConfigItem { get; set; }
        public D3ConfigItem d3ConfigItemSource { get; set; }
        public FrmCopyFun(D3ConfigItem d3ConfigItem)
        {
            InitializeComponent();
            this.d3ConfigItemSource = d3ConfigItem;
            this.textBox2.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox2.Text.Trim().Length == 0)
            { return; }

            d3ConfigItem = d3ConfigItemSource.JsonCopy();
            
            this.d3ConfigItem.ItemName = this.textBox2.Text.Trim();
            this.DialogResult = DialogResult.OK;
            this.Close();

        }
    }
}
