using Game.CollectableSystem;

namespace Game.ItemSystem
{
    public class ItemCollectable : CollectableObject<ItemType>
    {
        public override void Collect(ICollector<ItemType> collector)
        {
            base.Collect(collector);
            gameObject.SetActive(false);
        }
    }
}