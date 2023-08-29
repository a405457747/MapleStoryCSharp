using QFramework;
using UniRx;
using UnityEngine;

namespace CallPalCatGames.QFrameworkExtension
{
    public class MainScene : MonoBehaviour
    {
        private void Start()
        {
            var data = new MainPanelData
            {
                CanShowContinueBtn = new BoolReactiveProperty(SaveManager.Instance.SaveMap.CurrentGameLevel.Value != 1)
            };
            UIMgr.OpenPanel<MainPanel>(UILevel.Common, data, null, nameof(MainPanel));
        }

        private void OnDestroy()
        {
            UIMgr.ClosePanel(nameof(MainPanel));
        }
    }
}