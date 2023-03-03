﻿using KeyHelper.FunList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace KeyHelper.Common
{
    public class D3Fun
    {
        public D3Fun() { }


        public void StartBefore()
        {
            foreach (var f in funList)
            {
                f.StartBefore(d3FunSettings);
            }
        }
        public void Start()
        {
            foreach (var f in funList)
            {
                f.Start();
            }
        }
        public void Stop()
        {
            foreach (var f in funList)
            {
                f.Stop();
            }
        }
        
 
        public List<ID3Function> funList=new List<ID3Function>();
        public D3FunSetting d3FunSettings { get; set; }=new D3FunSetting();
        public Keys HotKey
        {
           get{ return d3FunSettings.HotKey; }
        }
    }
}
