using System;
using System.Collections.Generic;
using Services.ServiceResolver;
using UnityEngine;

namespace Services.EventBusSystem
{
    public class EventBus : BaseService
    {
        private Dictionary<Type, EventsList<IEvent>> _subscribers = new Dictionary<Type, EventsList<IEvent>>();
        
        public void Subscribe(IEvent iEvent)
        {
            List<Type> eventTypes = EventBusHelper.GetEventTypes(iEvent);
            foreach (Type t in eventTypes)
            {
                if (!_subscribers.ContainsKey(t))
                {
                    _subscribers[t] = new EventsList<IEvent>();
                }
                _subscribers[t].Add(iEvent);
            }
        }
        
        public void Unsubscribe(IEvent iEvent)
        {
            List<Type> subscriberTypes = EventBusHelper.GetEventTypes(iEvent);
            foreach (Type t in subscriberTypes)
            {
                if (_subscribers.ContainsKey(t))
                    _subscribers[t].Remove(iEvent);
            }
        }
        
        public void RaiseEvent<TEvent>(Action<TEvent> action) where TEvent : class, IEvent
        {
            EventsList<IEvent> events = _subscribers[typeof(TEvent)];
	
            events.SetExecuting(true);
            foreach (IEvent iEvent in events.EventList)
            {
                try
                {
                    action.Invoke(iEvent as TEvent);
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
            }
            events.SetExecuting(false);
            events.Cleanup();
        }
    }
}