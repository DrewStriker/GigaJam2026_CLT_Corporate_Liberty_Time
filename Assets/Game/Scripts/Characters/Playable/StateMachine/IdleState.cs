
using System.Diagnostics;

namespace Game.Characters
{
    public class IdleState : BaseState
    {
        public IdleState(StateMachine stateMachine) : base(stateMachine) { }

        public override void OnStateEnter()
        {
            UnityEngine.Debug.Log("Idle State");
        }

        public override void OnStateExit()
        {
        }
        public override void FixedUpdate()
        {
        }

        public override void Update()
        {
            (stateMachine as PlayerStateMachine).TryAttackState(this);
            (stateMachine as PlayerStateMachine).TryInteractState(this);
            (stateMachine as PlayerStateMachine).TryMovementState(this);
            (stateMachine as PlayerStateMachine).TryJumpState(this);
        }
    }
}

