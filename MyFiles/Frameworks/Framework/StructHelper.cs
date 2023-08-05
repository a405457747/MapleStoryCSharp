using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StructHelper
{
    public static void Swap<T>(ref T a, ref T b) where T : struct
    {
        var temp = a;
        a = b;
        b = temp;
    }

    public static int GetCharNumber(char temp)
    {
        return int.Parse(temp.ToString());
    }

    public static Color GetColor(int r, int g, int b, int a = 255)
    {
        return new Color(r / 255f, g / 255f, b / 255f, a / 255f);
    }

    public static T GetEnumByInt<T>(int val) where T : Enum
    {
        return (T)Enum.Parse(typeof(T), val.ToString());
    }

    public static Vector2 WorldToCanvasPos(Canvas canvas, Vector3 world)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform,
            world, canvas.GetComponent<Camera>(), out var pos);
        return pos;
    }

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

    public static bool IsEven(int num)
    {
        return (num & 1) != 1;
    }
}