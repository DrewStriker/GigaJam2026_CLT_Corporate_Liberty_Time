
namespace Game.Characters
{
    public class InteractState : PlayerBaseState
    {
        public InteractState(PlayerStateMachine stateMachine, IPlayableCharacter character) : base(stateMachine, character)
        {
        }

        public override void OnStateEnter()
        {
            UnityEngine.Debug.Log("Interact State");
        }

        public override void OnStateExit()
        {
        }
        public override void FixedUpdate()
        {
        }

        public override void Update()
        {
            stateMachine.TryIdleState(this);
            stateMachine.TryMovementState(this);
            stateMachine.TryJumpState(this);
            stateMachine.TryAttackState(this);
        }
    }

}

