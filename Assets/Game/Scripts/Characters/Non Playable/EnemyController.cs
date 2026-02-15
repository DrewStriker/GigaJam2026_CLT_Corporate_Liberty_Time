using System;
using DamageSystem;
using Game.Core;
using Game.StatsSystem;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Game.Characters
{
    public class EnemyController : MonoBehaviour, ICharacter
    {
        [SerializeField] private CharacterStatsSO config;
        private Transform playerTransform => playableCharacter.Transform;
        [Inject]private IPlayableCharacter playableCharacter;
        private NavMeshAgent navMeshAgent;

        public AnimationController AnimationController { get; private set; }
        public CharacterStats characterStats { get; private set; }
        public Rigidbody Rigidbody { get; private set; }

        public event Action OnAttackRange;
        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            characterStats = new CharacterStats(config);
            AnimationController = new AnimationController(GetComponentInChildren<Animator>());
        }
        

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            navMeshAgent.speed = config.Speed;
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

        public void TakeDamage(int damage)
        {
            characterStats.DecreaseHealth(damage);
        }
    }
}

