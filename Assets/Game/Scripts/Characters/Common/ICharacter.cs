using System;
using DamageSystem;
using Game.StatsSystem;
using UnityEngine;

namespace Game.Characters
{
    public interface ICharacter : IDamageable
    {
        public Transform Transform { get; }
        public AnimationController AnimationController { get; }
        public CharacterStats characterStats { get; }
        public Rigidbody Rigidbody { get; }
        public Collider Collider { get; }
        public event Action Death;
    }
}