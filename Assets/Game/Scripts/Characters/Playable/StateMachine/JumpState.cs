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
            UnityEngine.Debug.Log("Jump State");
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
        }
    }
}

