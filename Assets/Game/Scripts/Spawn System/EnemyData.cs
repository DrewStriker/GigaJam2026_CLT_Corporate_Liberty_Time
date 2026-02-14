using UnityEngine;
using System;
namespace Game.SpawnSystem
{
    [Serializable]
    public class EnemyData 
    {
        [field:SerializeField] public EnemyCatalog EnemyCatalog { get; private set; }
        [Range(0f, 1f)]
        [field:SerializeField] public float SpawnPercentage { get; private set; }
    }
}

