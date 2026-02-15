using DamageSystem;
using Game.StatsSystem;
using UnityEngine;
using Zenject;

namespace Game.Characters
{
    using Game.Input;
    public class PlayerController : CharacterBase, IPlayableCharacter
    {
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
        
        protected override void Die()
        {
            base.Die();
            Debug.Log("Player Died");
        }



    }
}

