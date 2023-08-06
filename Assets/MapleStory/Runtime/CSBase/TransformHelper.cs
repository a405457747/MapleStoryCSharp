using System;
using UnityEngine;

namespace MapleStory
{
    public static class TransformHelper
    {
        public static Transform[] GetChildArray(Transform t)
        {
            var res = new Transform[t.childCount];

            for (var i = 0; i < res.Length; i++) res[i] = t.GetChild(i);

            return res;
        }

        public static Transform LocalReset(Transform t)
        {
            t.localPosition = Vector3.zero;
            t.localRotation = Quaternion.identity;
            t.localScale = Vector3.one;
            return t;
        }

/*        public static T FindRecursion<T>(Transform parentTrans, string targetName) where T : Component
        {
            Transform GetTargetTrans(Transform parent, string target)
            {
                Transform tempTrans = null;
                tempTrans = parent.Find(target);
                if (tempTrans == null)
                    foreach (Transform child in parent)
                    {
                        tempTrans = GetTargetTrans(child, target);
                        if (tempTrans != null) break;
                    }

                return tempTrans;
            }

            T res = null;
            res = GetTargetTrans(parentTrans, targetName)?.GetComponent<T>();
            if (res == null) Debug.LogException(new NullReferenceException());
            return res;
        }*/

        public static RectTransform GetRect(Transform trans)
        {
            return trans as RectTransform;
        }


        public static RectTransform SetWidth(RectTransform rect, float x)
        {
            var sizeDelta = rect.sizeDelta;
            sizeDelta = new Vector2(x, sizeDelta.y);
            rect.sizeDelta = sizeDelta;
            return rect;
        }

        public static RectTransform SetHeight(RectTransform rect, float y)
        {
            var sizeDelta = rect.sizeDelta;
            sizeDelta = new Vector2(sizeDelta.x, y);
            rect.sizeDelta = sizeDelta;
            return rect;
        }

        public static RectTransform SetPosX(RectTransform rect, float val)
        {
            var anchoredPosition3D = rect.anchoredPosition3D;
            anchoredPosition3D = new Vector3(val, anchoredPosition3D.y, anchoredPosition3D.z);
            rect.anchoredPosition3D = anchoredPosition3D;
            return rect;
        }

        public static RectTransform SetPosY(RectTransform rect, float val)
        {
            var anchoredPosition3D = rect.anchoredPosition3D;
            anchoredPosition3D = new Vector3(anchoredPosition3D.x, val, anchoredPosition3D.z);
            rect.anchoredPosition3D = anchoredPosition3D;
            return rect;
        }
    }
}