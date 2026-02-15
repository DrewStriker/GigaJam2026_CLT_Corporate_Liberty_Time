using System;
using Game.Core;
using UnityEngine;

namespace Game.CollectableSystem
{
    public class CollectableObject<T>: MonoBehaviour, ICollectable<T> where T : Enum
    {
        [SerializeField] private T type;
        public Transform Transform => transform;

        public T Type => Type;
        public void Collect(ICollector<T> collector)
        {
            collector.Attach(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(Tags.Player)) return;
            if (!other.TryGetComponent<ICollector<T>>(out var collector)) return;
            Collect(collector);
        }


    }



}