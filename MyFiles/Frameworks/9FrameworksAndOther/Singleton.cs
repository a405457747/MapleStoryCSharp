/*⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵
 ☠ ©2020 CallPalCatGames. All rights reserved.                                                                        
 ⚓ Author: Sky_Allen                                                                                                                  
 ⚓ Email: 894982165@qq.com                                                                                                  
 ⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵*/

using System;
using System.Reflection;
using XLua;

namespace CallPalCatGames.Singleton
{
    /// <summary>
    ///     非Mono的单例模板。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [LuaCallCSharp]
    public abstract class Singleton<T> where T : Singleton<T>
    {
        private static T _instance;
        private static readonly object _syncRoot = new object();

        /// <summary>
        ///     返回单例。
        /// </summary>
        public static T Instance
        {
            get
            {
                if (_instance == null)
                    lock (_syncRoot)
                    {
                        // 双锁。
                        if (_instance == null)
                        {
                            // 利用反射可以使单例拥有private构造函数从而形成保护。
                            var ctors = typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);
                            var ctor = Array.Find(ctors, c => c.GetParameters().Length == 0);
                            if (ctor == null) Log.Log.LogError("非public的构造函数一定要持有一个");
                            _instance = ctor.Invoke(null) as T;
                        }
                    }

                return _instance;
            }
        }

        /// <summary>
        ///     销毁前最好置空吧，要严谨。
        ///     这里置空影响线程安全吗？
        /// </summary>
        protected virtual void OnDestory()
        {
            _instance = null;
        }
    }
}