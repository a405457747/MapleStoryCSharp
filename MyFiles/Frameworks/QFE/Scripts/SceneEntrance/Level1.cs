using QFramework;
using System;
using UnityEngine;

namespace CallPalCatGames.QFrameworkExtension
{
    /*
    public enum GamePlayEvent
    {
        Began = GameEvent.End,
        End,
    }
    */

    public class Level1 : MonoBehaviour
    {
        private void Awake()
        {
            GameManager.Instance.StartGame();//发送开始的事件
            BindRelyOn();
            OpenSomeUI();
        }

        private void BindRelyOn()
        {
            GameManager.Instance.QFrameworkContainer.RegisterInstance(this);
        }

        private void OpenSomeUI()
        {
            var levelTailNum = LevelManager.Instance.GetLevelTailNum();
            var title = MainManager.Instance.GetLevelMessage(levelTailNum - 1).title;
            UIMgr.OpenPanel<LevelMainPanel>(UILevel.Common,
                new LevelMainPanelData { LevelTailNum = levelTailNum, Title = title }, null, nameof(LevelMainPanel));

            /*
            if (levelTailNum == 1)
            {
                DialogueManager.Instance.OpenDialogue(0);
            }
            */
        }

        private void OnDestroy()
        {
            UIMgr.ClosePanel(nameof(LevelMainPanel));
        }
    }
}