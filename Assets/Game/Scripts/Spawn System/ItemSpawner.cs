using Game.ItemSystem;
using UnityEngine;
using Zenject;

namespace Game.SpawnSystem
{
    public class ItemSpawner : MonoBehaviour
    {
        [SerializeField] private ItemType itemType;
        [Inject] private PlaceholderFactory<ItemType, ItemCollectable> itemFactory;
        void Start()
        {
            SpawnItem();
        }

        private void SpawnItem()
        {
            itemFactory.Create(itemType).transform.position = transform.position + transform.forward * 2f;
        }
    }
}
