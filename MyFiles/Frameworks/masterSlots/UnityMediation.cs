using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Mediation;
using Unity.Services.Core;


public class UnityMediation : IAdvert
{
    public override void Init()
    {
        base.Init();

        InitSomeID();
        
        
        LoadInsert();
        LoadInsertDefault();
        LoadAward();
    }

    private void InitSomeID()
    {
        
        if (Differences.Android())
        {
            if (Game.CurrentAndroidChannel == AndroidChannel.Google)
            {
                gameId = "4705729";

                adUnitId = "Interstitial_Android";
                adUnitId2 = "Default_int_Android";
                adUnitId3_vedio = "Rewarded_Android";
            }
            else if (Game.CurrentAndroidChannel == AndroidChannel.Amazon)
            {
                // 也许id要更换
                gameId = "4705729";

                adUnitId = "Interstitial_Android";
                adUnitId2 = "Default_int_Android";
                adUnitId3_vedio = "Rewarded_Android"; 
            }

            if (Differences.Ediotr() || Game.IsOpenTestAD)
            {
            }
        }
        else if (Differences.IOS())
        {
            gameId = "4705728";



            adUnitId = "Interstitial_iOS";
            adUnitId2 = "Default_int_iOS";
            adUnitId3_vedio = "Rewarded_iOS"; 
            
            
            if (Differences.Ediotr() || Game.IsOpenTestAD)
            {
            }
        }
        
    }

    IInterstitialAd ad;
    string adUnitId = "int_Android_start";

    public async void InitServices()
    {
        try
        {
            InitializationOptions initializationOptions = new InitializationOptions();
            initializationOptions.SetGameId(gameId);
            await UnityServices.InitializeAsync(initializationOptions);

            InitializationComplete();
        }
        catch (Exception e)
        {
            InitializationFailed(e);
        }
    }

    public void SetupAd()
    {
        //Create
        ad = MediationService.Instance.CreateInterstitialAd(adUnitId);

        //Subscribe to events
        ad.OnLoaded += AdLoaded;
        ad.OnFailedLoad += AdFailedLoad;

        ad.OnShowed += AdShown;
        ad.OnFailedShow += AdFailedShow;
        ad.OnClosed += AdClosed;
        ad.OnClicked += AdClicked;

        // Impression Event
        MediationService.Instance.ImpressionEventPublisher.OnImpression += ImpressionEvent;
    }

    public void ShowAd()
    {
        if (ad.AdState == AdState.Loaded)
        {
            ad.Show();
        }
    }

    void InitializationComplete()
    {
        SetupAd();
        ad.Load();
    }

    void InitializationFailed(Exception e)
    {
        Debug.Log("Initialization Failed: " + e.Message);
    }

    void AdLoaded(object sender, EventArgs args)
    {
        Debug.Log("Ad loaded");
    }

    void AdFailedLoad(object sender, LoadErrorEventArgs args)
    {
        Debug.Log("Failed to load ad");
        Debug.Log(args.Message);
    }

    void AdShown(object sender, EventArgs args)
    {
        Debug.Log("Ad shown!");
    }

    void AdClosed(object sender, EventArgs e)
    {
        // Pre-load the next ad
       // ad.Load();
        Debug.Log("Ad has closed");
        // Execute logic after an ad has been closed.

        InsertClose();
    }

    void AdClicked(object sender, EventArgs e)
    {
        Debug.Log("Ad has been clicked");
        // Execute logic after an ad has been clicked.
    }

    void AdFailedShow(object sender, ShowErrorEventArgs args)
    {
        Debug.Log(args.Message);
    }

    void ImpressionEvent(object sender, ImpressionEventArgs args)
    {
        var impressionData = args.ImpressionData != null ? JsonUtility.ToJson(args.ImpressionData, true) : "null";
        Debug.Log("Impression event from ad unit id " + args.AdUnitId + " " + impressionData);
    }

    //------------------------------------------------------------
    public override void LoadInsert()
    {
        base.LoadInsert();

        // 需要释放资源吗
        /*ad.Dispose();
        ad = null;*/
        
        InitServices();
    }
    public override void InsertPlay()
    {
        base.InsertPlay();
        ad.Show();
    }
    public override bool InsertReady()
    {
        return ad != null && (ad.AdState == AdState.Loaded);
    }

