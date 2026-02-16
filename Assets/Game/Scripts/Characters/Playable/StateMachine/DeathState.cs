using UnityEngine;

namespace Game.Characters
{
    public class DeathState : PlayerBaseState
    {
        public DeathState(PlayerStateMachine stateMachine, IPlayableCharacter character) : base(stateMachine, character)
        {
        }

        public override void OnStateEnter()
        {
            AnimationController.Play(Animation.Death, 0.1f, 2);
            character.Collider.enabled = false;
            character.Rigidbody.isKinematic = true;
            character.Rigidbody.linearVelocity = Vector3.zero;
        }

        public override void Update()
        {
        }

        public override void FixedUpdate()
        {
        }

        public override void OnStateExit()
        {
        }
    }
}