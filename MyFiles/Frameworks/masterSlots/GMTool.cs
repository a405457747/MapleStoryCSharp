using UnityEditor;
using UnityEngine;

public class GMTool : EditorWindow
{
    private bool b1;
    private float f1;

    private void OnGUI()
    {
        GUILayout.BeginVertical();

        f1 = EditorGUILayout.Slider("f1", f1, -100, 100);
        if (GUILayout.Button(new GUIContent("bt1")))
        {
        }

        ;
        if (GUILayout.Button(new GUIContent("bt2")))
        {
        }

        ;
        b1 = GUILayout.Toggle(b1, "b1");

        GUILayout.EndVertical();
    }

    [MenuItem("Framework/GMTool/OpenWindow")]
    public static void OpenWindow()
    {
        var window = (GMTool) GetWindow(typeof(GMTool));
        window.position = new Rect(0, 0, 500, 500f);
        window.Show();
    }
}