﻿//using Dm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMTools.libs
{
    public class DMP
    {
        public static Idmsoft CreateDM()
        {
            var obj = Type.GetTypeFromProgID("dm.dmsoft");
            return Activator.CreateInstance(obj) as Idmsoft;

        }
        private Type obj = null;
        private object obj_object = null;
        public Idmsoft DM { get; set; }
        public DMP()
        {
            obj = Type.GetTypeFromProgID("dm.dmsoft");
            obj_object= Activator.CreateInstance(obj);
            DM= obj_object as Idmsoft;

        }
        public void ReleaseObj()
        {
            //if (obj_object != null)
            //{
            //    Marshal.ReleaseComObject(obj_object);
            //    obj_object = null;
            //}
        }

        ~DMP()
        {
            ReleaseObj();
        }
    }
}
