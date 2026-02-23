using UnityEngine;

namespace Game.Characters
{
    [CreateAssetMenu(menuName = "Game/Characters/NpcMeshCatalogSO", fileName = "NpcMeshCatalogSO")]
    public class NpcMeshCatalogSO : ScriptableObject
    {
        [field: SerializeField] public Mesh[] Heads { get; private set; }
        [field: SerializeField] public Mesh[] Bodies { get; private set; }

        public Mesh GetRandomHead()
        {
            return Heads[Random.Range(0, Heads.Length)];
        }

        public Mesh getRandomBody()
        {
            return Bodies[Random.Range(0, Bodies.Length)];
        }
    }
}