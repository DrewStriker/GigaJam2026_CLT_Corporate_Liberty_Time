using Game.StatsSystem;
using UnityEngine;

namespace Game.Characters
{
    using Game.Input;
    public class PlayerController : MonoBehaviour, IPlayableCharacter
    {
        public PlayerInputController InputController { get; private set; }
        public PlayableCharacterMovementController MovementController { get; private set; }
        public PlayerStateMachine StateMachine { get; private set; }

        public AnimationController AnimationController { get; private set; }
        public CharacterStats characterStats { get; private set; }

        public Transform Transform { get; private set; }

        [SerializeField] private CharacterStatsSO config;


        private void Awake()
        {
            Transform = transform;
            characterStats = new(config);
            AnimationController = new AnimationController(GetComponentInChildren<Animator>());
            InputController = new PlayerInputController();
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
    }
}

