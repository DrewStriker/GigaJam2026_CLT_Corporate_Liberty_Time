using System;
using Game.Characters;
using UnityEngine;

namespace Game.SpawnSystem
{
    [Serializable]
    public class EnemyCatalog 
    {
        [field:SerializeField] public EnemyController EnemyPrefab { get; private set; }
        [field:SerializeField] public EnemyType EnemyType { get; private set; }
    }
}
