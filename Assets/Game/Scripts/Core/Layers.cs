using UnityEngine;

namespace Game.Core
{
    public static class Layers
    {
        public static readonly int Ground = LayerMask.GetMask("Ground");
        public static readonly int Character = LayerMask.GetMask("Character");
    }
}
