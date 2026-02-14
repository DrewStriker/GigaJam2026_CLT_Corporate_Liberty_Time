using UnityEngine;
using Zenject;

namespace Game.SpawnSystem
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private SpawnConfig spawnConfig;

        [Inject]private EnemyFactory enemyFactory;

        private void Start()
        {
            enemyFactory.Create(EnemyType.Basic);
        }
    }
}
