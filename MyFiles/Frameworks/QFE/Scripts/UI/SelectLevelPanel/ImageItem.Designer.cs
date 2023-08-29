/****************************************************************************
 * 2020.1 DESKTOP-F30Q2JP
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace CallPalCatGames.QFrameworkExtension
{
    public partial class ImageItem
    {
        [SerializeField] public UnityEngine.UI.Text Text_head;
        [SerializeField] public UnityEngine.UI.Text Text_detail;
        [SerializeField] public  CommonButton StartCommonButton;
        [SerializeField] public UnityEngine.UI.Image LockImage;

        public void Clear()
        {
            Text_head = null;
            Text_detail = null;
            StartCommonButton = null;
            LockImage = null;
        }

        public override string ComponentName
        {
            get { return "ImageItem"; }
        }
    }
}
