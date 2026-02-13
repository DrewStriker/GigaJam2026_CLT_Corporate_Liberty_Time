
namespace Game.Characters
{
    public class InteractState : BaseState
    {
        public InteractState(StateMachine stateMachine) : base((PlayerStateMachine)stateMachine)
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
            (stateMachine as PlayerStateMachine).TryIdleState(this);
            (stateMachine as PlayerStateMachine).TryMovementState(this);
            (stateMachine as PlayerStateMachine).TryJumpState(this);
            (stateMachine as PlayerStateMachine).TryAttackState(this);
        }
    }

}

