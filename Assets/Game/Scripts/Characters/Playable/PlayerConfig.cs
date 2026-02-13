using UnityEngine;

namespace Game.Characters
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Game/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public float BaseVelocity { get; private set; }
        [field: SerializeField] public float JumpForce { get; private set; }
    }
}

