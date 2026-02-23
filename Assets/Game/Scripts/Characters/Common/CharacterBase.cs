using System;
using DG.Tweening;
using Game.Core;
using Game.StatsSystem;
using UnityEngine;

namespace Game.Characters
{
    public abstract class CharacterBase : MonoBehaviour, ICharacter
    {
        [SerializeField] private CharacterStatsSO config;
        public Renderer[] Renderers { get; private set; }
        public Collider Collider { get; private set; }
        public event Action Death;
        public Transform Transform => transform;
        public AnimationController AnimationController { get; private set; }
        public CharacterStats characterStats { get; private set; }
        public Rigidbody Rigidbody { get; private set; }

        public bool IsDamageActive { get; protected set; } = true;

        protected virtual void Awake()
        {
            Collider = GetComponent<Collider>();
            Renderers = GetComponentsInChildren<Renderer>();
            Rigidbody = GetComponent<Rigidbody>();
            characterStats = new CharacterStats(config);
            AnimationController = new AnimationController(GetComponentInChildren<Animator>());
        }


        public virtual void TakeDamage(DamageData damageData)
        {
            if (!IsDamageActive) return;
            if (!characterStats.DecreaseArmor(damageData.ArmorDamage)) characterStats.DecreaseHealth(damageData.Damage);
            HurtBlink();
            if (characterStats.CurrentHealth == 0) Death?.Invoke();
        }


        private void HurtBlink()
        {
            for (var i = 0; i < Renderers.Length; i++)
            {
                Renderers[i].DoColor(ShaderProperties.BaseColor, Color.red, 0);
                Renderers[i].DoColor(ShaderProperties.BaseColor, Color.white, 0.3f, Ease.Linear, 0.05f);
            }
        }
    }
}