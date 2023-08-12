using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using MapleStory;
using System.Linq;
using UnityEditor;
using static MapleStory.LogNote;

public static  class AudioNameCreate
{

    [MenuItem("MapleStory/Audio/CreateAudioName")]
    public static void CreateAudioName()
    {
        var audioDirPath =
            Path.Combine(Application.dataPath.Replace("/Assets",""),"Assets/GameApp/Audios/Resources/Audio");

        var pyPath=Path.Combine(Application.dataPath.Replace("/Assets",""),"MyFiles/PythonHelper/AudioName.py");

        if (File.Exists(pyPath))
        { 
            ToolRoot.RunPythonScript( pyPath,audioDirPath);
        }
        
        UnityEngine.Debug.Log("CreateAudioName Successful");
    }

}
