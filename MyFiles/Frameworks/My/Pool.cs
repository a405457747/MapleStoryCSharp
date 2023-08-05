/*⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵
 ☠ ©2020 CallPalCatGames. All rights reserved.                                                                        
 ⚓ Author: Sky_Allen                                                                                                                  
 ⚓ Email: 894982165@qq.com                                                                                                  
 ⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵*/

using System;
using System.Collections.Generic;
using UnityEngine;
using XLua;

namespace CallPalCatGames.Pool
{
    /// <summary>
    ///     对象池
    /// </summary>
    /// <typeparam name="T">Mono或者是非Mono</typeparam>
    [LuaCallCSharp]
    public class Pool<T> where T : class, IPoolObj
    {
        private readonly Queue<T> _objs = new Queue<T>();

        /// <summary>
        ///     对象工厂类，这里运用了策略模式切换。
        /// </summary>
        private readonly IPoolFactory<T> _poolFactory;

        /// <summary>
        ///     传入对象的生产工厂类
        /// </summary>
        /// <param name="poolFactory"></param>
        public Pool(IPoolFactory<T> poolFactory)
        {
            _poolFactory = poolFactory;
        }

        /// <summary>
        ///     获取对象
        /// </summary>
        /// <param go="gameObject>go只有Mono才用</param>
        /// <returns></returns>
        public T Spawn(GameObject gameObject = default)
        {
            T temp;
            if (_objs.Count == 0)
                temp = _poolFactory.CreateObj(gameObject);
            else
                temp = _objs.Dequeue();
            PoolCommon(temp, true);
            // 对象获取后调用OnSpawn。
            temp.OnSpawn();
            return temp;
        }

        /// <summary>
        ///     回收对象
        /// </summary>
        /// <param name="obj"></param>
        public void Recycle(T obj)
        {
            PoolCommon(obj, false);
            // 对象进队前调用OnRecycle。
            obj.OnRecycle();
            if (_objs.Contains(obj))
                Log.Log.LogError("重复回收。");
            else
                _objs.Enqueue(obj);
        }

        private void PoolCommon(T obj, bool setActive)
        {
            var trans = obj as MonoBehaviour;
            if (trans != null)
                trans.gameObject.SetActive(setActive);
            else
                Log.Log.LogException(new NullReferenceException());
        }
    }

    public interface IPoolObj
    {
        void OnSpawn();
        void OnRecycle();
    }
}