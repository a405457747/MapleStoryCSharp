using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MapleStory
{

    public class LogNote
    {
        public static int LogLv = 4;
        
        private static string LogPrint(params object[] paras)
        {
            var res = "";
            for (var i = 0; i < paras.Length; i++)
            {
                var obj = paras[i];

                res = $"{res}{obj} ";
            }

            return res;
        }

        public static string DictStr<Tk, Tv>(IDictionary<Tk, Tv> dic)
        {
            var res = "";
            foreach (var kv in dic)
            {
                var item = $"{kv.Key}:{kv.Value}";
                res = $"{res}{item},";
            }

            return "{" + res.Substring(0, res.Length - 1) + "}";
        }


        
        public static string ListStr<T>(IEnumerable<T> collection)
        {
            return "[" + string.Join(",", collection) + "]";
        }
		
		    public static string AnonymousObjStr(object obj)
    {
        var res = "";
        foreach (System.Reflection.PropertyInfo p in obj.GetType().GetProperties())
        {
        
            var n = p.Name;
            var v = p.GetValue(obj);
            string item = $"{n}={v}";
            res = $"{res}{item},";
        }
        return "{" + res.Substring(0, res.Length - 1) + "}";
    }

        public static string ColorLabel(string content, string color = "green")
        {
            return $"<color={color}>{content}</color>";
        }

        public static void Print<T>(params object[] paras)
        {
            var res = "";
            for (var i = 0; i < paras.Length; i++)
            {
                var obj = paras[i];
                if (obj is IEnumerable<T>)
                {
                    obj = ListStr<T>(obj as IEnumerable<T>);
                }
                else if (obj is IDictionary<string, T>)
                {
                    obj = DictStr<string, T>(obj as IDictionary<string, T>);
                }
                
                res = $"{res}{obj} ";// 如果被调用，产生两个空格。
            }

            Debug(res);
        }

        public static void Print(params object[] paras)
        {
            var res = "";
            for (var i = 0; i < paras.Length; i++)
            {
                var obj = paras[i];

                res = $"{res}{obj} ";
            }
            
            Debug(res);
        }

        public static void Debug(params object[] paras)
        {
            var res = $"{ColorLabel("[Debug] ", "#38B0DE")}{LogPrint(paras)}";
           if(LogLv>=4) UnityEngine.Debug.Log(res);
        }

        public static void Info(params object[] paras)
        {
            var res = $"{ColorLabel("[Info] ", "#66ff00")}{LogPrint(paras)}";
           if(LogLv>=3) UnityEngine.Debug.Log(res);
        }

        public static void Warning(params object[] paras)
        {
            var res = $"{ColorLabel("[Warning] ", "#FFFF00")}{LogPrint(paras)}";
            if(LogLv>=2)  UnityEngine.Debug.Log(res);
        }

        public static void Error(params object[] paras)
        {
            var res = $"{ColorLabel("[Error] ", "#FF0000")}{LogPrint(paras)}";
            if(LogLv>=1) UnityEngine.Debug.Log(res);
        }

        public static void Critical(params object[] paras)
        {
            var res = $"{ColorLabel("[Critical] ", "#FF0000")}{LogPrint(paras)}";
            if(LogLv>=0)  UnityEngine.Debug.Log(res);
            throw new Exception();
        }

        public static void Assert(bool condition, string debugStr = "")
        {
            if (condition == false) Critical(debugStr);
        }
    }
}