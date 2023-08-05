using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TipPanel : Panel
{
    public override void Initialize()
    {
        tier = PanelTier.AlwaysInFront;
        base.Initialize();
    }

    private void ErrorEventCallback(ErrorEvent obj)
    {
        Show();
        MainTextRefresh(obj.ErrorMsg);
        this.Delay(3f, () =>
        {
            Hide();
        });
    }

    //auto
    private void Awake()
    {
        MainText = transform.Find("MainText").GetComponent<Text>();

        Incident.DeleteEvent<ErrorEvent>(ErrorEventCallback);
        Incident.RigisterEvent<ErrorEvent>(ErrorEventCallback);
    }


    private Text MainText = null;

    public void MainTextRefresh(string t) => MainText.text = t;
}