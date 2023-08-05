using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MapleStory {
public interface IJSON<T>{

        string ToJson(object obj, bool prettyPrint=false);

        T FromJson(string json);
    }


    public class UnityJson<T> : IJSON<T>
    {
        public T FromJson(string json)
        {
            return JsonUtility.FromJson<T>(json);
        }

        public string ToJson(object obj, bool prettyPrint)
        {
            string json = JsonUtility.ToJson(obj,prettyPrint);
            return json;
        }
    }

    public static class JsonTool<T>
    {
        public static IJSON<T> CurTool = new UnityJson<T>();
    }
}
