using System;
using System.Collections.Generic;
using System.Linq;

namespace Services.EventBusSystem
{
    public static class EventBusHelper
    {
        private static Dictionary<Type, List<Type>> _cashedEventTypes = new Dictionary<Type, List<Type>>();

        public static List<Type> GetEventTypes(IEvent iEvent)
        {
            Type type = iEvent.GetType();
            if (_cashedEventTypes.ContainsKey(type))
            {
                return _cashedEventTypes[type];
            }

            List<Type> eventTypes = type.GetInterfaces().Where(t => t.GetInterfaces()
                    .Contains(typeof(IEvent))).ToList();

            _cashedEventTypes[type] = eventTypes;
            return eventTypes;
        }
    }
}