    public override void LoadInsertDefault()
    {
        base.LoadInsertDefault();
        InitServices2();
    }
    public override void InsertDefaultPlay()
    {
        base.InsertDefaultPlay();
            ad2_insert_default.Show();        
    }
    public override bool InsertDefaultReady()
    {
        return ad2_insert_default != null && (ad2_insert_default.AdState == AdState.Loaded);
    }


    public override void LoadAward()
    {
        base.LoadAward();
        InitServices3();
    }
    public override void AwardPlay()
    {
        base.AwardPlay();
        ad3_vedio.Show();
    }
    public override bool AwardReady()
    {
        return ad3_vedio != null &&( ad3_vedio.AdState == AdState.Loaded);
    }



    //----------------------------------------------------------------
    
    
      IInterstitialAd ad2_insert_default;
        string adUnitId2 = "Interstitial_Android";
        string gameId = "4684263";

        public async void InitServices2()
        {
            try
            {
                InitializationOptions initializationOptions = new InitializationOptions();
                initializationOptions.SetGameId(gameId);
                await UnityServices.InitializeAsync(initializationOptions);

                InitializationComplete2();
            }
            catch (Exception e)
            {
                InitializationFailed2(e);
            }
        }

        public void SetupAd2()
        {
            //Create
            ad2_insert_default = MediationService.Instance.CreateInterstitialAd(adUnitId2);

            //Subscribe to events
            ad2_insert_default.OnLoaded += AD2InsertDefaultLoaded2;
            ad2_insert_default.OnFailedLoad += AD2InsertDefaultFailedLoad2;

            ad2_insert_default.OnShowed += AD2InsertDefaultShown2;
            ad2_insert_default.OnFailedShow += AD2InsertDefaultFailedShow2;
            ad2_insert_default.OnClosed += AD2InsertDefaultClosed2;
            ad2_insert_default.OnClicked += AD2InsertDefaultClicked2;
            
            // Impression Event
            MediationService.Instance.ImpressionEventPublisher.OnImpression += ImpressionEvent2;
        }

        public void ShowAd2()
        {
            if (ad2_insert_default.AdState == AdState.Loaded)
            {
                ad2_insert_default.Show();
            }
        }

        void InitializationComplete2()
        {
            SetupAd2();
            ad2_insert_default.Load();
        }

        void InitializationFailed2(Exception e)
        {
            Debug.Log("Initialization 2 Failed: " + e.Message);
        }

        void AD2InsertDefaultLoaded2(object sender, EventArgs args)
        {
            Debug.Log("Ad  2 loaded");
        }

        void AD2InsertDefaultFailedLoad2(object sender, LoadErrorEventArgs args)
        {
            Debug.Log("Failed 2 to load ad");
            Debug.Log(args.Message);
        }

        void AD2InsertDefaultShown2(object sender, EventArgs args)
        {
            Debug.Log("Ad 2 shown!");
        }

        void AD2InsertDefaultClosed2(object sender, EventArgs e)
        {
            // Pre-load the next ad
           // ad2_insert_default.Load();
            Debug.Log("Ad 2 has closed");
            // Execute logic after an ad has been closed.

            InsertDefaultClose();
        }

        void AD2InsertDefaultClicked2(object sender, EventArgs e)
        {
            Debug.Log("Ad 2 has been clicked");
            // Execute logic after an ad has been clicked.
        }

        void AD2InsertDefaultFailedShow2(object sender, ShowErrorEventArgs args)
        {
            Debug.Log("faildShow 2"+args.Message);
        }

        void ImpressionEvent2(object sender, ImpressionEventArgs args)
        {
            var impressionData = args.ImpressionData != null ? JsonUtility.ToJson(args.ImpressionData, true) : "null";
            Debug.Log("Impression 2 event from ad unit id " + args.AdUnitId + " " + impressionData);
        }

        
        
        
        
        
        
        
        
        
          IRewardedAd ad3_vedio;
        string adUnitId3_vedio = "Rewarded_Android";

        public async void InitServices3()
        {
            try
            {
                InitializationOptions initializationOptions = new InitializationOptions();
                initializationOptions.SetGameId(gameId);
                await UnityServices.InitializeAsync(initializationOptions);

                InitializationComplete3();
            }
            catch (Exception e)
            {
                InitializationFailed3(e);
            }
        }

