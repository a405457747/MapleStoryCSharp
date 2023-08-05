using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PanelTipArgs : PanelArgs
{
    public string Content { get; set; }
}

public class PanelTip : Panel
{
    private Tween _tween;

    public override void OnInit(PanelArgs arguments)
    {
        base.OnInit(arguments);

        _tween = transform.GetRect().DOLocalMoveY(400, 2.5f).OnComplete(() =>
        {
            PanelManager.Instance.RemovePanel<PanelTip>();
        });
    }
}