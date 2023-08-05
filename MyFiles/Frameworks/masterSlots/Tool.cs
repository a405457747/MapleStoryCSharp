using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using DG.Tweening;

public class Tool
{

    public static T ListRemoveHead<T>(List<T> lis)
    {
        int headIndex = 0;
        T res = lis[headIndex];
        lis.RemoveAt(headIndex);
        return res;
    }
    
    public static string  StrRepeat(string demo ,int repeatCount)
    {
        StringBuilder sb = new StringBuilder();
        int step = 0;
        while (step<repeatCount)
        {
            sb.Append(demo);
            step += 1;
        }

        return sb.ToString();
    }
    public static void BtnClickAnimal(Transform t)
    {
        t.DOScale(1.15f, 0.05f).OnComplete(() => { t.DOScale(1, 0.05f); });
    }

    public static string ThousandsCoin(int v)
    {
        var str = v.ToString("N");
        var str1 = str.Split('.')[0];
        return str1;
    }

    public static bool Orderly(List<int> data, bool ascending = true)
    {
        for (int i = 0; i < data.Count - 1; i++)
        {
            int first = data[i];
            int second = data[i + 1];
            bool cur = ascending ? second >= first : second <= first;
            if (cur == false)
            {
                return false;
            }
        }

        return true;
    }

    public static T CycleValue<T>(ref int index, List<T> list)
    {
        return list[ (index++) % list.Count];
    }
    
    public static List<T> RingValue<T>(List<T> data, int startIndex, int count)
    {
        List<T> valResult = new List<T>();
        for (int i = 0; i < count; i++)
        {
            int perIndex = i + startIndex;
            int newIndex = perIndex % data.Count;
            T newVal = data[newIndex];
            valResult.Add(newVal);
        }

        return valResult;
    }

    public static string CurrencySerialization(string str)
    {
        Regex r = new Regex(@"[pP]");
        return "$" + r.Replace(str, ".");
    }

    public static int ChartToInt(char c)
    {
        return Convert.ToInt32(c.ToString());
    }

    public static void DestoryAllChild(Transform t)
    {
        int childCount = t.childCount;
        for (int i = 0; i < childCount; i++)
        {
            UnityEngine.GameObject.Destroy(t.GetChild(0).gameObject);
        }
    }

    public static void Integer(ref int val)
    {
        int coinNumLastChar = Tool.ChartToInt(val.ToString().Last());
        val -= coinNumLastChar;
    }

    /*public static string SecondsSeralization(int seconds)
    {
        return null;
    }*/
    public static void Swap<T>(int resIndex, int i, List<T> tmp)
    {
        T resItem = tmp[resIndex];
        tmp[resIndex] = tmp[i];
        tmp[i] = resItem;
    }
}