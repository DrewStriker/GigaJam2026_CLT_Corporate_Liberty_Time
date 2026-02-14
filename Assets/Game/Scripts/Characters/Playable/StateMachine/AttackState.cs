
namespace Game.Characters
{
    public class AttackState : PlayerBaseState
    {
        public AttackState(PlayerStateMachine stateMachine, IPlayableCharacter character) : base(stateMachine, character)
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
            stateMachine.TryIdleState(this);
            stateMachine.TryMovementState(this);
        }
    }
}

