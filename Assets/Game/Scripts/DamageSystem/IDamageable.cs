namespace DamageSystem
{
    public interface IDamageable
    {
        bool IsDamageActive { get; }
        void TakeDamage(DamageData damageData);
    }
}