using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoSingleton<PanelManager>
{
    public Transform CanvasTrans
    {
        get { return GameObject.Find("Root/Canvas").transform; }
    }

    private Dictionary<string, Panel> _panels;

    //横1竖0
    private void SetMatchWidthOrHeight(bool isLandscape)
    {
        const float longNumber = 1280;
        const float shortNumber = 720;

        CanvasScaler canvasScaler = CanvasTrans.GetComponent<CanvasScaler>();
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;

        if (isLandscape)
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
    private void Reset()
    {
        SetMatchWidthOrHeight(true);
    }

    protected override void Awake()
    {
        base.Awake();

        _panels = new Dictionary<string, Panel>();
    }

    public T GetPanel<T>() where T : Panel
    {
        string panelName = typeof(T).Name;

        if (_panels.ContainsKey(panelName))
        {
            return _panels[panelName] as T;
        }
        else
        {
            Log.LogException(new KeyNotFoundException());
            return default;
        }
    }

    public void OpenPanel<T>(PanelArgs args = null, PanelTier tier = PanelTier.Default) where T : Panel
    {
        var panelName = typeof(T).Name;

        if (!_panels.ContainsKey(panelName))
        {
            var tempGo = Instantiate(ResourcesManager.Instance.GetPanel(panelName), CanvasTrans.Find(tier.ToString()), false);
            tempGo.Name(panelName);

            _panels.Add(panelName, tempGo.GetComponent<Panel>());

            _panels[panelName].OnInit(args);
        }

        _panels[panelName].gameObject.Show();
        _panels[panelName].OnOpen(args);
    }

    public void ClosePanel<T>() where T : Panel
    {
        var panelName = typeof(T).Name;

        ClosePanel(panelName);
    }

    private void ClosePanel(string panelName)
    {
        if (_panels.ContainsKey(panelName))
        {
            _panels[panelName].OnClose();
            _panels[panelName].gameObject.Hide();
        }
    }

    private void RemoveAllPanel()
    {
        var names = _panels.Keys.ToList();
        foreach (var panelName in names)
        {
            RemovePanel(panelName);
        }
    }

    private void RemovePanel(string panelName)
    {
        if (_panels.ContainsKey(panelName))
        {
            ClosePanel(panelName);

            _panels[panelName].OnRemove();
            Destroy(_panels[panelName].gameObject);
            _panels.Remove(panelName);
        }
    }

    public void RemovePanel<T>() where T : Panel
    {
        string panelName = typeof(T).Name;
        RemovePanel(panelName);
    }

    public void OpenPanelCurtain(string sceneName)
    {
        RemoveAllPanel();

        OpenPanel<PanelCurtain>(new PanelCurtainArgs() { SceneName = sceneName }, tier: PanelTier.Curtain);
    }

}

public enum PanelTier
{
    Default,
    PopUp,
    AlwaysInFront,
    Guide,
    Effect,
    Curtain
};