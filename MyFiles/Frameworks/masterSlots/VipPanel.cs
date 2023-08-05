using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class VipPanel : Pop2View
{
    private Transform[] vipItems;

    public override void Initialize(string name)
    {
        base.Initialize(name);

        vipItems = textGameObject.transform.GetChildArray();
        for (var index = 0; index < vipItems.Length; index++)
        {
            var vipItem = vipItems[index];
            var lv = game.superValueVIPModel.GetVipIdByUIname(vipItem.name);
            VipDesign vipDes = game.superValueVIPModel.GetVipDesin(lv);
            var seletctImage = vipItem.transform.Find("selectImage").GetComponent<Image>();
            var levelText = vipItem.transform.Find("Text").GetComponent<Text>();
            var vipText = vipItem.transform.Find("Text (1)").GetComponent<Text>();
            var storeText = vipItem.transform.Find("Text (2)").GetComponent<Text>();
            var onlineText = vipItem.transform.Find("Text (3)").GetComponent<Text>();
            var dailyText = vipItem.transform.Find("Text (4)").GetComponent<Text>();
            var icon = vipItem.transform.Find("Image").GetComponent<Image>();

            levelText.text = vipDes.VipName.ToString();
            vipText.text = Tool.ThousandsCoin(vipDes.NeedPoint);
            storeText.text = StringHelper.GetRatio((float) vipDes.StoreCoins, "f0");
            onlineText.text = StringHelper.GetRatio((float) vipDes.OnlineCoins, "f0");
            dailyText.text = StringHelper.GetRatio((float) vipDes.DailyCoins, "f0");
            icon.sprite = game.superValueVIPModel.GetLvIcon(lv);
        }
    }

    public override void Show()
    {
        base.Show();

        for (var index = 0; index < vipItems.Length; index++)
        {
            var vipItem = vipItems[index];
            var lv = game.superValueVIPModel.GetVipIdByUIname(vipItem.name);
            var seletctImage = vipItem.transform.Find("selectImage").GetComponent<Image>();
            seletctImage.gameObject.SetActive(false);
            if (lv == game.superValueVIPModel.GetCurViPLV())
            {
                seletctImage.gameObject.SetActive(true);
            }
        }

        int nextLv = game.superValueVIPModel.GetNextVipLv();
        curStatusImage.sprite = game.superValueVIPModel.GetCurLvIcon();
        nextStatusImage.sprite = game.superValueVIPModel.GetLvIcon(nextLv);
        statusText.text = "YOUR STATUS:" + game.superValueVIPModel.GetCurLvName();
        nextStatusText.text = "NEXT STATUS:" + game.superValueVIPModel.GetLvName(nextLv);
        progressSlider.value = game.superValueVIPModel.GetLvSliderVal();

        msgTextRefresh(game.superValueVIPModel.vipPointMsg);
        if (game.superValueVIPModel.IsMaxLv())
        { 
	        Debug.LogWarning(" 已经是最大的等级了，也许将来要处理超过vip值,也有可能会有bug");
        }
    }

    public override void HandleEvent(string eventName, object data)
    {
    }

//auto
   private void Awake()
	{
		topGameObject=transform.Find("ImagePopWrap/topGameObject").gameObject;
	progressSlider=transform.Find("ImagePopWrap/topGameObject/progressSlider").GetComponent<Slider>();
	curStatusImage=transform.Find("ImagePopWrap/topGameObject/progressSlider/curStatusImage").GetComponent<Image>();
	nextStatusImage=transform.Find("ImagePopWrap/topGameObject/progressSlider/nextStatusImage").GetComponent<Image>();
	statusText=transform.Find("ImagePopWrap/topGameObject/progressSlider/statusText").GetComponent<Text>();
	nextStatusText=transform.Find("ImagePopWrap/topGameObject/progressSlider/nextStatusText").GetComponent<Text>();
	msgText=transform.Find("ImagePopWrap/topGameObject/progressSlider/msgText").GetComponent<Text>();
	textGameObject=transform.Find("ImagePopWrap/Image (1)/textGameObject").gameObject;
	selectImage=transform.Find("ImagePopWrap/Image (1)/textGameObject/ImageVipItem/selectImage").GetComponent<Image>();
	
        progressSlider.onValueChanged.AddListener((f)=>{progressSliderAction?.Invoke(f);});
	
	}
	private GameObject topGameObject=null;
	private Slider progressSlider=null;
	public Action<float> progressSliderAction{get;set;}
	private Image curStatusImage=null;
	private Image nextStatusImage=null;
	private Text statusText=null;
	private Text nextStatusText=null;
	private Text msgText=null;
	private GameObject textGameObject=null;
	private Image selectImage=null;
	
    private void topGameObjectSetChild(Transform t)=>t.transform.SetParent(topGameObject.transform, false);
	public void curStatusImageRefresh(Sprite s)=>curStatusImage.sprite=s;
	public void nextStatusImageRefresh(Sprite s)=>nextStatusImage.sprite=s;
	public void statusTextRefresh(string t)=>statusText.text=t;
	public void nextStatusTextRefresh(string t)=>nextStatusText.text=t;
	public void msgTextRefresh(string t)=>msgText.text=t;
	private void textGameObjectSetChild(Transform t)=>t.transform.SetParent(textGameObject.transform, false);
	public void selectImageRefresh(Sprite s)=>selectImage.sprite=s;
	    
}
