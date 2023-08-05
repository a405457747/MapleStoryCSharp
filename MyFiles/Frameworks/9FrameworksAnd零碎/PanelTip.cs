using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


public class PanelTipArgs:PanelArgs
{
    public string Content { get; set; }
}

public class PanelTip : Panel
{
    private Tween _tween;
    
    public override void OnInit(PanelArgs arguments)
    {
        base.OnInit(arguments);
        
        _tween = rectTransform.DOLocalMoveY(400, 2.5f).OnComplete(() =>
        {
            PanelManager.Instance.ClosePanel(nameof(PanelTip));
        });
    }

    public override void OnOpen(PanelArgs arguments)
    {
        base.OnOpen(arguments);
        transform.localPosition = localPosition;
        _tween.Restart();
        PanelTipArgs temp =  (this.args) as PanelTipArgs;
    }
}