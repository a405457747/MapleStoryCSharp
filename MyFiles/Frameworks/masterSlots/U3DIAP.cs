using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Purchasing;

public class U3DIAP : IPurchase, IStoreListener
{  IAppleExtensions m_AppleExtensions;
    private IStoreController m_StoreController; // The Unity Purchasing system.
    private IExtensionProvider m_StoreExtensionProvider;
    private int ConsumableMaxCount;
    private Dictionary<int, string> SkUDic;

    public override void Init()
    {
        base.Init();

        SkUDic = new Dictionary<int, string>();
        foreach (var item in Game.ShopMessageList)
        {
            if (item.SKU == "")
            {
                throw new Exception("The sku is empty");
            }

            string RealSKU = "";
            if (Differences.Android())
            {
                if (Game.VData.androidChannel==AndroidChannel.Google)
                {
                    RealSKU = item.SKUgoogleplay;
                }else if (Game.VData.androidChannel == AndroidChannel.Amazon)
                {
                    throw new Exception();
                }
                
            }else if (Differences.IOS())
            {
                RealSKU = item.SKU;
            }

            SkUDic.Add(item.ID, RealSKU);
        }
        
         Debug.Log("--- id 7 is "+SkUDic[7]);


        Prefix = "fortune_farm_slots_chip_";

        if (Differences.Android())
        {
            if (Game.CurrentAndroidChannel == AndroidChannel.Google)
            {
                //Prefix = "fortune_farm_slots_chip_";
            }
            else if (Game.CurrentAndroidChannel == AndroidChannel.Amazon)
            {
                //  Prefix = "fortune_farm_slots_chip_";
            }
        }
        else if (Differences.IOS())
        {
            // Prefix = "fortune_farm_slots_chip_";
        }

        StartCoroutine(repeatLoad());
    }

    IEnumerator repeatLoad()
    {
        for (int i = 0; i < 3; i++)
        {
            if (IsInit() == false)
            {
                  Debug.Log("repeat iap init count is :" + i);
                InitializePurchasing();
                yield return new WaitForSeconds(2f);
            }
            else
            {
                                Debug.Log("iap init success yiled break");
                yield break;
            }
        }
    }

    private void InitializePurchasing()
    {
        // Debug.Log("InitializePurchasing ......");
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        foreach (var item in SkUDic)
        {
            /*if (item.Key!=7)
            {
                builder.AddProduct(item.Value, ProductType.Consumable);
            }
            else
            {
                builder.AddProduct(item.Value, ProductType.NonConsumable);
            }*/
            
            builder.AddProduct(item.Value, ProductType.Consumable);
        }
        UnityPurchasing.Initialize(this, builder);
    }


    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        BuyFail(failureReason.ToString());
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;
        
        m_AppleExtensions = extensions.GetExtension<IAppleExtensions>();
        Log(nameof(OnInitialized));
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Log(nameof(OnInitializeFailed) + ":" + error);
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        var product = args.purchasedProduct;

        BuySuccess(product.definition.id);

        return PurchaseProcessingResult.Complete;
    }

    public override bool IsInit()
    {
        var res = m_StoreController != null && m_StoreExtensionProvider != null;
        //Log(nameof(IsInit) + ":" + res.ToString());
        return res;
    }

    public override void Buy(int i)
    {
        base.Buy(i);

        if (IsInit())
        {
            var buyID = SkUDic[i];
            Debug.Log("start buy id is :" + buyID);
            var product = m_StoreController.products.WithID(buyID);
            if (product != null && product.availableToPurchase)
            {
                m_StoreController.InitiatePurchase(product);
            }
        }
        else
        {
            Game.I._tipPanel.TipMsg("Make sure your network unobstructed, and then try again");
        }
    }

    public override void Log(string str)
    {
        string res = nameof(U3DIAP) + ":" + str;
        Debug.Log(res);
    }

    public override void BuyFail(string msg)
    {
        base.BuyFail(msg);
    }

    public string FindMsgGoodsId(string sku)
    {
        var list = ExcelData._purchasingList;
        
        foreach (var p in list)
        {
            // 如果有亚马逊这里需要改一下
            if (p.SKU==sku ||p.SKUgoogleplay==sku)
            {
                return p.ID.ToString();
            }
        }
        throw new Exception();
    }

    public override void BuySuccess(string msg)
    {
        base.BuySuccess(msg);
        var t = FindMsgGoodsId(msg);//  msg.Split('_')[1];
        int id = int.Parse(t);
        print("buysuccess id is " + id);

        var g = Game.I;
        g._tipPanel.TipMsg("Buy success!");
        g.myUserAndYouPlayerSettingsAndHappyModel.CoinFly();

        int coinNum = g._zhu_ba_jie_Model.GetCoinsByID(id);
        Debug.Log("Buy success coinNum is " + coinNum);
        int coinNumLastChar = Tool.ChartToInt(coinNum.ToString().Last());
        coinNum -= coinNumLastChar;

        g.myUserAndYouPlayerSettingsAndHappyModel.AddCoins(coinNum);

        int vipNum = g._zhu_ba_jie_Model.GetVipPointByID(id);
        g.superValueVIPModel.AddVip(vipNum);


        int freeAdCount = g._zhu_ba_jie_Model.GetFreeADCountById(id);
        g.myUserAndYouPlayerSettingsAndHappyModel.AddFreeAdCount(freeAdCount);

        Game.I.shopingPanel.BuySuccess();

        Game.I.saveMap.PurchasingCount += 1;
        Game.I.saveMap.PurchasingExcelIndex = 0;
        g.saveSystem.Save();
    }
    
    public override void Restore()
    {
        Debug.Log("Restore clicked");
        if (Differences.IOS())
        {
            if (HaveReceipt())
            {
                Debug.Log("have receipt");
                m_AppleExtensions.RestoreTransactions(OnRestore);
            }
            else
            {
             Game.I._tipPanel.TipMsg("You have not purchased this item");
            }
        }
    }

    bool HaveReceipt()
    {
        return HasNoAds();
    }
    
    bool HasNoAds()
    {
        var noAdsProduct = m_StoreController.products.WithID(SkUDic[7]);
        return noAdsProduct != null && noAdsProduct.hasReceipt;
    }
    
   // public string noAdsProductId = "com.mycompany.mygame.no_ads";
    void OnRestore(bool success)
    {
        var restoreMessage = "";
        if (success)
        {
            // This does not mean anything was restored,
            // merely that the restoration process succeeded.
            
            
            
            restoreMessage = "Restore Successful";

            //这里是复制粘贴的不是很优雅
            int id = 7;
            var g = Game.I;
            int coinNum = g._zhu_ba_jie_Model.GetCoinsByID(id);
            Debug.Log("restore success coinNum is " + coinNum);
            int coinNumLastChar = Tool.ChartToInt(coinNum.ToString().Last());
            coinNum -= coinNumLastChar;

            g.myUserAndYouPlayerSettingsAndHappyModel.AddCoins(coinNum);

            int vipNum = g._zhu_ba_jie_Model.GetVipPointByID(id);
            g.superValueVIPModel.AddVip(vipNum);


            int freeAdCount = g._zhu_ba_jie_Model.GetFreeADCountById(id);
            g.myUserAndYouPlayerSettingsAndHappyModel.AddFreeAdCount(freeAdCount);

            Game.I.shopingPanel.BuySuccess();
            g.saveSystem.Save();
            
        }
        else
        {
            // Restoration failed.
            restoreMessage = "Restore Failed";
        }

        Debug.Log(restoreMessage);
        
        Game.I._tipPanel.TipMsg(restoreMessage);
    }
}