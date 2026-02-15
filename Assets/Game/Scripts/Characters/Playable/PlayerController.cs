using DamageSystem;
using Game.CollectableSystem;
using Game.StatsSystem;
using Game.WeaponSystem;
using UnityEngine;
using Zenject;
using Vector2 = System.Numerics.Vector2;

namespace Game.Characters
{
    using Game.Input;
    public class PlayerController : CharacterBase, IPlayableCharacter
    {
        [SerializeField] private Transform handTransform;
       [Inject]  public PlayerInputController InputController { get; private set; }
        public PlayableCharacterMovementController MovementController { get; private set; }
        public PlayerStateMachine StateMachine { get; private set; }
        public Transform Transform { get; private set; }
        public IDamager Damager { get; private set; }



        protected override void Awake()
        {
            base.Awake();
            Transform = transform;
            Damager = GetComponentInChildren<IDamager>();
            MovementController = new PlayableCharacterMovementController(GetComponent<Rigidbody>(), characterStats, InputController);
            StateMachine = new PlayerStateMachine(this);

        }

  
        private void FixedUpdate()
        {
            StateMachine.FixedUpdate();
        }

        private void Update()
        {
            StateMachine.Update();
        }

        public void UpdateBaseAnimation()
        {
            float moveDirection = Mathf.Abs(InputController.Direction.sqrMagnitude);
            AnimationController.SetFloat(AnimationParameter.VelocityH,moveDirection );
        }
        
        public override void TakeDamage(DamageData damageData)
        {
            base.TakeDamage(damageData);
            Knockback(damageData.AttackerPosition);
        }
        
        protected override void Die()
        {
            base.Die();
            Debug.Log("Player Died");
        }

        private void Knockback(Vector3 position)
        {
            
            Vector3 direction = (transform.position - position).normalized;
            direction.y = 0;
            Rigidbody.AddForce(direction*7, ForceMode.Impulse);
            
        }

        public void Attach(ICollectable<WeaponType> item)
        {
            item.Transform.SetParent(handTransform);
            item.Transform.localPosition = Vector3.zero;
            item.Transform.rotation = Quaternion.identity;

        }
    }
}

