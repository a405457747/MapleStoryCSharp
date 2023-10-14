using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson.Extensions;

[CreateAssetMenu(fileName = "TestScriptableObj",menuName = "ColaFramework/TestScriptableObj")]
public class TestScriptableObj:ScriptableObject
{
    
    public Dictionary<string, int> charDict=new Dictionary<string, int>()
    {
        {"a",1}
    };
    
    public bool isDD=true;
    
    public int num1;

    public float num2;

    public Vector2 v2;

    public Vector3 v3;

    public Quaternion quaternion;

    public Color color1;

    public Color32 color2;

    public Bounds bounds;

    public Rect rect;

    public AnimationCurve animationCurve;

    [JsonIgnore]
    public string SerializeField;
}
