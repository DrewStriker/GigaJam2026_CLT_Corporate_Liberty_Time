using System;
using Zenject;

namespace Game.Core.SimplePool
{
    using System.Collections.Generic;
    using UnityEngine;

    namespace Game.Core.SimplePool
    {
        public class PoolManager : IPoolManager<PoolObject>
        {
            [SerializeField] private int initialPoolSize = 10;
            readonly Dictionary<PoolObject, Stack<PoolObject>> pools = new();
            readonly Dictionary<PoolObject, PoolObject> instanceToPrefab = new();
            private DiContainer container;

            public PoolManager(DiContainer container, int initialPoolSize)
            {
                this.container = container;
                this.initialPoolSize = initialPoolSize;
            }
            
            Stack<PoolObject> Initialize(PoolObject poolObject)
            {
                var stack = new Stack<PoolObject>(initialPoolSize);
                pools.Add(poolObject, stack);
                for (int i = 0; i < initialPoolSize; i++)
                    stack.Push(Create(poolObject));
                return stack;
            }

            PoolObject Create(PoolObject prefab)
            {
                var instance = container.InstantiatePrefabForComponent<PoolObject>(prefab);
                instance.gameObject.SetActive(false);

                instance.ReturnToPool += Despawn;
                instanceToPrefab.Add(instance, prefab);

                return instance;
            }

  

            public Stack<PoolObject> GetPool(PoolObject prefab)
            {
                if (!pools.ContainsKey(prefab))
                {
                    return Initialize(prefab);
                }
                return pools[prefab];
            }
            

            public T Spawn<T>(T prefab, Vector3 pos, Quaternion rot) where T : PoolObject
            {
                if(!pools.ContainsKey(prefab))
                {
                    Initialize(prefab);
                }
                var stack = pools[prefab];

                var obj = stack.Count > 0
                    ? stack.Pop()
                    : Create(prefab);

                obj.transform.SetPositionAndRotation(pos, rot);
                obj.gameObject.SetActive(true);

                return (T)obj;
            }

            private void Despawn(PoolObject instance)
            {
                var prefab = instanceToPrefab[instance];
                instance.gameObject.SetActive(false);
                pools[prefab].Push(instance);
            }
        }
    }
}