using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PanelGuideArgs : PanelArgs
{
    public RectTransform target;
    public string content;
    public Vector3 FingerOffset;
    public Vector3 TalkOffset;
    public int GuideID;
}

public class PanelGuide : Panel
{
    private Image ImageFinger;
    private Image ImageTalkt;
    private GuidanceRectController _controller;
    private Tween FingerTween;
    private RectTransform fingerRect;
    private RectTransform talkRect;
    private PanelGuideArgs temp;

    public override void OnRemove()
    {
        base.OnRemove();
        FingerTween.Kill();
        FingerTween = null;
    }

    public override void OnInit(PanelArgs arguments)
    {
        base.OnInit(arguments);

        ImageFinger = transform.Find("ImageFinger").GetComponent<Image>();
        ImageTalkt = transform.Find("ImageTalkt").GetComponent<Image>();
        fingerRect = ImageFinger.GetComponent<RectTransform>();
        talkRect = ImageTalkt.GetComponent<RectTransform>();

        _controller = GetComponent<GuidanceRectController>();

        temp = (PanelGuideArgs) this.args;

        _controller.ChangeTarget(temp.target);

        ImageTalkt.GetComponentInChildren<Text>().DOText(temp.content, 0.35f);
        fingerRect.DOAnchorPos3D(temp.FingerOffset, 0f);
        talkRect.DOAnchorPos3D(temp.TalkOffset, 0f);

        SendMessage($"FingerAnimation{temp.GuideID}", SendMessageOptions.DontRequireReceiver);
    }

    public void FingerAnimation1()
    {
        fingerRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0f, 142f);
        talkRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0f, 105f);
    }

    public void FingerAnimation2()
    {
                
        FingerTween = fingerRect.DOAnchorPosY(126, 1.35f).SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.Linear);
        
        fingerRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0f, 142f);
        talkRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0f, 105f);
    }

    public void FingerAnimation3()
    {
        fingerRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0f, 142f);
        talkRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0f, 105f);
    }

    public void FingerAnimation4()
    {
        fingerRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0f, 142f);
        talkRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0f, 105f);
    }
}