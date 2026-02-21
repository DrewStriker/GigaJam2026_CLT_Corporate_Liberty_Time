using Game.Characters;
using Game.Core;
using Game.RankSystem;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Timer = Game.TimeSystem.Timer;

namespace Game.SpawnSystem
{
    public enum SpawnSide { Left, Right, Top, Bottom }
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private SpawnConfig spawnConfig;
        [Inject] private PlaceholderFactory<EnemyType, EnemyController> enemyFactory;
        [Inject] private RankManager rankManager;

        private Timer timer;
        private RankConfig[] rankConfigs;
        private int currentRank => rankManager.CurrentRank;
        private Camera camera;
        private int iterations;

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

            timer = new Timer(TimeSystem.TimerMode.CountDown);
            timer.OnTimerCompleted += SpawnRandomEnemy;
            camera = Camera.main;
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
                    enemyFactory.Create(enemy.EnemyType).transform.position = SortRandomPosition();
                    timer.Start(rankConfigs[currentRank].SpawnInterval);
                    return;
                }
            }
        }

        private Vector3 SortRandomPosition()
        {
            Ray ray = camera.ViewportPointToRay(GetRandomViewportPoint());
            iterations++;
            if (Physics.Raycast(ray, out RaycastHit hit, 100f, Layers.Ground))
            {
                if (NavMesh.SamplePosition(hit.point, out NavMeshHit navMeshHit, 0.2f, NavMesh.AllAreas))
                {
                    Debug.Log($"Iterations :{iterations}");
                    iterations = 0;
                    return navMeshHit.position;
                }

                else
                {
                    return SortRandomPosition();
                }
            }

            else
            {
                return SortRandomPosition();
            }

        }


        private Vector3 GetRandomViewportPoint()
        {
            float randomOffset = Random.Range(0f, 1f);
            SpawnSide randomSide = (SpawnSide)Random.Range(0, System.Enum.GetValues(typeof(SpawnSide)).Length);
            Debug.Log($"Random side: {randomSide}");
            return randomSide switch
            {
                SpawnSide.Left => new Vector3(0f - spawnConfig.OutsideCameraSpawnOffset, randomOffset, 0f),
                SpawnSide.Right => new Vector3(1f + spawnConfig.OutsideCameraSpawnOffset, randomOffset, 0f),
                SpawnSide.Top => new Vector3(randomOffset, 1f + spawnConfig.OutsideCameraSpawnOffset, 0f),
                SpawnSide.Bottom => new Vector3(randomOffset, 0f - spawnConfig.OutsideCameraSpawnOffset, 0f),
                _ => Vector3.zero
            };
        }

        private void OnDisable()
        {
            timer.OnTimerCompleted -= SpawnRandomEnemy;
        }
    }
}
