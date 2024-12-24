using System;

namespace Services.Timer
{
    public abstract class Timer : IDisposable
    {
        private ITimerService _timerService;

        protected float _initialTime;
        protected bool _disposed;

        public float CurrentTime { get; protected set; }
        public bool IsRunning { get; private set; }
        
        protected Action onTimerComplete = delegate { };
        protected Action<float> onTimerUpdate = delegate { };

        protected Timer(ITimerService timerService, float value) 
        {
            _timerService = timerService;
            _initialTime = value;
        }

        public void SetOnComplete(Action onComplete)
        {
            onTimerComplete = onComplete;
        }

        public void SetOnUpdate(Action<float> onUpdate)
        {
            onTimerUpdate = onUpdate;
        }

        public void Start()
        {
            CurrentTime = _initialTime;
            if (!IsRunning) 
            {
                IsRunning = true;
                _timerService.RegisterTimer(this);
            }
        }

        public void Stop()
        {
            if (IsRunning)
            {
                IsRunning = false;
                _timerService.DeregisterTimer(this);
            }
        }

        public virtual void Tick(float deltaTime)
        {
            if (IsRunning)
            {
                OnTick(deltaTime);
                
                if (!IsFinished)
                {
                    onTimerUpdate?.Invoke(CurrentTime);
                }
            }
        }

        public abstract void OnTick(float deltaTime);
        public abstract bool IsFinished { get; }

        public void Resume()
        {
            IsRunning = true;
        }

        public void Pause()
        {
            IsRunning = false;
        }

        public virtual void Reset()
        {
            CurrentTime = _initialTime;
        }

        public virtual void Reset(float newTime)
        {
            _initialTime = newTime;
            Reset();
        }
        
        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing) {
                _timerService.DeregisterTimer(this);
            }

            _disposed = true;
        }
    }
}