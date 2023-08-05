using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SetPanel : PopView,IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
       // closeAniml();
       // Log.LogParas("click setPanel");
	   
    }
    public override void Initialize(string name)
    {
        base.Initialize(name);
        
        SetToggleState(Game.I.saveSystem.SaveMap._soundEnable,SoundToggle);
        SetToggleState(Game.I.saveSystem.SaveMap._musicEnable,MusicToggle);

        TermsButtonAction = () =>
        {
            Application.OpenURL("http://www.superwinslots.com/");
            print("11");
        };
        PrivacyButtonAction = () =>
        {
            Application.OpenURL("http://www.superwinslots.com/p.html");
            print("12");
        };
        ContactButtonAction = () =>
        {
            print("13");
            string gameMark = $"--From the game {Application.identifier} version {Application.version} ";
            Log.LogParas(gameMark);
            var str = "mailto:" + "zdian79543@gmail.com" + "?subject=" + "&body="+gameMark;
            
            var str2 ="mailto:" + "zdian79543@gmail.com" + "?subject=" + "&body=";
            Application.OpenURL(str2);
        };

        SoundToggleAction = (b) =>
        {
            Game.I.saveSystem.SaveMap._soundEnable = b;
            Game.I.saveSystem.Save();
            SetONOFFImage(SoundToggle, b);
        };

        MusicToggleAction = (b) =>
        {
            Game.I.saveSystem.SaveMap._musicEnable = b;
            Game.I.audioSystem.SwitchMusic();
            Game.I.saveSystem.Save();
            SetONOFFImage(MusicToggle, b);
        };
        verTextRefresh("Version: "+Application.version);
    }

    public void SetToggleState(bool enable, Toggle toggle)
    {
        if (enable)
        {
            toggle.isOn = true;
            SetONOFFImage(toggle, true);
        }
        else
        {
            toggle.isOn = false;
            SetONOFFImage(toggle, false);
        }
    }

    public void SetONOFFImage(Toggle t, bool isOn)
    {
        var on = t.transform.Find("ImageOn").gameObject;
        var off = t.transform.Find("ImageOff").gameObject;
        on.SetActive(false);
        off.SetActive(false);

        if (isOn)
        {
            on.SetActive(true);
        }
        else
        {
            off.SetActive(true);
        }
    }

    public override void HandleEvent(string eventName, object data)
    {
    }

//auto
   private void Awake()
	{
		TermsButton=transform.Find("ImagePopWrap/TermsButton").GetComponent<Button>();
	PrivacyButton=transform.Find("ImagePopWrap/PrivacyButton").GetComponent<Button>();
	ContactButton=transform.Find("ImagePopWrap/ContactButton").GetComponent<Button>();
	MusicToggle=transform.Find("ImagePopWrap/MusicToggle").GetComponent<Toggle>();
	SoundToggle=transform.Find("ImagePopWrap/SoundToggle").GetComponent<Toggle>();
	verText=transform.Find("ImagePopWrap/verText").GetComponent<Text>();
	
        TermsButton.onClick.AddListener(()=>{TermsButtonAction?.Invoke();});
	PrivacyButton.onClick.AddListener(()=>{PrivacyButtonAction?.Invoke();});
	ContactButton.onClick.AddListener(()=>{ContactButtonAction?.Invoke();});
	MusicToggle.onValueChanged.AddListener((b)=>{MusicToggleAction?.Invoke(b);});
	SoundToggle.onValueChanged.AddListener((b)=>{SoundToggleAction?.Invoke(b);});
	
	}
	private Button TermsButton=null;
	public Action TermsButtonAction{get;set;}
	private Button PrivacyButton=null;
	public Action PrivacyButtonAction{get;set;}
	private Button ContactButton=null;
	public Action ContactButtonAction{get;set;}
	private Toggle MusicToggle=null;
	public Action<bool> MusicToggleAction{get;set;}
	private Toggle SoundToggle=null;
	public Action<bool> SoundToggleAction{get;set;}
	private Text verText=null;
	
    public void verTextRefresh(string t)=>verText.text=t;
	    
}
