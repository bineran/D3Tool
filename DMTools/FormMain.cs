using DMTools.Config;
using DMTools.Control;
using DMTools.Forms;
using DMTools.FunList;

namespace DMTools
{
    public partial class FormMain : Form
    {
        D3Main d;
        public FormMain()
        {
            InitializeComponent();
            KeyboardHook kh = new KeyboardHook();
            kh.OnKeyDownEvent += Kh_OnKeyDownEvent;
            Dm.dmsoft objdm = new Dm.dmsoft();
            //dt.Rows.Add("小键盘/", 111);
            //dt.Rows.Add("小键盘*", 106);
            //dt.Rows.Add("小键盘-", 109);
            //dt.Rows.Add("小键盘+", 107);

            d = new D3Main((int)this.Handle);
            var t1 = new Config.D3Timers() { HotKey1 = Keys.LControlKey | Keys.Divide };
            var p1 = d.NewD3Param(t1);
            d.FunList.Add(new D3Fun(p1, EnumD3.一直按住移动键));
            var p2=d.NewD3Param(t1);
            d.FunList.Add(new D3Fun(p2, EnumD3.按1234LRMS)); 
        }

        private void Kh_OnKeyDownEvent(object? sender, KeyEventArgs e)
        {
            d.ProcessKeys(e.KeyData);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            d.ProcessKeys(Keys.LControlKey|Keys.Divide);
 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            d.StopAll();

        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {

        }

        private void tsbfadd_Click(object sender, EventArgs e)
        {
            D3Config d3Config=new D3Config();
            FrmAddConfig f=new FrmAddConfig(d3Config);
            if(f.ShowDialog()== DialogResult.OK)
            {
                UserFun.BindData(this.tbfun, d3Config);
            }
        }
    }
}