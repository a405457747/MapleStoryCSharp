//using QAssetBundle;
using QFramework;
using UnityEngine;

namespace CallPalCatGames.QFrameworkExtension
{
    public class LogoScene : MonoBehaviour
    {
        private void Start()
        {
            UIMgr.OpenPanel(nameof(LogoPanel));
        }

        private void OnDestroy()
        {
            UIMgr.ClosePanel(nameof(LogoPanel));
        }
    }
}