using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
using Game.Core;

namespace Game.SpawnSystem
{
    public class ChildMeshRandomizer : MonoBehaviour
    {
        [SerializeField, Range(0, 100)] private int hideObjectChance = 10;
        [SerializeField] private GameObject[] prefabs;
        private MeshFilter[] childrenMeshFilters;
        private Random random = new Random();
        private List<Mesh> meshes = new List<Mesh>();
        private Dictionary<Mesh, ItemSpawner> itemSpawners;

        private void Awake()
        {
            childrenMeshFilters = GetComponentsInChildren<MeshFilter>();
            GetMeshFromPrefabs();
            RandomizeMeshes();
        }

        private void RandomizeMeshes()
        {

            foreach (var meshFilter in childrenMeshFilters)
            {
                if (random.Next(100) > hideObjectChance)
                {
                    ConfigureMesh(meshFilter);
                }
                else
                {
                    meshFilter.gameObject.SetActive(false);
                }
            }
        }

        private void GetMeshFromPrefabs()
        {
            itemSpawners = new Dictionary<Mesh, ItemSpawner>();
            foreach (var prefab in prefabs)
            {
                Mesh mesh = prefab.GetComponent<MeshFilter>().sharedMesh;
                if (prefab.TryGetComponent(out ItemSpawner spawner))
                {
                    itemSpawners.Add(mesh, spawner);
                }
                meshes.Add(mesh);
            }
        }

        private void ConfigureMesh(MeshFilter meshFilter)
        {
            int index = random.Next(meshes.Count);
            meshFilter.mesh = meshes[index];

            if (itemSpawners.ContainsKey(meshes[index]))
            {
                ComponentExtensions.CopyComponent(itemSpawners[meshes[index]], meshFilter.gameObject);
            }

        }
    }
}