        public void SetupAd3()
        {
            //Create
            ad3_vedio = MediationService.Instance.CreateRewardedAd(adUnitId3_vedio);

            //Subscribe to events
            ad3_vedio.OnLoaded += AD3VedioLoaded3;
            ad3_vedio.OnFailedLoad += AD3VedioFailedLoad3;

            ad3_vedio.OnShowed += AD3VedioShown3;
            ad3_vedio.OnFailedShow += AD3VedioFailedShow3;
            ad3_vedio.OnClosed += AD3VedioClosed3;
            ad3_vedio.OnClicked += AD3VedioClicked3;
            ad3_vedio.OnUserRewarded += UserRewarded3;

            // Impression Event
            MediationService.Instance.ImpressionEventPublisher.OnImpression += ImpressionEvent3;
        }

        public void ShowAd3()
        {
            if (ad3_vedio.AdState == AdState.Loaded)
            {
                ad3_vedio.Show();
            }
        }

        void InitializationComplete3()
        {
            SetupAd3();
            ad3_vedio.Load();
        }

        void InitializationFailed3(Exception e)
        {
            Debug.Log("Initialization  3 Failed: " + e.Message);
        }

        void AD3VedioLoaded3(object sender, EventArgs args)
        {
            Debug.Log("Ad 3 loaded");
        }

        void AD3VedioFailedLoad3(object sender, LoadErrorEventArgs args)
        {
            Debug.Log("Failed 3 to load ad");
            Debug.Log(args.Message);
        }

        void AD3VedioShown3(object sender, EventArgs args)
        {
            Debug.Log("Ad 3 shown!");
        }

        void AD3VedioClosed3(object sender, EventArgs e)
        {
            // Pre-load the next ad
           // ad3_vedio.Load();
            Debug.Log("Ad 3 has closed");
            // Execute logic after an ad has been closed.

            AwardClose();
        }

        void AD3VedioClicked3(object sender, EventArgs e)
        {
            Debug.Log("Ad  3 has been clicked");
            // Execute logic after an ad has been clicked.
        }

        void AD3VedioFailedShow3(object sender, ShowErrorEventArgs args)
        {
            Debug.Log("failedShow 3 "+args.Message);
        }

        void ImpressionEvent3(object sender, ImpressionEventArgs args)
        {
            var impressionData = args.ImpressionData != null ? JsonUtility.ToJson(args.ImpressionData, true) : "null";
            Debug.Log("Impression 3 event from ad unit id " + args.AdUnitId + " " + impressionData);
        }
        
        void UserRewarded3(object sender, RewardEventArgs e)
        {
            Debug.Log($"Received 3 reward: type:{e.Type}; amount:{e.Amount}");

            Award();
        }
        
        
        
        
    
    //--------------不需要的

    public override bool InsertAwardReady()
    {
        return false;
    }
    
    public override bool BannerReady()
    {
        return false;
    }
    
    //--------------------
    public override void InsertClose()
    {
        base.InsertClose();
    }
    public override void AwardClose()
    {
        base.AwardClose();
    }
    
    public override void Award()
    {
        base.Award();

        if (Game.I.IsPureAwardAD)
        {
            Game.I.myUserAndYouPlayerSettingsAndHappyModel.AddCoins(500000);
            Game.I._watchvideosPanel.Hide();
            Game.I.myUserAndYouPlayerSettingsAndHappyModel.AddFreeAdCount(2);
        }
        else
        {
            Game.I._superTimeAndTimeModel.OffLineAwardCallback(true);
        }

        this.Delay(Time.fixedDeltaTime * 4, () =>
        {
            Game.I.myUserAndYouPlayerSettingsAndHappyModel.CoinFly();
            Game.I.saveSystem.Save();
        });


        if (Game.I._DoubleVideoPanel.gameObject.activeInHierarchy)
        {
            Game.I._DoubleVideoPanel.Hide();
        }
    }
    
    public override void InsertDefaultClose()
    {
        base.InsertDefaultClose();
        
        Game.I.shopingPanel.ForceHide();
        if (Game.I.playPanel.gameObject.activeInHierarchy == true)
        {
            if (Game.I.isClickBack == true)
            {
                
                Game.I._Curtain3Panel.Change(() =>
                {
                    MVC.SendEvent("GoHall");
                    Game.I.isClickBack = false;
                },1f);
                
                /*this.Delay(0.21f,
                    () =>
                    {
                        Game.I._CurtainPanel.Change(() =>
                        {
                            MVC.SendEvent("GoHall");
                            Game.I.isClickBack = false;
                        });
                    });*/
            }
            else
            {
                Game.I._Curtain3Panel.Change(() =>
                {
                },1.5f);
            }
        }
        else
        {
            Game.I._Curtain3Panel.Change(() =>
            {
            },1.5f);
        }
    }

}