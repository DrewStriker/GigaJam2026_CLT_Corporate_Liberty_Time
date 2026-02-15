using DG.Tweening;
using Game.Core;
using Game.StatsSystem;
using UnityEngine;

namespace Game.Characters
{
    public abstract class CharacterBase :  MonoBehaviour, ICharacter
    {
        private Renderer[] Renderers;
        [SerializeField] private CharacterStatsSO config;
        public AnimationController AnimationController { get; private set; }
        public CharacterStats characterStats { get; private set; }
        public Rigidbody Rigidbody { get; private set; }


        protected virtual void Awake()
        {           
            Renderers = GetComponentsInChildren<Renderer>();
            Rigidbody = GetComponent<Rigidbody>();
            characterStats = new(config);
            AnimationController = new AnimationController(GetComponentInChildren<Animator>());
            
        }

        
        
        public void TakeDamage(int damage)
        {
            Debug.Log("damaged: "+gameObject.name);
            characterStats.DecreaseHealth(damage);
            if (characterStats.CurrentHealth <= 0)
            {
                Die();
            }
            else
            {
                HurtBlink();
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
        
        protected abstract void Die();

        
    }
}