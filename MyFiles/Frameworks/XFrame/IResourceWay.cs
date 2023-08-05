/*/*⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵
 ☠ ©2020 CallPalCatGames. All rights reserved.                                                                        
 ⚓ Author: Sky_Allen                                                                                                                  
 ⚓ Email: 894982165@qq.com                                                                                                  
 ⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵#1#

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;
using QFramework;
using UnityEngine.SceneManagement;
using Object = System.Object;

[LuaCallCSharp]
public interface IResourceWay
{
    Dictionary<string, AudioClip> AudioClips { get; set; }
    Dictionary<string, GameObject> GameObjects { get; set; }
    Dictionary<string, TextAsset> TextAssets { get; set; }
    Dictionary<string, Scene> Scenes { get; set; }

    TextAsset GetTextAsset(string textAssetName);
    GameObject GetGameObject(string gameObjectName);

    AudioClip GetAudioClip(string audioClipName);
    //scene不用

    void LoadAudioClip(string key = "Audio", string label = "");
    void LoadGameObject(string key = "Obj", string label = "");
    void LoadTextAssets(string key = "t", string lable = "");
    void LoadScene(string key = "t", string lable = "");
}

[LuaCallCSharp]
public class ResourceWayQF : IResourceWay
{
    public Dictionary<string, AudioClip> AudioClips { get; set; } = new Dictionary<string, AudioClip>();
    public Dictionary<string, GameObject> GameObjects { get; set; } = new Dictionary<string, GameObject>();
    public Dictionary<string, TextAsset> TextAssets { get; set; } = new Dictionary<string, TextAsset>();
    public Dictionary<string, Scene> Scenes { get; set; } = new Dictionary<string, Scene>();

    private ResLoader _resLoaderTextAsset;
    private ResLoader _resLoaderGameObject;
    private ResLoader _resLoaderAudioClip;
    private ResLoader _resLoaderScene;

    public TextAsset GetTextAsset(string textAssetName)
    {
        return LoadThing<TextAsset>(_resLoaderTextAsset, textAssetName);
    }

    public GameObject GetGameObject(string gameObjectName)
    {
        return LoadThing<GameObject>(_resLoaderGameObject, gameObjectName);
    }

    public AudioClip GetAudioClip(string audioClipName)
    {
        return LoadThing<AudioClip>(_resLoaderAudioClip, audioClipName);
    }

    public T LoadThing<T>(ResLoader _resLoader, string name) where T : UnityEngine.Object
    {
        var t = typeof(T);
        if (t.Name.Equals("Sprite")) return _resLoader.LoadSprite(name) as T;
        return _resLoader.LoadSync(name) as T;
    }

    public void LoadAudioClip(string key = "Audio", string label = "")
    {
        _resLoaderAudioClip = ResLoader.Allocate();
    }

    public void LoadGameObject(string key = "Obj", string label = "")
    {
        _resLoaderGameObject = ResLoader.Allocate();
    }

    public void LoadTextAssets(string key = "t", string lable = "")
    {
        _resLoaderTextAsset = ResLoader.Allocate();
    }

    public void LoadScene(string key = "t", string lable = "")
    {
        //_resLoaderScene = ResLoader.Allocate();
        /*_resLoaderScene.Add2Load("gameplay2_unity", (succeed, res) =>
        {
            if(succeed) Debug.Log("加载了");
        });
        _resLoaderScene.LoadAsync(()=>{"资源都加载了的".LogInfo();});#1#
        //_resLoaderScene.LoadSync("gameplay2_unity");

#if UNITY_EDITOR
        if (AssetBundleSettings.SimulateAssetBundleInEditor)
        {
        }
        else
#endif
        {
            string path = "";
            if ( AssetBundleSettings.LoadAssetResFromStreammingAssetsPath)
            {
                path = PathSet.Instance.GetTempPathSceneFile();  
            }
            else
            {
                path = PathSet.Instance.GetSavePathSceneFile();
            }

            AssetBundle.LoadFromFile(path);
        }
    }
}

/*
[LuaCallCSharp]
public class ResourceWayAddressables : IResourceWay
{
    public Dictionary<string, AudioClip> AudioClips { get; set; } = new Dictionary<string, AudioClip>();
    public Dictionary<string, GameObject> GameObjects { get; set; } = new Dictionary<string, GameObject>();
    public Dictionary<string, TextAsset> TextAssets { get; set; } = new Dictionary<string, TextAsset>();

    /// <summary>
    ///     根据名字获取声音片段
    /// </summary>
    /// <param name="audioClipName"></param>
    /// <returns></returns>
    public AudioClip GetAudioClip(string audioClipName)
    {
        return GetSomeThing(audioClipName, AudioClips);
    }

    public TextAsset GetTextAsset(string textAssetName)
    {
        return GetSomeThing(textAssetName, TextAssets);
    }

    /// <summary>
    ///     根据名字获取游戏对象
    /// </summary>
    /// <param name="gameObjectName"></param>
    /// <returns></returns>
    public GameObject GetGameObject(string gameObjectName)
    {
        return GetSomeThing(gameObjectName, GameObjects);
    }

    /// <summary>
    ///     加载所有音频。
    /// </summary>
    /// <param name="key">地址重命名</param>
    /// <param name="label">标签</param>
    public void LoadAudioClip(string key = "Audio", string label = "")
    {
        LoadSomeThing(key, label, AudioClips);
    }

    public void LoadGameObject(string key = "Obj", string label = "")
    {
        LoadSomeThing(key, label, GameObjects);
    }

    public void LoadTextAssets(string key = "t", string lable = "")
    {
        LoadSomeThing(key, lable, TextAssets);
    }

    private T GetSomeThing<T>(string name, Dictionary<string, T> dic)
    {
        dic.TryGetValue(name, out var temp);
        if (temp == null) Debug.LogException(new NullReferenceException());
        return temp;
    }

    private void LoadSomeThing<T>(string key, string label, Dictionary<string, T> dic) where T : UnityEngine.Object
    {
        UnityEngine.AddressableAssets.Addressables.LoadAssetsAsync<T>(new List<object> {key, label}, null,
                UnityEngine.AddressableAssets.Addressables.MergeMode.None)
            .Completed += objs =>
        {
            foreach (var obj in objs.Result)
                if (dic.ContainsKey(obj.name))
                    Debug.LogError("缓存里面有这个了，或者就是某个同名了。");
                else
                    dic.Add(obj.name, obj);
        };
    }
}#1#*/