/*⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵
 ☠ ©2020 CallPalCatGames. All rights reserved.                                                                        
 ⚓ Author: Sky_Allen                                                                                                                  
 ⚓ Email: 894982165@qq.com                                                                                                  
 ⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵*/

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;
using  XLua;

namespace CallPalCatGames.Utility
{
    /// <summary>
    ///     这是一个杂项库。
    /// </summary>
    [LuaCallCSharp]
    public class Utility
    {
        /// <summary>
        ///     时间类型
        /// </summary>
        public enum TimeType
        {
            Null,
            Second,
            Minute,
            Hour
        }

        /// <summary>
        ///     根据Long得到枚举
        /// </summary>
        /// <param name="val"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetEnumByLong<T>(long val) where T : Enum
        {
            return (T) Enum.Parse(typeof(T), val.ToString());
        }

        /// <summary>
        ///     打印集合每一项
        /// </summary>
        /// <param name="collection"></param>
        /// <typeparam name="T"></typeparam>
        public static void LogNormalCollectionItem<T>(ICollection<T> collection)
        {
            foreach (var item in collection) Log.Log.LogNormal(item);
        }

        /// <summary>
        ///     交换
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <typeparam name="T"></typeparam>
        public static void Swap<T>(ref T a, ref T b) where T : struct
        {
            var temp = a;
            a = b;
            b = temp;
        }

        /// <summary>
        ///     获取集合里随机的东西
        /// </summary>
        /// <param name="container"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetArrayRandomItem<T>(ICollection<T> container)
        {
            var random = new Random();
            var randomIndex = random.Next(container.Count);
            return container.ElementAt(randomIndex);
        }

        /// <summary>
        ///     显示概率百分比数值。
        /// </summary>
        /// <param name="birthProbability"></param>
        /// <returns></returns>
        public static string GetProbabilityPercentage(double birthProbability)
        {
            var tempValue = (int) (birthProbability * 100);
            if (tempValue > 100 || tempValue < 0) Log.Log.LogError("概率是非法值");
            return $"{tempValue}%";
        }

        /// <summary>
        ///     获取鼠标位置的世界坐标。
        /// </summary>
        /// <returns></returns>
        public static Vector3 GetCursorPos()
        {
            var cursorPos = Camera.main.ScreenToWorldPoint(new Vector3(
                Input.mousePosition.x,
                Input.mousePosition.y,
                -Camera.main.transform.position.z
            ));
            return cursorPos;
        }

        public static string GetVersionTrans(int Version)
        {
            string tempV = Version.ToString();
            List<string> list = new List<string>();
            foreach (var cha in tempV)
            {
                list.Add(cha.ToString());
            }

            return string.Join(".", list);
        }

        public static string GetRatio(float res)
        {
            return $"{res * 100}%";
        }
    }
}