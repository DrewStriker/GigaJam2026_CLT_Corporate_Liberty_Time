using System;
using UnityEngine;

namespace Game.Core.SimplePool
{
    public  class PoolObject : MonoBehaviour, IPoolable
    {
        public event Action<PoolObject> ReturnToPool;

        protected virtual void OnDisable()
        {
            ReturnToPool?.Invoke(this);
        }

        public virtual void OnSpawn() {}

        public virtual void OnDespawn() {}
    }
}