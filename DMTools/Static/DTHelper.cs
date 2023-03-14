using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.Static
{
    public enum DataTableType
    {
        None,
        Key,
        HotKey
    }
    public sealed class DTHelper
    {
        
        public static SortedList<DataTableType, DataTable> TableList { get; set; } = new SortedList<DataTableType, DataTable>();
        static DTHelper()
        {
            TableList.Add(DataTableType.None, new DataTable());
            TableList.Add(DataTableType.Key, GetKey2Dt());
            TableList.Add(DataTableType.HotKey, GetKey1Dt());
        }

        private static DataTable GetKey1Dt()
        {


            DataTable dt = new DataTable();
            dt.Columns.Add("KeyName");
            dt.Columns.Add("KeyCode", typeof(Keys));
            #region code
            dt.Rows.Add("向下滚", Keys.Control | Keys.PageDown);
            dt.Rows.Add("向上滚", Keys.Control | Keys.PageUp);
            dt.Rows.Add("左键按下", ConvertKeys.HotKeyLeftDown);
            dt.Rows.Add("右键按下", ConvertKeys.HotKeyRightDown);
            dt.Rows.Add("~", 192);
            dt.Rows.Add("G1(ctrl + / )", Keys.Control | Keys.Divide);
            dt.Rows.Add("G2(ctrl + * )", Keys.Control | Keys.Multiply);
            dt.Rows.Add("G3(ctrl + - )", Keys.Control | Keys.Subtract);
            dt.Rows.Add("G4(ctrl + + )", Keys.Control | Keys.Add);
            #region code

            dt.Rows.Add("1", 49);
            dt.Rows.Add("2", 50);
            dt.Rows.Add("3", 51);
            dt.Rows.Add("4", 52);
            dt.Rows.Add("5", 53);
            dt.Rows.Add("6", 54);
            dt.Rows.Add("7", 55);
            dt.Rows.Add("8", 56);
            dt.Rows.Add("9", 57);
            dt.Rows.Add("~", 192);
            dt.Rows.Add("小键盘/", 111);
            dt.Rows.Add("小键盘*", 106);
            dt.Rows.Add("小键盘-", 109);
            dt.Rows.Add("小键盘+", 107);
            dt.Rows.Add("0", 48);
            dt.Rows.Add("A", 65);
            dt.Rows.Add("B", 66);
            dt.Rows.Add("C", 67);
            dt.Rows.Add("D", 68);
            dt.Rows.Add("E", 69);
            dt.Rows.Add("F", 70);
            dt.Rows.Add("G", 71);
            dt.Rows.Add("H", 72);
            dt.Rows.Add("I", 73);
            dt.Rows.Add("J", 74);
            dt.Rows.Add("K", 75);
            dt.Rows.Add("L", 76);
            dt.Rows.Add("M", 77);
            dt.Rows.Add("N", 78);
            dt.Rows.Add("O", 79);
            dt.Rows.Add("P", 80);
            dt.Rows.Add("Q", 81);
            dt.Rows.Add("R", 82);
            dt.Rows.Add("S", 83);
            dt.Rows.Add("T", 84);
            dt.Rows.Add("U", 85);
            dt.Rows.Add("V", 86);
            dt.Rows.Add("W", 87);
            dt.Rows.Add("X", 88);
            dt.Rows.Add("Y", 89);
            dt.Rows.Add("Z", 90);
            dt.Rows.Add("CTRL", 17);
            dt.Rows.Add("ALT", 18);
            dt.Rows.Add("SHIFT", 16);
            dt.Rows.Add("WIN", 91);
            dt.Rows.Add("空格", 32);
            dt.Rows.Add("CAP", 20);
            dt.Rows.Add("TAB", 9);

            dt.Rows.Add("UP", 38);
            dt.Rows.Add("DOWN", 40);
            dt.Rows.Add("LEFT", 37);
            dt.Rows.Add("RIGHT", 39);

            dt.Rows.Add("PrintScreen", Keys.PrintScreen);
            dt.Rows.Add("Pause", Keys.Pause);
            
            dt.Rows.Add("HOME", 36);
            dt.Rows.Add("END", 35);
            dt.Rows.Add("PGUP", 33);
            dt.Rows.Add("PGDN", 34);
            dt.Rows.Add("F1", 112);
            dt.Rows.Add("F2", 113);
            dt.Rows.Add("F3", 114);
            dt.Rows.Add("F4", 115);
            dt.Rows.Add("F5", 116);
            dt.Rows.Add("F6", 117);
            dt.Rows.Add("F7", 118);
            dt.Rows.Add("F8", 119);
            dt.Rows.Add("F9", 120);
            dt.Rows.Add("F10", 121);
            dt.Rows.Add("F11", 122);
            dt.Rows.Add("F12", 123);
            dt.Rows.Add("Num0", 96);
            dt.Rows.Add("Num1", 97);
            dt.Rows.Add("Num2", 98);
            dt.Rows.Add("Num3", 99);
            dt.Rows.Add("Num4", 100);
            dt.Rows.Add("Num5", 101);
            dt.Rows.Add("Num6", 102);
            dt.Rows.Add("Num7", 103);
            dt.Rows.Add("Num8", 104);
            dt.Rows.Add("Num9", 105);

            #endregion

            #endregion
            return dt;
        }
        private static DataTable GetKey2Dt()
        {


            DataTable dt = new DataTable();
            dt.Columns.Add("KeyName");
            dt.Columns.Add("KeyCode", typeof(Keys));
            #region code
            dt.Rows.Add("调试", ConvertKeys.HotKeyDebug);
            dt.Rows.Add("1", 49);
            dt.Rows.Add("2", 50);
            dt.Rows.Add("3", 51);
            dt.Rows.Add("4", 52);
            dt.Rows.Add("W", 87);
            dt.Rows.Add("左键", ConvertKeys.MouseLeft);
            dt.Rows.Add("右键", ConvertKeys.MouseRight);
            dt.Rows.Add("原地左键", ConvertKeys.MouseShiftLeft);
            dt.Rows.Add("左键↓shift↑", ConvertKeys.HotKeyLeftWhereShiftDown);
            dt.Rows.Add("右键↓shift↑", ConvertKeys.HotKeyRightWhereShiftDown);

            dt.Rows.Add("Q", 81);
            dt.Rows.Add("E", 69);
            dt.Rows.Add("R", 82);

            dt.Rows.Add("空格", 32);


            dt.Rows.Add("5", 53);
            dt.Rows.Add("6", 54);
            dt.Rows.Add("7", 55);
            dt.Rows.Add("8", 56);
            dt.Rows.Add("9", 57);
            dt.Rows.Add("~", 192);
            dt.Rows.Add("小键盘/", 111);
            dt.Rows.Add("小键盘*", 106);
            dt.Rows.Add("小键盘-", 109);
            dt.Rows.Add("小键盘+", 107);
            dt.Rows.Add("0", 48);
            dt.Rows.Add("A", 65); 
            dt.Rows.Add("B", 66); 
            dt.Rows.Add("C", 67); 
            dt.Rows.Add("D", 68); 
           
            dt.Rows.Add("F", 70); 
            dt.Rows.Add("G", 71); 
            dt.Rows.Add("H", 72); 
            dt.Rows.Add("I", 73); 
            dt.Rows.Add("J", 74); 
            dt.Rows.Add("K", 75); 
            dt.Rows.Add("L", 76); 
            dt.Rows.Add("M", 77); 
            dt.Rows.Add("N", 78); 
            dt.Rows.Add("O", 79);
            dt.Rows.Add("P", 80);
         
           
            dt.Rows.Add("S", 83); 
            dt.Rows.Add("T", 84); 
            dt.Rows.Add("U", 85); 
            dt.Rows.Add("V", 86); 
           
            dt.Rows.Add("X", 88); 
            dt.Rows.Add("Y", 89 ); 
            dt.Rows.Add("Z", 90);
            dt.Rows.Add("CTRL", 17);
            dt.Rows.Add("ALT", 18);
            dt.Rows.Add("SHIFT", 16);
            dt.Rows.Add("WIN", 91);
        
            dt.Rows.Add("CAP", 20);
            dt.Rows.Add("TAB", 9);

            dt.Rows.Add("UP", 38);
            dt.Rows.Add("DOWN", 40);
            dt.Rows.Add("LEFT", 37);
            dt.Rows.Add("RIGHT", 39);

            dt.Rows.Add("HOME", 36);
            dt.Rows.Add("END", 35);
            dt.Rows.Add("PGUP", 33);
            dt.Rows.Add("PGDN", 34);
            dt.Rows.Add("F1", 112);
            dt.Rows.Add("F2", 113);
            dt.Rows.Add("F3", 114);
            dt.Rows.Add("F4", 115);
            dt.Rows.Add("F5", 116);
            dt.Rows.Add("F6", 117);
            dt.Rows.Add("F7", 118);
            dt.Rows.Add("F8", 119);
            dt.Rows.Add("F9", 120);
            dt.Rows.Add("F10", 121);
            dt.Rows.Add("F11", 122);
            dt.Rows.Add("F12", 123);
            dt.Rows.Add("Num0", 96);
            dt.Rows.Add("Num1", 97);
            dt.Rows.Add("Num2", 98);
            dt.Rows.Add("Num3", 99);
            dt.Rows.Add("Num4", 100);
            dt.Rows.Add("Num5", 101);
            dt.Rows.Add("Num6", 102);
            dt.Rows.Add("Num7", 103);
            dt.Rows.Add("Num8", 104);
            dt.Rows.Add("Num9", 105);

            #endregion
            return dt;
        }
    }
}
