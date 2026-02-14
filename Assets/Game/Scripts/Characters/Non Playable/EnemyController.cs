using System;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Characters
{
    public class EnemyController : MonoBehaviour, ICharacter
    {
        [SerializeField] private EnemyConfig enemyConfig;
        private Transform playerTransform;
        private NavMeshAgent navMeshAgent;

        public AnimationController AnimationController => throw new NotImplementedException();

        public event Action OnAttackRange;
        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            playerTransform = FindAnyObjectByType<PlayerController>().transform;
        }

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            navMeshAgent.speed = enemyConfig.BaseSpeed;
        }

        private void Update()
        {
            ChasePlayer();
        }

        private void ChasePlayer()
        {
            if (Vector3.Distance(transform.position, playerTransform.position) > enemyConfig.AttackRange)
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
    }
}

