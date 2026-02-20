using UnityEngine;

namespace Game.InteractionSystem
{
    [CreateAssetMenu(fileName = "Throw forward", menuName = "Object Behaviour/Throw forward")]
    public class ThrowForwardBehaviourSO : InteractionBehaviourSO
    {
        [SerializeField, Min(1)] private float throwForce = 1; 
        
        public override void Execute(IInteractable interactable)
        {
            
            Vector3 forwardDirection = interactable.Transform.forward;
            forwardDirection.y = 0;
            interactable.Rigidbody.AddForce(throwForce * forwardDirection, ForceMode.Impulse);

        }
    }
}