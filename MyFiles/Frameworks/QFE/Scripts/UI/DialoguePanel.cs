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
    using DG.Tweening;
    using QFramework;
    
    public class DialoguePanelData : QFramework.UIPanelData
    {
    }
    
    public partial class DialoguePanel : QFramework.UIPanel
    {

        private List<DialogBox> _dialogBoxes;
        private int _index;
        private LoadHelper _loadHelper;

        protected override void ProcessMsg(int eventId, QFramework.QMsg msg)
        {
            throw new System.NotImplementedException ();
        }
        
        protected override void OnInit(QFramework.IUIData uiData)
        {
            mData = uiData as DialoguePanelData ?? new DialoguePanelData();
            // please add init code here
            GetComponent<RectTransform>().offsetMax = new Vector2(0, -1600);
            _loadHelper = gameObject.GetOrAddComponent<LoadHelper>();
            GoOnButton.Init ( () => { UpdateDialoguePanel();});
            SendMsg(new SetPanelColorQMsg(this, PanelColorType.PureBlack));
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

        public void StartDialogue(int plotIndex)
        {
            _index = -1;
            this._dialogBoxes = DialogueManager.Instance.DialogueDic[plotIndex];
            UpdateDialoguePanel();
        }

        private void UpdateDialoguePanel()
        {
            _index++;
            if (_index < _dialogBoxes.Count)
            {
                ContentText.text = "";
                HeadImage.sprite= _loadHelper.LoadThing<Sprite>(_dialogBoxes[_index].Image);
                NameText.text = _dialogBoxes[_index].Name;
                ContentText.DOText(_dialogBoxes[_index].SaidContent, 0.5f);
            }
            else
            {
                CloseSelf();
            }
        }
    }
}