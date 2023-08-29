using QFramework;
using UnityEngine;

namespace CallPalCatGames.QFrameworkExtension
{
    public class EndScene : MonoBehaviour
    {
        private void Start()
        {
            UIMgr.OpenPanel<EndPanel>(UILevel.Common, null, null, nameof(EndPanel));
        }

        private void OnDestroy()
        {
            UIMgr.ClosePanel<EndPanel>();
        }
    }
}