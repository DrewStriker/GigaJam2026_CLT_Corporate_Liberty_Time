namespace Game.Characters
{
    public class JumpState : BaseState
    {
        public JumpState(StateMachine stateMachine) : base(stateMachine)
        {
        }


        public override void OnStateEnter()
        {
            (stateMachine as PlayerStateMachine).PlayerMovementController.Jump();
            UnityEngine.Debug.Log("Jump State");
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
            (stateMachine as PlayerStateMachine).TryIdleState(this);
            (stateMachine as PlayerStateMachine).TryMovementState(this);
        }
    }
}

