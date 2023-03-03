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
            Dm.dmsoftClass objdm = null;

             d = new D3Main();
            d.FunList.Add(new D3Fun()
            {
                d3FunSetting = new Config.D3FunSetting(),
                funList = new List<Config.ID3Function>() {
                    //new TestA(objdm, (int)this.Handle),
                    new TestB(objdm, (int)this.Handle)}
            }); ;
        }
      

        private void button1_Click(object sender, EventArgs e)
        {

            d.FunList[0].Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            d.FunList[0].Stop();

        }
    }
}