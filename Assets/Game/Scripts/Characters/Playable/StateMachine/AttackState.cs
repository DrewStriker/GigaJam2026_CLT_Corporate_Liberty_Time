using Cysharp.Threading.Tasks;
using Game.CollectableSystem;
using Game.WeaponSystem;
using UnityEngine;

namespace Game.Characters
{
    public class AttackState : PlayerBaseState
    {
        private const int MaxAtkIndex = 2;
        private int attackIndex;
        private ICollectable<WeaponType> WeaponEquipped => character.WeaponEquipped;

        public AttackState(PlayerStateMachine stateMachine, IPlayableCharacter character) : base(stateMachine,
            character)
        {
        }

        private int GetNextIndex()
        {
            attackIndex = attackIndex % MaxAtkIndex + 1;
            return attackIndex;
        }

        public override void OnStateEnter()
        {
            WeaponEquipped?.CollectEffect?.Play();
            PlayCurrentAnimation();
            WaitToReturn().Forget();
        }

        public override void OnStateExit()
        {
            WeaponEquipped?.CollectEffect?.Stop();
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
            await UniTask.Delay(500);
            stateMachine.TryIdleState(this);
            stateMachine.TryMovementState(this);
        }


        private void PlayCurrentAnimation()
        {
            var index = GetNextIndex();
            var nextAnim = index == 1 ? Animation.Attack1 : Animation.Attack2;
            AnimationController.Play(nextAnim, 0, 1);
        }
    }
}