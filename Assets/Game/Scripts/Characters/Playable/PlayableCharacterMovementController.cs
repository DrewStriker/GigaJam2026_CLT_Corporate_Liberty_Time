using Game.StatsSystem;
using UnityEngine;

namespace Game.Characters
{
    using Game.Core;
    public class PlayableCharacterMovementController
    {
        CharacterStats characterStats;
        private Rigidbody rigidbody;
        private IMovementInfo movementInfo;
        private Collider[] groundHits = new Collider[1];

        private float jumpForce => characterStats.JumpForce;
        private float baseVelocity => characterStats.MoveSpeed;
        
        public PlayableCharacterMovementController(Rigidbody rigidbody, CharacterStats characterStats, IMovementInfo movementInfo)
        {
            this.characterStats = characterStats;
            this.rigidbody = rigidbody;
            this.movementInfo = movementInfo;
           
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

        public bool IsGrounded()
        {
            CapsuleCollider collider = rigidbody.GetComponent<CapsuleCollider>();
            int hitCount = Physics.OverlapSphereNonAlloc(
                rigidbody.position + collider.center + Vector3.down * collider.height / 2f,
                collider.radius,
                groundHits,
                Layers.Ground,
                QueryTriggerInteraction.Ignore);
            return hitCount > 0 && rigidbody.linearVelocity.y <= 0;
        }

        public void ZeroLinearVelocity()
        {
            rigidbody.linearVelocity = Vector3.zero;
        }
    }
}

