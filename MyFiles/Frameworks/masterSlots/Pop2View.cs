using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Pop2View : View
{
    protected Image bg;
    protected Transform main;
    protected CanvasGroup canvasGroup;
    protected Button closeBtn;
    private const float cost = 0.2f;

    public override void Initialize(string name)
    {
        base.Initialize(name);
        bg = GetComponent<Image>();
        bg.color = Color.white;
        main = this.transform;
        //main = transform.Find("ImagePopWrap");
        canvasGroup = main.gameObject.AddComponent<CanvasGroup>();
        closeBtn = main.Find("ImagePopWrap/Button").GetComponent<Button>();
        closeBtn.onClick.AddListener(() => { closeAniml(); });
    }

    public virtual void closeAniml()
    {
        // bg.color = ColorHelper.GetColor(0, 0, 0, 0);

        var s = DOTween.Sequence();
        s.Append(main.DOScale(0, cost));
        s.Insert(0, canvasGroup.DOFade(0, cost));
        s.OnComplete(() => { Hide(); });
    }

    public override void Show()
    {
        base.Show();
        openAnimal();
    }

    public virtual void openAnimal()
    {
        //  bg.color = ColorHelper.GetColor(0, 0, 0, 100);
        var s = DOTween.Sequence();
        s.SetEase(DG.Tweening.Ease.OutBounce);
        s.Append(canvasGroup.DOFade(1, 0));
        s.Append(main.DOScale(0.5f, 0));
        s.Append(main.DOScale(1f, cost));
    }

    public override void HandleEvent(string eventName, object data)
    {
    }
}