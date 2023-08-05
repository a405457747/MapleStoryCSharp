using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Balaso;

/*********** 添加新关卡策划和程序设置 ****************
-1.添加关卡的配置比如Level5.excel
0.设置基础的游戏的默认框
1.调制FreeOpen和Freeclose
4.MachineUI拖拽。
8.设置赔率 InitPayTableDic InitScatterDic
10. Resources/Sprite下面设置Level5 Paytable5 bj5 LvGo5
--------------------------
1.slotsModel类：
	initSML 5处
	initMachineMono
2.SaveMap类：
	sliderVal值
    PlaySliderVal设置相关呢a 特别是那个key别忘记了
3.slotsMachine类：
 	进入不同的小游戏 3处
4.playPanel
    slider类关卡的话添加关卡权限的设置在palyPanel
5.改线: Line40_4x5修改3处, InitMatrixLines  InitMatrix   不规则的形状还要做剔除呢(有两个一个数据外加一个动画线呢)
6. 如果图标多了一个，还有手动调整
 *************************************************/

/*************** 怕遗忘的功能 *******************
 * 转盘内购
 * 节假日彩蛋
 *  如果有比较做一个s,m,l校验
 *********************************************/

/** ios打包 
 * 设置VersionToolData
 * 编辑器去掉debug模式 和设置描述文件
 * VersitonTool运行一下
 *  编辑器运行一下，然后保存一下
  *  
 **/
/** 安卓打包
 *  设置VersionToolData
 *   去掉xx
 *    VersitionTool 运行一下啊
 *    编辑器运行一下，然后保存一下
 * 
 */


public class Game : MonoBehaviour
{
    public const string SystemSuffix = "System";
    public static Game I;

    //public MainPanel _mainPanel { get; set; }
    //public TipPanel tipPanel { get; set; }

    [FormerlySerializedAs("ctrlModel")] public CtrlModel ctollerModel;
    public LoadPanel loadPanel;
    public LoadModel loadModel;
    public HallPanel hallPanel;
    [FormerlySerializedAs("buyPanel")] public BuyPanel shopingPanel;
    [FormerlySerializedAs("_LevelGoodsPanel")] public LevelGoodsPanel _ShopingLevelGoodsPanel;
    [FormerlySerializedAs("slotsModel")] public SlotsModel SuPerSlotsAndCoreAndBrainsAndCoreGoodModel;
    public PlayPanel playPanel;
    [FormerlySerializedAs("userModel")] public UserModel myUserAndYouPlayerSettingsAndHappyModel;
    [FormerlySerializedAs("analysisModel")] public AnalysisModel fenxiModel;
    public LevelModel levelModel;
    private readonly Dictionary<string, Model> _models = new Dictionary<string, Model>();
    private readonly Dictionary<string, Panel> _panels = new Dictionary<string, Panel>();
    private readonly Dictionary<string, GameSystem> _systems = new Dictionary<string, GameSystem>();

    private readonly Dictionary<string, View> _views = new Dictionary<string, View>();

    private Game()
    {
    }

    public static Transform CanvasTrans => GameObject.Find("Game/Canvas").transform;

    public bool IsPlayInserSuccess { get; set; } = false;

    //public LuaSystem luaSystem { get; set; }
    public AdvertSystem advertSystem { get; set; }
    public SaveSystem saveSystem { get; set; }
    public AudioSystem audioSystem { get; set; }
    public PurchaseSystem purchaseSystem { get; set; }
    public HotUpdateSystem HotUpdateSystem { get; set; }

    public static VersionToolData VData => Factorys.GetAssetFactory().LoadScriptableObject<VersionToolData>();

    public static AndroidChannel CurrentAndroidChannel =>
        VData.androidChannel;

    public static bool IsOpenTestAD => VData.OpenTestAD;

    public static ProductConfig PConfig => Factorys.GetAssetFactory()
        .LoadScriptableObject<ProductConfigList>().list[0];

    public static List<purchasing> ShopMessageList =>
        Factorys.GetAssetFactory().LoadScriptableObject<purchasingList>().list;

    public static List<SDKNumber> SDKList => Factorys.GetAssetFactory().LoadScriptableObject<SDKNumberList>().list;
    public static SDKNumber SDKIOS => SDKList[0];
    public static SDKNumber SDKGoogle => SDKList[1];
    public static SDKNumber SDKAmazon => SDKList[2];

    public SaveMap saveMap => saveSystem.SaveMap;

    //是否是纯视频奖励的入口
    public bool IsPureAwardAD = true;

    public bool isClickBack = false;

