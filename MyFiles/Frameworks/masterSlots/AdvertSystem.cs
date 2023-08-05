using UnityEngine;
using UnityEngine.UI;


public class AdvertSystem : GameSystem
{
    private IAdvert CurAdvert;

    public AdvertSystem(Game game) : base(game)
    {
    }

    public override void Initialize()
    {
        base.Initialize();

        Init();
    }

    public void Init()
    {
        if (Differences.Android())
        {
            if (Game.CurrentAndroidChannel == AndroidChannel.Google)
            { //CurAdvert = gameObject.AddComponent<AdmobAD>();
            }
            else if (Game.CurrentAndroidChannel == AndroidChannel.Amazon)
            {
                //CurAdvert = gameObject.AddComponent<AdmobAD>();
            }

            //  Debug.Log("android detect!");
        }
        else if (Differences.IOS())
        {
          //  CurAdvert = gameObject.AddComponent<AdmobAD>();

            //  Debug.Log("ios detect!" + CurAdvert);
        }
        
        CurAdvert =gameObject.AddComponent<UnityMediation>();

        if (CurAdvert == null)
        {
//            print("CurAdver is NULL !!!!");
        }

        CurAdvert?.Init();

        //    print("AdvertSystem init");
    }


    //auto
    private void Awake()
    {
    }

    public bool InsertReadyPlay()
    {
        if (GData.MuteAD)
        {
            return false;
        }

        bool isReady = default;
        if (InsertReady())
        {
            isReady = true;
            InsertPlay();
        }

        return isReady;
    }

    public void InsertPlay()
    {
        CurAdvert.InsertPlay();
    }

    public bool InsertReady()
    {
        return CurAdvert.InsertReady();
    }

    public bool AwardReadyPlay()
    {
        bool isReady = default;

        if (AwardReady())
        {
            isReady = true;
            AwardPlay();
        }
        else
        {
              Game.I._tipPanel.TipMsg("Make sure your network unobstructed, and then try again");
        }

        return isReady;
    }

    public void AwardPlay()
    {
        CurAdvert.AwardPlay();
    }

    public void ChangeButtonState(Button btn)
    {
        btn.interactable = AwardReady();
    }

    public bool AwardReady()
    {
        return CurAdvert.AwardReady();
    }

    public bool InsertDefaultReady()
    {
        return CurAdvert.InsertDefaultReady();
    }

    public bool InsertDefaultReadyPlay()
    {
        if (game.SuPerSlotsAndCoreAndBrainsAndCoreGoodModel.IsAuto ||
            game.SuPerSlotsAndCoreAndBrainsAndCoreGoodModel.IsGameRuning)
        {
            Log.LogParas("返回了，自动时候不播放广告");
            return false;
        }
        
        bool isReady = false;

        if (GData.MuteAD)
        {
        }
        else
        {
            if (Game.I.saveMap.isRemoveAD)
            {
            }
            else
            {
                if (Game.I.levelModel.IsRookie())
                {
                }
                else
                {
                    bool isIosSave = Differences.IOS() && game._superTimeAndTimeModel.IsIOS72SaveTime();
                    if (isIosSave)
                    {
                        Log.LogParas("不是rookie，现在是ios 72 小时保护时间中");
                    }
                    else
                    {
                        Log.LogParas("非iOS 保护中了");
                        
                        var tempVal = Game.I.saveMap.FreeADCount - 1;
                        if (tempVal >= 0)
                        {
                            Game.I.saveMap.FreeADCount = tempVal;
                            Log.LogParas("cur freeAdCount is" + Game.I.saveMap.FreeADCount);
                            Game.I.saveSystem.Save();
                        }
                        else
                        {
                            if (InsertDefaultReady())
                            {
                                isReady = true;
                                InsertDefaultPlay();
                            }
                        }
                    }
                }
            }
        }

        return isReady;
    }

    public void InsertDefaultPlay()
    {
        CurAdvert.InsertDefaultPlay();
    }

    public override void Release()
    {
        base.Release();
    }
}