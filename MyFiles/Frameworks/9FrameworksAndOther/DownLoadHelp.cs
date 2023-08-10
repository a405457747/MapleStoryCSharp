using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CallPalCatGames.Singleton;
using UnityEngine;
using UnityEngine.Networking;
using XLua;
using Log = CallPalCatGames.Log.Log;
using Utility = CallPalCatGames.Utility.Utility;
using QFramework;

[LuaCallCSharp]
public class DownLoadHelp : CallPalCatGames.Singleton.MonoSingleton<DownLoadHelp>
{
    protected override void Awake()
    {
    }

    private void NetError()
    {
        GameInitUI.Instance.UpdateErrorTipText("请确保网络通信正常，并且重新重启游戏！");
    }

    private Ver _NetVer;

    public IEnumerator DownLoadVersionFile()
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(PathSet.Instance.ServerPathVersionTextFile);

        yield return webRequest.SendWebRequest();

        if (webRequest.isHttpError || webRequest.isNetworkError)
        {
            NetError();
        }
        else
        {
            var jsonStr = System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data, 0,
                webRequest.downloadHandler.data.Length);
            _NetVer = JsonUtility.FromJson<Ver>(jsonStr);
            if (_NetVer.version > HotManager.Instance.GetSelfVersion())
            {
                GameInitUI.Instance.OpenHotPanel();
                GameInitUI.Instance.UpdateTipContentText(_NetVer.version, _NetVer.NewVersionABFileTotalSize);
                yield return new WaitUntil(() => GameInitUI.Instance.IsClickUpdateButton == true);
                yield return StartCoroutine(DownLoadABAbout());
                yield return StartCoroutine(GameInitUI.Instance.DelayCloseHotPanel());
                GameInit.Instance.StartGame();
            }
            else
            {
                GameInit.Instance.StartGame();
            }
        }
    }

    private IEnumerator DownLoadABAbout()
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(PathSet.Instance.ServerPathABConfigBinFile);

        yield return webRequest.SendWebRequest();

        if (webRequest.isHttpError || webRequest.isNetworkError)
        {
            NetError();
        }
        else
        {
            using (Stream openS = new MemoryStream(webRequest.downloadHandler.data))
            {
                ResDatas.SerializeData serializeData =
                    (ResDatas.SerializeData) SerializeHelper.DeserializeBinary(openS);
                var savePath = PathSet.Instance.GetSavePath();
                savePath.DeleteDirIfExists();
                savePath.CreateDirIfNotExists();

                List<string> nameList = new List<string>();
                foreach (AssetDataGroup.SerializeData data in serializeData.AssetDataGroup)
                {
                    foreach (var assetd in data.abUnitArray)
                    {
                        nameList.Add(assetd.abName);
                    }

                    nameList.Add("asset_bindle_config.bin");
                }

                for (var index = 0; index < nameList.Count; index++)
                {
                    var fileName = nameList[index];
                    var progress = (index + 1) / (float) nameList.Count;
                    GameInitUI.Instance.UpdateUpdateContentProgressText(
                        $"正在下载资源，当前进度：{Utility.GetRatio(progress)}");
                    var www = new UnityWebRequest(PathSet.Instance.GetServerPathABFile(fileName));
                    www.downloadHandler = new DownloadHandlerFile(PathSet.Instance.GetSavePathABFile(fileName));
                    yield return www.SendWebRequest();

                    if (www.isHttpError || www.isNetworkError)
                    {
                        NetError();
                    }
                }

                AssetBundleSettings.LoadAssetResFromStreammingAssetsPath = false;
                HotManager.Instance.SetSelfVersion(_NetVer.version);
            }
        }
    }
}