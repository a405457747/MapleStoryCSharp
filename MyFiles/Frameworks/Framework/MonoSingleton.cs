using System;
using UnityEngine;

[DisallowMultipleComponent]
public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static bool _isQuit = false;
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_isQuit)
            {
                return null;
            }
            else
            {
                if (_instance == null)
                {
                    var t = FindObjectsOfType<T>();
                    if (t.Length == 0)
                    {
                        var obj = new GameObject();
                        _instance = obj.AddComponent<T>();
                    }
                    else if (t.Length == 1)
                    {
                        _instance = t[0];
                    }
                    else
                    {
                        Log.LogError("The instance more than 1.");
                    }
                }

                return _instance;
            }
            
        }
    }

    protected virtual void Awake()
    {
        if (gameObject.name != "Root")
        {
            gameObject.Name(typeof(T).Name);
            transform.LocalReset();
        }
        
        DontDestroyOnLoad(gameObject);
    }

    protected virtual  void OnDestroy()
    {
        _isQuit = true;
    }
}