using Cysharp.Threading.Tasks;
using DamageSystem;
using Game.Core;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Game.Characters
{
    public interface IEnemyCharacter : ICharacter
    {
        NavMeshAgent NavMeshAgent { get; }
        DamageData damageData { get; }
    }

    public class EnemyController : CharacterBase, IEnemyCharacter
    {
        [Inject] protected PlayerController playerTarget;

        public NavMeshAgent NavMeshAgent { get; private set; }
        public DamageData damageData { get; private set; } = new();
        protected BehaviorGraphAgent behaviorAgent;

        protected override void Awake()
        {
            base.Awake();
            NavMeshAgent = GetComponent<NavMeshAgent>();
            behaviorAgent = GetComponent<BehaviorGraphAgent>();
        }

        protected virtual void Start()
        {
            behaviorAgent.SetVariableValue("PlayerController", playerTarget);
            behaviorAgent.SetVariableValue("EnemyController", GetComponent<EnemyController>());
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag(Tags.Player))
                if (other.gameObject.TryGetComponent(out IDamageable damageable))
                {
                    damageData.Configure(1, transform.position);
                    damageable.TakeDamage(damageData);
                }
        }


        public override void TakeDamage(DamageData damageData)
        {
            base.TakeDamage(damageData);
            if (characterStats.CurrentHealth <= 0)
                Die();
        }


        protected async void Die()
        {
            behaviorAgent.Restart();
            behaviorAgent.enabled = false;
            NavMeshAgent.enabled = false;
            Rigidbody.isKinematic = true;
            Collider.isTrigger = true;
            AnimationController.Play(Animation.Death, 0.1f, 2);
            await UniTask.Delay(2000);
            gameObject.SetActive(false);
        }
    }
}