//Author: SkyAllen                                                                                                                  
//Email: 894982165@qq.com      

using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace PurpleGarlic
{
    public static class CreateTool
    {
        public static void NewText()
        {
            CrateFile(".txt", "NewText");
        }

        private static void CrateFile(string fileEx, string fileName = "N", string fileContain = "")
        {
            var selectFolderPath = AssetDatabase.GetAssetPath(Selection.activeObject);
            var fileFullName = $"{fileName}{fileEx}";
            var selectFilePath = Path.Combine(selectFolderPath, fileFullName);

            string GetRealPath(string willPath)
            {
                var tempPath = willPath.Replace("Assets/", "");
                var RealPath = Path.Combine(Application.dataPath, tempPath);
                return RealPath;
            }

            var realFolderPath = GetRealPath(selectFolderPath);

            var writePath = $"{Path.Combine(realFolderPath, fileFullName)}";
            File.WriteAllText(writePath, fileContain, Encoding.UTF8);
            AssetDatabase.Refresh();

            Selection.activeObject = AssetDatabase.LoadAssetAtPath(selectFilePath, typeof(Object));
        }
    }
}