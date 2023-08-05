using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//步骤安卓：设置CB，Tapjoy，设置IAP，基本设置ABTestMode打版本移动文件等等
//步骤IOS：
public class Game : MonoBehaviour
{
    public const string SystemSuffix = "System";
    public static Game I;
    private readonly Dictionary<string, Panel> _panels = new Dictionary<string, Panel>();
    private readonly Dictionary<string, GameSystem> _systems = new Dictionary<string, GameSystem>();

    private Game()
    {
    }

    public static Transform CanvasTrans => GameObject.Find("Game/Canvas").transform;


    public bool IsPlayInserSuccess { get; set; } = false;

    public LuaSystem luaSystem { get; set; }
    public AdvertSystem advertSystem { get; set; }
    public PurchaseSystem purchaseSystem { get; set; }
    public HotUpdateSystem HotUpdateSystem { get; set; }

    //public MainPanel _mainPanel { get; set; }
    //public TipPanel tipPanel { get; set; }

    public static VersionToolData VData => Factorys.GetAssetFactory().LoadScriptableObject<VersionToolData>();

    public static AndroidChannel CurrentAndroidChannel =>
        VData.androidChannel;

    public static bool IsOpenTestAD => VData.OpenTestAD;

    //public VersionToolData _versionToolData { get; set; }

    private void Reset()
    {
    }

    private void Start()
    {
        var config = Factorys.GetAssetFactory()
            .LoadScriptableObject<ProductConfigList>().list[0];

        var r = GameObject.Find("Reporter");
        if (VData.OpenLog)
        {
            r.SetActive(true);
        }
        else
        {
            r.SetActive(false);
        }

        var isPad = Differences.Pad();
        print("isPad:" + isPad.ToString());
        SetMatchWidthOrHeight(true, isPad);


        /*if (Game.VData.Debug == true)
        {
            GameObject.Find("Reporter").SetActive(true);
        }
        else
        {
            GameObject.Find("Reporter").SetActive(false);
        }*/

        Initinal();
    }

    public void Update()
    {
        InputProcess();
        foreach (var s in _systems.Values) s.EachFrame();
        foreach (var p in _panels.Values) p.EachFrame();
    }

    private void OnDestroy()
    {
        Release();
    }

    public void Initinal()
    {
        DontDestroyOnLoad(gameObject);

        I = this;

        advertSystem = AddSystem<AdvertSystem>();
        purchaseSystem = AddSystem<PurchaseSystem>();

        HotUpdateSystem = AddSystem<HotUpdateSystem>();
        luaSystem = AddSystem<LuaSystem>();


        /*_mainPanel = AddPanel<MainPanel>();
        tipPanel = AddPanel<TipPanel>();*/
    }

    private void Release()
    {
        foreach (var s in _systems.Values) s.Release();
        foreach (var p in _panels.Values) p.Release();
    }

    private void InputProcess()
    {
        if (Input.GetKeyDown(KeyCode.E)) luaSystem.LuaBuySuccess("7");
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

    public void SetMatchWidthOrHeight(bool isLandscape, bool pad) //横1竖0
    {
        const float longNumber = 2400;
        const float shortNumber = 1080;

        var canvasScaler = CanvasTrans.GetComponent<CanvasScaler>();
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;

        if (isLandscape)
        {
            canvasScaler.referenceResolution = new Vector2(longNumber, shortNumber);
            if (pad)
            {
                canvasScaler.matchWidthOrHeight = 0;
            }
            else
            {
                canvasScaler.matchWidthOrHeight = 1;
            }
        }
        else
        {
            canvasScaler.referenceResolution = new Vector2(shortNumber, longNumber);

            if (pad)
            {
                canvasScaler.matchWidthOrHeight = 1;
            }
            else
            {
                canvasScaler.matchWidthOrHeight = 0;
            }
        }

        Debug.Log("SetMatchWidthOrHeight Start Action");
    }

    /*private static void SetMatchWidthOrHeight(ProductConfig config) //横1竖0
    {
        float longNumber = config.ScreenLong;
        float shortNumber = config.ScreenShort;

        var canvasScaler = CanvasTrans.GetComponent<CanvasScaler>();
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;

        if (config.LandScape)
        {
            canvasScaler.referenceResolution = new Vector2(longNumber, shortNumber);
            canvasScaler.matchWidthOrHeight = 1;
        }
        else
        {
            canvasScaler.referenceResolution = new Vector2(shortNumber, longNumber);
            canvasScaler.matchWidthOrHeight = 0;
        }
    }*/
}