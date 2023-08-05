/*⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵
 ☠ ©2020 CallPalCatGames. All rights reserved.                                                                        
 ⚓ Author: Sky_Allen                                                                                                                  
 ⚓ Email: 894982165@qq.com                                                                                                  
 ⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵*/

using UnityEngine;
using XLua;

namespace CallPalCatGames.Utility
{
    /// <summary>
    ///     Unity的拓展方法，主要作用在Transform。
    /// </summary>
    [LuaCallCSharp]
    public static class UnityExpand
    {
        /// <summary>
        ///     递归的获取组件，需要主要获取到第一个递归就终止了，默认是从上到下的第一个哦。
        /// </summary>
        /// <param name="parentTrans">组件所在对象的祖先的Transform</param>
        /// <param name="targetName">名字</param>
        /// <typeparam name="T">组件类型</typeparam>
        /// <returns></returns>
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
            if (res == null) Log.Log.LogError("没有找到目标的Transform和目标没有挂载该组件两种错误的排列组合。");
            return res;
        }

        public static Color GetColor(this MonoBehaviour monoBehaviour, float r, float g, float b, float a)
        {
            return new Color(r / 255f, g / 255f, b / 255f, a / 255f);
        }

        #region selfObj的链式拓展

        public static T Name<T>(this T selfObj, string name) where T : Object
        {
            selfObj.name = name;
            return selfObj;
        }

        #endregion

        #region selfComponent的链式拓展

        public static T LocalIdentity<T>(this T selfComponent) where T : Component
        {
            var transform = selfComponent.transform;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
            return selfComponent;
        }

        public static T Parent<T>(this T selfComponent, Component parentComponent) where T : Component
        {
            selfComponent.transform.SetParent(parentComponent == null ? null : parentComponent.transform);
            return selfComponent;
        }

        #endregion
    }
}