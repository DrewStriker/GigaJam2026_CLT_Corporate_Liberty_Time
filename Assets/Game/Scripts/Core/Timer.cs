
using System;

namespace Game.Core
{
    public enum TimerMode { CountUp, CountDown }
    public class Timer
    {
        private float duration;
        private bool isRunning;
        private float timeRemaining;
        private TimerMode mode;

        public float TimeRemanining => timeRemaining; 
        public event Action OnTimerCompleted;

        public bool IsFinished => isRunning && timeRemaining <= 0f;

        public Timer(TimerMode timerMode)
        {
            mode = timerMode;
        }

        public void Start(float duration)
        {
            this.duration = duration;
            if (mode == TimerMode.CountDown) timeRemaining = duration;
            else timeRemaining = 0f;
            isRunning = true;
        }

        public void Stop()
        {
            isRunning = false;
        }

        public void Resume()
        {
            isRunning = true;
        }

        public void UpdateTimer(float deltaTime)
        {
            if (!isRunning) return;

            if (mode == TimerMode.CountUp) IncreaseTimer(deltaTime);
            else DecreaseTimer(deltaTime);
        }

        private void DecreaseTimer(float deltaTime)
        {
            timeRemaining -= deltaTime;
            CheckCompletion();
        }

        private void IncreaseTimer(float deltaTime)
        {
            timeRemaining += deltaTime;
            CheckCompletion();
        }

        private void CheckCompletion()
        {
            if (mode == TimerMode.CountUp)
            {
                if (timeRemaining >= duration)
                {
                    isRunning = false;
                    OnTimerCompleted?.Invoke();
                }
            }

            else
            {
                if (timeRemaining <= 0f)
                {
                    isRunning = false;
                    OnTimerCompleted?.Invoke();
                }
            }
        }
    }
}

