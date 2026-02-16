using DamageSystem;
using Game.CollectableSystem;
using Game.Input;
using Game.ItemSystem;
using Game.WeaponSystem;
using UnityEngine;
using Zenject;

namespace Game.Characters
{
    public class PlayerController : CharacterBase, IPlayableCharacter
    {
        [SerializeField] private Transform handTransform;

        protected override void Awake()
        {
            base.Awake();
            Transform = transform;
            Damager = GetComponentInChildren<IDamager>();
            MovementController =
                new PlayableCharacterMovementController(GetComponent<Rigidbody>(), characterStats, InputController);
            StateMachine = new PlayerStateMachine(this);
        }

        private void Update()
        {
            StateMachine.Update();
        }


        private void FixedUpdate()
        {
            StateMachine.FixedUpdate();
        }

        [Inject] public PlayerInputController InputController { get; private set; }
        public PlayableCharacterMovementController MovementController { get; private set; }
        public PlayerStateMachine StateMachine { get; private set; }
        public Transform Transform { get; private set; }
        public IDamager Damager { get; private set; }

        public void UpdateBaseAnimation()
        {
            var moveDirection = Mathf.Abs(InputController.Direction.sqrMagnitude);
            AnimationController.SetFloat(AnimationParameter.VelocityH, moveDirection);
        }

        public override void TakeDamage(DamageData damageData)
        {
            base.TakeDamage(damageData);
            Knockback(damageData.AttackerPosition);
        }

        public void Collect(ICollectable<WeaponType> item)
        {
            item.Transform.SetParent(handTransform);
            item.Transform.localPosition = Vector3.zero;
            item.Transform.rotation = Quaternion.identity;
            item.BuffData.ApplyBuffTo(characterStats);
        }

        public void Collect(ICollectable<ItemType> item)
        {
            item.BuffData.ApplyBuffTo(characterStats);
        }


        protected override void Die()
        {
            base.Die();
            Debug.Log("Player Died");
        }

        private void Knockback(Vector3 position)
        {
            var direction = (transform.position - position).normalized;
            direction.y = 0;
            Rigidbody.AddForce(direction * 7, ForceMode.Impulse);
        }

        public void Attack()
        {
            Damager.DoDamage(characterStats.Damage);
        }
    }
}