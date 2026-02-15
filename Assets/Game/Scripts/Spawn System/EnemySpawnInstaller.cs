using System.Collections.Generic;
using Game.Characters;
using UnityEngine;
using Zenject;

namespace Game.SpawnSystem
{
    
    // public class SpawnSystemInstaller<TEnum, TController> : MonoInstaller where TEnum : System.Enum where TController : MonoBehaviour
    // {
    //     [SerializeField] private EnemyCatalogRegistry enemyCatalogRegistry;
    //     public override void InstallBindings()
    //     {
    //         Container.BindInstance(enemyCatalogRegistry.GenerateDictionary());
    //         Container.BindFactory<EnemyType, EnemyController, EnemyFactory>().FromMethod(CreateEnemy);
    //     }
    //
    //     private EnemyController CreateEnemy(DiContainer container, EnemyType type)
    //     {
    //         var dictionary = Container.Resolve<Dictionary<EnemyType, EnemyController>>();
    //         if (!dictionary.TryGetValue(type, out var enemy)) 
    //             Debug.LogError($"Enemy not Found by key {type}");
    //         return container.InstantiatePrefabForComponent<EnemyController>(enemy);
    //     }
    // }
    //
    
    public class EnemySpawnInstaller : MonoInstaller
    {
        [SerializeField] private EnemyCatalogRegistry enemyCatalogRegistry;
        public override void InstallBindings()
        {
            Container.BindInstance(enemyCatalogRegistry.GenerateDictionary());
            Container.BindFactory<EnemyType, EnemyController, PlaceholderFactory<EnemyType,EnemyController>>().FromMethod(CreateEnemy);
        }

        private EnemyController CreateEnemy(DiContainer container, EnemyType type)
        {
            var dictionary = Container.Resolve<Dictionary<EnemyType, EnemyController>>();
            if (!dictionary.TryGetValue(type, out var enemy)) 
                Debug.LogError($"Enemy not Found by key {type}");
            return container.InstantiatePrefabForComponent<EnemyController>(enemy);
        }
    }
}
