using System;
using UnityEngine;

namespace Game.Characters
{
    public class PlayableCharacterMovementController
    {
        private Rigidbody rigidbody;
        private Func<Vector3> getMovementDirection;
        private float jumpForce;
        private float baseVelocity;
        public PlayableCharacterMovementController(Rigidbody rigidbody, PlayerConfig playerConfig, Func<Vector3> getMovementDirection)
        {
            this.rigidbody = rigidbody;
            this.getMovementDirection = getMovementDirection;
            jumpForce = playerConfig.JumpForce;
            baseVelocity = playerConfig.BaseVelocity;
        }

        public void UpdateMovement()
        {
            Vector3 horizontalVelocity = getMovementDirection() * baseVelocity * Time.fixedDeltaTime;

            rigidbody.linearVelocity = new Vector3(
                horizontalVelocity.x,
                rigidbody.linearVelocity.y,
                horizontalVelocity.z
            );
        }

        public void Jump()
        {
            rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}