    private AppTrackingTransparencyExample atte;

    public bool isAllowAtte = false;

    private void Awake()
    {
        atte = FindObjectOfType<AppTrackingTransparencyExample>();
        atte.game = this;

        AuthorRequestAwake();

        I = this;
        saveSystem = AddSystem<SaveSystem>();
        Log.LogParas("saveSystem map " + saveSystem.SaveMap);
        Log.LogParas("saveSystem str is " + saveSystem.GetSaveMapString());
    }

    private void AuthorRequestAwake()
    {
        atte.A();
    }

    private void AuthorRequestStart()
    {
        atte.S();
    }

    private void Reset()
    {
        Log.LogParas("reset and isPad" + Differences.Pad());
        SetMatchWidthOrHeight(PConfig.LandScape, Differences.Pad());

#if UNITY_EDITOR
        UnityEditor.AssetDatabase.SaveAssets();
        UnityEditor.AssetDatabase.Refresh();
#endif
    }

    private void Start()
    {
        AuthorRequestStart();

        var r = GameObject.Find("Reporter");
        if (VData.OpenLog)
        {
            r.GetComponent<Reporter>().enabled = true;
        }
        else
        {
            r.GetComponent<Reporter>().enabled = false;
        }



        Initinal();
    }

    public void Update()
    {
        InputProcess();
        foreach (var s in _systems.Values) s.EachFrame();
        foreach (var p in _panels.Values) p.EachFrame();

        foreach (var m in _models.Values)
            if (m.IsShow)
                m.EachFrame();

        foreach (var v in _views.Values)
            if (v.IsShow)
                v.EachFrame();
    }

    private void OnDestroy()
    {
        Release();
    }

    public void Initinal()
    {
        DontDestroyOnLoad(gameObject);

        advertSystem = AddSystem<AdvertSystem>();
        purchaseSystem = AddSystem<PurchaseSystem>();

        audioSystem = AddSystem<AudioSystem>();

        /*        HotUpdateSystem = AddSystem<HotUpdateSystem>();
                luaSystem = AddSystem<LuaSystem>();*/

        /*_mainPanel = AddPanel<MainPanel>();
        tipPanel = AddPanel<TipPanel>();*/

        StartCoroutine(consolePanelWait());
    }

    public HolidayDayPanel _HolidayDayPanel;

    [FormerlySerializedAs("_setPanel")] public SetPanel _settingsPanel;
    [FormerlySerializedAs("_evaluationPanel")] public EvaluationPanel _commentsPanel;
    public VipPanel _vipPanel;
    [FormerlySerializedAs("_VedioPanel")] public VedioPanel _watchvideosPanel;
    public LoginPanel _LoginPanel;
    public pig2Panel _Pig2Panel;
    public CenterWheelPanel _CenterWheelPanel;

    [FormerlySerializedAs("_MiniGameI1Panel")]
    public Mini1Panel mini1Panel;

    public Curtain3Panel _Curtain3Panel;
    public CurtainPanel _CurtainPanel;
    public BigLevelTipPanel _BigLevelTipPanel;

    [FormerlySerializedAs("_timeModel")] public TimeModel _superTimeAndTimeModel;
    public PigPanel _PigPanel;
    [FormerlySerializedAs("_VipModel")] public VipModel superValueVIPModel;
    [FormerlySerializedAs("_pigModel")] public PigModel _zhu_ba_jie_Model;
    public payTablePanel _PayTablePanel;
    [FormerlySerializedAs("_VipCheckPanel")] public vipCheckPanel _superValueVIPCheckPanel;
    public Guide1Panel _guide1Panel;
    public Guide2Panel _guide2Panel;
    public Guide3Panel _guide3Panel;
    public RemoveADPanel _RemoveADPanel;
    public TipPanel _tipPanel;
    public FirstBuyPanel _FirstBuyPanel;
    private ConsolePanel _consolePanel;
    public Curtain2Panel _Curtain2Panel;
    public DoubleVideoPanel _DoubleVideoPanel;

    public Mini8Panel mini8Panel;
    public Mini2Panel mini2Panel;
    public bool isCloseConsole { get; set; } = false;

