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
            var instance =  enemyFactory.Create(EnemyType.Basic);
            instance.gameObject.transform.position = transform.position;
        }
    }
}
