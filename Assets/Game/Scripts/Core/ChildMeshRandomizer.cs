using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class ChildMeshRandomizer : MonoBehaviour
{
    [SerializeField, Range(0,100)] private int hideObjectChance = 10;
    [SerializeField] private GameObject[] prefabs;
    private MeshFilter[] childrenMeshFilters;
    private Random random =  new Random();
    private List<Mesh> meshes = new List<Mesh>();
    
    private void Awake()
    {
        childrenMeshFilters = GetComponentsInChildren<MeshFilter>();
       GetMeshFromPrefbas();
    }

    private void Start()
    {
        RandomizeMeshes();
    }


    private void RandomizeMeshes()
    {
        
        foreach (var meshFilter in childrenMeshFilters)
        {
            if(random.Next(100) >  hideObjectChance)
            {
                int index = random.Next(meshes.Count);
                meshFilter.mesh = meshes[index];
            }
            else
            {
                meshFilter.gameObject.SetActive(false);
            }
            
        }
    }

    private void GetMeshFromPrefbas()
    {
        foreach (var prefab in prefabs)
        {
            meshes.Add(prefab.GetComponent<MeshFilter>().sharedMesh);
            
        }
    }
}
