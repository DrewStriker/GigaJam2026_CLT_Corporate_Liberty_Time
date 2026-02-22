using Game.ItemSystem;
using Game.TimeSystem;
using UnityEngine;
using System;
namespace Game.SpawnSystem
{
    public class ItemSpawner : MonoBehaviour
    {
        [field: SerializeField] public ItemType ItemType { get; private set; }
        [SerializeField] private float itemSpawnInterval;
        private Timer timer;
        public event Action<ItemSpawner> OnTimerCompleted;
        void Start()
        {
            timer = new Timer(TimerMode.CountDown);
            timer.Start(itemSpawnInterval);
            timer.OnTimerCompleted += () => OnTimerCompleted?.Invoke(this);
        }

        public void ResetSpawnCooldown()
        {
            timer.Stop();
            timer.Start(itemSpawnInterval);
        }

        private void Update()
        {
            timer.UpdateTimer(Time.deltaTime);
        }

        private void OnDestroy()
        {
            timer.OnTimerCompleted -= () => OnTimerCompleted?.Invoke(this);
        }
    }
}
