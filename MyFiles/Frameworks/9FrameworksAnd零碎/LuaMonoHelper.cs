using System;
using System.Collections;
using System.Collections.Generic;
using CallPalCatGames.Log;
using UnityEngine;
using XLua;

[LuaCallCSharp]
public static class LuaMonoHelper
{
    public static LuaTable AddCom(GameObject go, string luaTxtPrevName)
    {
        TextAsset luatxt = ResManager.GetTextAsset($"{luaTxtPrevName}.lua");

        var luaMonos = go.GetComponents(typeof(LuaMonoBehaviour));

        foreach (var com in luaMonos)
        {
            var Mono = com as LuaMonoBehaviour;
            if (Mono.luaMonoBehaviourText != null && Mono.luaMonoBehaviourText == luatxt)
                Log.LogError("相同组件重复添加到一个物体身上");
        }

        var luaMono = go.AddComponent<LuaMonoBehaviour>();
        if (luaMono.luaMonoBehaviourText == null)
        {
            luaMono.luaMonoBehaviourText = luatxt;
            luaMono.Awake();
        }

        return luaMono.LuaMonoBehaviourLua;
    }

    public static LuaTable GetCom(GameObject go, string luaTxtPrevName)
    {
        var luaMonos = go.GetComponents(typeof(LuaMonoBehaviour));

        foreach (var com in luaMonos)
        {
            var Mono = com as LuaMonoBehaviour;
            if (Mono.luaMonoBehaviourText != null && Mono.LuaMonoBehaviourLua.Get<string>("PrevName") == luaTxtPrevName)
                return Mono.LuaMonoBehaviourLua;
        }

        Log.LogException(new NullReferenceException());
        return null;
    }

    public static void RemoveCom(GameObject go, string luaTxtPrevName)
    {
        var luaMonos = go.GetComponents(typeof(LuaMonoBehaviour));

        foreach (var com in luaMonos)
        {
            var Mono = com as LuaMonoBehaviour;
            if (Mono.luaMonoBehaviourText != null && Mono.LuaMonoBehaviourLua.Get<string>("PrevName") == luaTxtPrevName)
            {
                UnityEngine.Object.Destroy(com);
                return;
            }
        }

        Log.LogError("物体身上没有挂载此组件");
    }
}