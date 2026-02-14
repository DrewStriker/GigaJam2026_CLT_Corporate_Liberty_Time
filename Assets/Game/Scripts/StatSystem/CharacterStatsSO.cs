using UnityEngine;

namespace Game.StatsSystem
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Game/PlayerConfig")]
    public class CharacterStatsSO : ScriptableObject
    {
        [field: SerializeField] public int Heatlh { get; private set; } = 1;
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float JumpForce { get; private set; }
    }
}

