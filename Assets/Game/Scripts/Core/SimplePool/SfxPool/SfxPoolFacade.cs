using System.Collections.Generic;
using Game.Core.FactorySystem;
using UnityEngine;
using Zenject;

namespace Game.Core.SimplePool.SfxPool
{
    public class SfxPoolFacade
    {
        private readonly Dictionary<SfxType, AudioClip> clips;

        private readonly AudioPoolObject runtimePrefab;

        [Inject] private IPoolManager<PoolObject> pool;

        public SfxPoolFacade(CatalogRegistry<Catalog<SfxType, AudioClip>, SfxType, AudioClip> registry)
        {
            clips = registry.GenerateDictionary();
            runtimePrefab = CreateRuntimePrefab();
        }

        private AudioPoolObject CreateRuntimePrefab()
        {
            var go = new GameObject("audioPoolPrefab");
            var audio = go.AddComponent<AudioSource>();
            var poolObj = go.AddComponent<AudioPoolObject>();

            go.SetActive(false);
            return poolObj;
        }

        public void Play(SfxType type, Vector3 pos, float volume = 1, bool pitchVariation = false)
        {
            var obj = pool.Spawn(runtimePrefab, pos, Quaternion.identity);
            obj.Play(clips[type], volume, pitchVariation);
        }

        public void Play(AudioClip clip, Vector3 pos, float volume = 1, bool pitchVariation = false)
        {
            var obj = pool.Spawn(runtimePrefab, pos, Quaternion.identity);
            obj.Play(clip, volume, pitchVariation);
        }
    }
}