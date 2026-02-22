using Game.Core;
using Game.StatsSystem;
using UnityEngine;

namespace Game.Characters
{
    public class PlayableCharacterMovementController
    {
        private readonly CharacterStats characterStats;
        private readonly Collider[] groundHits = new Collider[1];
        private readonly IMovementInfo movementInfo;
        private readonly Rigidbody rigidbody;

        private Transform cameraTransform;
        private float jumpForce => characterStats.JumpForce;
        private float baseVelocity => characterStats.MoveSpeed;

        public PlayableCharacterMovementController(Rigidbody rigidbody, CharacterStats characterStats,
            IMovementInfo movementInfo)
        {
            this.characterStats = characterStats;
            this.rigidbody = rigidbody;
            this.movementInfo = movementInfo;
            cameraTransform = Camera.main.transform;
        }


        public void UpdateMovement()
        {
            var forward = cameraTransform.forward.normalized;
            var right = cameraTransform.right.normalized;
            forward.y = 0;
            right.y = 0;
            var moveDirection = forward * movementInfo.Direction.y + right * movementInfo.Direction.x;

            var horizontalVelocity = moveDirection * (baseVelocity * Time.fixedDeltaTime);
            horizontalVelocity.z = horizontalVelocity.z;
            horizontalVelocity.y = rigidbody.linearVelocity.y;

            rigidbody.linearVelocity = horizontalVelocity;

            UpdateRotation(forward);
        }

        private void UpdateRotation(Vector3 forward)
        {
            var rotationSpeed = 15f;
            // var velocity = rigidbody.linearVelocity;
            // var horizontal = velocity;
            var horizontal = forward;
            horizontal.y = 0;
            if (horizontal.sqrMagnitude > 0.01f)
            {
                var targetRotation = Quaternion.LookRotation(forward);
                var smooth = Quaternion.Slerp(
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
            var collider = rigidbody.GetComponent<CapsuleCollider>();
            var hitCount = Physics.OverlapSphereNonAlloc(
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