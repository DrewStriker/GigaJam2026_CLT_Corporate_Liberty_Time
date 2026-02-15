using System;
using System.Collections.Generic;
using Game.Core.FactorySystem;
using UnityEngine;
using Zenject;

namespace Game.Core
{
    public abstract class FactoryInstaller<TKey, TContract> : MonoInstaller
        where TKey : Enum
         where
        TContract : MonoBehaviour
    {
        [SerializeField] 
        private CatalogRegistry<Catalog<TKey, TContract>, TKey, TContract> registry;

        protected Dictionary<TKey, TContract> BuildDictionary() => registry.GenerateDictionary();
        public override void InstallBindings()
        {
            Container.BindInstance(BuildDictionary()).AsSingle();
            Container.BindFactory<TKey, TContract, PlaceholderFactory<TKey, TContract>>().FromMethod(CreateEnemy);
        }


        private TContract CreateEnemy(DiContainer container, TKey type)
        {
            var dictionary = Container.Resolve<Dictionary<TKey, TContract>>();
            if (!dictionary.TryGetValue(type, out var enemy))
                Debug.LogError($" key not found {type}");
            return container.InstantiatePrefabForComponent<TContract>(enemy);
        }

    }




}