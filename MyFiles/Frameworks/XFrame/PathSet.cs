/*using System.Collections;
using System.Collections.Generic;
using System.IO;
using QFramework;
using UnityEngine;
using XLua;

public class PathSet : CallPalCatGames.Singleton.MonoSingleton<PathSet>
{
    public const string ServerPath = "http://122.51.186.74/Book";
    public const string ProjectName = "BoomMan";
    public const string VersionTextName = "ver.txt";

    [HideInInspector] public string ServerPathVersionTextFile = $"{ServerPath}/{ProjectName}/Bundle/{VersionTextName}";

    [HideInInspector]
    public string ServerPathABConfigBinFile = $"{ServerPath}/{ProjectName}/Bundle/asset_bindle_config.bin";

    //唯一一个路径是控制3种的
    public string GetLuaFileTextStr(string fileNamePrev)
    {
#if UNITY_EDITOR
        if (AssetBundleSettings.SimulateAssetBundleInEditor)
        {
            string EditorLuaToolPath = $"{Application.dataPath}/My/DoNotChangeCSharp/LuaTool/{fileNamePrev}.lua.txt";
            return File.ReadAllText(EditorLuaToolPath);
        }
        else
#endif
        {
            TextAsset temp = ResManager.GetTextAsset($"{fileNamePrev}.lua");
            return temp.text;
        }
    }

    public string GetServerPathABFile(string fileName)
    {
        return $"{ServerPath}/{ProjectName}/Bundle/{fileName}";
    }

    public string GetSavePath()
    {
        return Application.persistentDataPath.CombinePath("AssetBundles")
            .CombinePath(AssetBundleSettings.GetPlatformName());
    }

    public string GetTempPath()
    {
        return Application.streamingAssetsPath.CombinePath("AssetBundles")
            .CombinePath(AssetBundleSettings.GetPlatformName());
    }

    public string GetTempPathServerVersionFile()
    {
        return GetTempPath().CombinePath(VersionTextName);
    }

    public string GetSavePathABFile(string fileName)
    {
        return GetSavePath().CombinePath(fileName);
    }

    public const string GamePlay2SceneName = "gameplay2_unity";

    public string GetTempPathSceneFile()
    {
        return Path.Combine(PathSet.Instance.GetTempPath(), GamePlay2SceneName);
    }

    public string GetSavePathSceneFile()
    {
        return Path.Combine(PathSet.Instance.GetSavePath(), GamePlay2SceneName);
    }
}

[LuaCallCSharp]
public class Ver
{
    public int version;
    public string NewVersionABFileTotalSize;
}*/