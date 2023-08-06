using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MapleStory;
using UnityEngine.Events;
using static MapleStory.LogNote;

public class TodoMainPanel : MonoBehaviour
{
    ///AC
    [SerializeField] public Text titleText = null;

    public void titleTextText(string txt)
    {
        titleText.text = txt;
    }

    [SerializeField] public InputField txtInputField = null;

    public string txtInputFieldInput()
    {
        return txtInputField.text;
    }

    [SerializeField] public Button submitButton = null;

    public void submitButtonOnClick(UnityAction clickAction)
    {
        submitButton.onClick.AddListener(clickAction);
    }

    [SerializeField] public RectTransform TodoItemsRectTransform = null;

    public void TodoItemsRectTransformParent(Transform parent, bool worldPositionStays)
    {
        TodoItemsRectTransform.SetParent(parent, worldPositionStays);
    }

    [SerializeField] public Transform FinishTodoItemsTransform = null;

    private void FindAllComponent()
    {
        titleText = ToolRoot.FindComponent<Text>(this, "titleText");
        txtInputField = ToolRoot.FindComponent<InputField>(this, "txtInputField");
        submitButton = ToolRoot.FindComponent<Button>(this, "submitButton");
        TodoItemsRectTransform = ToolRoot.FindComponent<RectTransform>(this, "TodoItemsRectTransform");
        FinishTodoItemsTransform = ToolRoot.FindComponent<Transform>(this, "FinishTodoItemsTransform");
    }

    ///AC

    private void Awake()
    {
        FindAllComponent();
        Debug(
            titleText == null, "titleText is null?");
        /*Debug(
            TodoItemsGameObject==null,"TodoItemsGameObject is null?");*/
    }

    public TodoItemUIGameObject CreateTodoItemUI()
    {
        var uiGo = Resources.Load<GameObject>(nameof(TodoItemUIGameObject
        ));
        uiGo = Object.Instantiate(
            uiGo, AppRoot.Instance
                .TodoMainPanel
                .TodoItemsRectTransform
            , false) as GameObject;

        return uiGo.AddComponent<TodoItemUIGameObject>();;
    }
}