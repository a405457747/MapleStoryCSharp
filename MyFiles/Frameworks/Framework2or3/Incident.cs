using System;
using System.Collections.Generic;

public static class Incident
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
    public static void RigisterEvent<T>(Action<T> onReceive)
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
    public static void DeleteEvent<T>(Action<T> onReceive)
    {
        EventCommon<T>(eventObjType =>
        {
            var reg = _registerations[eventObjType] as Registerations<T>;
            reg.OnReceives -= onReceive;
        }, eventObjType => Log.LogWarning("Don't have the key"));
    }

    /// <summary>
    ///     发送事件。
    /// </summary>
    /// <param name="eventObj">事件即消息参数类的实例。</param>
    /// <param name="eventSender">事件发送者的实例。</param>
    /// <typeparam name="T"></typeparam>
    public static void SendEvent<T>(T eventObj)
    {
        EventCommon<T>(eventObjType =>
        {
            var reg = _registerations[eventObjType] as Registerations<T>;
            reg.OnReceives(eventObj);
        }, eventObjType => Log.LogWarning("Don't have the key"));
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

/// <summary>
///     为了能够在字典里面保存泛型T所做的接口。
/// </summary>
public interface IRegisterations
{
}

/// <summary>
///     持有一些属性方便字典使用。
/// </summary>
/// <typeparam name="T"></typeparam>
public class Registerations<T> : IRegisterations
{
    /// <summary>
    ///     持有单个事件的所有委托。
    /// </summary>
    public Action<T> OnReceives { get; set; }
}