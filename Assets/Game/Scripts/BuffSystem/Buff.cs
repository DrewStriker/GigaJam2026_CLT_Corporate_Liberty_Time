namespace Game.Scripts.BuffSystem
{
    public readonly struct Buff
    {
        public Buff(float duration, int health, int damage, float moveSpeedMultiplier)
        {
            Duration = duration;
            Health = health;
            Damage = damage;
            MoveSpeedMultiplier = moveSpeedMultiplier;
        }

        public float Duration { get; }
        public int Health { get; }
        public int Damage { get; }
        public float MoveSpeedMultiplier { get; }
    }
}