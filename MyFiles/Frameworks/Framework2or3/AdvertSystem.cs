public class AdvertSystem : GameSystem
{
    private readonly IAdvert CurAdvert = new GoogleAdvert();

    public AdvertSystem(Game game) : base(game)
    {
    }

//auto
    private void Awake()
    {
    }

    public void LoadBanner()
    {
        CurAdvert.LoadBanner();
    }

    public void LoadInsert()
    {
        CurAdvert.LoadInsert();
    }

    public void LoadAward()
    {
        CurAdvert.LoadAward();
    }

    public void LoadInsertAward()
    {
        CurAdvert.LoadInsertAward();
    }

    public bool BannerReady()
    {
        return CurAdvert.BannerReady();
    }

    public bool InsertReady()
    {
        return CurAdvert.InsertReady();
    }

    public bool AwardReady()
    {
        return CurAdvert.AwardReady();
    }

    public bool InsertAwardReady()
    {
        return CurAdvert.InsertAwardReady();
    }

    public void BannerPlay()
    {
        CurAdvert.BannerPlay();
    }

    public void InsertPlay()
    {
        CurAdvert.InsertPlay();
    }

    public void AwardPlay()
    {
        CurAdvert.AwardPlay();
    }

    public void InsertAwardPlay()
    {
        CurAdvert.InsertAwardPlay();
    }

    public override void Initialize()
    {
        base.Initialize();
    }

    public override void Release()
    {
        base.Release();
    }
}