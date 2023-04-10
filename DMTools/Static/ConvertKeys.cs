using DMTools.Static;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools
{
    public static class ConvertKeys
    {
        public const Keys MouseLeft = Keys.Control | Keys.Left;
        public const Keys MouseRight = Keys.Control | Keys.Right;
        public const Keys MouseShiftLeft = Keys.Shift | Keys.Left;
        public const Keys MouseShiftRight = Keys.Shift | Keys.Right;
        public const Keys HotKeyMouseUp = Keys.Control | Keys.PageUp;
        public const Keys HotKeyMouseDown = Keys.Control | Keys.PageDown;
        public const Keys HotKeyDebug = Keys.OemClear;
    
        public const Keys HotKeyRightUp = Keys.Right | Keys.Alt;
        public const Keys HotKeyRightDown = Keys.Right | Keys.Alt ;
        public const Keys HotKeyRightWhereShiftDown = Keys.Control| Keys.Right | Keys.Alt;
        public const Keys HotKeyLeftWhereShiftDown = Keys.Control | Keys.Left | Keys.Alt;
        public const Keys HotKeyLeftUp = Keys.Left | Keys.Alt ;
        public const Keys HotKeyLeftDown = Keys.Left | Keys.Alt ;
        public static List<Keys> MouseKeys { get; set; } = new List<Keys>();
        static ConvertKeys()
        {
            InitMouseKeys();
        }
        public static void InitMouseKeys()
        {
            MouseKeys= new List<Keys>();
            var ps = typeof(ConvertKeys).GetFields();
            foreach (var p in ps)
            {
                if (p.FieldType == typeof(Keys))
                {
                    var k = (Keys)p.GetRawConstantValue();
                    MouseKeys.Add(k);
                }
            }

        }
        public static bool NoMouseKey(Keys key)
        {
            return !MouseKeys.Any(x => x == key);
        }
    }
    
}
