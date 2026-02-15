using System;

namespace Game.CollectableSystem
{
    public interface ICollector<T> where T : Enum
    {
        void Attach(ICollectable<T> item);
    }
}