using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class PathSet
{
    public static string GetABOutPath()
    {
        return Application.streamingAssetsPath;
        // return GetPlatformPath() + "/" + GetPlatformName();
    }

    public static string GetABSavePath()
    {
        return Path.Combine(Application.streamingAssetsPath, GetPlatformName());
    }

    private static string GetPlatformPath()
    {
        string strReturenPlatformPath = string.Empty;
        
        switch (Application.platform)
        {
            case RuntimePlatform.WindowsPlayer:
            case RuntimePlatform.WindowsEditor:
                strReturenPlatformPath = Application.streamingAssetsPath;
                break;

            case RuntimePlatform.IPhonePlayer:
            case RuntimePlatform.Android:
                strReturenPlatformPath = Application.persistentDataPath;
                break;
        }

        return strReturenPlatformPath;
    }

    private static string GetPlatformName()
    {
        string strReturenPlatformName = string.Empty;

        switch (Application.platform)
        {
            case RuntimePlatform.WindowsPlayer:
            case RuntimePlatform.WindowsEditor:
                strReturenPlatformName = "Windows";
                break;

            case RuntimePlatform.IPhonePlayer:
                strReturenPlatformName = "IOS";
                break;

            case RuntimePlatform.Android:
                strReturenPlatformName = "Android";
                break;
        }

        return strReturenPlatformName;
    }
}