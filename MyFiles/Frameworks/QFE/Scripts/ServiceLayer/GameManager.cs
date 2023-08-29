using QFramework;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace CallPalCatGames.QFrameworkExtension
{
    public enum GameEvent//用这个再做状态,但是我只用到了Start,Over,Win3个状态要注意下啊
    {
        Began = QMgrID.Game,
        PauseGame,
        UnPauseGame,
        GameStart,
        GameOver,
        GameWin,
        End,
    }

    public class GameManager : QMgrBehaviour, ISingleton
    {
        public override int ManagerId => QMgrID.Game;
        public static GameManager Instance => MonoSingletonProperty<GameManager>.Instance;
        public QFrameworkContainer QFrameworkContainer { get; private set; }
        private GameEvent _gameState;

        public void OnSingletonInit()
        {
            RegisterEvent(GameEvent.PauseGame);
            RegisterEvent(GameEvent.UnPauseGame);
            QEventSystem.RegisterEvent(GameEvent.GameOver, GameEventCallback);
            QEventSystem.RegisterEvent(GameEvent.GameWin, GameEventCallback);
            QEventSystem.RegisterEvent(GameEvent.GameStart, GameEventCallback);
        }

        public void StartGame()
        {
            QFrameworkContainer = new QFrameworkContainer();
            QEventSystem.SendEvent(GameEvent.GameStart);
        }

        public void GameWinCallBack()
        {
            if (_gameState != GameEvent.GameWin)
            {
                QEventSystem.SendEvent(GameEvent.GameWin);
            }
        }

        public void GameOverCallBack()
        {
            if (_gameState != GameEvent.GameOver)
            {
                QEventSystem.SendEvent(GameEvent.GameOver);
            }
        }

        protected override void ProcessMsg(int eventId, QMsg msg)
        {
            switch (msg.EventID)
            {
                case (int)GameEvent.PauseGame:
                    PauseGame();
                    break;
                case (int)GameEvent.UnPauseGame:
                    UnPauseGame();
                    break;
            }
        }

        private void GameEventCallback(int key, object[] param)
        {
            switch (key)
            {
                case (int)GameEvent.GameOver:
                    UIMgr.OpenPanel(nameof(GameLosePanel), UILevel.PopUI);
                    SendMsg(new VoiceMsgQMsg("GameLose", (int)VoiceEvent.PlaySound));
                    _gameState = GameEvent.GameOver;
                    break;
                case (int)GameEvent.GameWin:
                    this.Delay(0.5f, () =>
                    {
                        UIMgr.OpenPanel<GameWinPanel>(UILevel.PopUI,
    new GameWinPanelData
    {
        IsArriveMaxLevelNum = MainManager.Instance.IsArriveMaxLevelNum(),
        NextLevelNum = LevelManager.Instance.GetLevelTailNum(),
    }, null,
    nameof(GameWinPanel));
                        SendEvent(MainEvent.GameWinOrSave);
                        SendMsg(new VoiceMsgQMsg("GameWin", (int)VoiceEvent.PlaySound)); //音乐
                    });
                    _gameState = GameEvent.GameWin;
                    break;
                case (int)GameEvent.GameStart:
                    _gameState = GameEvent.GameStart;
                    //可以做成倒计时吧
                    break;
            }
        }

        private void PauseGame()
        {
            SendMsg(new VoiceSwitchQMsg(false, (int)VoiceEvent.MusicSwitch));
            Time.timeScale = 0f;
        }

        private void UnPauseGame()
        {
            SendMsg(new VoiceSwitchQMsg(true, (int)VoiceEvent.MusicSwitch));
            Time.timeScale = 1f;
        }
    }
}