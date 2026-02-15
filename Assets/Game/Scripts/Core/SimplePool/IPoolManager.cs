using System.Collections.Generic;
using UnityEngine;

namespace Game.Core.SimplePool
{
    public interface IPoolManager<T> where T : PoolObject
    {
        public Stack<PoolObject> GetPool(PoolObject prefab);
        public T Spawn<T>(T prefab, Vector3 position, Quaternion rotation) where T : PoolObject;
    }
}