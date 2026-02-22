using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Characters
{
    public class HurtState : PlayerBaseState
    {
        public HurtState(PlayerStateMachine stateMachine, IPlayableCharacter character) : base(stateMachine, character)
        {
        }
        public override void OnStateEnter()
        {            
            AnimationController.Play(Animation.Hurt, 0.1f, 1);
            WaitToReturn().Forget();
        }
        public override void Update()
        {
                character.UpdateBaseAnimation();
        }

        public override void FixedUpdate()
        {
            // MovementController.UpdateMovement();
            
        }

        public override void OnStateExit()
        {
        }


        
        private async UniTask WaitToReturn()
        {
            await UniTask.Delay(500);
            stateMachine.TryIdleState(this);
            stateMachine.TryMovementState(this);
        }
    }
}