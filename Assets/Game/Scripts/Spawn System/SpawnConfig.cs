using UnityEngine;

namespace Game.SpawnSystem
{
    [CreateAssetMenu(fileName = "SpawnConfig", menuName = "Game/Spawn/SpawnConfig")]
    public class SpawnConfig : ScriptableObject
    {
        [field: SerializeField] public RankConfig Rank0Config { get; private set; }
        [field: SerializeField] public RankConfig Rank1Config { get; private set; }
        [field: SerializeField] public RankConfig Rank2Config { get; private set; }
        [field: SerializeField] public RankConfig Rank3Config { get; private set; }
        [field: SerializeField] public RankConfig Rank4Config { get; private set; }
        [field: SerializeField] public RankConfig Rank5Config { get; private set; }
        [field: SerializeField] public RankConfig Rank6Config { get; private set; }
        
    }
}