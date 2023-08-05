using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickEffect : MonoBehaviour
{
    private void Start()
    {
        var btn = GetComponent<Button>();
        btn.onClick.AddListener(() =>
        {
            ActivityManager.Instance.ScaleBigToOriginal(btn.transform);
        });
    }
}
