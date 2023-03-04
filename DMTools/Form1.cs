using DMTools.Config;
using DMTools.Control;
using DMTools.FunList;

namespace DMTools
{
    public partial class Form1 : Form
    {
        D3Main d;
        public Form1()
        {
            InitializeComponent();
            KeyboardHook kh = new KeyboardHook();
            kh.OnKeyDownEvent += Kh_OnKeyDownEvent;
            Dm.dmsoft objdm = new Dm.dmsoft();
            //dt.Rows.Add("Ð¡¼üÅÌ/", 111);
            //dt.Rows.Add("Ð¡¼üÅÌ*", 106);
            //dt.Rows.Add("Ð¡¼üÅÌ-", 109);
            //dt.Rows.Add("Ð¡¼üÅÌ+", 107);
            d = new D3Main();
            d.FunList.Add(new D3Fun()
            {
                d3FunSetting = new Config.D3FunSetting() { HotKey1=Keys.LControlKey| Keys.Divide },
                funList = new List<Config.ID3Function>() {
                    //new TestA(objdm, (int)this.Handle),
                    new TestB(objdm, (int)this.Handle)}
            }); ;
            d.FunList.Add(new D3Fun()
            {
                d3FunSetting = new Config.D3FunSetting() { HotKey1 = Keys.LControlKey | Keys.Divide  },
                funList = new List<Config.ID3Function>() {
                    new TestA(objdm, (int)this.Handle)}
            }); ;
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
            d.FunList[0].Stop();

        }


    }
}