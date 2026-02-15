using Game.Characters;

namespace Game.SpawnSystem
{
    using System.Collections.Generic;
    using UnityEngine;
    using Zenject;
    
    public class EnemyFactoryInstaller 
        : FactoryInstaller<EnemyType, EnemyController>
    {
        [SerializeField] EnemyCatalogRegistry _registry;

        protected override Dictionary<EnemyType, EnemyController> BuildDictionary()
            => _registry.GenerateDictionary();
    }
}
