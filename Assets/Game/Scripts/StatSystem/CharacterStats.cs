using System;
using UnityEngine;

namespace Game.StatsSystem
{
    public class CharacterStats
    {
        public CharacterStats(CharacterStatsSO characterStatSo)
        {
            MaxHealth = characterStatSo.Heatlh;
            CurrentHealth = MaxHealth;
            MoveSpeed = characterStatSo.Speed;
            JumpForce = characterStatSo.JumpForce;
        }

        public int MaxHealth { get; }
        public int Damage { get; private set; } = 1;
        public int CurrentHealth { get; private set; }
        public float MoveSpeed { get; private set; }
        public float JumpForce { get; private set; }

        public event Action<int> HealthChanged;
        public event Action<float> MoveSpeedChanged;
        public event Action<int> DamageChanged;


        public void IncreaseHealth(int amount)
        {
            var newValue = CurrentHealth + amount;
            if (!Mathf.Approximately(newValue, CurrentHealth))
            {
                CurrentHealth = Mathf.Clamp(newValue, 0, MaxHealth);
                HealthChanged?.Invoke(CurrentHealth);
            }
        }

        public void IncreaseMoveSpeed(float amount)
        {
            if (!Mathf.Approximately(amount, 0f))
            {
                MoveSpeed += amount;
                MoveSpeedChanged?.Invoke(MoveSpeed);
            }
        }

        public void IncreaseDamage(int amount)
        {
            if (!Mathf.Approximately(amount, 0f))
            {
                Damage += amount;
                DamageChanged?.Invoke(Damage);
            }
        }

        public void SetJumpForce(float value)
        {
            JumpForce = value;
        }
    }
}