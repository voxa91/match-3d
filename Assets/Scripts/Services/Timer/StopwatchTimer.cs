namespace Services.Timer
{
    public class StopwatchTimer : Timer
    {
        public StopwatchTimer(ITimerService timerService) : base(timerService, 0)
        {
        }

        public override void OnTick(float deltaTime)
        {
            CurrentTime += deltaTime;
        }

        public override bool IsFinished => false;
    }
}