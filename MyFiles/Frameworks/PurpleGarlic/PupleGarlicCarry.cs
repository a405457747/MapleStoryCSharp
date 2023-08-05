//Author: SkyAllen                                                                                                                  
//Email: 894982165@qq.com      

using System.IO;
using UnityEditor;
using UnityEngine;

public class PupleGarlicCarry
{
    public static string Path
    {
        get => Application.dataPath.Replace("/Assets", "");
    }

    [MenuItem("Tools/PupleGarlicCarry/ExportPupleGarlic")]
    public static void ExportPupleGarlic()
    {
        if (!IsPurpleGarlicProject()) return;

        var fileName = "PurpleGarlic.unitypackage";
        AssetDatabase.ExportPackage("Assets/Plugins", fileName, ExportPackageOptions.Recurse);
        AssetDatabase.Refresh();
        Debug.Log("export success");
    }

    private static void RemovePlugins()
    {
        var PluginsPath = System.IO.Path.Combine(Application.dataPath, "Plugins");
        if (Directory.Exists(PluginsPath))
        {
            Directory.Delete(PluginsPath, true);
            AssetDatabase.Refresh();
            Debug.Log("remove success");
        }
    }

    [MenuItem("Tools/PupleGarlicCarry/ImportPupleGarlic")]
    public static void ImportPupleGarlic()
    {
        if (IsPurpleGarlicProject()) return;

        RemovePlugins();

        var filePath = Path + "/PurpleGarlic.unitypackage";
        AssetDatabase.ImportPackage(filePath, false);
        AssetDatabase.Refresh();
        Debug.Log("import success");
    }

    private static bool IsPurpleGarlicProject()
    {
        var garlicProjectPath = Path + "/Assets";
        return garlicProjectPath == Application.dataPath;
    }
}