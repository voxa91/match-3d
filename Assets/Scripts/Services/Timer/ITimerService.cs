using System;
using Services.ServiceResolver;

namespace Services.Timer
{
    public interface ITimerService : IService
    {
        public Timer StartCountdownTimer(float duration, Action onComplete, Action<float> onUpdate = null);
        public Timer StartStopwatchTimer(Action<float> onUpdate = null);
        public void RegisterTimer(Timer timer);
        public void DeregisterTimer(Timer timer);
    }
}