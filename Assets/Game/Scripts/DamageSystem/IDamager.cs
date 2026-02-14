using UnityEditor.UIElements;
using UnityEngine;

namespace DamageSystem
{
    public interface IDamager
    {
        void DoDamage(int damage);
    }
}