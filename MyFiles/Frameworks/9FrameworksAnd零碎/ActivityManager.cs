using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class ActivityManager : MonoSingleton<ActivityManager>
{
    protected override void Awake()
    {
        base.Awake();
    }

    public void Jelly(Transform t)
    {
        var origin = t.localScale;
        t.DOPunchScale(new Vector3(-0.2f, 0.2f, 0f), 0.35f, 12, 0.5f)
            .OnComplete(() => { t.localScale = origin; });
    }

    public void Rotate(Transform t)
    {
        t.DOLocalRotate(Vector3.forward * 360, 1f, RotateMode.FastBeyond360).SetLoops(-1);
    }

    public void ScaleBigToOriginal(Transform t, Action callBack = null, float totalTime = 0.35f, float addScale = 1.05f)
    {
        var origin = t.localScale;
        var big = origin * addScale;
        t.DOScale(big, totalTime / 2f).OnComplete(() =>
        {
            t.DOScale(origin, totalTime / 2f);
            callBack?.Invoke();
        });
    }

    public void Shake(Transform t)
    {
        var origin = t.localScale;
        t.DOShakePosition(0.5f).OnComplete(() => { t.localPosition = origin; });
    }

    public IEnumerator Flash<T>(Transform t) where T : MonoBehaviour
    {
        var item = transform.GetComponent<T>();

        var count = 6;
        var per = 0.53f / count;

        for (var i = 0; i < count; i++)
        {
            yield return new WaitForSeconds(per);
            if (i % 2 == 0)
                item.enabled = false;
            else
                item.enabled = true;
        }
    }

    public void ScaleZeroToOriginal(Transform t)
    {
        var originalScale = t.transform.localScale;
        t.transform.localScale = Vector3.zero;
        t.DOScale(originalScale, 0.3f);
    }

    public void CanvasGroupFadeIn(GameObject go, Action endCallback = null, float time = 0.65f)
    {
        var canvasGroup = go.AddOrGetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        canvasGroup.DOFade(1f, time).OnComplete(() =>
        {
            canvasGroup.alpha = 0f;
            endCallback?.Invoke();
        });
    }

    public void CanvasGroupFadeOut(GameObject go, Action endCallback = null, float time = 0.65f)
    {
        var canvasGroup = go.AddOrGetComponent<CanvasGroup>();
        canvasGroup.alpha = 1f;
        canvasGroup.DOFade(0f, time).OnComplete(() =>
        {
            endCallback?.Invoke();
            canvasGroup.alpha = 1f;
        });
    }
}