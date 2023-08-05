using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using XLua;

[DefaultExecutionOrder(1)]
public class LuaManager : MonoBehaviour
{
    //public string Uri;

    //public bool IsDevelopment;

    //public TextAsset LuaScriptTextMain;

    public static readonly LuaEnv LuaEnv = new LuaEnv();

    private void Awake()
    {
        //base.Awake();

        LuaEnv.AddLoader(CustomLoader);

        
        LuaEnv.DoString("require('main')", "main");
        /*if (IsDevelopment)
        {
           
        }
        else
        {
            LuaHelp.AllMonoLuaSetActive(false);
            StartCoroutine(GetRequest(Uri));
        }*/
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                Debug.Log("Error:" + webRequest.error);
            }
            else
            {
                LuaEnv.DoString(webRequest.downloadHandler.text, "main");
                LuaHelp.AllMonoLuaSetActive(true);
            }
        }
    }

    private byte[] CustomLoader(ref string filepath)
    {
        filepath = Application.dataPath + "/LuaScript/" + filepath.Replace('.', '/') + ".lua";

        if (File.Exists(filepath))
        {
            return File.ReadAllBytes(filepath);
        }
        else
        {
            return null;
        }


        /*string front = filepath.Substring(0, filepath.Length - 4);
        front = front.Replace(".", "/");
        string rear = filepath.Substring(filepath.Length - 4);
        TextAsset textAsset = Resources.Load<TextAsset>($"{front}{rear}");
        return textAsset.bytes;*/
    }

    private void Start()
    {
    }
}