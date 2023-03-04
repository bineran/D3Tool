
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private Tuple<D3Fun, D3Fun> GetNextAndRunFun(List<D3Fun> d3Funs)
        {
            
            var runIndex = -1;
            for (int i = 0; i < d3Funs.Count; i++)
            {
                var fun = d3Funs[i];
                if (fun.RunState)
                {
                    runIndex = i;
                    break;
                }
            }
            D3Fun runFun= null;
            if (runIndex > -1)
            {
                runFun= d3Funs[runIndex];
            }
            runIndex++;
            int nextIndex= runIndex;
            if (nextIndex == d3Funs.Count)
            {
                nextIndex = 0;
            }
            return new Tuple<D3Fun, D3Fun>(d3Funs[nextIndex], runFun);
        }
        private void StartAndStop(D3Fun fun)
        {
            if (fun.StartBeforeStopOther)
            {
                foreach (var f in this.FunList)
                {
                    if (f != fun)
                    {
                        if (f.OtherStopFlag)
                        {
                            if (f.RunState)
                                f.Stop();
                        }
                    }
                }
            }
            fun.StartAndStop();
        }
        public bool ProcessKeys(Keys keys)
        {
            var tmpList = this.FunList.Where(r => r.HotKey1 == keys || r.HotKey2 == keys).ToList();
            if (tmpList.Count == 0)
                return false;

            if (tmpList.Count == 1)
            {
                StartAndStop(tmpList[0]);
              
            }
            else
            {
                var tu=GetNextAndRunFun(tmpList);
                if (tu != null)
                {
                    if(tu.Item2!=null)
                        tu.Item2.Stop();
                    tu.Item1.Start();

                }

            }
            return true;
  
        }
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
