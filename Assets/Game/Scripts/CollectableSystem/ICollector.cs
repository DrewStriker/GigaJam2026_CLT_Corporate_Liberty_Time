using System;

namespace Game.CollectableSystem
{
    public interface ICollector<T> where T : Enum
    {
        void Collect(ICollectable<T> item);
    }
}