using CallPalCatGames.QFrameworkExtension;

namespace QFramework
{
    public partial class QMsgCenter
    {
        partial void ForwardMsg(QMsg msg)
        {
            switch (msg.ManagerID)
            {
                case QMgrID.Game:
                    GameManager.Instance.SendMsg(msg);
                    break;
                case QMgrID.Level:
                    LevelManager.Instance.SendMsg(msg);
                    break;
                case QMgrID.Main:
                    MainManager.Instance.SendMsg(msg);
                    break;
                case QMgrID.Menu:
                    MenuManager.Instance.SendMsg(msg);
                    break;
                case QMgrID.Pool:
                    PoolManager.Instance.SendMsg(msg);
                    break;
                case QMgrID.Save:
                    SaveManager.Instance.SendMsg(msg);
                    break;
                case QMgrID.Dialogue:
                    DialogueManager.Instance.SendMsg(msg);
                    break;
                case QMgrID.Gui:
                    GuiManager.Instance.SendMsg(msg);
                    break;
                case QMgrID.Voice:
                    VoiceManager.Instance.SendMsg(msg);
                    break;
                case  QMgrID.HotFix:
                    HotFixManager.Instance.SendMsg(msg);
                    break;
            }
        }
    }
}