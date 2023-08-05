public class PurchaseSystem : GameSystem
{
    private IPurchase CurPurchase;

    public PurchaseSystem(Game game) : base(game)
    {
    }
    
    public override void Initialize()
    {
        base.Initialize();

        Init();
    }

    public void Init()
    {
        CurPurchase = gameObject.AddOrGetComponent<U3DIAP>();
        CurPurchase.Init();
        //print("PurchaseSystem init");
    }

    public void Restore()
    {
        CurPurchase.Restore();
    }

//auto
    private void Awake()
    {
    }

    public void Buy(int i)
    {
        if (CurPurchase!=null)
        {
               CurPurchase.Buy(i);
        }
        else
        {
            Log.LogParas("CurPurchase is null");
        }
     
    }
}