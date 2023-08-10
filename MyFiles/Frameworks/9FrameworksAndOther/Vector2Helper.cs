using UnityEngine;

public class Vector2Helper : MonoBehaviour
{
    public static Vector2 WorldToCanvasPos(Canvas canvas, Vector3 world)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform,
            world, canvas.GetComponent<Camera>(), out var pos);
        return pos;
    }
}