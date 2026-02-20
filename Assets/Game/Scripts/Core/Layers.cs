using UnityEngine;

namespace Game.Core
{
    public static class Layers
    {
        public static readonly int Ground = LayerMask.GetMask("Ground");
        public static readonly int Character = LayerMask.GetMask("Character");
    }

    public static class Tags
    {
        public const string Player = "Player";
        public const string Enemy = "Enemy";
        public const string InteractableObject = "InteractableObject";

    }
}
