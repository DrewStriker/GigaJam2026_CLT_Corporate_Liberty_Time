using System;
using System.Collections.Generic;
using Game.Characters;
using UnityEngine;
namespace Game.SpawnSystem
{
    [Serializable]
    public class EnemyCatalogRegistry
    {
        [SerializeField] public List<EnemyCatalog> EnemyCatalogs;

        public Dictionary<EnemyType, EnemyController> GenerateDictionary()
        {
            Dictionary<EnemyType, EnemyController> dictionary = new ();
            foreach (var catalog in EnemyCatalogs)
            {
                dictionary.Add(catalog.EnemyType, catalog.EnemyPrefab);
            }
            return dictionary;
        }
    }
}
