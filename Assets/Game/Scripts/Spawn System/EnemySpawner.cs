using Game.Characters;
using Game.RankSystem;
using UnityEngine;
using Zenject;
using Timer = Game.Core.Timer;

namespace Game.SpawnSystem
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private SpawnConfig spawnConfig;
        [Inject] private PlaceholderFactory<EnemyType,EnemyController> enemyFactory;
        [Inject] private RankManager rankManager;

        private Timer timer;
        private RankConfig[] rankConfigs;
        private int currentRank => rankManager.CurrentRank;

        private void Awake()
        {
            rankConfigs = new RankConfig[7]
            {
                spawnConfig.Rank0Config,
                spawnConfig.Rank1Config,
                spawnConfig.Rank2Config,
                spawnConfig.Rank3Config,
                spawnConfig.Rank4Config,
                spawnConfig.Rank5Config,
                spawnConfig.Rank6Config
            };

            timer = new Timer(Core.TimerMode.CountDown);
            timer.OnTimerCompleted += SpawnRandomEnemy;
        }

        private void Start()
        {
            timer.Start(rankConfigs[currentRank].SpawnInterval);
            //enemyFactory.Create(EnemyType.Basic);
        }

        private void Update()
        {
            timer.UpdateTimer(Time.deltaTime);
        }

        private void SpawnRandomEnemy()
        {
            timer.Stop();
            float totalSpawnChance = rankConfigs[currentRank].TotalSpawnChance();
            float randomSpawn = Random.Range(0f, totalSpawnChance);
            foreach (var enemy in rankConfigs[currentRank].Enemies)
            {
                randomSpawn -= enemy.SpawnChance;
                if (randomSpawn <= 0f)
                {
                    enemyFactory.Create(enemy.EnemyType);
                    timer.Start(rankConfigs[currentRank].SpawnInterval);
                    return;
                }
            }
        }

        private void OnDisable()
        {
            timer.OnTimerCompleted -= SpawnRandomEnemy;
        }
    }
}
