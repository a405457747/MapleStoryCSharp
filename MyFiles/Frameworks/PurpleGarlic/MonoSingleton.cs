//Author: SkyAllen                                                                                                                  
//Email: 894982165@qq.com      

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                var t = FindObjectOfType<T>();
                if (t == null)
                {
                    var obj = new GameObject(_name);
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

    private static string _name => typeof(T).Name;

    protected virtual void Start()
    {
        InitPartial();

        DontDestroyOnLoad(gameObject);
        gameObject.transform.LocalReset();
        if (gameObject.name != _name)
        {
            throw new Exception("The name has to be fixed");
        }

        SceneManager.sceneLoaded += OnLoadScene;
    }

    protected virtual void InitPartial()
    {
    }

    private void OnLoadScene(Scene arg0, LoadSceneMode arg1)
    {
        var go = GameObject.Find(_name);
        if (go != null)
        {
            Debug.LogWarning("Make sure the properties of multiple objects are consistent");
            Destroy(go);
        }
    }
}