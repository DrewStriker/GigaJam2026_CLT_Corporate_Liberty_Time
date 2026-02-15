
using Cysharp.Threading.Tasks;

namespace Game.Characters
{
    public class AttackState : PlayerBaseState
    {

        public AttackState(PlayerStateMachine stateMachine, IPlayableCharacter character) : base(stateMachine, character)
        {
        }

        public override void OnStateEnter()
        {
            AnimationController.Play(Animation.Attack2,0,1);
            WaitToReturn().Forget();
        }

        public override void OnStateExit()
        {
        }
        public override void FixedUpdate()
        {
            MovementController.UpdateMovement();
        }

        public override void Update()
        {
            character.UpdateBaseAnimation();
        }

        private async UniTask WaitToReturn()
        {
            await UniTask.Delay(1000);
            stateMachine.TryIdleState(this);
            stateMachine.TryMovementState(this);
            
  
        }
    }
}
