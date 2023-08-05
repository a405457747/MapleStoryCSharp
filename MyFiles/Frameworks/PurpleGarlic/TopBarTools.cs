//Author: SkyAllen                                                                                                                  
//Email: 894982165@qq.com      

using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace PurpleGarlic
{
    public static class TopBarTools
    {
        [MenuItem("Tools/PurpleGarlic/OpenScriptTemplates")]
        public static void OpenScriptTemplates()
        {
            var path = @"C:\Program Files\Unity\Hub\Editor\2019.4.16f1c1\Editor\Data\Resources\ScriptTemplates";
            Process.Start("explorer.exe", path);
        }

        [MenuItem("Tools/PurpleGarlic/OpenRootPath")]
        public static void OpenRootPath()
        {
            var targetPath = Application.dataPath.Replace("/Assets", "");
            Application.OpenURL("file:///" + targetPath);
        }

        [MenuItem("Tools/PurpleGarlic/TestSomething")]
        public static void TestSomething()
        {
            Debug.Log(Application.dataPath);
            Debug.Log(PupleGarlicCarry.Path);
        }
    }
}