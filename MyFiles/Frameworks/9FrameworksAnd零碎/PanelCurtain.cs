using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class PanelCurtainArgs:PanelArgs
{
    public string SceneName { get; set; }
}

public class PanelCurtain : Panel
{
    public float totalTime = 0.3f;
    
    public override void OnInit(PanelArgs arguments)
    {
        base.OnInit(arguments);
        var image = GetComponent<Image>();
         totalTime = 1f;

        image.DOFade(1f, totalTime / 2f).OnComplete(() =>
        {
            PanelManager.Instance.RemoveAllPanelExceptPanelCurtain();
            var sceneName = ((PanelCurtainArgs) this.args).SceneName;
            SceneryManager.Instance.LoadScene(sceneName);
            image.DOFade(0f, totalTime / 2f).OnComplete(() =>
            {
                PanelManager.Instance.RemovePanel(nameof(PanelCurtain));
            });
        });
    }
}