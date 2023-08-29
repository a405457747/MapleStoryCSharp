/****************************************************************************
 * 2020.1 DESKTOP-F30Q2JP
 ****************************************************************************/

using System;
using QFramework;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace CallPalCatGames.QFrameworkExtension
{
    public partial class CommonButton : UIComponent
    {
        [FormerlySerializedAs("Btn")] [HideInInspector] public Button btn;

        private void Awake()
        {
        }

        public void Init(Action clickCallback)
        {
            btn = GetComponent<Button>();
            btn.onClick.AddListener(() =>
            {
                clickCallback?.Invoke();
                SendMsg(new VoiceMsgQMsg("BtnClick", (int) VoiceEvent.PlaySound));
            });
        }

        protected override void OnBeforeDestroy()
        {
        }
    }
}