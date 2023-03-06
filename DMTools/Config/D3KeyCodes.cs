﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools
{

    public class D3KeyCodes
    {
        [KeyName("1")]
        public int Key1 { get; set; } = 49;
        [KeyName("2")]
        public int Key2 { get; set; } = 50;

        [KeyName("3")]
        public int Key3 { get; set; } = 51;
        [KeyName("4")]
        public int Key4 { get; set; } = 52;
        [KeyName("移动键")]
        public int KeyMove { get; set; } = 87;
        [KeyName("原地站立键")]
        public int KeyStand { get; set; } = 16;
        [KeyName("暂停键")]
        /// <summary>
        /// 
        /// </summary>
        public int KeyPause { get; set; } = 32;
        [KeyName("喝药键")]
        /// <summary>
        /// 药
        /// </summary>
        public int KeyDrug { get; set; } = 81;
        /**
         * key_str     虚拟键码

"1",          49

"2",          50

"3",          51

"4",          52

"5",          53

"6",          54

"7",          55

"8",          56

"9",          57

"0",          48

"-",          189

"=",          187

"back",       8

 

"a",          65

"b",          66

"c",          67

"d",          68

"e",          69

"f",          70

"g",          71

"h",          72

"i",          73

"j",          74

"k",          75

"l",          76

"m",          77

"n",          78

"o",          79

"p",          80

"q",          81

"r",          82

"s",          83

"t",          84

"u",          85

"v",          86

"w",          87

"x",          88

"y",          89

"z",          90

 

"ctrl",       17

"alt",        18

"shift",      16

"win",        91

"space",      32

"cap",        20

"tab",        9

"~",          192

"esc",        27

"enter",      13

 

"up",         38

"down",       40

"left",       37

"right",      39

 

"option",     93

 

"print",      44

"delete",     46

"home",       36

"end",        35

"pgup",       33

"pgdn",       34

 

"f1",         112

"f2",         113

"f3",         114

"f4",         115

"f5",         116

"f6",         117

"f7",         118

"f8",         119

"f9",         120

"f10",        121

"f11",        122

"f12",        123

 

"[",          219

"]",          221

"\\",         220

";",          186

"'",          222

",",          188

".",          190

"/",          191
**/
    }
    public class KeyNameAttribute : Attribute
    {
        public KeyNameAttribute(string name) { this.Name = name; }
        public string Name { get; set; }
    }

}
