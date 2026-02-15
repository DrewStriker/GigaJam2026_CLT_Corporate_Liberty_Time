using System.Collections.Generic;
using Game.Core.FactorySystem;
using UnityEngine;
using Zenject;

namespace Game.Core.SimplePool.SfxPool
{
    public class SfxPoolFacade
    {
        private readonly Dictionary<SfxType, AudioClip> clips;

        private readonly AudioSourcePool runtimePrefab;

        [Inject] private IPoolManager<PoolObject> pool;

        public SfxPoolFacade(CatalogRegistry<Catalog<SfxType, AudioClip>, SfxType, AudioClip> registry)
        {
            clips = registry.GenerateDictionary();
            runtimePrefab = CreateRuntimePrefab();
        }

        private AudioSourcePool CreateRuntimePrefab()
        {
            var go = new GameObject("audioPoolPrefab");
            var audio = go.AddComponent<AudioSource>();
            var poolObj = go.AddComponent<AudioSourcePool>();

            go.SetActive(false);
            return poolObj;
        }

        public void Play(SfxType type, Vector3 pos)
        {
            var obj = pool.Spawn(runtimePrefab, pos, Quaternion.identity);
            obj.Play(clips[type]);
        }
    }
}