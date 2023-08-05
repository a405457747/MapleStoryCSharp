/*using System;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;

//默认就是IOS
public class AdmobAD : IAdvert
{
    private RewardedInterstitialAd rewardedInterstitialAd { get; set; }
    private RewardedAd rewardedAd { get; set; }
    private InterstitialAd interstitial { get; set; }
    private InterstitialAd interstitialDefault { get; set; }

    public override void Init()
    {
        base.Init();

        if (Differences.Android())
        {
            if (Game.CurrentAndroidChannel == AndroidChannel.Google)
            {
                InsertDefaultID = Game.SDKGoogle.Admob_Insert_Default;
                AwardID = Game.SDKGoogle.Admob_Reward;
                InsertID = Game.SDKGoogle.Admob_Insert_Open;
            }
            else if (Game.CurrentAndroidChannel == AndroidChannel.Amazon)
            {
                InsertDefaultID = Game.SDKAmazon.Admob_Insert_Default;
                AwardID = Game.SDKAmazon.Admob_Reward;
                InsertID = Game.SDKAmazon.Admob_Insert_Open;
            }

            if (Differences.Ediotr() || Game.IsOpenTestAD)
            {
                InsertDefaultID = Andriod_Test_Insert_ID;
                AwardID = Andriod_Test_Award_ID;
                InsertID = Andriod_Test_Insert_ID;
            }
        }
        else if (Differences.IOS())
        {
            InsertDefaultID = Game.SDKIOS.Admob_Insert_Default;
            AwardID = Game.SDKIOS.Admob_Reward;
            InsertID = Game.SDKIOS.Admob_Insert_Open;

            if (Differences.Ediotr() || Game.IsOpenTestAD)
            {
                InsertDefaultID = IOS_Test_Insert_ID;
                AwardID = IOS_Test_Award_ID;
                InsertID = IOS_Test_Insert_ID;
            }
        }

        MobileAds.Initialize(initStatus => { InitSuccess(); });

        /*LoadInsertDefault();
        LoadAward();
        LoadInsert();#1#

        LoadThreeAD();
    }

    private void LoadThreeAD()
    {
        StartCoroutine(repeatLoad(InsertDefaultReady, LoadInsertDefault, "LoadInsertDefault"));
        StartCoroutine(repeatLoad(AwardReady, LoadAward, "LoadAward"));
        StartCoroutine(repeatLoad(InsertReady, LoadInsert, "LoadInsert"));
    }
    
    IEnumerator repeatLoad(Func<bool> IsInit,Action init,string flag)
    {
        for (int i = 0; i < 3; i++)
        {
            if (IsInit() == false)
            {
                Debug.Log($"Repeat {flag} init countIndex is :" + i);
                init?.Invoke();
                yield return new WaitForSeconds(10f);
            }
            else
            {
                Debug.Log($"{flag} init success so yiled break");
                yield break;
            }
        }
    }
    
    public override void InitFail()
    {
        base.InitFail();
    }

    public override void InitSuccess()
    {
        base.InitSuccess();
    }

    public override void Log(string str)
    {
        Debug.Log(nameof(AdmobAD) + ":" + str);
    }

    public override void LoadBanner()
    {
        base.LoadBanner();
    }

    public override void LoadInsert()
    {
        base.LoadInsert();

        interstitial?.Destroy();
        interstitial = new InterstitialAd(InsertID);

        interstitial.OnAdClosed += OnAdClosed;

        var request = new AdRequest.Builder().Build();
        interstitial.LoadAd(request);
    }

    private void OnAdClosed(object sender, EventArgs e)
    {
        InsertClose();
    }

    public override void LoadAward()
    {
        base.LoadAward();
        rewardedAd?.Destroy();
        rewardedAd = new RewardedAd(AwardID);

        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        var request = new AdRequest.Builder().Build();
        rewardedAd.LoadAd(request);
    }

    private void HandleUserEarnedReward(object sender, Reward args)
    {
        Award();
    }

    private void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        AwardClose();
    }

    public override void LoadInsertDefault()
    {
        base.LoadInsertDefault();

        interstitialDefault?.Destroy();
        interstitialDefault = new InterstitialAd(InsertDefaultID);

        interstitialDefault.OnAdClosed += OnAdClosed2;

        var request = new AdRequest.Builder().Build();
        interstitialDefault.LoadAd(request);
    }

    private void OnAdClosed2(object sender, EventArgs e)
    {
        InsertDefaultClose();
    }

    public override bool BannerReady()
    {
        throw new Exception();
    }

    public override bool InsertReady()
    {
        var res = interstitial != null && interstitial.IsLoaded();
        Log(nameof(InsertReady) + res.ToString());
        return res;
    }

    public override bool AwardReady()
    {
        var res = rewardedAd != null && rewardedAd.IsLoaded();
        Log(nameof(AwardReady) + res.ToString());
        return res;
    }

    public override bool InsertDefaultReady()
    {
        var res = interstitialDefault != null && interstitialDefault.IsLoaded();
        Log(nameof(InsertDefaultReady) + res.ToString());
        return res;
    }

    public override void BannerPlay()
    {
    }

    public override void InsertPlay()
    {
        base.InsertPlay();
        interstitial.Show();
    }

    public override void AwardPlay()
    {
        base.AwardPlay();
        rewardedAd.Show();
    }

    public override void InsertDefaultPlay()
    {
        base.InsertDefaultPlay();
        interstitialDefault.Show();
    }

    public override void BannerClose()
    {
    }

    public override void InsertClose()
    {
        base.InsertClose();
    }

    public override void AwardClose()
    {
        base.AwardClose();
    }

    public override void InsertDefaultClose()
    {
        base.InsertDefaultClose();
        if (Game.I.playPanel.gameObject.activeInHierarchy == true)
        {
            if (Game.I.isClickBack == true)
            {
                this.Delay(0.1f,
                    () =>
                    {
                        Game.I._CurtainPanel.Change(() =>
                        {
                            MVC.SendEvent("GoHall");
                            Game.I.isClickBack = false;
                        });
                    });
            }
        }
    }

    public override void Award()
    {
        base.Award();

        if (Game.I.IsPureAwardAD)
        {
            Game.I.userModel.AddCoins(500000);
            Game.I._VedioPanel.Hide();
            Game.I.userModel.AddFreeAdCount(2);
        }
        else
        {
            Game.I._timeModel.OffLineAwardCallback(true);
        }

        this.Delay(Time.fixedDeltaTime * 4, () =>
        {
            Game.I.userModel.CoinFly();
            Game.I.saveSystem.Save();
        });


        if (Game.I._DoubleVideoPanel.gameObject.activeInHierarchy)
        {
            Game.I._DoubleVideoPanel.Hide();
        }
    }

    public override void InsertAward()
    {
        //throw new NotImplementedException();
        base.InsertAward();
    }

    public override bool InsertAwardReady()
    {
        return false;
        //throw new NotImplementedException();
    }
}*/