    private IEnumerator consolePanelWait()
    {
#if UNITY_IOS
        AppTrackingTransparency.AuthorizationStatus currentStatus = AppTrackingTransparency.TrackingAuthorizationStatus;
        Debug.Log(string.Format("Current authorization status in consolePanelWait!: {0}", currentStatus.ToString()));
        if (currentStatus != AppTrackingTransparency.AuthorizationStatus.AUTHORIZED)
        {
            Log.LogParas("wait the is isAllowAtte is true");
            yield return new WaitUntil(() => isAllowAtte);
            Log.LogParas("isAllowAtte is true");
        }
#endif

        if (VData.OpenLog)
        {
            _consolePanel = MVC.RegisterView<ConsolePanel>(PanelTier.Curtain);
            _consolePanel.Show();
            yield return null;
            yield return new WaitUntil(() => isCloseConsole);
        }
        
        Log.LogParas("reset and isPad" + Differences.Pad());
        SetMatchWidthOrHeight(PConfig.LandScape, Differences.Pad());
        
        yield return StartCoroutine(NewGameStart());
    }

    public Mini6Panel _Mini6Panel;

    public Free12Panel free12Panel;
    public Free7Panel free7Panel;
    public Mini3Panel mini3Panel;
    public Mini10Panel _Mini10Panel;
    public Mini7Panel mini7Panel { get; set; }
    public Mini13Panel mini13Panel { get; set; }
    public Mini14Panel mini14Panel { get; set; }
    public Mini15Panel mini15Panel { get; set; }
    public Mini16Panel mini16Panel { get; set; }
    public Mini17Panel mini17Panel { get; set; }

    
    public requestPanel _requestPanel { get; set; }

    public requestModel _requestModel { get; set; }
    
    
    public StampsPanel _StampsPanel { get; set; }
    public StampsModel _StampsModel { get; set; }
    private IEnumerator NewGameStart()
    {
        yield return null;

        loadModel = MVC.RegisterModel<LoadModel>();
        SuPerSlotsAndCoreAndBrainsAndCoreGoodModel = MVC.RegisterModel<SlotsModel>();
        myUserAndYouPlayerSettingsAndHappyModel = MVC.RegisterModel<UserModel>();
        fenxiModel = MVC.RegisterModel<AnalysisModel>();
        levelModel = MVC.RegisterModel<LevelModel>();
        _superTimeAndTimeModel = MVC.RegisterModel<TimeModel>();
        superValueVIPModel = MVC.RegisterModel<VipModel>();
        _zhu_ba_jie_Model = MVC.RegisterModel<PigModel>();
        ctollerModel = MVC.RegisterModel<CtrlModel>();
        _requestModel = MVC.RegisterModel<requestModel>();
        _StampsModel = MVC.RegisterModel<StampsModel>();
        

        loadPanel = MVC.RegisterView<LoadPanel>();
        _settingsPanel = MVC.RegisterView<SetPanel>(PanelTier.PopUp);
        _commentsPanel = MVC.RegisterView<EvaluationPanel>(PanelTier.PopUp);
        _vipPanel = MVC.RegisterView<VipPanel>(PanelTier.PopUp);
        _watchvideosPanel = MVC.RegisterView<VedioPanel>(PanelTier.PopUp);
        _LoginPanel = MVC.RegisterView<LoginPanel>(PanelTier.Guide);
        hallPanel = MVC.RegisterView<HallPanel>();
        shopingPanel = MVC.RegisterView<BuyPanel>(PanelTier.AlwaysInFront);
        playPanel = MVC.RegisterView<PlayPanel>();
        _PigPanel = MVC.RegisterView<PigPanel>(PanelTier.PopUp);
        _Pig2Panel = MVC.RegisterView<pig2Panel>(PanelTier.PopUp);
        _CenterWheelPanel = MVC.RegisterView<CenterWheelPanel>(PanelTier.PopUp);
        _PayTablePanel = MVC.RegisterView<payTablePanel>(PanelTier.PopUp);
        _HolidayDayPanel = MVC.RegisterView<HolidayDayPanel>(PanelTier.PopUp);

        mini1Panel = MVC.RegisterView<Mini1Panel>(PanelTier.PopUp);
        mini4Panel = MVC.RegisterView<Mini4Panel>(PanelTier.PopUp);
        _Mini10Panel = MVC.RegisterView<Mini10Panel>(PanelTier.PopUp);
        mini5Panel = MVC.RegisterView<Mini5Panel>(PanelTier.PopUp);
        mini9Panel = MVC.RegisterView<Mini9Panel>(PanelTier.PopUp);
        mini3Panel = MVC.RegisterView<Mini3Panel>(PanelTier.PopUp);
        mini7Panel = MVC.RegisterView<Mini7Panel>(PanelTier.PopUp);
        mini8Panel = MVC.RegisterView<Mini8Panel>(PanelTier.PopUp);
        mini2Panel = MVC.RegisterView<Mini2Panel>(PanelTier.PopUp);
        _Mini6Panel = MVC.RegisterView<Mini6Panel>(PanelTier.PopUp);
        free7Panel = MVC.RegisterView<Free7Panel>(PanelTier.PopUp);
        free12Panel = MVC.RegisterView<Free12Panel>(PanelTier.PopUp);

        mini13Panel = MVC.RegisterView<Mini13Panel>(PanelTier.PopUp);
        mini14Panel = MVC.RegisterView<Mini14Panel>(PanelTier.PopUp);
        mini15Panel = MVC.RegisterView<Mini15Panel>(PanelTier.PopUp);
        mini16Panel = MVC.RegisterView<Mini16Panel>(PanelTier.PopUp);
        mini17Panel = MVC.RegisterView<Mini17Panel>(PanelTier.PopUp);

        _superValueVIPCheckPanel = MVC.RegisterView<vipCheckPanel>();
        _tipPanel = MVC.RegisterView<TipPanel>(PanelTier.AlwaysInFront);
        _guide1Panel = MVC.RegisterView<Guide1Panel>(PanelTier.Guide);
        _guide2Panel = MVC.RegisterView<Guide2Panel>(PanelTier.Guide);
        _guide3Panel = MVC.RegisterView<Guide3Panel>(PanelTier.Guide);
        _RemoveADPanel = MVC.RegisterView<RemoveADPanel>(PanelTier.PopUp);
        _FirstBuyPanel = MVC.RegisterView<FirstBuyPanel>(PanelTier.PopUp);
        _CurtainPanel = MVC.RegisterView<CurtainPanel>(PanelTier.Curtain);
        _ShopingLevelGoodsPanel = MVC.RegisterView<LevelGoodsPanel>(PanelTier.PopUp);
        _Curtain2Panel = MVC.RegisterView<Curtain2Panel>(PanelTier.Curtain);
        _BigLevelTipPanel = MVC.RegisterView<BigLevelTipPanel>(PanelTier.PopUp);
        _DoubleVideoPanel = MVC.RegisterView<DoubleVideoPanel>(PanelTier.PopUp);
        _Curtain3Panel = MVC.RegisterView<Curtain3Panel>(PanelTier.Curtain);

        _requestPanel = MVC.RegisterView<requestPanel>(PanelTier.PopUp);
        _StampsPanel = MVC.RegisterView<StampsPanel>(PanelTier.PopUp);
            
        MVC.RegisterController("GameStart");
        MVC.RegisterController("LoadOver");
        MVC.RegisterController("GoPlay");
        MVC.RegisterController("GoHall");
        MVC.RegisterController("SpinStart");
        MVC.RegisterController("GameOver");

        MVC.SendEvent("GameStart");
    }

