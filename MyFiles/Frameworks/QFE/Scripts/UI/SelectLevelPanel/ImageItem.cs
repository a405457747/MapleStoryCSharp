/****************************************************************************
 * 2020.1 DESKTOP-F30Q2JP
 ****************************************************************************/

using QFramework;

namespace CallPalCatGames.QFrameworkExtension
{
    public partial class ImageItem : UIElement
    {
        private void Awake()
        {
        }

        public void Init(string title, string content, bool isShowLockImage, int nameIndex)
        {
            StartCommonButton.Init(() => { SendMsg(new LevelQMsg(nameIndex)); });
            Text_head.text = title;
            Text_detail.text = content;
            if (isShowLockImage)
                LockImage.Show();
            else
                LockImage.Hide();
        }

        protected override void OnBeforeDestroy()
        {
        }
    }
}