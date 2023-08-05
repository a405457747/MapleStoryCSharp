using UnityEngine;

public enum ADType
{
    Null,
    Banner,
    Insert,
    Award,
    InsertAward
}

public abstract class IAdvert : UnityEngine.MonoBehaviour
{
    protected const string Andriod_Test_Insert_ID = "ca-app-pub-3940256099942544/1033173712";
    protected const string Andriod_Test_Award_ID = "ca-app-pub-3940256099942544/5224354917";
    protected const string IOS_Test_Insert_ID = "ca-app-pub-3940256099942544/4411468910";
    protected const string IOS_Test_Award_ID = "ca-app-pub-3940256099942544/1712485313";


    protected bool InsertPlaySuccess = false;

    public string AppID { get; set; }

    public string BannerID { get; set; }
    public string InsertID { get; set; }
    public string InsertDefaultID { get; set; }
    public string AwardID { get; set; }
    public string InsertAwardID { get; set; }

    public virtual void Init()
    {
        Log(nameof(Init));
    }

    public virtual void InitFail()
    {
        Log(nameof(InitFail));
    }

    public virtual void InitSuccess()
    {
        Log(nameof(InitSuccess));
    }

    public virtual void Log(string str)
    {
    }

    public virtual void LoadBanner()
    {
        Log(nameof(LoadBanner));
    }

    public virtual void LoadInsert()
    {
        Log(nameof(LoadInsert));
    }

    public virtual void LoadAward()
    {
        Log(nameof(LoadAward));
    }

    public virtual void LoadInsertDefault()
    {
        Log(nameof(LoadInsertDefault));
    }

    public virtual void LoadInsertAward()
    {
        Log(nameof(LoadInsertAward));
    }

    public abstract bool BannerReady();
    public abstract bool InsertReady();

    public abstract bool InsertDefaultReady();

    public abstract bool AwardReady();
    public abstract bool InsertAwardReady();

    public virtual void BannerPlay()
    {
        Log(nameof(BannerPlay));
    }

    public virtual void InsertPlay()
    {
        Log(nameof(InsertPlay));
        InsertPlaySuccess = true;
    }

    public virtual void AwardPlay()
    {
        Log(nameof(AwardPlay));
    }

    public virtual void InsertAwardPlay()
    {
        Log(nameof(InsertAwardPlay));
    }

    public virtual void InsertDefaultPlay()
    {
        Log(nameof(InsertDefaultPlay));
    }

    public virtual void BannerClose()
    {
        Log(nameof(BannerClose));
    }

    public virtual void InsertClose()
    {
        Log(nameof(InsertClose));
        
        
        Game.I._Curtain3Panel.Change(() =>
        {
            MVC.SendEvent("LoadOver");
        },1f);
        
        /*this.Delay(0.21f, () =>
        {
            //Game.I.IsPlayInserSuccess = true;
           
        });*/
        

    }

    public virtual void AwardClose()
    {
        Log(nameof(AwardClose));
        LoadAward();
    }

    public virtual void InsertAwardClose()
    {
        Log(nameof(InsertAwardClose));
        LoadInsertAward();
    }

    public virtual void InsertDefaultClose()
    {
        Log(nameof(InsertDefaultClose));
        LoadInsertDefault();
    }

    public virtual void Award()
    {
        Log(nameof(Award));
    }

    public virtual void InsertAward()
    {
        Log(nameof(InsertAward));
    }
}