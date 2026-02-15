using System;
using UnityEngine;

namespace Game.CollectableSystem
{
    public interface ICollectable<T> where T : Enum
    {
        T Type { get; }
        void Collect(ICollector<T> collector);
        Transform Transform { get; }

 
    }
}