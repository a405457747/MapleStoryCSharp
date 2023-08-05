using UnityEngine;
using Debug = System.Diagnostics.Debug;

public class Vector3Helper : MonoBehaviour
{
    public static Vector3 GetCursorPos()
    {
        Debug.Assert(Camera.main != null, "Camera.main != null");
        var cursorPos = Camera.main.ScreenToWorldPoint(new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            -Camera.main.transform.position.z
        ));
        return cursorPos;
    }
}