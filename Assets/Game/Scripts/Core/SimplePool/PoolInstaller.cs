    using Zenject;
    using Game.Core.SimplePool.Game.Core.SimplePool;
    using UnityEngine;


    namespace Game.Core.SimplePool
{

    public class PoolInstaller : MonoInstaller
    {
        [SerializeField] PoolManager poolManager;

        public override void InstallBindings()
        {
            Container
                .Bind<IPoolManager<PoolObject>>()
                .FromInstance(poolManager)
                .AsSingle();
        }
    }
}