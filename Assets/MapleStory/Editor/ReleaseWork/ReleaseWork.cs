using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

namespace MapleStory
{
    public static class ReleaseWork
    {
        private static string PackPath = Path.Combine(Application.dataPath + "/..", "MyFiles/Pack");

        [MenuItem("MapleStory/ReleaseWork/StartPack")]
        public static void StartPack()
        {
            BuildTarget buildTarget = BuildTarget.StandaloneWindows64;

            var PlatformPackPath = GetPlatformPackPath(buildTarget);
            Application.OpenURL(PlatformPackPath);
            Debug.Log("StartPack Success");
        }

        private static string GetPlatformPackPath(BuildTarget buildTarget)
        {
            return Path.Combine(ReleaseWork.PackPath, buildTarget.ToString());
        }
    }
}
