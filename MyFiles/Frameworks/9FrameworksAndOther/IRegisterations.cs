/*⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵
 ☠ ©2020 CallPalCatGames. All rights reserved.                                                                        
 ⚓ Author: Sky_Allen                                                                                                                  
 ⚓ Email: 894982165@qq.com                                                                                                  
 ⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵*/

using System;
using XLua;

namespace CallPalCatGames.EventManager
{
    /// <summary>
    ///     为了能够在字典里面保存泛型T所做的接口。
    /// </summary>
    [LuaCallCSharp]
    public interface IRegisterations
    {
    }

    /// <summary>
    ///     持有一些属性方便字典使用。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [LuaCallCSharp]
    public class Registerations<T> : IRegisterations
    {
        /// <summary>
        ///     第一次事件发送者的实例的HashCode。
        /// </summary>
        public int EventFirstSenderHashCode;

        /// <summary>
        ///     持有单个事件的所有委托。
        /// </summary>
        public Action<T> OnReceives { get; set; }
    }
}