    private void Release()
    {
        foreach (var s in _systems.Values) s.Release();
        foreach (var p in _panels.Values) p.Release();
    }


    private RandomHelper.Weight<string> testWeight =
        new RandomHelper.Weight<string>(RandomHelper.GetWeightItemByNormalRange(new List<int>() {1, 2, 3, 3}),
            new List<string>(){"a","b","c","d"});
    private int  testWIndex =1;
    private void InputProcess()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            // print( testWeight.GetItemByNumber(testWIndex));
            // testWIndex += 1;
            
            /*List<string> sss = new List<string>() {"a", "b", "c", "c"};
            int curIndex = 1;

            List<string> sp = new List<string>() {"dd", "dd"};
            sss.InsertRange(curIndex,sp);*/
            
            //Log.LogCollection(sss);

            /*
            string tmp = SlotsConfig.LL.ToString();
            print(tmp);*/
            
        print(int.MaxValue);
        }
        
        
        if (Input.GetKeyDown(KeyCode.H))
        {
            _HolidayDayPanel.Show();
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (GData.BakingMode)
            {
                StartCoroutine(hallPanel.AutoSpinButtonAction());
            }
            // luaSystem.LuaBuySuccess("7");
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
          //  mini2Panel.Hide();
            // mini8Panel.Hide();
            _HolidayDayPanel.Show();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            //_evaluationPanel.Show();
            //_LoginPanel.Show();
            //_CenterWheelPanel.Show();

            // _VipModel.AddVip(100);
            // _MiniGameI1Panel.Show();
            //_VipCheckPanel.Show();
            //userModel.CoinFly();
            //userModel.guideOpen(1);
            //_RemoveADPanel.Show();

            /*saveMap.gg += 11;
            print(saveMap.gg);
            saveSystem.Save();*/

            // audioSystem.PlaySound(AudioEnum.public_megawin);


            //mini4Panel.Show();
            //   _Mini10Panel.Show();
            // mini9Panel.Show();

            /*var list1 = new List<int>() {1, 2, 3, 4};
            var list2 = new List<int>(list1);
            list2.Remove(2);
            list2.Remove(3);
            list1.Remove(1);
            Log.LogParas("list2 count is" + list2.Count, "list1 count is " + list1.Count);

            mini3Panel.Show();*/

            //Debug.Log(_timeModel.IsIOS72SaveTime());
            /*_HolidayDayPanel.Show();

            int res =(int)( 135000000 * 1.8f);
            Log.LogParas(res);*/

            // mini8Panel.Show();
            //  mini8Panel.Show();
            // mini7Panel.Show();
            /*var list = Enumerable.Range(0, 3).ToList();
            Log.LogCollection(list);*/
            //  free7Panel.Show();

            //_Mini6Panel.Show();

          //  mini13Panel.Show();
            //mini14Panel.Show();
           // mini15Panel.Show();
           //_requestPanel.Show();
           //mini16Panel.Show();
           //mini16Panel.Show();
           mini17Panel.Show();
        }
    }

    private int testT = 0;

    [FormerlySerializedAs("_MiniGame2Panel")]
    public Mini4Panel mini4Panel;

    public Mini5Panel mini5Panel;
    public Mini9Panel mini9Panel;

    public T AddView<T>(PanelTier panelTier = PanelTier.Default) where T : View
    {
        var viewName = typeof(T).Name;
        if (_views.ContainsKey(viewName) == false)
        {
            Log.LogParas("viewName is" + viewName);
            var go = Instantiate(Factorys.GetAssetFactory().LoadPanel(viewName));
            go.Name(viewName);
            var t = go.AddOrGetComponent<T>();
            t.transform.SetParent(CanvasTrans.Find(panelTier.ToString()), false);
            t.Initialize(viewName);
            _views[viewName] = t;
        }

        return _views[viewName] as T;
    }

    public T AddModel<T>() where T : Model
    {
        var name = typeof(T).Name;
        var go = new GameObject(name);
        var t = go.AddOrGetComponent<T>();
        go.transform.SetParent(transform);
        go.transform.LocalReset();

        _models.Add(name, t);
        _models[name].Initialize(name);

        return _models[name] as T;
    }

    private T AddSystem<T>() where T : GameSystem
    {
        var systemName = typeof(T).Name;
        var t = GetComponentInChildren<T>();
        var go = t.gameObject;

        go.transform.LocalReset();

        _systems.Add(systemName, t);
        _systems[systemName].Initialize();

        return _systems[systemName] as T;
    }

    public T AddPanel<T>() where T : Panel
    {
        var panelName = typeof(T).Name;
        if (!_panels.ContainsKey(panelName))
        {
            var
                go = Resources
                    .Load<GameObject>(panelName); //  Factorys.GetAssetFactory().LoadPanel(panelName);
            var tempGo = Instantiate(go);
            tempGo.Name(panelName);

            _panels.Add(panelName, tempGo.GetComponent<Panel>());
            _panels[panelName].Initialize();
        }

        return _panels[panelName] as T;
    }

    public void BigSmooth(ref Sequence se, bool isNormal, Func<Sequence> func)
    {
        if (isNormal)
        {
            if (se != null)
            {
                se.onComplete();
                se.Kill();
                se = null;
            }
        }
        else
        {
            se = func();
        }
    }

    public void SetMatchWidthOrHeight(bool isLandscape, bool pad) //横1竖0
    {
        var longNumber = PConfig.ScreenLong;
        var shortNumber = PConfig.ScreenShort;

        var canvasScaler = CanvasTrans.GetComponent<CanvasScaler>();
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;

        if (isLandscape)
        {
            canvasScaler.referenceResolution = new Vector2(longNumber, shortNumber);
            if (pad)
                canvasScaler.matchWidthOrHeight = 0;
            else
                canvasScaler.matchWidthOrHeight = 1;
        }
        else
        {
            canvasScaler.referenceResolution = new Vector2(shortNumber, longNumber);

            if (pad)
                canvasScaler.matchWidthOrHeight = 1;
            else
                canvasScaler.matchWidthOrHeight = 0;
        }
    }

    public int GetMessionLockLv()
    {
        if (GData.QuickTestMission)
        {
            return 2;
        }
        
        return 5;
    }

    public int GetStampLockLv()
    {
        
        return 1;
    }

    public static int stampLImit = 500000;
}