using UnityEngine;

namespace Game.Characters
{
    using Game.Input;
    public class PlayerController : MonoBehaviour, IPlayableCharacter
    {
        public PlayerInputController InputController { get; private set; }
        public PlayableCharacterMovementController MovementController { get; private set; }
        public PlayerStateMachine StateMachine { get; private set; }

        [SerializeField] private PlayerConfig playerConfig;

        private void Awake()
        {
            InputController = new PlayerInputController();
            MovementController = new PlayableCharacterMovementController(GetComponent<Rigidbody>(), playerConfig, () => InputController.MovementDirection);
            StateMachine = new PlayerStateMachine(InputController, MovementController);
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

