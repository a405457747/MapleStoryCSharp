using UnityEditor;
using UnityEngine;

public class CopyTool : EditorWindow
{
    //最终输出的数据
    private static string logText;

    [MenuItem("GameObject/CopyTool/CopyPosition", false, -100)]
    public static void CopyPosition()
    {
        CopyCommon(0);
    }

    [MenuItem("GameObject/CopyTool/CopyRotation", false, -100)]
    public static void CopyRotation()
    {
        CopyCommon(1);
    }

    [MenuItem("GameObject/CopyTool/CopyScale", false, -100)]
    public static void CopyScale()
    {
        CopyCommon(2);
    }

    private static void CopyCommon(int flag)
    {
        //重置数据
        logText = "";
        //获取编辑器中当前选中的物体
        var obj = Selection.activeGameObject;

        //如果没有选择任何物体，弹出提示并退出
        if (obj == null)
        {
            EditorUtility.DisplayDialog("ERROR", "No select obj!!", "ENTRY");
            return;
        }

        //记录数据
        GetContent(obj, flag);

        //复制到剪贴板  
        EditorGame.CopyToBoard(logText);
    }

    private static void GetContent(GameObject obj, int flag)
    {
        Vector3 temp;
        if (flag == 0)
            temp = obj.transform.localPosition;
        else if (flag == 1)
            temp = obj.transform.localRotation.eulerAngles;
        else if (flag == 2) temp = obj.transform.localScale;

        logText +=
            $"{obj.transform.localPosition.x}f,{obj.transform.localPosition.y}f,{obj.transform.localPosition.z}f";
    }

    [MenuItem("GameObject/CopyTool/CopyPath", priority = 20)]
    private static void CopyPath()
    {
        var trans = Selection.activeTransform;
        if (null == trans) return;
        EditorGame.CopyToBoard(GetPath(trans));
    }

    private static string GetPath(Transform trans)
    {
        if (null == trans) return string.Empty;
        if (null == trans.parent) return trans.name;
        return GetPath(trans.parent) + "/" + trans.name;
    }
}