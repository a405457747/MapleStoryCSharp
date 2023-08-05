using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions;

public class ActivityManager : MonoSingleton<ActivityManager>
{
    protected override void Awake()
    {
        base.Awake();
    }

    // 果冻效果
    public void Jelly(Transform t, Vector3 punch = default, float duration = 0.35f, int vibrato = 12, float elasticity = 0.5f)
    {
        if (punch == default)
        {
            punch = new Vector3(-0.2f, 0.2f, 0f);
        }

        var origin = t.localScale;
        t.DOPunchScale(punch, duration, vibrato, elasticity)
            .OnComplete(() => { t.localScale = origin; });
    }

    public void LoopRotate(Transform t, float duration = 1f)
    {
        t.DOLocalRotate(Vector3.forward * 360, duration, RotateMode.FastBeyond360).SetLoops(-1);
    }

    public void ScaleBigToOriginal(Transform t, Action completecallBack = null, float totalTime = 0.35f, float scale = 1.05f)
    {
        var origin = t.localScale;
        var big = origin * scale;

        t.DOScale(big, totalTime / 2f).OnComplete(() =>
        {
            t.DOScale(origin, totalTime / 2f);
            completecallBack?.Invoke();
        });
    }

    public void Shake(Transform t, float duration = 0.5f)
    {
        var origin = t.localScale;
        t.DOShakePosition(duration).OnComplete(() => { t.localPosition = origin; });
    }

    public IEnumerator Flash<T>(Transform t, int count = 6, float totalTime = 0.53f) where T : MonoBehaviour
    {
        var item = transform.GetComponent<T>();
        Assert.IsTrue(StructHelper.IsEven(count));

        var per = totalTime / count;

        for (var i = 0; i < count; i++)
        {
            yield return new WaitForSeconds(per);

            if (StructHelper.IsEven(i))
                item.enabled = false;
            else
                item.enabled = true;
        }
    }

    public void ScaleZeroToOriginal(Transform t, float duration = 0.3f)
    {
        var originalScale = t.transform.localScale;

        t.transform.localScale = Vector3.zero;

        t.DOScale(originalScale, duration);
    }

    public void CanvasGroupFadeIn(GameObject go, Action endCallback = null, float time = 0.65f)
    {
        var canvasGroup = go.AddOrGetComponent<CanvasGroup>();
        //canvasGroup.alpha = 0f;

        canvasGroup.DOFade(1f, time).OnComplete(() =>
        {
            endCallback?.Invoke();
        });
    }

    public void CanvasGroupFadeOut(GameObject go, Action endCallback = null, float time = 0.65f)
    {
        var canvasGroup = go.AddOrGetComponent<CanvasGroup>();
        //canvasGroup.alpha = 1f;

        canvasGroup.DOFade(0f, time).OnComplete(() =>
        {
            endCallback?.Invoke();
        });
    }
}