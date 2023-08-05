using System;
using System.Collections.Generic;
using System.Linq;

public class StructHelper
{
    public static int RoundNumbers(int number, int place = 1) //如果是1取整为10
    {
        var arr = number.ToString().ToCharArray();
        for (var i = arr.Length - 1; i >= 0 && place > 0; i--, place--) arr[i] = '0';

        return int.Parse(new string(arr));
    }

    public static void Swap<T>(ref T a, ref T b) where T : struct
    {
        var temp = a;
        a = b;
        b = temp;
    }

    public static double GetFractionalPart(double f) //获取小数部分
    {
        return f - Math.Floor(f);
    }

    public static int GetCharNumber(char temp)
    {
        return int.Parse(temp.ToString());
    }

    public static T GetEnumByInt<T>(int val) where T : Enum
    {
        return (T) Enum.Parse(typeof(T), val.ToString());
    }

    public static T StrToEnum<T>(string enumStr) where T : Enum
    {
        return (T)Enum.Parse(typeof(T), enumStr);
    }

    public static List<T> GetEnums<T>() where T : Enum
    {
        return Enum.GetValues(typeof(T)).Cast<T>().ToList();
    }

    /*
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
    */
}