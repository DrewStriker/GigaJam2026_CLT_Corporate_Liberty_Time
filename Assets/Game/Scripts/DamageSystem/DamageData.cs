using UnityEngine;

public class DamageData
{
    public int Damage { get; private set; } = 1;
    public int ArmorDamage { get; private set; } = 1;
    public Vector3 AttackerPosition { get; private set; }
        
    public void Configure(int damage, Vector3 attackerPosition, int armorDamage = 1)
    {
        this.Damage = damage;
        this.AttackerPosition = attackerPosition;
        ArmorDamage = armorDamage;
    }
}