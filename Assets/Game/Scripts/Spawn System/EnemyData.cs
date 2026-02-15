using UnityEngine;
using System;
namespace Game.SpawnSystem
{
    [Serializable]
    public class EnemyData 
    {
        [field:SerializeField] public EnemyType EnemyType { get; private set; }
        [field: Range(0f, 1f)]
        [field:SerializeField] public float SpawnChance { get; private set; }
    }
}

