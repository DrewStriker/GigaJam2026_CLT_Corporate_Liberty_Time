using System;
using Game.StatsSystem;
using UnityEngine;

namespace Game.Characters
{
    public class PlayableCharacterMovementController
    {
        CharacterStats characterStats;
        private Rigidbody rigidbody;
        private IMovementInfo movementInfo;
        private float jumpForce;
        private float baseVelocity;
        
        public PlayableCharacterMovementController(Rigidbody rigidbody, CharacterStats characterStats, IMovementInfo movementInfo)
        {
            this.characterStats = characterStats;
            this.rigidbody = rigidbody;
            this.movementInfo = movementInfo;
            jumpForce = characterStats.JumpForce;
            baseVelocity = characterStats.MoveSpeed;
        }

        public void UpdateMovement()
        {
            Vector3 horizontalVelocity = movementInfo.Direction * (baseVelocity * Time.fixedDeltaTime);
            horizontalVelocity.z = horizontalVelocity.y;
            horizontalVelocity.y = rigidbody.linearVelocity.y;

            rigidbody.linearVelocity = horizontalVelocity;

            UpdateRotation();
        }

        private void UpdateRotation()
        {
            float rotationSpeed = 15f;
            Vector3 velocity = rigidbody.linearVelocity;
            Vector3 horizontal = velocity;
            horizontal.y = 0;
            if (horizontal.sqrMagnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(horizontal);
                Quaternion smooth = Quaternion.Slerp(
                    rigidbody.rotation,
                    targetRotation,
                    rotationSpeed * Time.fixedDeltaTime);

                rigidbody.MoveRotation(smooth);
            }
        }

        public void Jump()
        {
            rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}

