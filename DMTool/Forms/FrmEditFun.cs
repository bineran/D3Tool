﻿using DMTool.Config;
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
    public partial class FrmEditFun : Form
    {
        D3ConfigItem d3ConfigItem;
        public FrmEditFun(D3ConfigItem d3ConfigItem)
        {
            InitializeComponent();
            this.d3ConfigItem = d3ConfigItem;
            this.textBox2.Text = d3ConfigItem.ItemName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox2.Text.Trim().Length == 0)
            { return; }



            this.d3ConfigItem.ItemName = this.textBox2.Text.Trim();
            this.DialogResult = DialogResult.OK;
            this.Close();

        }
    }
}
