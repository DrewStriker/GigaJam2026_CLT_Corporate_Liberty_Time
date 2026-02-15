using DamageSystem;
using Game.StatsSystem;
using UnityEngine;
using Zenject;

namespace Game.Characters
{
    using Game.Input;
    public class PlayerController : MonoBehaviour, IPlayableCharacter
    {
       [Inject]  public PlayerInputController InputController { get; private set; }
        public PlayableCharacterMovementController MovementController { get; private set; }
        public PlayerStateMachine StateMachine { get; private set; }

        public AnimationController AnimationController { get; private set; }
        public CharacterStats characterStats { get; private set; }
        public Rigidbody Rigidbody { get; private set; }

        public Transform Transform { get; private set; }
        public IDamager Damager { get; private set; }

        [SerializeField] private CharacterStatsSO config;


        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            Transform = transform;
            Damager = GetComponentInChildren<IDamager>();
            characterStats = new(config);
            AnimationController = new AnimationController(GetComponentInChildren<Animator>());
            // InputController = new PlayerInputController();
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
        
    }
}

