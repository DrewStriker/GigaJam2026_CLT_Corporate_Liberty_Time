using UnityEngine;

namespace Game.RankSystem
{
    [CreateAssetMenu(fileName = "RankConfig", menuName = "Game/RankConfig")]
    public class RankConfig : ScriptableObject
    {
        [field:SerializeField] public float PointsToRank1 { get; private set; }
        [field:SerializeField] public float PointsToRank2 { get; private set; }
        [field:SerializeField] public float PointsToRank3 { get; private set; }
        [field:SerializeField] public float PointsToRank4 { get; private set; }
        [field:SerializeField] public float PointsToRank5 { get; private set; }
        [field:SerializeField] public float PointsToRank6 { get; private set; }
    }
}

