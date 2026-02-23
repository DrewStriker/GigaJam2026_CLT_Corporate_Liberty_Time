using UnityEngine;

namespace Game.InteractionSystem
{
    public enum GrabbableType { Box1, Box2, Rock1, Rock2, Vase, }
    public class GrabbableObject : InteractableObject, IGrabbable
    {
        public Transform Holder { get; private set; }


        public override bool IsDamageValid()
        {
            return (IsMoving && Holder == null);
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            if (Holder)
                Grabbing();
        }
        
        private void Grabbing()
        {
            Rigidbody.MovePosition(Holder.transform.position + Holder.forward);
            Rigidbody.MoveRotation(Holder.rotation);
        }
        public void Grab(Transform holder)
        {
            Holder = holder;
            
        }

        public void Release()
        {
            Holder = null;
            Interact();
        }
    }
}