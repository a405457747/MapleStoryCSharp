using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoBGMask : MonoBehaviour
{
    private Image img;
    private RectTransform rect;

    private float width;
    private float height;
    private Sprite sp;

    public float addRatio = 0.2f;
    public float AnimalCost = 1f;


    private void Awake()
    {
        img = GetComponent<Image>();

        if (img==null)
        {
            return;
        }

        rect = img.GetComponent<RectTransform>();
        width = rect.sizeDelta.x;
        height = rect.sizeDelta.y;

        rect.SetAnchor(AnchorPresets.StretchAll);

        rect.offsetMin = new Vector2(0, 0);
        rect.offsetMax = new Vector2(0, 0);

        sp = img.sprite;

        img.sprite = null;
        img.color = ColorHelper.GetColor(0, 0, 0, 255 / 2);

        var go = new GameObject("child", typeof(Image));


        var newPopImg = go.AddOrGetComponent<PopImage>();
        newPopImg.animalCost = AnimalCost;
        newPopImg.addRatio = addRatio;

        Image newImg = go.GetComponent<Image>();
        newImg.sprite = sp;

        go.transform.SetParent(this.transform, false);
        var newRect = newImg.GetComponent<RectTransform>();
        newRect.sizeDelta = new Vector2(width, height);
        go.transform.SetAsFirstSibling();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}