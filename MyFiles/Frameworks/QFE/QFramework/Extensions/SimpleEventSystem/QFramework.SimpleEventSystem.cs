using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;

namespace QFramework
{
    public class EventId : Attribute
    {
        public EventId(int identifier)
        {
            Identifier = identifier;
        }

        public EventId()
        {
        }

        public int Identifier { get; set; }
    }

    public interface IEventManager
    {
        int EventId { get; set; }
        Type For { get; }
        void Publish(object evt);
    }

    public class EventManager<TEventType> : IEventManager
    {
        private int mEventId;
        private Subject<TEventType> mEventType;

        public Subject<TEventType> EventSubject
        {
            get => mEventType ?? (mEventType = new Subject<TEventType>());
            set => mEventType = value;
        }

        public int EventId
        {
            get
            {
                if (mEventId > 0)
                    return mEventId;

                var eventIdAttribute =
                    For.GetCustomAttributes(typeof(EventId), true).FirstOrDefault() as
                        EventId;
                if (eventIdAttribute != null) return mEventId = eventIdAttribute.Identifier;

                return mEventId;
            }
            set => mEventId = value;
        }

        public Type For => typeof(TEventType);

        public void Publish(object evt)
        {
            if (mEventType != null) mEventType.OnNext((TEventType) evt);
        }
    }

    public class EventAggregator : IEventAggregator
    {
        private Dictionary<Type, IEventManager> mManagers;

        private Dictionary<int, IEventManager> mManagersById;

        public Dictionary<Type, IEventManager> Managers
        {
            get => mManagers ?? (mManagers = new Dictionary<Type, IEventManager>());
            set => mManagers = value;
        }

        public Dictionary<int, IEventManager> ManagersById
        {
            get => mManagersById ?? (mManagersById = new Dictionary<int, IEventManager>());
            set => mManagersById = value;
        }

        public IObservable<TEvent> GetEvent<TEvent>()
        {
            IEventManager eventManager;
            if (!Managers.TryGetValue(typeof(TEvent), out eventManager))
            {
                eventManager = new EventManager<TEvent>();
                Managers.Add(typeof(TEvent), eventManager);
                var eventId = eventManager.EventId;
                if (eventId > 0) ManagersById.Add(eventId, eventManager);
            }

            var em = eventManager as EventManager<TEvent>;
            if (em == null) return null;
            return em.EventSubject;
        }

        public void Publish<TEvent>(TEvent evt)
        {
            IEventManager eventManager;

            if (!Managers.TryGetValue(typeof(TEvent), out eventManager))
                // No listeners anyways
                return;

            eventManager.Publish(evt);
        }

        public IEventManager GetEventManager(int eventId)
        {
            if (ManagersById.ContainsKey(eventId))
                return ManagersById[eventId];
            return null;
        }

        public void PublishById(int eventId, object data)
        {
            var evt = GetEventManager(eventId);
            if (evt != null)
                evt.Publish(data);
        }
    }

    public static class SimpleEventSystem
    {
        private static readonly EventAggregator mEventAggregator = new EventAggregator();

        public static IObservable<TEvent> GetEvent<TEvent>()
        {
            return mEventAggregator.GetEvent<TEvent>();
        }

        public static void Publish<TEvent>(TEvent evt)
        {
            mEventAggregator.Publish(evt);
        }
    }

    public interface IEventAggregator
    {
        IObservable<TEvent> GetEvent<TEvent>();
        void Publish<TEvent>(TEvent evt);
    }
}