using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public enum UIType
{
    Null,
    Text, //Txt
    Button, //Btn
    Image, //Img
    Transform, //Trans
    Toggle, //Tog
    Slider, //Sld
    InputField, //Inp
    Dropdown, //Drop
}

[InitializeOnLoad]
public static class Kit4
{
    static Kit4()
    {
       // EditorApplication.hierarchyChanged += OnHierarchyChanged; //不知会重复注册不，不然用到后面性能严重下降。
    }

    static void OnHierarchyChanged()
    {
        var activeGo = Selection.activeGameObject;
        if (activeGo != null)
        {
            string name = activeGo.name;
            UIType uiType = GetUIType(name);
            if (uiType != UIType.Null) //无法做到分割历史记录
            {
                string strSpecial = "";
                if (uiType == UIType.Text)
                {
                    strSpecial = StrText(name);
                }
                else if (uiType == UIType.Image)
                {
                    strSpecial = StrImage(name);
                }
                else if (uiType == UIType.Button)
                {
                    strSpecial = StrButton(name);
                }

                string strField = StrField(uiType, name);
                string strFind = StrFind(uiType, name);
                if (strSpecial == "")
                {
                    GUIUtility.systemCopyBuffer = strField + "\n" + strFind;
                }
                else
                {
                    GUIUtility.systemCopyBuffer = strField + "\n" + strSpecial + "\n" + strFind;
                }
            }
            Debug.Log("uiType is " + uiType);
        }
    }

    /*自动生成UI*/
    static UIType GetUIType(string name)
    {
        List<string> matchList = new List<string>()
        {
            "Txt|Text",
            "Btn|Button",
            "Img|Image",
            "Trans|Transform",
            "Tog|Toggle",
            "Sld|Slider",
            "Inp|InputField",
            "Drop|Dropdown",
        };
        UIType uiType = UIType.Null;
        for (var index = 0; index < matchList.Count; index++)
        {
            var pattern = matchList[index];
            bool match = Regex.IsMatch(name, pattern);
            if (match)
            {
                uiType = (UIType)(index + 1);
                break;
            }
        }

        return uiType;
    }

    static string StrField(UIType uiType, string name)
    {
        string model = $@"    private {uiType} {name};";
        return model;
    }

    static string StrText(string name)
    {
        string model = $@"    private void {name}Change(string arg = null) => Kit.Change({name},arg);";
        return model;
    }

    static string StrImage(string name)
    {
        string model = $@"    private void {name}Change(string arg = null) => Kit.Change({name},arg);";
        return model;
    }

    static string StrButton(string name)
    {
        string model = $@"    private void {name}Click(Action click) => Kit.Click({name},click);";
        return model;
    }

    static string StrFind(UIType uiType, string name)
    {
        string model = $"        {name} = this.Find<{uiType}>(\"{name}\");";
        return model;
    }
}