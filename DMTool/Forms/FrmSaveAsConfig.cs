using DMTool.Config;
using DMTool.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMTool.Forms
{
    public partial class FrmSaveAsConfig : Form
    {
       public D3Config d3Config { get; set; }
        public FrmSaveAsConfig(D3Config d3Config)
        {
            InitializeComponent();
            this.d3Config = d3Config.JsonCopy();
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(this.textBox1.Text.Trim().Length == 0)
            { return; }

      
            d3Config.ConfigName = this.textBox1.Text.Trim();

            if(FrmAddConfig.SaveConfig(d3Config,true))
            {
                this.DialogResult = DialogResult.OK;

                this.Close();
            }
          


        }
       
    }
}
