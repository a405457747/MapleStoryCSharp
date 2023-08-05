/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MapleStory;
using System.Linq;
using static MapleStory.LogNote;
using UniRx;

[System.Serializable]
public class TodoListItem
{
    public StringReactiveProperty todoCoent;
    public BoolReactiveProperty isFinish;
    
    private TodoList _todoList;
    public TodoItemUIGameObject todoItemUIGameObject;

    public void Init(string content, TodoList todoList)
    {
        
         todoItemUIGameObject = AppRoot.Instance.TodoMainPanel.CreateTodoItemUI();


        todoCoent = new StringReactiveProperty(content);
        _todoList = todoList;
        isFinish = new BoolReactiveProperty(false);


        todoCoent.Subscribe(_ => { todoItemUIGameObject.txtTextText(_); });
        isFinish.Subscribe(_ =>
        {
            if (_ == true)
            {
                DelItem();
            }
        });
        
           
        todoItemUIGameObject.delButtonOnClick(
            () => { isFinish.Value = true; });
        
        todoItemUIGameObject.changeButtonOnClick(
            () =>
            {
                todoCoent.Value = AppRoot.Instance.TodoMainPanel.txtInputFieldInput();
            }
            );
    }


    public void DelItem()
    {
        todoItemUIGameObject.DelGo();
        todoItemUIGameObject = null;
        _todoList.RemoveItem(this);
    }
}*/