using System;
using UnityEngine;

namespace Game.StatsSystem
{

    public sealed class CharacterStats
    {
        public  float MaxHealth { get; }
        public float CurrentHealth { get; private set; }
        public float MoveSpeed { get; private set; }
        public float JumpForce { get; private set; }
        
        public event Action<float> OnHealthChanged;
        public event Action<float> OnMoveSpeedChanged;

        public CharacterStats(CharacterStatsSO characterStatSo)
        {
            MaxHealth = characterStatSo.Heatlh;
            CurrentHealth = MaxHealth;
            MoveSpeed = characterStatSo.Speed;
            JumpForce = characterStatSo.JumpForce;
        }

        public void DecreaseHealth(float amount)
        {
            float newValue = CurrentHealth - amount;
            if (!Mathf.Approximately(newValue, CurrentHealth))
            {
                CurrentHealth = Mathf.Clamp( newValue,0, MaxHealth);
                OnHealthChanged?.Invoke(CurrentHealth);
            }
        }
        

       
        
        public void SetMoveSpeed(float value)
        {
            if (value > 0f)
            {
                MoveSpeed = value;
                OnMoveSpeedChanged?.Invoke(MoveSpeed);
            }
        }

        public void SetJumpForce(float value)
        {
            JumpForce = value;
        }
    }
}