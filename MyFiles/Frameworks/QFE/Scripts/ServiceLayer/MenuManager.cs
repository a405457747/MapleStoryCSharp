using System;
using DG.Tweening;
using QFramework;
using UnityEngine;
using UnityEngine.UI;

namespace CallPalCatGames.QFrameworkExtension
{
    public enum PanelColorType
    {
        PureWhite,
        TranslucentBlack,
        TranslucentWhite,
        PureBlack,
        Normal,
        Transparency,
        Translucent
    }

    public enum MenuEvent
    {
        Began = QMgrID.Menu,
        SetPanelColor,
        AddCanvasGroupFade
    }

    public class AddCanvasGroupFadeQMsg : QMsg
    {
        public AddCanvasGroupFadeQMsg(Transform trans, bool isFadeIn, Action endCallback = null) : base(
            (int)MenuEvent.AddCanvasGroupFade)
        {
            Trans = trans;
            EndCallback = endCallback;
            IsFadeIn = isFadeIn;
        }

        public Transform Trans { get; set; }
        public Action EndCallback { get; set; }
        public bool IsFadeIn { get; set; }
    }

    public class SetPanelColorQMsg : QMsg
    {
        public SetPanelColorQMsg(UIPanel uIPanel, PanelColorType panelColorType) : base((int)MenuEvent.SetPanelColor)
        {
            UiPanel = uIPanel;
            PanelColorType = panelColorType;
        }

        public UIPanel UiPanel { get; set; }
        public PanelColorType PanelColorType { get; set; }
    }

    public class MenuManager : QMgrBehaviour, ISingleton
    {
        private LoadHelper _loadHelper;
        public static MenuManager Instance => MonoSingletonProperty<MenuManager>.Instance;
        public override int ManagerId => QMgrID.Menu;

        public void OnSingletonInit()
        {
            _loadHelper = gameObject.GetOrAddComponent<LoadHelper>();
            RegisterEvent(MenuEvent.SetPanelColor);
            RegisterEvent(MenuEvent.AddCanvasGroupFade);
        }

        public void LogoAnimation(Transform trans, Action endCallback = null, float totalTime = 2f,
            float scaleRatio = 1.1f)
        {
            var fadeTime = totalTime * 0.25f;
            var saveTime = totalTime * 0.5f;
            this.Sequence()
                .Event(() =>
                {
                    trans.DOScale(scaleRatio, totalTime);
                })
                .OnlyBegin(action => { AddCanvasGroupFadeIn(trans, () => { action.Finish(); }, fadeTime); })
                .Delay(saveTime)
                .OnlyBegin(action =>
                {
                    AddCanvasGroupFadeOut(trans, () =>
                    {
                        action.Finish();
                        endCallback?.Invoke();
                    }, fadeTime);
                })
                .Begin();
        }

        protected override void ProcessMsg(int key, QMsg msg)
        {
            switch (msg.EventID)
            {
                case (int)MenuEvent.SetPanelColor:
                    var panelColorMsg = msg as SetPanelColorQMsg;
                    SetPanelColor(panelColorMsg.UiPanel, panelColorMsg.PanelColorType);
                    break;
                case (int)MenuEvent.AddCanvasGroupFade:
                    var addCanvasGroupFadeMsg = msg as AddCanvasGroupFadeQMsg;
                    if (addCanvasGroupFadeMsg.IsFadeIn)
                        AddCanvasGroupFadeIn(addCanvasGroupFadeMsg.Trans, addCanvasGroupFadeMsg.EndCallback);
                    else
                        AddCanvasGroupFadeOut(addCanvasGroupFadeMsg.Trans, addCanvasGroupFadeMsg.EndCallback);
                    break;
            }
        }

        private void SetPanelColor(UIPanel uIPanel, PanelColorType panelColorType = PanelColorType.Normal)
        {
            var image = uIPanel.GetComponent<Image>();
            switch (panelColorType)
            {
                case PanelColorType.PureWhite:
                    image.sprite = _loadHelper.LoadThing<Sprite>("White");
                    image.ColorAlpha(1f);
                    break;
                case PanelColorType.TranslucentBlack:
                    image.sprite = _loadHelper.LoadThing<Sprite>("Black");
                    image.ColorAlpha(0.5f);
                    break;
                case PanelColorType.TranslucentWhite:
                    image.sprite = _loadHelper.LoadThing<Sprite>("White");
                    image.ColorAlpha(0.5f);
                    break;
                case PanelColorType.PureBlack:
                    image.sprite = _loadHelper.LoadThing<Sprite>("Black");
                    image.ColorAlpha(1f);
                    break;
                case PanelColorType.Normal:
                    image.ColorAlpha(1f);
                    image.color = Color.white;
                    break;
                case PanelColorType.Transparency:
                    image.ColorAlpha(0f);
                    break;
                case PanelColorType.Translucent:
                    image.ColorAlpha(0.5f);
                    break;
            }
        }

        private void Springing(Transform trans)
        {
            trans.DOPunchScale(new Vector3(-0.2f, 0, 0), 0.4f, 12, 0.5f)
                .OnComplete(() => { trans.LocalScale(Vector3.one); });
        }

        private void HoodleDrop(Transform rectTransform, float endvalue, float duration) //类似弹珠落地
        {
            rectTransform.DOLocalMoveY(endvalue, duration).SetEase(Ease.OutBounce);
        }

        private void AddCanvasGroupFadeIn(Transform trans, Action endCallback = null, float time = 0.45f)
        {
            var canvasGroup = trans.gameObject.GetOrAddComponent<CanvasGroup>();
            canvasGroup.alpha = 0f;
            canvasGroup.DOFade(1f, time).OnComplete(() => { endCallback?.Invoke(); });
        }

        public void ChangeBigToNormal(Transform transform, float InitRatio, float TotalTimeCost = 0.2f, float ShrinkRatio = 1.32f)
        {
            transform.DOScale(ShrinkRatio*InitRatio, TotalTimeCost / 2f).OnComplete(() => { transform.DOScale(InitRatio, TotalTimeCost / 2f); });
        }

        private void AddCanvasGroupFadeOut(Transform trans, Action endCallback = null, float time = 0.45f)
        {
            var canvasGroup = trans.gameObject.GetOrAddComponent<CanvasGroup>();
            canvasGroup.alpha = 1f;
            canvasGroup.DOFade(0f, time).OnComplete(() => { endCallback?.Invoke(); });
        }

        private void Seesaw(RectTransform rectTransform, int bigDeg = 20, int smallDeg = 10,
            float cdTime = 0.3f)
        {
            this.Sequence()
                .OnlyBegin(action =>
                {
                    rectTransform.DORotate(new Vector3(0, 0, bigDeg), cdTime)
                        .OnComplete(() => { action.Finish(); });
                })
                .OnlyBegin(action =>
                {
                    rectTransform.DORotate(new Vector3(0, 0, -bigDeg), cdTime)
                        .OnComplete(() => { action.Finish(); });
                })
                .OnlyBegin(action =>
                {
                    rectTransform.DORotate(new Vector3(0, 0, smallDeg), cdTime)
                        .OnComplete(() => { action.Finish(); });
                })
                .OnlyBegin(action =>
                {
                    rectTransform.DORotate(new Vector3(0, 0, -smallDeg), cdTime)
                        .OnComplete(() => { action.Finish(); });
                })
                .OnlyBegin(action =>
                {
                    rectTransform.DORotate(new Vector3(0, 0, 0), cdTime).OnComplete(() => { action.Finish(); });
                })
                .Begin();
        }
    }
}