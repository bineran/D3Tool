using System;
using System.Collections.Generic;
using System.Text;

namespace D3Tool
{
    public class D3Config
    {
        public const string KeyPath = "Config\\key.txt";
        public const string DefaultPath = "Config\\defaultPath.txt";
        public static string D3Class
        {
            get
            {

                if (D3Config.KEYS.GameClass == null || D3Config.KEYS.GameClass.Length == 0)
                {
                    return "D3 Main Window Class VVideoClass";
                }
                return KEYS.GameClass;
            }
        }
        public const string PlanPath = "Config\\Plan";
        static D3Config()
        {
            ReadKey();
            LoadPlanList();
        }

        public static List<T_Plan> alplan = new List<T_Plan>();
        public static void LoadPlanList()
        {
            var defaultfile = System.IO.File.ReadAllText(DefaultPath, Encoding.GetEncoding("gb2312"));
            alplan.Clear();
            PLAN = null;
            var strs = System.IO.Directory.GetFiles(PlanPath);
            foreach (var str in strs)
            {
                try
                {

                    var tp = ReadDefaultSetting(str);
                    alplan.Add(tp);
                    if (str == defaultfile)
                    {
                        PLAN = tp;
                    }
                }
                catch
                { }
            }
            if (PLAN == null && alplan.Count > 0)
                PLAN = alplan[0];
            if (PLAN == null)
            {
                PLAN = new T_Plan()
                {
                    Name = "默认",
                    Path = PlanPath + "\\默认.txt",
                    t1 = new T_Time()
                    {
                        fmode = EnumD3.不做操作
                    },
                    t2 = new T_Time()
                    {
                        fmode = EnumD3.不做操作
                    }
                };
                alplan.Add(PLAN);
            }


        }

        public static T_Plan ReadDefaultSetting(string _DefaultPath)
        {
            var objplan = new T_Plan();
            var str = "";
            try
            {

                str = System.IO.File.ReadAllText(_DefaultPath, Encoding.GetEncoding("gb2312"));
                objplan = str.FromJson<T_Plan>();
                return objplan;
            }
            catch
            {
                return null;
            }
        }
        private static void ReadKey()
        {
            KEYS = new T_Key();
            try
            {
                var str = System.IO.File.ReadAllText(KeyPath);
                KEYS = str.FromJson<T_Key>();
                //throw new Exception();
            }
            catch
            {
                KEYS = new T_Key()
                {
                    Key_FN1 = 112,
                    Key_FN2 = 113,
                    Key_FN3 = 114,
                    Key_FN4 = 115,
                    Key_Move = 32,
                    Key_Stand = 16,
                    Key_Start = 116,
                    Key_Stop = 117,
                    Key1 = 49,
                    Key2 = 50,
                    Key3 = 51,
                    Key4 = 52
                };
            }
        }
        public static T_Key KEYS { get; set; }
        public static T_Plan PLAN { get; set; }


        public static List<int> ALFN
        {
            get
            {
                List<int> alint = new List<int>();
                if (KEYS != null)
                {
                    alint.Add(KEYS.Key_FN1);
                    alint.Add(KEYS.Key_FN2);
                    alint.Add(KEYS.Key_FN3);
                    alint.Add(KEYS.Key_FN4);
                    alint.Add(KEYS.Key_FN10);
                    alint.Add(KEYS.Key_FN20);
                    alint.Add(KEYS.Key_FN30);
                    alint.Add(KEYS.Key_FN40);
                }
                return alint;
            }
        }
        public static bool IsMouseWheel
        {
            get
            {
                if (KEYS != null)
                {
                    if (KEYS.Key_FN1 == -1 || KEYS.Key_FN1 == -2)
                        return true;
                    if (KEYS.Key_FN2 == -1 || KEYS.Key_FN2 == -2)
                        return true;
                    if (KEYS.Key_FN3 == -1 || KEYS.Key_FN3 == -2)
                        return true;
                    if (KEYS.Key_FN4 == -1 || KEYS.Key_FN4 == -2)
                        return true;
                }
                return false;
            }
        }
        public static bool IsRight
        {
            get
            {
                if (KEYS != null)
                {
                    if (KEYS.Key_FN1 == -3)
                        return true;
                    if (KEYS.Key_FN2 == -3)
                        return true;
                    if (KEYS.Key_FN3 == -3)
                        return true;
                    if (KEYS.Key_FN4 == -3)
                        return true;
                }
                return false;
            }
        }
        public static bool IsLeft
        {
            get
            {
                if (KEYS != null)
                {
                    if (KEYS.Key_FN1 == -4)
                        return true;
                    if (KEYS.Key_FN2 == -4)
                        return true;
                    if (KEYS.Key_FN3 == -4)
                        return true;
                    if (KEYS.Key_FN4 == -4)
                        return true;
                }
                return false;
            }
        }


    }
}