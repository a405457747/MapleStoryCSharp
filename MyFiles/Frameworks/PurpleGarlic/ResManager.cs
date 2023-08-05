//Author: SkyAllen                                                                                                                  
//Email: 894982165@qq.com      

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AddressableAssets;


public partial class ResManager : MonoSingleton<ResManager>
{
    private Dictionary<string, GameObject> Gos = new Dictionary<string, GameObject>();

    protected override void Start()
    {
        base.Start();
    }

    public GameObject GetGameObject(string pathName)
    {
#if IsQuiick
        return AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/" + pathName + ".prefab");
#endif

        if (!Gos.ContainsKey(pathName))
        {
            var ab = AssetBundle.LoadFromFile(Path.Combine("jar:file://" + Application.dataPath + "!/assets/", pathName));
            DebugText.Instance.ShowInfo(ab.ToString());
            if (ab == null)
            {
                DebugText.Instance.ShowInfo("Failed to load AssetBundle");
                Debug.LogError("Failed to load AssetBundle");
            }

            var go = ab.LoadAsset<GameObject>(pathName);
            DebugText.Instance.ShowInfo(go.ToString());
            Gos.Add(pathName, go);
        }

        return Gos[pathName];
    }

    public AudioClip GetAudioClip(string pathName)
    {
        var ab = AssetBundle.LoadFromFile(Path.Combine("jar:file://" + Application.dataPath + "!/assets/", pathName));
        if (ab == null)
        {
            Debug.LogError("Failed to load AssetBundle");
        }

        return ab.LoadAsset<AudioClip>(pathName);
        
    }
}