using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson.Extensions;


namespace MapleStory2
{
    [CreateAssetMenu(fileName = "AppJson", menuName = "ColaFramework/AppJson")]
    public class AppJson : ScriptableObject
    {
        public bool isRelease;
        public Dictionary<string, int> charDict=new Dictionary<string, int>()
        {
            {"a",1},
            {"b",2},
             {"c",3},
        };
        public Vector2 v2;
        public float nk;
    }
}