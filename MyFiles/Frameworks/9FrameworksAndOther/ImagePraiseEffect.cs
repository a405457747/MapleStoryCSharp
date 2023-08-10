using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ImagePraiseEffect : MonoBehaviour
{
    [FormerlySerializedAs("spriteName")] public Sprite[] spriteNames;

    public float saveTime = 3f;

    private Image _image;
    private Sequence _s;

    private void Awake()
    {
        var arr = new Sprite[spriteNames.Length];
        for (var i = 0; i < arr.Length; i++) arr[i] = ResourcesManager.Instance.GetSprite(spriteNames[i].name);

        var randomSprite = arr[Random.Range(0, arr.Length)];

        _image = GetComponent<Image>();
        _image.sprite = randomSprite;

        var fadeTime = 0.3f;
        _s = DOTween.Sequence();
        _s.Append(_image.GetComponent<RectTransform>().DOAnchorPosY(230, saveTime).SetRelative(true));
        _s.Insert(0f, _image.transform.DOScale(0.2f, saveTime).SetRelative(true));
        _s.Insert(saveTime - fadeTime, _image.DOFade(0, fadeTime));
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, RandomHelper.GetRandomPlusAndMinus(15f)));
        Destroy(gameObject, saveTime);
    }

    /*private void Update()
    {
        transform.Translate(transform.up*Time.time*1.5f,Space.Self);
    }*/

    private void OnDestroy()
    {
        _s.Kill();
    }
}