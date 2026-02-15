
using Game.Core;

namespace Game.Characters
{
    public class IdleState : PlayerBaseState
    {
        
        public IdleState(PlayerStateMachine stateMachine, IPlayableCharacter character) : base(stateMachine, character) { }
        
        public override void OnStateEnter()
        {
            character.MovementController.ZeroLinearVelocity();
            AnimationController.Play(Animation.Idle);
            UnityEngine.Debug.Log("IDLE State");
        }

        public override void OnStateExit()
        {
        }
        public override void FixedUpdate()
        {
            character.UpdateBaseAnimation();
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

