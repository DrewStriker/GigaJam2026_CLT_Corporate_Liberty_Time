using System.Collections.Generic;
using UnityEngine;

namespace Game.Core.SimplePool
{
public class SimpleObjectPool : MonoBehaviour
{
    [SerializeField] PoolObject  prefab;
    [SerializeField] int initialSize = 10;

    readonly Stack<PoolObject> pool = new();

    void Awake()
    {
        Prewarm();
    }

    void Prewarm()
    {
        for (int i = 0; i < initialSize; i++)
        {
            pool.Push(Create());
        }
    }

    PoolObject Create()
    {
        var instance = Instantiate(prefab, transform);
        instance.gameObject.SetActive(false);
        instance.ReturnToPool += () => Despawn(instance);
        return instance;
    }

    public PoolObject Spawn(Vector3 position, Quaternion rotation)
    {
        var go = pool.Count > 0 ? pool.Pop() : Create();

        go.transform.SetPositionAndRotation(position, rotation);
        go.gameObject.SetActive(true);

        return go;
    }

    public void Despawn(PoolObject instance)
    {
        pool.Push(instance);
    }
	
}
}
