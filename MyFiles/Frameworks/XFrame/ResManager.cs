/*/*⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵
 ☠ ©2020 CallPalCatGames. All rights reserved.                                                                        
 ⚓ Author: Sky_Allen                                                                                                                  
 ⚓ Email: 894982165@qq.com                                                                                                  
 ⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵#1#

using System.Net.Sockets;
using UnityEngine;
using XLua;

[LuaCallCSharp]
public  class ResManager
{
    private static readonly IResourceWay _resourceWay = new ResourceWayQF();

    /// <summary>
    ///     根据名字获取游戏对象
    /// </summary>
    /// <param name="gameObjectName"></param>
    /// <returns></returns>
    public static GameObject GetGameObject(string gameObjectName)
    {
        return _resourceWay.GetGameObject(gameObjectName);
    }

    public static TextAsset GetTextAsset(string textAssetName)
    {
        return _resourceWay.GetTextAsset(textAssetName);
    }

    /// <summary>
    ///     根据名字获取声音片段
    /// </summary>
    /// <param name="audioClipName"></param>
    /// <returns></returns>
    public static AudioClip GetAudioClip(string audioClipName)
    {
        return _resourceWay.GetAudioClip(audioClipName);
    }

    public static void LoadAudioClip(string key = "Audio", string label = "")
    {
        _resourceWay.LoadAudioClip(key, label);
    }

    public static void LoadGameObject(string key = "Obj", string label = "")
    {
        _resourceWay.LoadGameObject(key, label);
    }

    public static void LoadTextAssets(string key = "t", string lable = "")
    {
        _resourceWay.LoadTextAssets(key, lable);
    }
    
    public static void LoadScene(string key = "t", string lable = "")
    {
        _resourceWay.LoadScene(key,lable);
    }
}*/