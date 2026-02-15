using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Core.FactorySystem
{
    [Serializable]
    public class Catalog<TEnum, TPrefab> where TEnum : Enum where TPrefab : Object
    {
        [field: SerializeField] public TEnum Type { get; private set; }
        [field: SerializeField] public TPrefab Prefab { get; private set; }
    }
}