using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public const string SystemSuffix = "System";
    public static Game I;
    private readonly Dictionary<string, Panel> _panels = new Dictionary<string, Panel>();
    private readonly Dictionary<string, GameSystem> _systems = new Dictionary<string, GameSystem>();
    public static Transform CanvasTrans => GameObject.Find("Game/Canvas").transform;

    private Game()
    {
    }

    private void Start()
    {
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

    public BreadMakerSystem _breadMakerSystem;
    public MainPanel _mainPanel;
    public ContentPanel contentPanel;
    public TipPanel tipPanel;

    public VersionToolData _versionToolData { get; set; }

    private void Reset()
    {
        var config = Factorys.GetAssetFactory()
            .LoadScriptableObject<ProductConfigList>().list[0];
        SetMatchWidthOrHeight(config);
    }

    public void Initinal()
    {
        DontDestroyOnLoad(gameObject);

        I = this;

        _breadMakerSystem = AddSystem<BreadMakerSystem>();

        _mainPanel = AddPanel<MainPanel>();
        contentPanel = AddPanel<ContentPanel>();
        tipPanel = AddPanel<TipPanel>();
        
    }

    private void Release()
    {
        foreach (var s in _systems.Values) s.Release();
        foreach (var p in _panels.Values) p.Release();
    }

    private void InputProcess()
    {
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
//            Log.LogPrint(panelName);
            GameObject
                go = Resources
                    .Load<GameObject>(panelName) as GameObject; //  Factorys.GetAssetFactory().LoadPanel(panelName);
            // Log.LogParas(go, "go");
            GameObject tempGo = Instantiate(go) as GameObject;
            // Log.LogParas(tempGo,"tempGo");
            tempGo.Name(panelName);

            _panels.Add(panelName, tempGo.GetComponent<Panel>());
            _panels[panelName].Initialize();
        }

        return _panels[panelName] as T;
    }

    private static void SetMatchWidthOrHeight(ProductConfig config) //横1竖0
    {
        float longNumber = config.ScreenLong;
        float shortNumber = config.ScreenShort;

        var canvasScaler = Game.CanvasTrans.GetComponent<CanvasScaler>();
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
    }
}