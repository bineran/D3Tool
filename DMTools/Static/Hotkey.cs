using DMTools.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.Static
{
    public static class Hotkey
    {
        public const string HotKeyMouse = "MouseKey";
        public const string HotKeyStopAll = "StopAllKey";
        public const string HotKeyReplay = "Replay";
        public const string HotKeyPtrScr = "PtrScr";
        public const string HotKeyScrLk = "ScrLk";
        public const string HotKeyTestBind = "Bind";
        public static List<string> HotKeys { get; set; } = new List<string>();
        static Hotkey()
        {
            ConfigKeys = GetConfigKey();
            var ps=  typeof(Hotkey).GetFields();
            foreach (var p in ps)
            {
                if (p.Name.StartsWith("HotKey") && p.FieldType== typeof(string))
                {
                    HotKeys.Add(p.GetRawConstantValue().ToString());
                }
            }
            //HotKeys.Add(Hotkey.HotKeyStopAll);
            //HotKeys.Add(Hotkey.HotKeyMouse);
            //HotKeys.Add(Hotkey.HotKeyReplay);
            //HotKeys.Add(Hotkey.HotKeyPtrScr);
        }
        public static List<D3ConfigKey> ConfigKeys = new List<D3ConfigKey>();
        public static List<D3ConfigKey> GetConfigKey()
        {
            var ps = typeof(D3KeyCodes).GetProperties().Where(r => r.PropertyType == typeof(int));
            List<D3ConfigKey> _d3ConfigKeys = new List<D3ConfigKey>();
            foreach (var p in ps)
            {

                var ka = p.GetCustomAttribute(typeof(KeyNameAttribute)) as KeyNameAttribute;
                if (ka == null)
                    continue;
                D3KeyCodes keySetting = new D3KeyCodes();
                int keyCode = Convert.ToInt32(p.GetValue(keySetting));
                D3ConfigKey d = new D3ConfigKey();
                d.KeyInfo = ka.Name;
                d.KeyCode = (Keys)keyCode;
                d.KeyName = p.Name;
                _d3ConfigKeys.Add(d);
            }
            _d3ConfigKeys.Add(new D3ConfigKey() { KeyInfo = "获取鼠标处信息", KeyName = Hotkey.HotKeyMouse, KeyCode = Keys.Home });
            _d3ConfigKeys.Add(new D3ConfigKey() { KeyInfo = "停止所有功能", KeyName = Hotkey.HotKeyStopAll, KeyCode = Keys.End });
            _d3ConfigKeys.Add(new D3ConfigKey() { KeyInfo = "录制功能", KeyName = Hotkey.HotKeyReplay, KeyCode = Keys.Pause });
            _d3ConfigKeys.Add(new D3ConfigKey() { KeyInfo = "取图取色", KeyName = Hotkey.HotKeyScrLk, KeyCode = Keys.Scroll });
            _d3ConfigKeys.Add(new D3ConfigKey() { KeyInfo = "截屏", KeyName = Hotkey.HotKeyPtrScr, KeyCode = Keys.PrintScreen });
            _d3ConfigKeys.Add(new D3ConfigKey() { KeyInfo = "测试绑定", KeyName = Hotkey.HotKeyTestBind, KeyCode = Keys.F12 });
            return _d3ConfigKeys;

        }
        #region 系统api
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool RegisterHotKey(IntPtr hWnd, int id, HotkeyModifiers fsModifiers, Keys vk);

        [DllImport("user32.dll")]
        static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        #endregion

        /// <summary> 
        /// 注册快捷键 
        /// </summary> 
        /// <param name="hWnd">持有快捷键窗口的句柄</param> 
        /// <param name="fsModifiers">组合键</param> 
        /// <param name="vk">快捷键的虚拟键码</param> 
        /// <param name="callBack">回调函数</param> 
        public static void Register(IntPtr hWnd, HotkeyModifiers fsModifiers, Keys vk, HotKeyCallBackHanlder callBack)
        {
            int id = keyid++;
            if (!RegisterHotKey(hWnd, id, fsModifiers, vk))
                throw new Exception("regist hotkey fail.");
            keymap[id] = callBack;
        }

        /// <summary> 
        /// 注销快捷键 
        /// </summary> 
        /// <param name="hWnd">持有快捷键窗口的句柄</param> 
        /// <param name="callBack">回调函数</param> 
        public static void UnRegist(IntPtr hWnd)
        {
            foreach (KeyValuePair<int, HotKeyCallBackHanlder> var in keymap)
            {
                    UnregisterHotKey(hWnd, var.Key);
            }
        }

        /// <summary> 
        /// 快捷键消息处理 
        /// </summary> 
        public static void ProcessHotKey(System.Windows.Forms.Message m)
        {
            if (m.Msg == WM_HOTKEY)
            {
                int id = m.WParam.ToInt32();
                HotKeyCallBackHanlder callback;
                if (keymap.TryGetValue(id, out callback))
                {
                    callback();
                }
            }
        }

        const int WM_HOTKEY = 0x312;
        static int keyid = 10;
        static Dictionary<int, HotKeyCallBackHanlder> keymap = new Dictionary<int, HotKeyCallBackHanlder>();

        public delegate void HotKeyCallBackHanlder();
    }

    public enum HotkeyModifiers
    {
        MOD_ALT = 0x1,
        MOD_CONTROL = 0x2,
        MOD_SHIFT = 0x4,
        MOD_WIN = 0x8
    }
}
