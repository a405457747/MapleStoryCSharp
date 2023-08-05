using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MapleStory;
using System.Linq;
using UnityEngine.Events;
using static MapleStory.LogNote;

public class TodoItemUIGameObject : MonoBehaviour
{
    ///AC
    [SerializeField] public Button changeButton = null;

    public void changeButtonOnClick(UnityAction clickAction)
    {
        changeButton.onClick.AddListener(clickAction);
    }

    [SerializeField] public Button confirmButton = null;

    public void confirmButtonOnClick(UnityAction clickAction)
    {
        confirmButton.onClick.AddListener(clickAction);
    }

    [SerializeField] public Button delButton = null;

    public void delButtonOnClick(UnityAction clickAction)
    {
        delButton.onClick.AddListener(clickAction);
    }

    [SerializeField] public Text txtText = null;

    public void txtTextText(string txt)
    {
        txtText.text = txt;
    }

    private void FindAllComponent()
    {
        changeButton = ToolRoot.FindComponent<Button>(this, "changeButton");
        confirmButton = ToolRoot.FindComponent<Button>(this, "confirmButton");
        delButton = ToolRoot.FindComponent<Button>(this, "delButton");
        txtText = ToolRoot.FindComponent<Text>(this, "txtText");
    }

    ///AC
    private void Awake()
    {
        FindAllComponent();
    }

    public void DelGo()
    {
        Destroy(this.gameObject);
    }
}