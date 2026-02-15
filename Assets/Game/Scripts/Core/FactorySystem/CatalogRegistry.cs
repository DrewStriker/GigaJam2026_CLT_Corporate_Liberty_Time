using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core.FactorySystem
{
    [Serializable]
    public class CatalogRegistry<TCatalog, TKey, TValue> 
        where TCatalog : Catalog<TKey, TValue> 
        where TValue : MonoBehaviour
        where TKey : Enum
    {
        [SerializeField] public List<TCatalog> catalogs;

        public Dictionary<TKey, TValue> GenerateDictionary()
        {
            Dictionary<TKey, TValue> dictionary = new ();
            foreach (var catalog in catalogs)
            {
                dictionary.Add(catalog.Type, catalog.Prefab);
            }
            return dictionary;
        }
    }
}