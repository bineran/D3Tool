
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMTools.Control
{
    public class D3Main
    {
        public D3Main() { }
        public List<D3Fun> FunList { get; set; }=new List<D3Fun>();
        public Keys StopAllKey { get; set; }
        
        public void StopAll(Keys keys)
        {
            if (keys == StopAllKey && FunList!= null)
            {
                foreach (var f in FunList)
                {
                    f.Stop();
                }
            }
        }
    }
}
