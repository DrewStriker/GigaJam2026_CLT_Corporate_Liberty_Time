using System;
using UnityEngine;

namespace Game.TimeSystem
{
    public class TimerManager : MonoBehaviour, ITimerManager
    {
        [SerializeField] [Range(0, 1)] private float timeScale = 1;
        [SerializeField] private float gameDuration;
        [Range(0, 5)] [SerializeField] private int extraTimeRange;
        public Timer Timer { get; private set; }
        public int ExtraTime { get; private set; }

        public event Action MainTimeExpired;
        public event Action ExtraTimeExpired;

        private void Awake()
        {
            Timer = new Timer(TimerMode.CountUp);
            Timer.OnTimerCompleted += OnMainTimeExpired;
        }

        public void Start()
        {
            StartTimer();
        }

        private void Update()
        {
            Timer.UpdateTimer(Time.deltaTime * timeScale);
        }

        public void StartTimer()
        {
            Timer.Start(gameDuration);
        }

        private void OnMainTimeExpired()
        {
            Timer.OnTimerCompleted -= OnMainTimeExpired;
            Timer.ExtendDuration(GetRandomOvertime());
            Timer.OnTimerCompleted += OnExtraTimeExpired;
            MainTimeExpired?.Invoke();
        }

        private void OnExtraTimeExpired()
        {
            ExtraTimeExpired?.Invoke();
            Timer.OnTimerCompleted -= OnExtraTimeExpired;
        }

        private float GetRandomOvertime()
        {
            var random = ExtraTime = UnityEngine.Random.Range(0, extraTimeRange + 1);
            return Timer.GetFractionedDuration() * random;
        }

        private void OnDisable()
        {
            Timer.OnTimerCompleted -= OnMainTimeExpired;
            Timer.OnTimerCompleted -= OnExtraTimeExpired;
        }
    }
}