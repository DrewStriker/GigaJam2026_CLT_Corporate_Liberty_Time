using System;
using Game.StatsSystem;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Game.Characters
{
    public class EnemyController : MonoBehaviour, ICharacter
    {
        [SerializeField] private CharacterStatsSO config;
        private Transform playerTransform;
        private NavMeshAgent navMeshAgent;

        public AnimationController AnimationController => throw new NotImplementedException();
        public CharacterStats characterStats { get; private set; }

        public log4net.Util.Transform Transform => throw new NotImplementedException();

        public event Action OnAttackRange;
        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            characterStats = new CharacterStats(config);
        }

        [Inject]
        public void Construct(IPlayableCharacter playableCharacter)
        {
            playerTransform = playableCharacter.Transform;
        }

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            navMeshAgent.speed = config.Speed;
        }

        private void Update()
        {
            ChasePlayer();
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
    }
}

