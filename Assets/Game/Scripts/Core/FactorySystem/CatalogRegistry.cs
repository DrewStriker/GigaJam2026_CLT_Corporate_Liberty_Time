using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Core.FactorySystem
{
    [Serializable]
    public class CatalogRegistry<TCatalog, TKey, TValue>
        where TCatalog : Catalog<TKey, TValue>
        where TValue : Object
        where TKey : Enum
    {
        [SerializeField] public List<TCatalog> catalogs = new();

        private Dictionary<TKey, TValue> dictionary = new();

        public Dictionary<TKey, TValue> GenerateDictionary()
        {
            if (dictionary.Count > 0) return dictionary;
            foreach (var catalog in catalogs) dictionary.Add(catalog.Type, catalog.Prefab);
            return dictionary;
        }

        public void AddCatalog(TCatalog catalog)
        {
            if (catalogs.Exists(c => c.Type.Equals(catalog.Type)))
            {
                Debug.LogWarning($"Catalog with key {catalog.Type} already exists. Overwriting value.");
                catalogs.RemoveAll(c => c.Type.Equals(catalog.Type));
                catalogs.Add(catalog);
                dictionary[catalog.Type] = catalog.Prefab;
                return;
            }

            catalogs.Add(catalog);
            dictionary.Add(catalog.Type, catalog.Prefab);
        }

    }
}