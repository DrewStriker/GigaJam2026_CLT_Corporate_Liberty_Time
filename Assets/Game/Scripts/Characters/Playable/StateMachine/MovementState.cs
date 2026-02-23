
using System.Diagnostics;

namespace Game.Characters
{
    public class MovementState : PlayerBaseState
    {
        public MovementState(PlayerStateMachine stateMachine, IPlayableCharacter character) : base(stateMachine, character)
        {
        }

        public override void OnStateEnter()
        {
            AnimationController.Play(Animation.Run);
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
            character.UpdateBaseAnimation();
            stateMachine.TryAttackState(this);
            stateMachine.TryIdleState(this);
            stateMachine.TryJumpState(this);
            stateMachine.TryInteractState(this);
        }
    }
}

