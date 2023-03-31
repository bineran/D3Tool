using DMTools.Config;
using DMTools.Control;
using DMTools.libs;
using DMTools.Static;
using System.Threading;

namespace DMTools
{
    public partial class FormMain
    {
        public object lockObject { get; set; } = new object();
        DMP objDMP = new DMP();
        public SortedList<int, D3Main> sld3 = new SortedList<int, D3Main>();
        public Idmsoft objdm
        {
            get { return objDMP.DM; }
        }
        public List<Keys> GetALLHotKey(D3Config d3Config)
        {
            List<Keys> keys = new List<Keys>();
            foreach (var item in d3Config.d3ConfigItems)
            {
                if (!item.EnabledFlag) { continue; }
                if (!keys.Contains(item.HotKey1))
                    keys.Add(item.HotKey1);
                if (!keys.Contains(item.HotKey2))
                    keys.Add(item.HotKey2);

            }
            var cks = d3Config.ConfigKeys.Where(r => Hotkey.HotKeys.Contains(r.KeyName));
            foreach (var ck in cks)
            {
                if (!keys.Contains(ck.KeyCode))
                    keys.Add(ck.KeyCode);
            }
            return keys;
        }


        public void RestD3Main(D3Config d3Config, int hd)
        {

            if (sld3.ContainsKey(hd))
            {
                sld3[hd].StopAll();
            }

            var d3Main = D3Main.BuildD3Main(d3Config, hd);
            if (d3Main != null)
            {
                lock (lockObject)
                {
                    if (!sld3.ContainsKey(hd))
                    {

                        sld3.Add(hd, d3Main);
                    }
                    else
                    {
                        sld3[hd] = d3Main;
                    }
                }
            }


        }
        public void RestD3Main(D3Config d3Config)
        {
            try
            {
      
                for (int i = 0; i < sld3.Count; i++)
                {
                    
                    RestD3Main(d3Config, sld3.Keys[i]);
                }
            }
            catch (Exception ex) { log.Error(ex); }
        }

        Tuple<int, bool, string> HDINFO()
        {
            var hd = objdm.GetMousePointWindow();
            LastHandle = hd;
            var isbl = sld3.ContainsKey(hd);
            var str = objdm.GetWindowClass(hd);
            return new Tuple<int, bool, string>(hd, isbl, str);
        }


    }
}
