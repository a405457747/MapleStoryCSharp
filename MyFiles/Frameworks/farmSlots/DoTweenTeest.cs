using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
          Image img;
        print(transform.GetSiblingIndex());
        var k = DOTween.Sequence();
        k.Complete();
        transform.DOScale(2, 2);
        transform.DOScale(2f, 2f);
        DOTween.Pause(k);
        DOTween.Play(k);
        k.IsPlaying();
        k.PlayForward();
        k.Pause();
        k.Complete();
        k.IsPlaying();
        RectTransform re = transform as RectTransform;
        re.DOAnchorPosY(10, 2).SetRelative(true);
        re.DOAnchorPosY(3, 2).SetSpeedBased(true).SetEase(Ease.Flash);
        //img.DO
        var g222 = Ease.InBounce;
        //re.DOAnchorPos(Vector2.one, 2f)
        Image g;
        GameObject go;
        Button b;
        // g.DOFade(0,)
        CanvasGroup canvasGroup;
        var k1 = gameObject.AddComponent<CanvasGroup>();
        //g.transform.DORotate()
        // transform.localEulerAngles =
        /*myTweener =
            Model4.transform.DOLocalRotate(new Vector3(0, 360, 0), 3, RotateMode.FastBeyond360)
                .SetAs(_tParams).SetLoops(-1, LoopType.Restart)
                .Pause();*/

        Canvas c = null;
        c.overrideSorting = true;
        c.sortingOrder = 2;
        c.overrideSorting = true;
        transform.parent.GetSiblingIndex();
    }

    // Update is called once per frame
    void Update()
    {
    }
}