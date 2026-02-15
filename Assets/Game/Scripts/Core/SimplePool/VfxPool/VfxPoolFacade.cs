
namespace Game.Core.SimplePool.VfxPool
{
    using FactorySystem;
    using System.Collections.Generic;
    using UnityEngine;
    using Zenject;
    public class VfxPoolFacade
    {
        [Inject]
        private IPoolManager<PoolObject> pool;
        readonly Dictionary<VfxType, PoolObject> dictionary;
        
        public VfxPoolFacade(CatalogRegistry<Catalog<VfxType, PoolObject>, VfxType, PoolObject> registry)
        {
            dictionary = registry.GenerateDictionary();
            
        }

        public PoolObject Spawn(VfxType type, Vector3 pos, Quaternion rot = default)
        {
            var prefab = dictionary[type];
            return pool.Spawn(prefab, pos, rot);
        }
    
    }
}