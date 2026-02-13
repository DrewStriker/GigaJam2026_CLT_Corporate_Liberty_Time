
namespace Game.Characters
{
    public class MovementState : BaseState
    {
        public MovementState(StateMachine stateMachine) : base(stateMachine)
        {
        }


        public override void OnStateEnter()
        {
            UnityEngine.Debug.Log("Movement State");
        }

        public override void OnStateExit()
        {
        }
        public override void FixedUpdate()
        {
            (stateMachine as PlayerStateMachine).PlayerMovementController.UpdateMovement();
        }

        public override void Update()
        {
            (stateMachine as PlayerStateMachine).TryAttackState(this);
            (stateMachine as PlayerStateMachine).TryIdleState(this);
            (stateMachine as PlayerStateMachine).TryJumpState(this);
            (stateMachine as PlayerStateMachine).TryInteractState(this);
        }
    }
}

