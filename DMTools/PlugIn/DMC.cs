using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.PlugIn
{
    public class DMP
    {
        
        private Type obj = null;
        private object obj_object = null;
        public Idmsoft DM { get; set; }
        public DMP() {
            obj = Type.GetTypeFromProgID("dm.dmsoft");
            obj_object = Activator.CreateInstance(obj);
            DM = obj_object as Idmsoft;
            //var sss=DM.Ver();
        }
        public void ReleaseObj()
        {
            if (obj_object != null)
            {
                Marshal.ReleaseComObject(obj_object);
                obj_object = null;
            }
        }

        ~DMP()
        {
            ReleaseObj();
        }
    }
}
