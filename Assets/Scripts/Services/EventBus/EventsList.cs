using System.Collections.Generic;

namespace Services.EventBusSystem
{
    public class EventsList<TEvent> where TEvent : class
    {
        private readonly List<TEvent> _eventList = new List<TEvent>();
        private bool _needsCleanUp = false;
        private bool _executing;

        public IReadOnlyList<TEvent> EventList => _eventList;
        
        public void Add(TEvent tEvent)
        {
            _eventList.Add(tEvent);
        }

        public void Remove(TEvent tEvent)
        {
            if (_executing)
            {
                int i = _eventList.IndexOf(tEvent);
                if (i >= 0)
                {
                    _needsCleanUp = true;
                    _eventList[i] = null;
                }
            }
            else
            {
                _eventList.Remove(tEvent);
            }
        }

        public void Cleanup()
        {
            if (!_needsCleanUp)
            {
                return;
            }

            _eventList.RemoveAll(s => s == null);
            _needsCleanUp = false;
        }

        public void SetExecuting(bool executing)
        {
            _executing = executing;
        }
    }
}