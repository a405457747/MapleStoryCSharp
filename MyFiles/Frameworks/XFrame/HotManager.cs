/*using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using QFramework;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using XLua;
using Log = CallPalCatGames.Log.Log;
using Utility = CallPalCatGames.Utility.Utility;

[LuaCallCSharp]
public class HotManager : CallPalCatGames.Singleton.MonoSingleton<HotManager>
{
    public int GetSelfVersion()
    {
        /*TextAsset selfVersionText = ResManager.GetTextAsset("ver");
        Ver selfVer = JsonUtility.FromJson<Ver>(selfVersionText.text);
        return selfVer;#1#
        return PlayerPrefs.GetInt("Version", 100);
    }

    public void SetSelfVersion(int newVersionNum)
    {
        PlayerPrefs.SetInt("Version", newVersionNum);
    }

    public void DetestionHotUpdate()
    {
        StartCoroutine(DownLoadHelp.Instance.DownLoadVersionFile());
    }

    #region 非项目调用
    public void  StartDownLoadVersionFile()
    {
        StartCoroutine(DownLoadVersionFile());
    }
    
    private void VerifyUpdateVersion()
    {
        var str1 = Application.version;
        var str2 = CallPalCatGames.Utility.Utility.GetVersionTrans(HotManager.Instance.GetSelfVersion());
        /*var str3Num = EditorPrefs.GetString("KEY_QAssetBundleBuilder_RESVERSION");
        var str3 = Utility.GetVersionTrans(int.Parse(str3Num));#1#
        HashSet<string> hash = new HashSet<string>() {str1, str2};

        if (hash.Count == 1)
        {
            CallPalCatGames.Log.Log.LogNormal(" 本地2种版本号都是同步的,只需要检测资源号就行了");
        }
        else
        {
            CallPalCatGames.Log.Log.LogNormal("本地2种版本号有错或者没有同步，资源号不确定");
        }
        Debug.Log("测试确保服务器版本为100，打包顺序是先打本地包，然后再打服务器文件，然后再上传");
        Debug.Log("打包只打包场景，和所有的LuaTool脚本");
        Debug.Log("还要检测项目名字，防止Win10保存记忆");
        Debug.Log("还要生成xlua代码");
    }

    private IEnumerator DownLoadVersionFile()
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(PathSet.Instance.ServerPathVersionTextFile);
        yield return webRequest.SendWebRequest();
        if (webRequest.isHttpError || webRequest.isNetworkError)
        {
            throw new Exception("网络连接错误");
        }
        else
        {
            double sizeB = IOExpand.GetDirectorySize(PathSet.Instance.GetTempPath());
            double sizeM = (sizeB / 1024) / 1024;
            string sizeMStr = sizeM.ToString("0.00");

            var jsonStr = System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data,0 ,
                webRequest.downloadHandler.data.Length);
            Ver _NetVer = JsonUtility.FromJson<Ver>(jsonStr);
            _NetVer.version = _NetVer.version + 1;
            _NetVer.NewVersionABFileTotalSize = sizeMStr;

            string _NetVerStr = JsonUtility.ToJson(_NetVer);
            File.WriteAllText(PathSet.Instance.GetTempPathServerVersionFile(), _NetVerStr);
            Debug.Log("新版本文件写入成功了");
            VerifyUpdateVersion();
        }
    }

    #endregion
  
}*/