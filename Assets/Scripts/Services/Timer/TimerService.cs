using System;
using System.Collections.Generic;
using Services.ServiceResolver;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Services.Timer
{
    public class TimerService : BaseService, ITimerService
    {
        private List<Timer> _timers = new List<Timer>();
        
        private TimerUpdateComponent _timerUpdateComponent;
        
        public void Initialize(IServiceLocator serviceLocator)
        {
            base.Initialize(serviceLocator);
            _timerUpdateComponent = new GameObject("[Timer]").AddComponent<TimerUpdateComponent>();
            _timerUpdateComponent.OnUpdate += UpdateTimers;
        }
        
        public override void Dispose()
        {
            if (_timerUpdateComponent != null)
            {
                _timerUpdateComponent.OnUpdate -= UpdateTimers;
                Object.Destroy(_timerUpdateComponent.gameObject);
            }
        }

        public Timer StartCountdownTimer(float duration, Action onComplete, Action<float> onUpdate = null)
        {
            var timer = new CountdownTimer(this, duration);
            timer.SetOnComplete(onComplete);
            timer.SetOnUpdate(onUpdate);
            timer.Start();
            return timer;
        }

        public Timer StartStopwatchTimer(Action<float> onUpdate = null)
        {
            var timer = new StopwatchTimer(this);
            timer.SetOnUpdate(onUpdate);
            timer.Start();
            return timer;
        }

        public void RegisterTimer(Timer timer)
        {
            _timers.Add(timer);
        }

        public void DeregisterTimer(Timer timer)
        {
            _timers.Remove(timer);
        }

        private void UpdateTimers()
        {
            if (_timers.Count == 0) return;
            
            foreach (var timer in _timers)
            {
                timer.Tick(Time.deltaTime);
            }
        }
    }
}