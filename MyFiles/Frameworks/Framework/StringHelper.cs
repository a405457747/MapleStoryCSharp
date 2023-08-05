using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class StringHelper
{
    public static int GetPureNumber(string str)
    {
        var newStr = Regex.Replace(str, "[^0-9]*", "");
        if (newStr == "") return 0;
        return int.Parse(newStr);
    }

    public static string GetRatio(float val, string format = "f1")
    {
        var tempVal = val;
        tempVal *= 100;
        return tempVal.ToString(format) + "%";
    }

    public static string GetFormatVersion(int version)
    {
        var charList = version.ToString().ToList();

        while (charList.Count != 6) charList.Insert(0, '0');

        var list = new List<string>();
        for (var i = 0; i < charList.Count; i += 2)
        {
            var tempStr = charList[i] + charList[i + 1].ToString();
            var tempInt = int.Parse(tempStr);
            list.Add(tempInt.ToString());
        }

        return string.Join(".", list);
    }

    public static string GetShortForNumber(long num, string digits = "f1")
    {
        var numBit = num.ToString().Length;

        long numFloat;
        checked
        {
            numFloat = num;
        }

        if (numBit < 4)
        {
            return num.ToString();
        }
        else if (numBit < 7)
        {
            numFloat /= 1000L;
            return $"{numFloat.ToString(digits)}K";
        }
        else if (numBit < 10)
        {
            numFloat /= 1000000L;
            return $"{numFloat.ToString(digits)}M";
        }
        else if (numBit < 13)
        {
            numFloat /= 1000000000L;
            return $"{numFloat.ToString(digits)}B";
        }
        else if (numBit < 16)
        {
            numFloat /= 1000000000000L;
            return $"{numFloat.ToString(digits)}T";
        }
        else if (numBit < 19)
        {
            numFloat /= 1000000000000000L;
            return $"{numFloat.ToString(digits)}aa";
        }
        else if (num < long.MaxValue)
        {
            numFloat /= 1000000000000000000L;
            return $"{numFloat.ToString(digits)}bb";
        }

        //cc uu
        return "Max";
    }
}