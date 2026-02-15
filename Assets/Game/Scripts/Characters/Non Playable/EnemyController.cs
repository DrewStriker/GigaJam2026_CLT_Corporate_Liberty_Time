using System;
using Cysharp.Threading.Tasks;
using DamageSystem;
using Game.Core;
using Game.StatsSystem;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Game.Characters
{
    public class EnemyController : CharacterBase
    {
        private Transform playerTransform => playableCharacter.Transform;
        [Inject]private IPlayableCharacter playableCharacter;
        private NavMeshAgent navMeshAgent;
        
        public event Action OnAttackRange;
        protected override void Awake()
        {
            base.Awake();
            navMeshAgent = GetComponent<NavMeshAgent>();
        }
        

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            navMeshAgent.speed = characterStats.MoveSpeed;
            AnimationController.Play(Animation.Run);
        }

        private void Update()
        {
            ChasePlayer();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag(Tags.Player))
            {
                if(other.gameObject.TryGetComponent(out IDamageable damageable))
                {
                    damageable.TakeDamage(1);
                }
            }
        }

        private void ChasePlayer()
        {
            if (Vector3.Distance(transform.position, playerTransform.position) > 1)
            {
                navMeshAgent.SetDestination(playerTransform.position);
                navMeshAgent.isStopped = false;
                return;
            }

            Stop();
        }

        private void Stop()
        {
            navMeshAgent.isStopped = true;
            OnAttackRange?.Invoke();
        }
        

        protected  override  async void Die()
        {
            base.Die();
            AnimationController.Play(Animation.Death, 0.1f,2);
            await UniTask.Delay(2000);
            gameObject.SetActive(false);
        }
    }
}

