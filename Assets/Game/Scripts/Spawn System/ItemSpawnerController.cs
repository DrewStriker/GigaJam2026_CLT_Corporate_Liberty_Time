using Game.CollectableSystem;
using Game.ItemSystem;
using UnityEngine;
using Zenject;

namespace Game.SpawnSystem
{
    public class ItemSpawnerController : MonoBehaviour
    {
        [SerializeField] private ChildMeshRandomizer childMeshRandomizer;
        [Inject] private PlaceholderFactory<ItemType, ItemCollectable> itemFactory;

        private void Start()
        {
            childMeshRandomizer.Initialize();
            foreach (var spawner in childMeshRandomizer.ItemSpawners)
            {
               spawner.OnTimerCompleted += Spawn;
            }
        }

        private void Spawn(ItemSpawner itemSpawner)
        {
            var spawnedItem = itemFactory.Create(itemSpawner.ItemType);
            spawnedItem.transform.position = itemSpawner.transform.position +  itemSpawner.transform.forward * 4f;
            spawnedItem.OnCollected += (ICollectable<ItemType> item) => OnItemCollected(item, itemSpawner);
            Debug.Log($"Spawned Item: {spawnedItem.name}");
        }

        private void OnItemCollected(ICollectable<ItemType> collectable, ItemSpawner itemSpawner)
        {
            collectable.OnCollected -= (ICollectable<ItemType> item) => OnItemCollected(item, itemSpawner);
            itemSpawner.ResetSpawnCooldown();
        }

        private void OnDestroy()
        {
            foreach (var spawner in childMeshRandomizer.ItemSpawners)
            {
                spawner.OnTimerCompleted -= Spawn;
            }
        }
    }
}
