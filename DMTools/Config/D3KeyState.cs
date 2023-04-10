using DMTools.Static;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.Config
{

    public class D3KeyState
    {
        public static SortedList<Keys, bool> slkeydata { get; set; } = new SortedList<Keys, bool>();
        static D3KeyState()
        {
            var dt = DTHelper.TableList[DataTableType.Key];
            foreach (DataRow dr in dt.Rows)
            {
                var keys = (Keys)dr["KeyCode"];

                slkeydata.Add(keys, false);


            }
        }
        public void SetState(Keys keys, bool value)
        {
            if (slkeydata.ContainsKey(keys))
            {
                slkeydata[keys] = value;
            }
        }
        public void SetPauseState(Keys keys, bool value)
        {
            if (slkeydata.ContainsKey(keys))
            {
                slkeydata[keys] = value;
                this.isPause = value;
            }
        }
        public bool this[int key]
        {
            get
            {
                return this[(Keys)key];
            }
        }

        public bool this[Keys k]
        {
            get
            {
                if (slkeydata.ContainsKey(k))
                {
                    return slkeydata[k];
                }
                return false;
            }
        }
        public bool isLeft { get { return FormMain.isLeft; } }
        public bool isRight { get { return FormMain.isRight; } }
        public bool isD3 { get; set; }
        public bool isPause { get; set; }

        public void Rest()
        {
            //this.isD3 = false;
            //this.isPause = false;
        }
    }
}
