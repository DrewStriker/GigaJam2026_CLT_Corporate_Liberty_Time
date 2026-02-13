
namespace Game.Characters
{
    public class PlayerStateFactory
    {
        public BaseState IdleState { get; private set; }
        public BaseState MovementState { get; private set; }
        public BaseState JumpState { get; private set; }
        public BaseState AttackState { get; private set; }
        public BaseState InteractState { get; private set; }

        public PlayerStateFactory(StateMachine stateMachine)
        {
            IdleState = new IdleState(stateMachine);
            MovementState = new MovementState(stateMachine);
            JumpState = new JumpState(stateMachine);
            AttackState = new AttackState(stateMachine);
            InteractState = new InteractState(stateMachine);
        }
    }
}

