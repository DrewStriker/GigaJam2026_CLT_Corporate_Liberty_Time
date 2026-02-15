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

        Dictionary<TKey, TValue> dictionary = new ();
       
        public Dictionary<TKey, TValue> GenerateDictionary()
        {
            if(dictionary.Count > 0) return dictionary;
            foreach (var catalog in catalogs)
            {
                dictionary.Add(catalog.Type, catalog.Prefab);
            }
            return dictionary;
        }
    }
}