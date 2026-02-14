using System;
using System.Collections.Generic;
using Game.Characters;
using UnityEngine;
namespace Game.SpawnSystem
{
    [Serializable]
    public class EnemyCatalogRegistry
    {
        public Dictionary<EnemyType, EnemyController> dictionary = new Dictionary<EnemyType, EnemyController>();
        [field:SerializeField] public List<EnemyCatalog> EnemyCatalogs = new List<EnemyCatalog>();

        public Dictionary<EnemyType, EnemyController> GenerateDictionary()
        {
            dictionary.Clear();
            foreach (var catalog in EnemyCatalogs)
            {
                dictionary.Add(catalog.EnemyType, catalog.EnemyPrefab);
            }
            return dictionary;
        }
    }
}
