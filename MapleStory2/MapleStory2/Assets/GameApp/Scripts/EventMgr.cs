using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace MapleStory2
{
    interface IEventMsg
    {
    }

    class EventMsg<T> : IEventMsg
    {
        internal Action<T> act;

        internal EventMsg(Action<T> a)
        {
            this.act += a;
        }
    }

    class EventMsg : IEventMsg
    {
        internal Action act;

        internal EventMsg(Action a)
        {
            this.act += a;
        }
    }

    class EventManager
    {
        private Dictionary<string, IEventMsg> eventDict = new Dictionary<string, IEventMsg>();

        internal void AddEvent(string eventName, Action act)
        {
            if (eventDict.ContainsKey(eventName))
            {
                (eventDict[eventName] as EventMsg).act += act;
            }
            else
            {
                eventDict.Add(eventName, new EventMsg(act));
            }
        }

        internal void AddEvent<T>(string eventName, Action<T> act)
        {
            if (eventDict.ContainsKey(eventName))
            {
                (eventDict[eventName] as EventMsg<T>).act += act;
            }
            else
            {
                eventDict.Add(eventName, new EventMsg<T>(act));
            }
        }

        internal void SendEvent(string eventName)
        {
            if (eventDict.ContainsKey(eventName))
            {
                (eventDict[eventName] as EventMsg).act();
            }
        }

        internal void SendEvent<T>(string eventName, T obj)
        {
            if (eventDict.ContainsKey(eventName))
            {
                (eventDict[eventName] as EventMsg<T>).act(obj);
            }
        }

        internal void RemoveEvent(string eventName, Action act)
        {
            if (eventDict.ContainsKey(eventName))
            {
                (eventDict[eventName] as EventMsg).act -= act;
            }
        }

        internal void RemoveEvent<T>(string eventName, Action<T> act)
        {
            if (eventDict.ContainsKey(eventName))
            {
                (eventDict[eventName] as EventMsg<T>).act -= act;
            }
        }

        internal void ClearEvent()
        {
            eventDict.Clear();
        }
    }


    class EventMgr : MonoBehaviour
    {
        static EventMgr Inst;

        private EventManager _eventManager = new EventManager();

        void Awake()
        {
            Inst = this;
        }

        internal void Sample()
        {
        }

        internal void AddEvent(string eventName, Action act)
        {
            _eventManager.AddEvent(eventName, act);
        }

        internal void AddEvent<T>(string eventName, Action<T> act)
        {
            _eventManager.AddEvent<T>(eventName, act);
        }

        internal void SendEvent(string eventName)
        {
            _eventManager.SendEvent(eventName);
        }

        internal void SendEvent<T>(string eventName, T obj)
        {
            _eventManager.SendEvent(eventName, obj);
        }

        internal void RemoveEvent(string eventName, Action act)
        {
            _eventManager.RemoveEvent(eventName, act);
        }

        internal void RemoveEvent<T>(string eventName, Action<T> act)
        {
            _eventManager.RemoveEvent(eventName, act);
        }
    }
}