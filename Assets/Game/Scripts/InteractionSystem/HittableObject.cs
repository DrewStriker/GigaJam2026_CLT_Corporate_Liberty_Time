using Game.Core;
using UnityEngine;

namespace Game.InteractionSystem
{
    public class HittableObject : InteractableObject, IHittable
    {
        public bool IsDamageActive { get; }
        
        public override bool IsDamageValid()
        {
            return IsMoving;
        }

        public void TakeDamage(DamageData damageData)
        {
            Rigidbody.isKinematic = false;
            // Vector3 inverseHitDirection = (transform.position - damageData.AttackerPosition).normalized;
            // Debug.DrawLine(transform.position, damageData.AttackerPosition, Color.red, 10);
            // transform.rotation = Quaternion.Euler(inverseHitDirection);
            Interact();
            
        }


        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other); 
            if (IsMoving) TryDamageEnemy(other);

        
        }
    }
}