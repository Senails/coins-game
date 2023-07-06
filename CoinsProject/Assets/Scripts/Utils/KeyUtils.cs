using System.Collections;
using System.Collections.Generic;
using UnityEngine;
static class KeyUtils {
    public static Dictionary<KeyCode,string> KyeDictionary = new Dictionary<KeyCode,string>{
        { KeyCode.Alpha0 , "0"},
        { KeyCode.Alpha1 , "1"},
        { KeyCode.Alpha2 , "2"},
        { KeyCode.Alpha3 , "3"},
        { KeyCode.Alpha4 , "4"},
        { KeyCode.Alpha5 , "5"},
        { KeyCode.Alpha6 , "6"},
        { KeyCode.Alpha7 , "7"},
        { KeyCode.Alpha8 , "8"},
        { KeyCode.Alpha9 , "9"},
        { KeyCode.Minus , "-"},
        { KeyCode.Equals , "="},

        { KeyCode.Q , "Q"},
        { KeyCode.W , "W"},
        { KeyCode.E , "E"},
        { KeyCode.R , "R"},
        { KeyCode.T , "T"},
        { KeyCode.Y , "Y"},
        { KeyCode.U , "U"},
        { KeyCode.I , "I"},
        { KeyCode.O , "O"},
        { KeyCode.P , "P"},
        { KeyCode.LeftBracket , "["},
        { KeyCode.RightBracket , "]"},

        { KeyCode.A , "A"},
        { KeyCode.S , "S"},
        { KeyCode.D , "D"},
        { KeyCode.F , "F"},
        { KeyCode.G , "G"},
        { KeyCode.H , "H"},
        { KeyCode.J , "J"},
        { KeyCode.K , "K"},
        { KeyCode.L , "L"},
        { KeyCode.Semicolon , ":"},
        { KeyCode.DoubleQuote , "\""},

        { KeyCode.Z , "Z"},
        { KeyCode.X , "X"},
        { KeyCode.C , "C"},
        { KeyCode.V , "V"},
        { KeyCode.B , "B"},
        { KeyCode.N , "N"},
        { KeyCode.M , "M"},
        { KeyCode.Comma , ","},
        { KeyCode.Period , "."},
        { KeyCode.Slash , "/"},

        { KeyCode.Space , "Space"},
        { KeyCode.LeftAlt , "Alt"},
        { KeyCode.RightAlt , "Alt"},
    };

    public static List<KeyCode> GetPressedKeys(){
        List<KeyCode> list = new List<KeyCode>();

        foreach(var elem in KyeDictionary){
            if (Input.GetKey(elem.Key)){
                list.Add(elem.Key);
            }
        }

        return list;
    }

    public static string GetNameKey(KeyCode code){
        return KyeDictionary[code];
    }
}