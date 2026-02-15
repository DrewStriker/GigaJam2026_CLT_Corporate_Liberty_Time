
namespace Game.Characters
{
    public class PlayerStateFactory
    {
        public PlayerBaseState IdleState { get; private set; }
        public PlayerBaseState MovementState { get; private set; }
        public PlayerBaseState JumpState { get; private set; }
        public PlayerBaseState AttackState { get; private set; }
        public PlayerBaseState InteractState { get; private set; }
        public PlayerBaseState HurtState { get; private set; }
        public PlayerBaseState DeathState { get; private set; }

        public PlayerStateFactory(PlayerStateMachine stateMachine, IPlayableCharacter character)
        {
            IdleState = new IdleState(stateMachine, character);
            MovementState = new MovementState(stateMachine, character);
            JumpState = new JumpState(stateMachine, character);
            AttackState = new AttackState(stateMachine, character);
            InteractState = new InteractState(stateMachine, character);
            HurtState = new HurtState(stateMachine, character);
            DeathState = new DeathState(stateMachine, character);
        }
        
    }
}

