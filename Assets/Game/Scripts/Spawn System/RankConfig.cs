using UnityEngine;

namespace Game.SpawnSystem
{
    [CreateAssetMenu(fileName = "RankConfig", menuName = "Game/Spawn/RankConfig")]
    public class RankConfig : ScriptableObject
    {
        [field:SerializeField] public EnemyData[] Enemies { get; private set; }
        [field:SerializeField] public float SpawnInterval { get; private set; }
    }
}