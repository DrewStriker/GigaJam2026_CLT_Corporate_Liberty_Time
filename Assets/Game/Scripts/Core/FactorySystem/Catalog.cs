using System;
using UnityEngine;

namespace Game.Core.FactorySystem
{
    [Serializable]
    public class Catalog<TEnum, TPrefab> where TEnum : Enum where TPrefab : MonoBehaviour
    {
        [field:SerializeField] public TPrefab Prefab { get; private set; }
        [field:SerializeField] public TEnum Type { get; private set; }
    }
}