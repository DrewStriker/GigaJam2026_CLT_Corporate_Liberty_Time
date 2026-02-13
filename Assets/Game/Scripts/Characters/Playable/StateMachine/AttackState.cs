
namespace Game.Characters
{
    public class AttackState : BaseState
    {
        public AttackState(StateMachine stateMachine) : base(stateMachine)
        {
        }


        public override void OnStateEnter()
        {
            UnityEngine.Debug.Log("Attack State");
        }

        public override void OnStateExit()
        {
        }
        public override void FixedUpdate()
        {
            //Enable Movement??
        }

        public override void Update()
        {
            (stateMachine as PlayerStateMachine).TryIdleState(this);
            (stateMachine as PlayerStateMachine).TryMovementState(this);
        }
    }
}

