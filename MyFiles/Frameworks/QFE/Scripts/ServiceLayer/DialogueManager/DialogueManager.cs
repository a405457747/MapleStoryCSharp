using System.Collections.Generic;
using System.Xml;
using QFramework;
using UnityEngine;
using UnityEngine.Serialization;

namespace CallPalCatGames.QFrameworkExtension
{
    public enum DialogueEvent
    {
        Began = QMgrID.Dialogue,
        OpenDialogue
    }

    public class OpenDialogueQMsg : QMsg
    {
        public OpenDialogueQMsg() : base((int) DialogueEvent.OpenDialogue)
        {
        }

        public int PlotIndex { get; set; }
    }

    public class DialogueManager : QMgrBehaviour, ISingleton
    {
        private LoadHelper _loadHelper;
        [FormerlySerializedAs("DialogueDic")]  public Dictionary<int, List<DialogBox>> DialogueDic; //key是下标从0开始;
        public override int ManagerId => QMgrID.Dialogue;
        public static DialogueManager Instance => MonoSingletonProperty<DialogueManager>.Instance;

        public void OnSingletonInit()
        {
            _loadHelper = gameObject.GetOrAddComponent<LoadHelper>();
            AnalysisLinesByXml(_loadHelper.LoadThing<TextAsset>("XMLDialogue").text);
            RegisterEvent(DialogueEvent.OpenDialogue);
        }

        protected override void ProcessMsg(int eventId, QMsg msg)
        {
            switch (msg.EventID)
            {
                case (int) DialogueEvent.OpenDialogue:
                    var tempMsg = msg as OpenDialogueQMsg;
                    OpenDialogue(tempMsg.PlotIndex);
                    break;
            }
        }

        private void AnalysisLinesByXml(string strContent)
        {
            DialogueDic = new Dictionary<int, List<DialogBox>>();
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(strContent);
            var nodeList = xmlDocument.GetElementsByTagName("Plot");
            for (var i = 0; i < nodeList.Count; i++)
            {
                var node = nodeList[i];
                var list = new List<DialogBox>();
                for (var index = 0; index < node.ChildNodes.Count; index++)
                {
                    var nodeChildren = node.ChildNodes[index]; //此时的nodeChildren是DialogBox
                    string name = "", image = "", saidContent = "";
                    for (var j = 0; j < nodeChildren.ChildNodes.Count; j++)
                    {
                        var nodeChildrenChildren = nodeChildren.ChildNodes[j];
                        switch (j)
                        {
                            case 0:
                                name = SaveManager.Instance.IsChineseLanguage()
                                    ? nodeChildrenChildren.InnerText
                                    : nodeChildrenChildren.Attributes["EN"].Value;
                                break;
                            case 1:
                                image = nodeChildrenChildren.InnerText;
                                break;
                            case 2:
                                saidContent = SaveManager.Instance.IsChineseLanguage()
                                    ? nodeChildrenChildren.InnerText
                                    : nodeChildrenChildren.Attributes["EN"].Value;
                                break;
                        }
                    }
                    var dialogBox = new DialogBox(name, image, saidContent);
                    list.Add(dialogBox);
                }
                DialogueDic.Add(i, list);
            }
        }

        public void OpenDialogue(int plotIndex)
        {
            UIMgr.OpenPanel<DialoguePanel>(UILevel.Toast).StartDialogue(plotIndex);
        }
    }
}