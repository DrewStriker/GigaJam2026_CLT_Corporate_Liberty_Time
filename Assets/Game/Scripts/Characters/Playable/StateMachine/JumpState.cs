namespace Game.Characters
{
    public class JumpState : PlayerBaseState
    {
        public JumpState(PlayerStateMachine stateMachine, IPlayableCharacter character) : base(stateMachine, character)
        {
        }

        public override void OnStateEnter()
        {
            stateMachine.PlayerMovementController.Jump();
            AnimationController.Play(Animation.Jump);
            character.SfxPoolFacade.Play(Core.SimplePool.SfxPool.SfxType.Jump, (character as ICharacter).Transform.position, 1, true);
        }

        public override void OnStateExit()
        {           

        }
        public override void FixedUpdate()
        {
            character.MovementController.UpdateMovement();
        }

        public override void Update()
        {
            stateMachine.TryIdleState(this);
            stateMachine.TryMovementState(this);
            stateMachine.TryAttackState(this);
        }
    }
}

