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
        private Renderer[] Renderers;
        public Collider Collider { get; private set; }
        public event Action Death;


        protected virtual void Awake()
        {
            Collider = GetComponent<Collider>();
            Renderers = GetComponentsInChildren<Renderer>();
            Rigidbody = GetComponent<Rigidbody>();
            characterStats = new CharacterStats(config);
            AnimationController = new AnimationController(GetComponentInChildren<Animator>());
        }

        public Transform Transform => transform;
        public AnimationController AnimationController { get; private set; }
        public CharacterStats characterStats { get; private set; }
        public Rigidbody Rigidbody { get; private set; }

        public virtual void TakeDamage(DamageData damageData)
        {
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