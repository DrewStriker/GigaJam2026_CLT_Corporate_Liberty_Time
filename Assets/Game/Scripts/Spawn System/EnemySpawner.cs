using UnityEngine;
using Zenject;
using Timer = Game.Core.Timer;

namespace Game.SpawnSystem
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private SpawnConfig spawnConfig;
        [Inject]private EnemyFactory enemyFactory;
        private Timer timer;

        private void Awake()
        {
            timer = new Timer(Core.TimerMode.CountDown);
        }

        //private void Sort
    }
}
