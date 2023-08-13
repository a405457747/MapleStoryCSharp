using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

namespace MapleStory
{
    public interface IJSON<T>
    {
        string ToJson(object obj, bool prettyPrint = false);

        T FromJson(string json);
    }

    public class ListJson<T> : IJSON<T>
    {
        public T FromJson(string json)
        {
            return JsonMapper.ToObject<T>(json);
        }

        public string ToJson(object obj, bool prettyPrint = false)
        {
            return JsonMapper.ToJson(obj);
        }
    }

    public class UnityJson<T> : IJSON<T>
    {
        public T FromJson(string json)
        {
            return JsonUtility.FromJson<T>(json);
        }

        public string ToJson(object obj, bool prettyPrint)
        {
            string json = JsonUtility.ToJson(obj, prettyPrint);
            return json;
        }
    }

    public static class JsonHelper<T>
    {
        public static IJSON<T> CurTool = new ListJson<T>();
    }
}