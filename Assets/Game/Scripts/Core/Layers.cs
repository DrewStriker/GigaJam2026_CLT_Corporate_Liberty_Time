using UnityEngine;

namespace Game.Core
{
    public static class Layers
    {
        public static readonly int Ground = LayerMask.GetMask("Ground");
        public static readonly int Character = LayerMask.GetMask("Character");
        public static readonly int Interactable = LayerMask.GetMask("InteractableObject");
    }

    public static class Tags
    {
        public const string Player = "Player";
        public const string Enemy = "Enemy";
        public const string Grabbable = "Grabbable";
        public const string Hittable = "Hittable";

    }
}
