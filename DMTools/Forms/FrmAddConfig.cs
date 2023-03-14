using DMTools.Config;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMTools.Forms
{
    public partial class FrmAddConfig : Form
    {
       public D3Config d3Config { get; set; }
        public FrmAddConfig( )
        {
            InitializeComponent();
 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(this.textBox1.Text.Trim().Length == 0 || this.textBox2.Text.Trim().Length == 0)
            { return; }
            d3Config = new D3Config();
            d3Config.ConfigName = this.textBox1.Text.Trim();
            d3Config.d3ConfigItems = new List<D3ConfigItem>();
            d3Config.d3ConfigItems.Add(new D3ConfigItem() { ItemName=this.textBox2.Text.Trim() });

            if (FrmAddConfig.SaveConfig(d3Config,true))
            {
                this.DialogResult = DialogResult.OK;

                this.Close();
            }


        }
        public static void RemoveDebugData(D3Config d3Config)
        {
            d3Config.DebugTimes = new List<KeyTimeSetting>();
            d3Config.d3ConfigItems.ForEach(
               r =>
               {
                   r.d3ConfigFuns.ForEach(f =>
                   {
                       var kts = f.Times.Where(t => t.KeyCode == ConvertKeys.HotKeyDebug).ToList();
                       kts.ForEach(kt => f.Times.Remove(kt));

                   });
               }
            );

        }
        public static bool SaveConfig(D3Config d3Config, bool isAdd = false)
        {
            RemoveDebugData(d3Config);
       
            
            var r = d3Config.ToJsonFormat();
            var path = Application.StartupPath + "Config";
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            var filePath = path + "\\" + d3Config.ConfigName.Trim() + ".config";
        
            if (isAdd)
            {

                if (File.Exists(filePath))
                {
                    SaveFileDialog sfg = new SaveFileDialog();
                    sfg.Filter = "配置文件|*.config";
                    sfg.InitialDirectory = path;
                    sfg.FileName = d3Config.ConfigName;
                    sfg.RestoreDirectory = false;
                    if (sfg.ShowDialog() == DialogResult.OK)
                    {
                        System.IO.File.WriteAllText(sfg.FileName, r, System.Text.Encoding.UTF8);
                        return true;
                    }
                    else { return false; }
                }
            }

            System.IO.File.WriteAllText(filePath, r, System.Text.Encoding.UTF8);
            return true;





        }
      
    }
}
