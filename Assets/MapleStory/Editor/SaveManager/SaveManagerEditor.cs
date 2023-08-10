using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace MapleStory
{
    public static class SaveManagerEditor
    {

        [MenuItem("MapleStory/SaveManager/OpenSaveFilePath")]
        public static void OpenSaveFilePath()
        {
            Application.OpenURL(Application.persistentDataPath);
        }
    }
}