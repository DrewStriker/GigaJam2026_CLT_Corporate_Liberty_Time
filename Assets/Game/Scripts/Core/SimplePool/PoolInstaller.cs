using Game.Core.FactorySystem;
using Game.Core.SimplePool.Game.Core.SimplePool;
using Game.Core.SimplePool.SfxPool;
using Game.Core.SimplePool.VfxPool;
using UnityEngine;
using Zenject;

namespace Game.Core.SimplePool
{
    public class PoolInstaller : MonoInstaller
    {
        [SerializeField] private int initialPoolSize = 10;

        [Space(20)] [SerializeField] private CatalogRegistry<Catalog<VfxType, PoolObject>,
            VfxType, PoolObject> visualFxRegistry;

        [SerializeField] private CatalogRegistry<Catalog<SfxType, AudioClip>,
            SfxType, AudioClip> soundFxRegistry;

        public override void InstallBindings()
        {
            Container
                .Bind<IPoolManager<PoolObject>>().To<PoolManager>().AsSingle()
                .WithArguments(Container, initialPoolSize);
            Container.Bind<VfxPoolFacade>().AsSingle().WithArguments(visualFxRegistry);

            Container.Bind<SfxPoolFacade>().AsSingle().WithArguments(soundFxRegistry);
        }
    }
}