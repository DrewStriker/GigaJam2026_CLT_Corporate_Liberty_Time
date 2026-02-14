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

        [SerializeField] private PlayerConfig playerConfig;

        private void Awake()
        {
            AnimationController = new AnimationController(GetComponentInChildren<Animator>());
            InputController = new PlayerInputController();
            MovementController = new PlayableCharacterMovementController(GetComponent<Rigidbody>(), playerConfig, InputController);
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

