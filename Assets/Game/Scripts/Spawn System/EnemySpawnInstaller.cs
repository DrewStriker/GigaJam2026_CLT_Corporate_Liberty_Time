using System.Collections.Generic;
using Game.Characters;
using UnityEngine;
using Zenject;

namespace Game.SpawnSystem
{
    public class EnemySpawnInstaller : MonoInstaller
    {
        [SerializeField] private EnemyCatalogRegistry enemyCatalogRegistry;
        public override void InstallBindings()
        {
            Container.BindInstance(enemyCatalogRegistry.GenerateDictionary());
            Container.BindFactory<EnemyType, EnemyController, EnemyFactory>().FromMethod(CreateEnemy);
        }

        private EnemyController CreateEnemy(DiContainer container, EnemyType type)
        {
            var dictionary = Container.Resolve<Dictionary<EnemyType, EnemyController>>();
            if (dictionary.TryGetValue(type, out var enemy)) Debug.Log("Enemy Found");
            return Container.InstantiatePrefabForComponent<EnemyController>(dictionary[type]);
        }
    }
}
