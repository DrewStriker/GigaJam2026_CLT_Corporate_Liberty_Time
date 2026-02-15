
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Characters
{
    public class AttackState : PlayerBaseState
    {
        private const int MaxAtkIndex = 2;
        private int attackIndex = 0;

        private int GetNextIndex()
        {
            attackIndex = (attackIndex % MaxAtkIndex) + 1;
            return attackIndex;
        }
        
        public AttackState(PlayerStateMachine stateMachine, IPlayableCharacter character) : base(stateMachine, character)
        {
        }

        public override void OnStateEnter()
        {
            PlayCurrentAnimation();
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
        
        
        private void PlayCurrentAnimation()
        {
            var index = GetNextIndex();
            var nextAnim = index == 1 ? Animation.Attack1 : Animation.Attack2;        
            AnimationController.Play(nextAnim,0,1);

        }

    }
}
