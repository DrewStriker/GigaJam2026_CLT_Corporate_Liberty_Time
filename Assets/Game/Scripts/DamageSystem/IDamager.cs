using UnityEditor.UIElements;
using UnityEngine;

namespace DamageSystem
{
    public interface IDamager
    {
        public static readonly LayerMask TargetLayer = LayerMask.GetMask("Character");

        void DoDamage(int damage);


    }
}