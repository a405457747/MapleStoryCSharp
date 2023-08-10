using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using System.Text.RegularExpressions;
using MapleStory;
using Debug = UnityEngine.Debug;


namespace MapleStory
{

    public enum CreateUIType
    {
        Null,
        Text, //Txt
        Button, //Btn
        Image, //Img
        RectTransform,
        Transform, //Trans
        Toggle, //Tog
        Slider, //Sld
        InputField, //Inp
        Dropdown, //Drop
        GameObject,

    }

    [Serializable]
    public class SendObj
    {
        public Dictionary<string, string> data1;
        public string csFilePath;
    }

    public static class AutoCreateUI
    {
        static string jsonPath = Path.Combine(Application.dataPath.Replace("/Assets", ""), "MyFiles/PythonHelper/SendObj.json");

        [MenuItem("MapleStory/UI/OpenScriptTemplates")]
        static void OpenScriptTemplates()
        {
            var url = @"C:\Program Files\Unity\Hub\Editor\2021.3.27f1c1\Editor\Data\Resources\ScriptTemplates";
            Application.OpenURL(url);
        }

        [MenuItem("MapleStory/UI/CreateUI %t")]
        static void CreateUI()
        {
            string targetScriptDir = Path.Combine(Application.dataPath, "GameApp/UI/Panel");

            GameObject selectedObject = Selection.activeGameObject;
            string goName = selectedObject.name;
            if (Regex.IsMatch(goName, ".+Panel") || Regex.IsMatch(goName, ".+GameObject"))
            {
                List<Transform> transforms = FindWantTransform(selectedObject);

                Dictionary<string, string> nameTypeData = new Dictionary<string, string>();
                nameTypeData.Clear();

                foreach (var trans in transforms)
                {
                    if (trans.name.Contains(" (Legacy)"))
                    {
                        trans.name = trans.name.Replace(" (Legacy)", "");
                    }

                    var transName = trans.name;

                    if ((transNameCanMatch(transName) == true) && nameTypeData.ContainsKey(transName) == false)
                    {
                        var nameType = getNameTypeValue(transName);
                        if(nameType!="") nameTypeData.Add(transName, nameType);
                    }
                }

                var sendObj = new SendObj()
                { data1 = nameTypeData, csFilePath = Path.Combine(targetScriptDir, goName + ".cs") };

                var sendObjJson = JsonTool<SendObj>.CurTool.ToJson(sendObj, false);

                ToolRoot.WriteToFile(jsonPath, sendObjJson);

                Debug.Log("Write Successful");

                CreateUI2();
            }
        }

        static void CreateUI2()
        {
            var pyPath = Path.Combine(Application.dataPath.Replace("/Assets", ""), "MyFiles/PythonHelper/UICreate.py");

            if (File.Exists(pyPath))
            {
                ToolRoot.RunPythonScript(pyPath, jsonPath);
            }

            Debug.Log("RunPy Successful");
        }

        static void FindTrans(Transform aParent, List<Transform> transforms)
        {
            foreach (Transform child in aParent)
            {
                transforms.Add(child);

                if (child.name.Contains("GameObject") && (child.name.StartsWith("GameObject") == false))
                {
                }
                else
                {
                    FindTrans(child, transforms);
                }
            }

        }

        public static List<Transform> FindWantTransform(GameObject selectedObject)//这个方法自动去除root，暂时估计没有bug
        {
            Transform root = selectedObject.transform;

            List<Transform> transforms = new List<Transform>();

            FindTrans(root, transforms);

            return transforms;
        }


        static bool transNameCanMatch(string transName)
        {
            var typeList = Enum.GetValues(typeof(CreateUIType))
                .Cast<CreateUIType>()
                .ToList();

            foreach (var t in typeList)
            {
                var strT = t.ToString();

                if ((Regex.IsMatch(transName, ".+" + strT)))
                {
                    return true;
                }
            }

            return false;
        }

        static string getNameTypeValue(string transName)
        {
            var typeList = Enum.GetValues(typeof(CreateUIType))
                .Cast<CreateUIType>()
                .ToList();

            for (int i = 0; i < typeList.Count; i++)
            {
                var t = typeList[i];
                var strT = t.ToString();
                if (transName.Contains(strT))
                {
                    if (strT == "GameObject")
                    {
                        return transName;
                    }
                    else
                    {
                        return strT;
                    }
                }
            }

            return "";
        }
    }
}