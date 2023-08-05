using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;

public class TipPanel : View
{
    public void TipMsg(string content)
    {
        float cost = game.SuPerSlotsAndCoreAndBrainsAndCoreGoodModel.GetHopeTime(3.5f);
        float hCost = cost / 2;
        float hhCost = hCost / 2;

        Show();
        var effect = Factorys.GetAssetFactory().LoadEffect("tipImage");
        var go = GameObject.Instantiate(effect, tipGameObject.transform, false);
        go.transform.Find("Text").GetComponent<Text>().text = content;
        var rect = go.GetComponent<RectTransform>();
        var s = DOTween.Sequence();
        s.Append(rect.DOScale(0, 0));
        s.Append(rect.DOScale(1, hhCost));
        s.AppendInterval(hCost);
        s.Append(rect.DOAnchorPosY(800, hhCost).SetRelative(true));
        s.OnComplete(() => { GameObject.Destroy(go); });
    }

    public override void Initialize(string name)
    {
        base.Initialize(name);
    }

    private void FixedUpdate()
    {
        if (tipGameObject.transform.childCount == 0)
        {
            Hide();
        }
    }

    public override void HandleEvent(string eventName, object data)
    {
    }

//auto
    private void Awake()
    {
        tipGameObject = transform.Find("tipGameObject").gameObject;
    }

    private GameObject tipGameObject = null;

    private void tipGameObjectSetChild(Transform t) => t.transform.SetParent(tipGameObject.transform, false);
}