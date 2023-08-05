using System;
using System.Collections;
using System.Collections.Generic;
using CallPalCatGames.QFrameworkExtension;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class ImageComment : MonoBehaviour
{
    public GameObject effect;
    
    private Image _head;
    private Text _name;
    private Text _comment;
    private Text _praise;

    private void Start()
    {
        _head = transform.FindRecursion<Image>("ImageHead");
        _name = transform.FindRecursion<Text>("TextName");
        _comment = transform.FindRecursion<Text>("TextCommentContent");
        _praise = transform.FindRecursion<Text>("TextPraise");

        int number = gameObject.Number();
        var messageComment = DataManager.Instance.GetCurrentSceneData().messageComments[number];
        _head.sprite = ResourcesManager.Instance.GetSprite(messageComment.sender.head);
        _name.text = messageComment.sender.name;
        _comment.text = messageComment.content;
        
        if (gameObject.Number()==2)
        {
            var componentInChildren = gameObject.GetComponentInChildren<NumberSlowMotion>();
            componentInChildren.AttributeValue = messageComment.praise;
            SaveManager.Instance.SaveMap.praise += messageComment.praise;
            CreateEffect();
        }
        else
        {
            _praise.text = messageComment.praise+"";
        }
    }

    private void CreateEffect()
    {
        var effectPos = transform.FindRecursion<Transform>("EffectPos");
        var image = transform.FindRecursion<Image>("ImageHeart");

        float totalTime = GetComponentInChildren<NumberSlowMotion>().slowMotionTime;
        int totalCount = 15;
        float perTime = totalTime / totalCount;

        IEnumerator CreateSome()
        {
            for (int i = 0; i < totalCount; i++)
            {
                var go = GameObject.Instantiate(effect, effectPos, false);
                go.transform.localPosition = effectPos.transform.localPosition;
                go.GetComponent<RectTransform>().DOAnchorPosX(RandomHelper.GetRandomPlusAndMinus(25f), 0f).SetRelative(true);
                
                image.transform.DOScale(1.4f, perTime).OnComplete(() =>
                {
                    image.transform.localScale = Vector3.one;
                });
                
                yield return new WaitForSeconds(perTime);
            }
        }

        StartCoroutine(CreateSome());
    }
}