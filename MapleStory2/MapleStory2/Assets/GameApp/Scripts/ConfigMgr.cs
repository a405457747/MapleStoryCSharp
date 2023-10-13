using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using LitJson;

namespace MapleStory2
{
    public class ConfigMgr : MonoBehaviour
    {

        private void Awake()
        {

        }

        private void Start()
        {

        }

        void Sample()
        {
            AppJson a = JsonLoad<AppJson>("Text/AppJson");
            print(a.charDict["a"]);
            foreach (var kv in a.charDict)
            {
                 Debug.LogFormat("k is {0} v is {1} v type is {2}",kv.Key,kv.Value,kv.Value.GetType());
            }
        }

        internal T JsonLoad<T>(string jsonPath)
        {
            string t = Resources.Load<TextAsset>(jsonPath).text;
            return JsonMapper.ToObject<T>(t);
        }
    }
}