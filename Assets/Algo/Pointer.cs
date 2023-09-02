using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MapleStory;
using UnityEngine.Events;
using UniRx;
using System.Linq;
using UnityEngine.Serialization;
using static MapleStory.LogNote;


public class Pointer : MonoBehaviour
{
    private LIST _list;
    private Image pointerImage;

    [FormerlySerializedAs("curIdx")] public int val=0;

    private void Awake()
    {
        pointerImage = GetComponentInChildren<Image>();
    }

    public void Init(int idx,LIST list,Color pColor)
    {
        val = idx;
        this._list = list;
        pointerImage.color = pColor;

        UpdateIndexPos();
    }

    void UpdateIndexPos()
    {
        transform.position = _list.transform.localPosition+new Vector3(0,1,0)  + new Vector3(1, 0, 0) * val;
    }

    public void SetVal(int val)
    {
        this.val = val;
        UpdateIndexPos();
    }

    public void Add(int num)
    {
        val += num;
        UpdateIndexPos();
    }

    public void Reduce(int num)
    {
        val -= num;
        UpdateIndexPos();
    }
}
