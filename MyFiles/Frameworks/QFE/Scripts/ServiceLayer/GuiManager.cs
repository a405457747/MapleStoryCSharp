using QFramework;
using UniRx;
using UnityEngine.UI;

namespace CallPalCatGames.QFrameworkExtension
{
    public enum GuiEvent
    {
        Began = QMgrID.Gui,
        ButtJointToggle
    }

    public class ButtJointToggleQMsg : QMsg
    {
        public ButtJointToggleQMsg(Toggle toggle, BoolReactiveProperty isOnProperty) : base(
            (int) GuiEvent.ButtJointToggle)
        {
            Toggle = toggle;
            IsOnProperty = isOnProperty;
        }

        public Toggle Toggle { get; set; }
        public BoolReactiveProperty IsOnProperty { get; set; }
    }

    public class GuiManager : QMgrBehaviour, ISingleton
    {
        public override int ManagerId => QMgrID.Gui;
        public static GuiManager Instance => MonoSingletonProperty<GuiManager>.Instance;

        public void OnSingletonInit()
        {
            RegisterEvent(GuiEvent.ButtJointToggle);
        }

        protected override void ProcessMsg(int eventId, QMsg msg)
        {
            switch (msg.EventID)
            {
                case (int) GuiEvent.ButtJointToggle:
                    var tempMsg = msg as ButtJointToggleQMsg;
                    ButtJointToggle(tempMsg.Toggle, tempMsg.IsOnProperty);
                    break;
            }
        }

        private void ButtJointToggle(Toggle toggle, BoolReactiveProperty isOnProperty)
        {
            toggle.isOn = isOnProperty.Value;
            toggle.onValueChanged.AddListener(isOn => { isOnProperty.Value = isOn; });
        }
    }
}