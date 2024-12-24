namespace Services.Timer
{
    public class CountdownTimer : Timer
    {
        public CountdownTimer(ITimerService timerService, float value) : base(timerService, value)
        {
        }

        public override void OnTick(float deltaTime)
        {
            if (CurrentTime > 0) 
            {
                CurrentTime -= deltaTime;
            }

            if (CurrentTime <= 0) 
            {
                onTimerComplete?.Invoke();  
                Stop();
            }
        }

        public override bool IsFinished => CurrentTime <= 0;
    }
}