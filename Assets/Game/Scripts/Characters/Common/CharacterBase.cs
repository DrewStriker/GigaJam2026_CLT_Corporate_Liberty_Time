using DamageSystem;
using DG.Tweening;
using Game.Core;
using Game.StatsSystem;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace Game.Characters
{
    public abstract class CharacterBase :  MonoBehaviour, ICharacter
    {
        private Collider Collider;
        private Renderer[] Renderers;
        [SerializeField] private CharacterStatsSO config;
        public AnimationController AnimationController { get; private set; }
        public CharacterStats characterStats { get; private set; }
        public Rigidbody Rigidbody { get; private set; }


        protected virtual void Awake()
        {
            Collider = GetComponent<Collider>();
            Renderers = GetComponentsInChildren<Renderer>();
            Rigidbody = GetComponent<Rigidbody>();
            characterStats = new(config);
            AnimationController = new AnimationController(GetComponentInChildren<Animator>());
            
        }

        
        
        public virtual  void TakeDamage(DamageData damageData)
        {
            characterStats.DecreaseHealth(damageData.Damage);
            HurtBlink();
            if (characterStats.CurrentHealth <= 0)
            {
                Die();
            }
        }



        private void HurtBlink()
        {
            for(int i = 0; i < Renderers.Length; i++)
            {
                Renderers[i].DoColor(ShaderProperties.BaseColor, Color.red, 0, Ease.Linear, 0, false);
                Renderers[i].DoColor(ShaderProperties.BaseColor, Color.white, 0.3f, Ease.Linear, 0.05f, false);
            }

        }

        protected virtual void Die()
        {
            Collider.enabled = false;
            Rigidbody.isKinematic = true;
            Rigidbody.linearVelocity = Vector3.zero;
           
        }





    }
}