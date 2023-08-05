/*⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵
 ☠ ©2020 CallPalCatGames. All rights reserved.                                                                        
 ⚓ Author: Sky_Allen                                                                                                                  
 ⚓ Email: 894982165@qq.com                                                                                                  
 ⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵*/

using System;
using System.Collections.Generic;
using XLua;

namespace CallPalCatGames.EventManager
{
    /// <summary>
    ///     事件管理器。
    /// </summary>
    [LuaCallCSharp]
    public class EventManager
    {
        /// <summary>
        ///     字典键是事件即消息参数类的Type,值是这个事件的所有委托的持有类。
        /// </summary>
        private static readonly Dictionary<Type, IRegisterations> _registerations =
            new Dictionary<Type, IRegisterations>();

        /// <summary>
        ///     注册事件。
        /// </summary>
        /// <param name="onReceive">事件委托。</param>
        /// <typeparam name="T"></typeparam>
        public static void RigisterEvent<T>(Action<T> onReceive) where T : class
        {
            EventCommon<T>(eventObjType =>
            {
                var reg = _registerations[eventObjType] as Registerations<T>;
                reg.OnReceives += onReceive;
            }, eventObjType =>
            {
                var reg1 = new Registerations<T>();
                reg1.OnReceives += onReceive;
                _registerations.Add(eventObjType, reg1);
            });
        }

        /// <summary>
        ///     移除事件。
        /// </summary>
        /// <param name="onReceive">事件委托。</param>
        /// <typeparam name="T"></typeparam>
        public static void RemoveEvent<T>(Action<T> onReceive) where T : class
        {
            EventCommon<T>(eventObjType =>
            {
                var reg = _registerations[eventObjType] as Registerations<T>;
                reg.OnReceives -= onReceive;
                if (reg.OnReceives == null) _registerations.Remove(eventObjType);
            }, eventObjType => Log.Log.LogException(new KeyNotFoundException()));
        }

        /// <summary>
        ///     发送事件。
        /// </summary>
        /// <param name="eventObj">事件即消息参数类的实例。</param>
        /// <param name="eventSender">事件发送者的实例。</param>
        /// <typeparam name="T"></typeparam>
        public static void SendEvent<T>(T eventObj, object eventSender) where T : class
        {
            EventCommon<T>(eventObjType =>
            {
                var reg = _registerations[eventObjType] as Registerations<T>;
                ref var eventFirstSenderHashCode = ref reg.EventFirstSenderHashCode;
                if (eventFirstSenderHashCode == default) eventFirstSenderHashCode = eventSender.GetHashCode();
                if (eventFirstSenderHashCode == eventSender.GetHashCode())
                    reg.OnReceives(eventObj);
                else
                    Log.Log.LogError("某事件发送者实例只能有一个");
            }, eventObjType => Log.Log.LogException(new KeyNotFoundException()));
        }

        private static void EventCommon<T>(Action<Type> containsKeyAction, Action<Type> unContainsKeyAction)
        {
            var eventObjType = typeof(T);
            if (_registerations.ContainsKey(eventObjType))
                containsKeyAction.Invoke(eventObjType);
            else
                unContainsKeyAction.Invoke(eventObjType);
        }
    }
}