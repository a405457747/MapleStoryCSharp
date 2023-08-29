namespace QFramework
{
    public partial class QMgrID 
    {
        public const int Level = FrameworkEnded + QMsgSpan.Count;
        public const int Main = Level + QMsgSpan.Count;
        public const int Menu = Main + QMsgSpan.Count;
        public const int Pool = Menu + QMsgSpan.Count;
        public const int Save = Pool + QMsgSpan.Count;
        public const int Dialogue = Save + QMsgSpan.Count;
        public const int Voice = Dialogue + QMsgSpan.Count;
        public const int Gui = Voice + QMsgSpan.Count;
        public const int HotFix = Gui + QMsgSpan.Count;
    }
}