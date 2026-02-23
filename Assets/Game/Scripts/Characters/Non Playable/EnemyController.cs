using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DamageSystem;
using Game.Core;
using Game.Core.SimplePool.SfxPool;
using Game.Core.SimplePool.VfxPool;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Game.TimeSystem;

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
        [SerializeField] private VfxType hitVfxType;
        public NavMeshAgent NavMeshAgent { get; private set; }
        public DamageData damageData { get; private set; } = new();
        protected BehaviorGraphAgent behaviorAgent;
        public static event Action<int> LostAllHealth;
        [Inject] private VfxPoolFacade vfxPoolFacade;
        [Inject] public SfxPoolFacade sfxPoolFacade;

        private TimeSystem.Timer gruntTimer;

        protected override void Awake()
        {
            base.Awake();
            gruntTimer = new TimeSystem.Timer(TimerMode.CountDown);
            gruntTimer.OnTimerCompleted += PlayGrunt;
            SetRandomTimer();
            NavMeshAgent = GetComponent<NavMeshAgent>();
            behaviorAgent = GetComponent<BehaviorGraphAgent>();
            behaviorAgent.SetVariableValue("PlayerController", playerTarget);
            behaviorAgent.SetVariableValue("EnemyController", GetComponent<EnemyController>());
        }

        private void Update()
        {
            //gruntTimer.UpdateTimer(Time.deltaTime);
        }

        private void PlayGrunt()
        {
            sfxPoolFacade.Play(SfxType.Enemy, transform.position, 1, true);
            SetRandomTimer();
        }

        private void SetRandomTimer()
        {
            gruntTimer.Start(UnityEngine.Random.Range(5f, 15f));
        }

        private void OnEnable()
        {
            behaviorAgent.Restart();
        }

        protected virtual void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag(Tags.Player))
                if (other.gameObject.TryGetComponent(out IDamageable damageable))
                {
                    var hitPoint = other.collider.ClosestPoint(transform.position);
                    vfxPoolFacade.Spawn(hitVfxType, hitPoint);
                    damageData.Configure(1, transform.position);
                    damageable.TakeDamage(damageData);
                }
        }


        public override async void TakeDamage(DamageData damageData)
        {
            NavMeshAgent.enabled = false;
            base.TakeDamage(damageData);
            if (characterStats.CurrentHealth <= 0)
                Die();
            await UniTask.Delay(500);
        }


        protected async void Die()
        {
            LostAllHealth?.Invoke(characterStats.MaxHealth);

            behaviorAgent.enabled = false;
            NavMeshAgent.enabled = false;
            Rigidbody.isKinematic = true;
            Collider.isTrigger = true;
            AnimationController.Play(Animation.Death, 0.1f, 2);
            await UniTask.Delay(2000);
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            gruntTimer.OnTimerCompleted -= PlayGrunt;
        }
    }
}