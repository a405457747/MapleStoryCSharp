using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MapleStory2
{
    //代替原先的helper
    public  class ToolRoot 
    {
        public static T FindComponent<T>(MonoBehaviour mono, string targetName) where T : Component
        {

            Transform FindTrans(Transform aParent, string aName)
            {
                foreach (Transform child in aParent)
                {
                    if (child.name == aName) return child;

                    if (child.name.Contains("GameObject") && (child.name.StartsWith("GameObject") == false))
                    {
                    }
                    else
                    {
                        var result = FindTrans(child, aName);
                        if (result != null) return result;
                    }
                }

                return null;
            }

            var t = FindTrans(mono.transform, targetName);
            return t != null ? t.GetComponent<T>() : default;
        }
    }
}