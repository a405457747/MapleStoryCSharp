//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CallPalCatGames.QFrameworkExtension
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.UI;
    using UniRx;
    using QFramework;
    using DG.Tweening;

    public class MainPanelData : QFramework.UIPanelData
    {
        public BoolReactiveProperty CanShowContinueBtn { get; set; }
    }

    public partial class MainPanel : QFramework.UIPanel
    {
        protected override void ProcessMsg(int eventId, QFramework.QMsg msg)
        {
            throw new System.NotImplementedException();
        }

        protected override void OnInit(QFramework.IUIData uiData)
        {
            mData = uiData as MainPanelData ?? new MainPanelData();
            // please add init code here
            VerMCommonText.text = $"{Application.version} {SaveManager.Instance.ReturnLanguageMessage(4)}";
            mData.CanShowContinueBtn.Subscribe(Can =>
                {
                    if (Can)
                    {
                        ContinueCommonButton.Show();
                        StartCommonButton.Init (() =>
                        {
                            UIMgr.OpenPanel<AffirmPanel>(UILevel.PopUI, new AffirmPanelData()
                            {
                                AffirmContent =SaveManager.Instance.ReturnLanguageMessage(16),
                                AffirmCallBack = () =>
                                {
                                    SendEvent(SaveEvent.CreateNewSaveMap);
                                    SendEvent(LevelEvent.LoadLevelSelectScene);
                                }
                            }, null, nameof(AffirmPanel));
                        });
                    }
                    else
                    {
                        ContinueCommonButton.Hide();
                        StartCommonButton.Init (() => { SendEvent(LevelEvent.LoadLevelSelectScene);
                        });
                    }
                })
                .AddTo(this);
            QuitCommonButton.Init(() => { Application.Quit(); });
            ContinueCommonButton.Init( () => { SendEvent(LevelEvent.LoadLevelSelectScene); });
            SetCommonButton.Init ( () =>
            {
                SaveMap saveMap = SaveManager.Instance.SaveMap;
                UIMgr.OpenPanel<SetPanel>(UILevel.PopUI, new SetPanelData() { IsShake = saveMap.IsShake, MusicOn = saveMap.MusicOn, SoundOn = saveMap.SoundOn }, null, nameof(SetPanel));
            });
            SendMsg(new VoiceMsgQMsg("BG2", (int)VoiceEvent.PlayMusic));
            SendMsg(new SetPanelColorQMsg(this, PanelColorType.Normal));
        }

        protected override void OnOpen(QFramework.IUIData uiData)
        {
        }

        protected override void OnShow()
        {
        }

        protected override void OnHide()
        {
        }

        protected override void OnClose()
        {
        }
    }
}