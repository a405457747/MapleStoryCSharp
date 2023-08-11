using System;
using System.Collections;
using System.Collections.Generic;
using MapleStory;
using UnityEngine;

public class SaveManagerSample : MonoBehaviour
{
    private SaveManager save;

    private void Start()
    {
        save = FindObjectOfType<SaveManager>();

        print(save == null);
        print(save.SaveMap == null);
        print(save.SaveMap.userSet.musicEnable.Value);
        print(save.SaveMap.userSet.rDic["a"]);
        save.SaveMap.userSet.rDic["a"] = 6;
        save.SaveMap.userSet.musicEnable.Value = false;
        print(save.SaveMap.userSet.musicEnable.Value);

        save.SaveData();
    }
}