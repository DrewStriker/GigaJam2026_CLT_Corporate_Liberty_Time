using System;
using UnityEngine;

namespace Game.Core.SimplePool
{
    public class PoolObject : MonoBehaviour
    {
        public event Action ReturnToPool;

        protected virtual void OnDisable()
        {
            ReturnToPool?.Invoke();
        }
    }
}