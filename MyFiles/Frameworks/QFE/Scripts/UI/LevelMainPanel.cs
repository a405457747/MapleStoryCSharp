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
    using QFramework;
    using UniRx;

    public class LevelMainPanelData : QFramework.UIPanelData
    {
        public int LevelTailNum { get; set; }
        public string Title { get; set; }
    }

    public partial class LevelMainPanel : QFramework.UIPanel
    {
        protected override void ProcessMsg(int eventId, QFramework.QMsg msg)
        {
            throw new System.NotImplementedException();
        }

        protected override void OnInit(QFramework.IUIData uiData)
        {
            mData = uiData as LevelMainPanelData ?? new LevelMainPanelData();
            // please add init code here
            //LevelNumLCommonText.text = $"{SaveManager.Instance.ReturnLanguageMessage(28)}{mData.LevelTailNum}{mData.Title}";
            LevelNumLCommonText.text = $"{SaveManager.Instance.ReturnLanguageMessage(28)}{mData.LevelTailNum}";
            PauseCommonButton.Init(() =>
            {
                var pausePanelData = new PausePanelData() {EndCallback = () => { SendEvent(GameEvent.PauseGame);}};
                UIMgr.OpenPanel<PausePanel>(UILevel.Const, pausePanelData, null, nameof(PausePanel));
            });
            SendMsg(new SetPanelColorQMsg(this, PanelColorType.Transparency));
            SendMsg(new VoiceMsgQMsg("BG1", (int)VoiceEvent.PlayMusic));
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