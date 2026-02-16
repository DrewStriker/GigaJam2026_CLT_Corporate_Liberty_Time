using System;
using PlasticGui.WorkspaceWindow.Locks;

namespace Game.TimeSystem
{
    public enum TimerMode { CountUp, CountDown }
    public class Timer
    {
        private bool isRunning;
        private float duration;
        private float baseDuration;
        private float timer;
        private float timeRemaining;
        private float elapsedTime;
        private TimerMode mode;

        public float TimerValue => timer;
        public float TimeRemanining => timeRemaining;
        public float RelativeNormalizedProgress => (duration - timeRemaining) / duration;
        public float AbsNormalizedProgress => elapsedTime / baseDuration;
        public bool IsFinished => isRunning && timeRemaining <= 0f;

        public event Action OnTimerCompleted;

        public Timer(TimerMode timerMode)
        {
            mode = timerMode;
        }

        public void Start(float duration)
        {
            this.duration = duration;
            baseDuration = duration;
            if (mode == TimerMode.CountDown) timer = duration;
            else timer = 0f;
            timeRemaining = duration;
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

        public void ExtendDuration(float duration)
        {
            this.duration += duration;
            if (mode == TimerMode.CountDown) timer += duration;
            timeRemaining = duration;
            isRunning = true;
        }

        public void UpdateTimer(float deltaTime)
        {
            if (!isRunning) return;

            if (mode == TimerMode.CountUp) IncreaseTimer(deltaTime);
            else DecreaseTimer(deltaTime);
            timeRemaining -= deltaTime;
            elapsedTime += deltaTime;
            CheckCompletion();
        }

        public float GetFractionedDuration() => duration / 17;

        private void DecreaseTimer(float deltaTime)
        {
            timer -= deltaTime;
        }

        private void IncreaseTimer(float deltaTime)
        {
            timer += deltaTime;
        }

        private void CheckCompletion()
        {
            if (timeRemaining <= 0)
            {
                isRunning = false;
                OnTimerCompleted?.Invoke();
            }
        }
    }
}

