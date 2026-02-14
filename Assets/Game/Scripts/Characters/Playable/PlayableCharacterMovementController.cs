using System;
using UnityEngine;

namespace Game.Characters
{
    public class PlayableCharacterMovementController
    {
        private Rigidbody rigidbody;
        private Func<Vector3> getMovementDirection;
        PlayerConfig playerConfig;
        public PlayableCharacterMovementController(Rigidbody rigidbody, PlayerConfig playerConfig, Func<Vector3> getMovementDirection)
        {
            this.rigidbody = rigidbody;
            this.getMovementDirection = getMovementDirection;
            this.playerConfig = playerConfig;
        }

        public void UpdateMovement()
        {
            Vector3 horizontalVelocity = getMovementDirection() * playerConfig.BaseVelocity * Time.fixedDeltaTime;

            rigidbody.linearVelocity = new Vector3(
                horizontalVelocity.x,
                rigidbody.linearVelocity.y,
                horizontalVelocity.z
            );
        }

        public void Jump()
        {
            rigidbody.AddForce(Vector3.up * playerConfig.JumpForce, ForceMode.Impulse);
        }

        public bool IsGrounded()
        {
            CapsuleCollider collider = rigidbody.GetComponent<CapsuleCollider>();

            Vector3 origin = collider.bounds.center;
            float castDistance = collider.bounds.extents.y + 0.05f;

            return Physics.SphereCast(origin,collider.radius * 0.9f,
                Vector3.down,out _,castDistance,LayerMask.GetMask("Ground"));
        }
    }
}

