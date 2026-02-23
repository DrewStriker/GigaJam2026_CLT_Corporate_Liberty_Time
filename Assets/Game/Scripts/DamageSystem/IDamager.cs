using UnityEngine;

namespace DamageSystem
{
    public interface IDamager
    {
        Bounds Bounds { get; set; }
        void DoDamage(int damage);
    }
}