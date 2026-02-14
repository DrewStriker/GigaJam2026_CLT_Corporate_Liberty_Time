
namespace Game.Characters
{
    public class IdleState : PlayerBaseState
    {
        public IdleState(PlayerStateMachine stateMachine, IPlayableCharacter character) : base(stateMachine, character) { }

        public override void OnStateEnter()
        {
            AnimationController.Play(Animation.Idle);
        }

        public override void OnStateExit()
        {
        }
        public override void FixedUpdate()
        {
        }

        public override void Update()
        {
            stateMachine.TryAttackState(this);
            stateMachine.TryInteractState(this);
            stateMachine.TryMovementState(this);
            stateMachine.TryJumpState(this);
        }
    }
}

