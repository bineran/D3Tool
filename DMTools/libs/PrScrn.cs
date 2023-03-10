using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.libs
{

    public class PrScrnHelper
    {
        //public  delegate int PrScrn();
        //public void PrScrnNew(string path)
        //{
        //    var hModeule = LoadLibrary(path);
        //    IntPtr intPtr = GetProcAddress(hModeule, "PrScrn");
        //    PrScrn fn =(PrScrn) Marshal.GetDelegateForFunctionPointer(intPtr, typeof(PrScrn));
        //    fn();
        //}
        //[DllImport("kernel32.dll",EntryPoint= "LoadLibrary")]
        //public static extern Int32 LoadLibrary([MarshalAs(UnmanagedType.LPStr)] string dllpath);
        //[DllImport("kernel32.dll", EntryPoint = "GetProcAddress")]
        //public static extern IntPtr GetProcAddress(Int32 hModule, [MarshalAs(UnmanagedType.LPStr)] string procName);

        [DllImport("./libs/PrScrn.dll", EntryPoint = "PrScrn")]

        public extern static int PrScrn();//与dll中一致   
    }
}
