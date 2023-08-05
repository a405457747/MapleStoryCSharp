using UnityEngine;
using DG.Tweening;

public class BtnBreath : MonoBehaviour
{
// Start is called before the first frame update
    void Start()
    {
//在自身的大小上加上0.2倍
        Vector3 effectScale = transform.localScale - new Vector3(0.2f, 0.2f, 0.2f);
//设置动画
        Tweener tweener = transform.DOScale(effectScale, 1f);
//设置动画loop属性
        tweener.SetLoops(-1, LoopType.Yoyo);
        tweener.Play();
    }
}