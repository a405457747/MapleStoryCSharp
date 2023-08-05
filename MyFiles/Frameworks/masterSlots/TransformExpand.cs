using System;
using UnityEngine;

public static class TransformExpand
{
    public static Transform[] GetChildArray(this Transform t)
    {
        var res = new Transform[t.childCount];

        for (var i = 0; i < res.Length; i++) res[i] = t.GetChild(i);

        return res;
    }

    public static Transform LocalReset(this Transform t)
    {
        t.localPosition = Vector3.zero;
        t.localRotation = Quaternion.identity;
        t.localScale = Vector3.one;
        return t;
    }

    public static T FindRecursion<T>(this Transform parentTrans, string targetName) where T : Component
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
    }

    public static RectTransform GetRect(this Transform trans)
    {
        return trans as RectTransform;
    }
}