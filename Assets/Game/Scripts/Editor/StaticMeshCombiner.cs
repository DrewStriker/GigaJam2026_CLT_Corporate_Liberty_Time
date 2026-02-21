namespace EditorTools
{
    using UnityEngine;
    using UnityEditor;
    using System.Linq;
    using System.Collections.Generic;

    public static class StaticMeshCombiner
    {
        [MenuItem("Tools/Combine Selected Static Meshes")]
        private static void Combine()
        {
            var selected = Selection.gameObjects;
            if (selected.Length == 0) return;

            var filters = selected
                .SelectMany(go => go.GetComponentsInChildren<MeshFilter>())
                .Where(f => f.GetComponent<MeshRenderer>()?.enabled == true)
                .ToArray();

            if (filters.Length == 0) return;

            var groups = filters.GroupBy(f =>
                f.GetComponent<MeshRenderer>().sharedMaterial);

            var parent = new GameObject("Combined_StaticMesh");
            parent.isStatic = true;

            foreach (var group in groups)
            {
                var combines = new List<CombineInstance>();

                foreach (var mf in group)
                    combines.Add(new CombineInstance
                    {
                        mesh = mf.sharedMesh,
                        transform = mf.transform.localToWorldMatrix
                    });

                var mesh = new Mesh();
                mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
                mesh.CombineMeshes(combines.ToArray(), true, true);

                var child = new GameObject(group.Key.name);
                child.transform.SetParent(parent.transform);

                var mfNew = child.AddComponent<MeshFilter>();
                var mrNew = child.AddComponent<MeshRenderer>();
                var mcNew = child.AddComponent<MeshCollider>();

                mfNew.sharedMesh = mesh;
                mrNew.sharedMaterial = group.Key;
                mcNew.sharedMesh = mesh;
                mcNew.convex = false;
            }

            // desativa originais
            foreach (var f in filters)
            {
                f.GetComponent<MeshRenderer>().enabled = false;
                var col = f.GetComponent<Collider>();
                if (col) col.enabled = false;
            }
        }
    }
}