namespace Game.Core.SimplePool
{
    public interface IPoolable
    {
        void OnSpawn();
        void OnDespawn();
    }
}