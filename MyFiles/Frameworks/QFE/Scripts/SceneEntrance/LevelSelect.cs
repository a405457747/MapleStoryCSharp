using QFramework;
using UnityEngine;

namespace CallPalCatGames.QFrameworkExtension
{
    public class LevelSelect : MonoBehaviour
    {
        private void Start()
        {
            UIMgr.OpenPanel<SelectLevelPanel>(UILevel.Common, null, null, nameof(SelectLevelPanel));
        }

        private void OnDestroy()
        {
            UIMgr.ClosePanel(nameof(SelectLevelPanel));
        }
    }
}