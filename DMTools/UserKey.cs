using DMTools.Config;
using DMTools.Static;
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
using static System.Runtime.CompilerServices.RuntimeHelpers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DMTools
{
    public partial class UserKey : UserControl
    {
        public const string HotKeyMouse = "MouseKey";
        public const string HotKeyStopAll = "StopAllKey";
        public const string ReplayKey = "Replay";

        public static List<string> HotKeys { get; set; } = new List<string>();
        D3Config d3Config;
        public UserKey(D3Config d3Config)
        {
            InitializeComponent();
            this.txtClass.DataBindings.Add("Text", d3Config, "WindowClass");
            this.dataGridView1.AutoGenerateColumns = false;
            this.Column2.DisplayMember = "KeyName";
            this.Column2.ValueMember = "KeyCode";
            this.Column2.DataSource = UserFun.dtkey.Copy();
            this.d3Config = d3Config;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView2.AutoGenerateColumns = false;

            dataGridViewComboBoxColumn2.DataSource = Enum.GetValues(typeof(KeyClickType));
            dataGridViewComboBoxColumn2.ValueType = typeof(KeyClickType);

            dataGridViewComboBoxColumn1.DisplayMember = "KeyName";
            dataGridViewComboBoxColumn1.ValueMember = "KeyCode";
            dataGridViewComboBoxColumn1.DataPropertyName = "KeyCode";
            this.dataGridViewComboBoxColumn1.DataSource = DTHelper.TableList[DataTableType.Key].Copy();
            BindData();
        }
        static UserKey()
        {
            ConfigKeys = GetConfigKey();
            HotKeys.Add(UserKey.HotKeyStopAll);
            HotKeys.Add(UserKey.HotKeyMouse);
            HotKeys.Add(UserKey.ReplayKey);

        }
        public static List<D3ConfigKey> ConfigKeys = new List<D3ConfigKey>();
        public static List<D3ConfigKey> GetConfigKey()
        {
            var ps = typeof(D3KeyCodes).GetProperties().Where(r => r.PropertyType == typeof(int));
            List<D3ConfigKey> _d3ConfigKeys = new List<D3ConfigKey>();
            foreach (var p in ps)
            {

                var ka = p.GetCustomAttribute(typeof(KeyNameAttribute)) as KeyNameAttribute;
                if (ka == null)
                    continue;
                D3KeyCodes keySetting = new D3KeyCodes();
                int keyCode = Convert.ToInt32(p.GetValue(keySetting));
                D3ConfigKey d = new D3ConfigKey();
                d.KeyInfo = ka.Name;
                d.KeyCode = (Keys)keyCode;
                d.KeyName = p.Name;
                _d3ConfigKeys.Add(d);
            }
            _d3ConfigKeys.Add(new D3ConfigKey() { KeyInfo = "获取鼠标处信息", KeyName = UserKey.HotKeyMouse, KeyCode = Keys.Home });
            _d3ConfigKeys.Add(new D3ConfigKey() { KeyInfo = "停止所有功能", KeyName = UserKey.HotKeyStopAll, KeyCode = Keys.End });
            _d3ConfigKeys.Add(new D3ConfigKey() { KeyInfo = "录制功能", KeyName = UserKey.ReplayKey, KeyCode = Keys.Pause });
            return _d3ConfigKeys;

        }


        public void BindData()
        {
            var cks = ConfigKeys.JsonCopy();
            foreach (var ck in cks)
            {
                if (this.d3Config.ConfigKeys != null)
                {
                    var tmpck = this.d3Config.ConfigKeys.FirstOrDefault(r => r.KeyName == ck.KeyName);
                    if (tmpck != null)
                    {
                        ck.KeyCode = tmpck.KeyCode;
                    }
                }
            }

            this.d3Config.ConfigKeys = cks;
            this.dataGridView1.DataSource = this.d3Config.ConfigKeys;
            this.dataGridView2.DataSource = new BindingList<D3TimeSetting>(this.d3Config.DebugTimes);
        }

        private void dataGridView2_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
    }
}
