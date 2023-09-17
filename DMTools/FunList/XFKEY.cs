using DMTools.libs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using DMTools.Config;
using DMTools.Control;
//using Dm;
using Idmsoft = DMTools.libs.Idmsoft;
namespace DMTools.FunList
{

    [KeyName("建议1920*1080 循环按顺序执行")]
    public class XFKEY : BaseD3
    {
       public  const EnumD3 enumD3Name = EnumD3.循环按顺序执行;

       private SortedList<int, BagPoint> bagPointList=new SortedList<int, BagPoint>();

        public XFKEY(D3Param d3Param, List<KeyTimeSetting> Times, EnumD3 enumD3) : base(d3Param, Times,enumD3)
        {
            this.StartEvent += D3FJ_StartEvent;

        }

     
        private void D3FJ_StartEvent()
        {
            while (!cs.IsCancellationRequested)
            {
                StartKeyRank();
            }
           
        }
 

       
    }
    
 
}
