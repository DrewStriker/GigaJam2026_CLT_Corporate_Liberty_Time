
namespace Game.Characters
{
    public abstract class PlayerBaseState : BaseState
    {
        protected readonly IPlayableCharacter character;
        protected readonly new PlayerStateMachine stateMachine;
       protected AnimationController AnimationController => character.AnimationController;
        protected PlayableCharacterMovementController MovementController => character.MovementController;

        public PlayerBaseState(PlayerStateMachine stateMachine, IPlayableCharacter character) : base(stateMachine)
        {
            this.character = character;
            this.stateMachine = stateMachine;
        }

        public abstract override void FixedUpdate();

        public abstract override void OnStateEnter();

        public abstract override void OnStateExit();

        public abstract override void Update();
    }
}

