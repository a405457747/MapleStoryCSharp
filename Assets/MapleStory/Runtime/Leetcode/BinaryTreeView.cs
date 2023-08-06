using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MapleStory;
using System.Linq;
using static MapleStory.LogNote;

public class BinaryTreeView : MonoBehaviour
{
    public List<Image> _Images;


    private void Start()
    {
        //Sleep(2f);
    }

    private void Sleep(float f)
    {
        var now = DateTime.Now;
        var want = now + new TimeSpan(TimeSpan.FromSeconds(f).Ticks);

        while (true)
        {
            if (DateTime.Now >= want)
            {
                break;
            }
        }
    }

    public void ShowOne(int val)
    {
        Image wantOne = null;
        foreach (var VARIABLE in _Images)
        {
            VARIABLE.color =Color.white;
            if (int.Parse(VARIABLE.GetComponentInChildren<Text>().text) == val)
            {
                wantOne = VARIABLE;
                Debug("val is ",val);
            }
        }
        wantOne.color=Color.red;
        Sleep(2f);
    }
}
