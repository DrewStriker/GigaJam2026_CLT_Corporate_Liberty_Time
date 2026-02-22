using System;
using Game.Scripts.BuffSystem;
using UnityEngine;

namespace Game.CollectableSystem
{
    public interface ICollectable<T> where T : Enum
    {
        public event Action<ICollectable<T>> OnCollected;
        public BuffDataSO BuffData { get; }
        Transform Transform { get; }

        T Type { get; }
        void Collect(ICollector<T> collector);
        void UnCollect();
        public void SetParent(Transform parent);
        public ParticleSystem CollectEffect { get; }
    }
}