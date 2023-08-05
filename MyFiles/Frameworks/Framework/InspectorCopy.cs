using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InspectorCopy : EditorWindow
{
    //最终输出的数据
    static string logText;

    [MenuItem("Framework/InspectorCopy/CopyPosition", false, -100)]
    public static void CopyPosition()
    {
        CopyCommon(0);
    }

    [MenuItem("Framework/InspectorCopy/CopyRotation", false, -100)]
    public static void CopyRotation()
    {
        CopyCommon(1);
    }

    [MenuItem("Framework/InspectorCopy/CopyScale", false, -100)]
    public static void CopyScale()
    {
        CopyCommon(2);
    }

    static void CopyCommon(int flag)
    {
        //重置数据
        logText = "";
        //获取编辑器中当前选中的物体
        GameObject obj = Selection.activeGameObject;

        //如果没有选择任何物体，弹出提示并退出
        if (obj == null)
        {
            EditorUtility.DisplayDialog("ERROR", "No select obj!!", "ENTRY");
            return;
        }

        //记录数据
        GetContent(obj, flag);

        //复制到剪贴板  
        EditorBasic.CopyToBoard(logText);
    }

    static void GetContent(GameObject obj, int flag)
    {
        Vector3 temp;
        if (flag == 0)
        {
            temp = obj.transform.localPosition;
        }
        else if (flag == 1)
        {
            temp = obj.transform.localRotation.eulerAngles;
        }
        else if (flag == 2)
        {
            temp = obj.transform.localScale;
        }

        logText += $"{obj.transform.localPosition.x}f,{obj.transform.localPosition.y}f,{obj.transform.localPosition.z}f";
    }

}