/*⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵
 ☠ ©2020 CallPalCatGames. All rights reserved.                                                                        
 ⚓ Author: Sky_Allen                                                                                                                  
 ⚓ Email: 894982165@qq.com                                                                                                  
 ⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵*/

using UnityEngine;
using XLua;

namespace CallPalCatGames.Singleton
{
    /// <summary>
    ///     MonoBehaviour的单例模板。
    /// </summary>
    /// <typeparam name="T">想成为单例的类型</typeparam>

    [LuaCallCSharp]
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        private static T _instance;

        /// <summary>
        ///     解决退出编辑器报错的布尔变量。
        /// </summary>
        private static bool _isQuit { get; set; }

        /// <summary>
        ///     获取单例对象。
        /// </summary>
        public static T Instance
        {
            get
            {
                if (_isQuit) return null;
                if (_instance == null)
                {
                    T t = GameObject.FindObjectOfType<T>();
                    if (t == null)
                    {
                        var obj = new GameObject(typeof(T).Name);
                        _instance = obj.AddComponent<T>();
                    }
                    else
                    {
                        _instance = t;
                    }
                }

                return _instance;
            }
        }

        protected virtual void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        /// <summary>
        ///     要把_instance字段置空严谨点。
        /// </summary>
        protected virtual void OnDestroy()
        {
            _isQuit = true;
            _instance = null;
        }
    }
}