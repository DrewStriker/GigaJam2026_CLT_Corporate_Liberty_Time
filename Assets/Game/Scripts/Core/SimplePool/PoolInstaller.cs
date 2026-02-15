    using Game.Core.FactorySystem;
    using Zenject;
    using Game.Core.SimplePool.Game.Core.SimplePool;
    using Game.Core.SimplePool.VfxPool;
    using UnityEngine;


    namespace Game.Core.SimplePool
{

    public class PoolInstaller : MonoInstaller
    {
        [SerializeField] private int initialPoolSize = 10;
        
        [Space(20)]
        [SerializeField] 
        private CatalogRegistry<Catalog<VfxType, PoolObject>,
            VfxType, PoolObject> vfxRegistry;

        public override void InstallBindings()
        {
            Container
                .Bind<IPoolManager<PoolObject>>().To<PoolManager>().AsSingle()
                .WithArguments(Container, initialPoolSize);
            Container.Bind<VfxPoolFacade>().AsSingle().WithArguments(vfxRegistry);
        }
    }
}