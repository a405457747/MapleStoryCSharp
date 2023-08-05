using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class JsonTool
{
    [MenuItem("Assets/JsonTool/ClearCache &1")]
    public static void ClearCache()
    {
        PlayerPrefs.DeleteAll();
    }
}