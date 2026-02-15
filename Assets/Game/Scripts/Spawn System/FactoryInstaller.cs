using System.Collections.Generic;
using Game.Characters;
using UnityEngine;
using Zenject;

namespace Game.SpawnSystem
{
    public abstract class FactoryInstaller<TKey, TContract> : MonoInstaller
        where TContract : Component
    {
        protected abstract Dictionary<TKey, TContract> BuildDictionary();

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