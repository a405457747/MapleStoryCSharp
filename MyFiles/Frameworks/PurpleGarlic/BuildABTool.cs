using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class BuildABTool
{
    public static string ABOutPath = PathSet.GetABOutPath();

    [MenuItem("Tools/BuildABTool/BuildAllAssetBundles")]
    public static void BuildAllAssetBundles()
    {
        if (Directory.Exists(ABOutPath) == false)
        {
            Directory.CreateDirectory(ABOutPath);
        }

        BuildPipeline.BuildAssetBundles(ABOutPath, BuildAssetBundleOptions.None,
            EditorUserBuildSettings.activeBuildTarget);

        AssetDatabase.Refresh();
    }

    [MenuItem("Tools/BuildABTool/ClearAllAssetBundles")]
    public static void ClearAllAssetBundles()
    {
        if (Directory.Exists(ABOutPath))
        {
            Directory.Delete(ABOutPath, true);

            AssetDatabase.Refresh();
        }
    }

    [MenuItem("Tools/BuildABTool/SetAllAssetBundles")]
    public static void SetAssetbundleName()
    {
        Object[] selects = Selection.objects;

        foreach (Object selected in selects)
        {
            string path = AssetDatabase.GetAssetPath(selected);

            AssetImporter assetImporter = AssetImporter.GetAtPath(path);
            assetImporter.assetBundleName = selected.name;
            //assetImporter.assetBundleVariant = "unity3d";
            //assetImporter.SaveAndReimport();
        }

        AssetDatabase.Refresh();
    }

    public static string GetPlatformForAssetBundles(BuildTarget target)
    {
        switch (target)
        {
            case BuildTarget.Android:
                return "Android";
            case BuildTarget.iOS:
                return "IOS";
            case BuildTarget.StandaloneWindows:
            case BuildTarget.StandaloneWindows64:
                return "Windows";
        }
        return null;
    }
}