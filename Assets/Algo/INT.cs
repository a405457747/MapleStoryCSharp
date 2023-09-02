using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MapleStory;
using UnityEngine.Events;
using UniRx;
using System.Linq;
using TMPro;
using static MapleStory.LogNote;


public class INT : MonoBehaviour
{
    public TextMeshProUGUI textValue;
    public TextMeshProUGUI textIdx;
    public int val;
    private LIST _list;

    public void Init(int val, LIST l)
    {
        this.val = val;
        this._list = l;


        UpdateValUI(this.val + "");

        UpdateIndexAbout();
    }


    public void UpdateIndexAbout()
    {
        int idx = _list.GetIdx(this.val);
        UpdateIdxUI(idx + "");
        transform.position = this._list.transform.localPosition + new Vector3(1, 0, 0) * idx;
    }

    public void UpdateValUI(string str)
    {
        textValue.text = str;
    }

    public void UpdateIdxUI(string str)
    {
        textIdx.text = str;
    }
}