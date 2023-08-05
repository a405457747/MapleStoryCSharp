/*using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MapleStory;
using System.Linq;
using UniRx;
using static MapleStory.LogNote;


public class TodoList : MonoBehaviour
{
    private TodoMainPanel _todoMainPanel;
    [SerializeField] public ReactiveCollection<TodoListItem> _items;

    private void Start()
    {
        _items= new ReactiveCollection<TodoListItem>();
        _todoMainPanel = AppRoot.Instance
            .TodoMainPanel;
        Print(
            _todoMainPanel == null, "mainPanel is null");
        _todoMainPanel
            .titleTextText(
                "Todo List App");

        sumbitContent();

        _items.ObserveAdd().Subscribe(_ =>
        {
            _.Value.Init( _todoMainPanel.txtInputFieldInput(),this);
            Debug("add item info", _.Value.GetHashCode());
        });
        _items.ObserveRemove().Subscribe(_ => { Debug("del item info", _.Value.GetHashCode()); });

        
        
        
        //var tests = _items.Where(item => item.isFinish.Value).Select(item=>item.isFinish.Value).ToList();
    }

    private void sumbitContent()
    {
        _todoMainPanel
            .submitButtonOnClick(() =>
            {
              

                if (string.IsNullOrEmpty(_todoMainPanel.txtInputFieldInput()) == false)
                {
                    *//*Info(
                        "submitContent"
                        , submitContent);*//*
                    _items.Add(new TodoListItem());
                }
            });
    }

    public void RemoveItem(TodoListItem todoListItem)
    {
        foreach (var VARIABLE in _items)
        {
            Debug("show all",VARIABLE.isFinish);
        }
    }
}*/