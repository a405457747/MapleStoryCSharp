/****************************************************************************
 * 2020.1 DESKTOP-F30Q2JP
 ****************************************************************************/

using QFramework;
using UnityEngine;

namespace CallPalCatGames.QFrameworkExtension
{
    public partial class Content : UIElement
    {
        public void Init(MainManager mainManager, SaveManager saveManager)
        {
            var loadHelper = gameObject.GetOrAddComponent<LoadHelper>();
            var imageItemPrefab = loadHelper.LoadThing<GameObject>("ItemImage");
            for (var i = 0; i < LevelManager.MaxGameLevel; i++)
            {
                var imageItemObj = Instantiate(imageItemPrefab);
                imageItemObj.Name($"ItemImage{i}").transform.SetParent(transform, false);
                var imageItem = imageItemObj.GetComponent<ImageItem>();
                var levelMessage = mainManager.GetLevelMessage(i);
                imageItem.Init(levelMessage.title, levelMessage.content,
                    saveManager.SaveMap.CurrentGameLevel.Value <= i, i); //i是nameIndex;
            }
        }
    }
}