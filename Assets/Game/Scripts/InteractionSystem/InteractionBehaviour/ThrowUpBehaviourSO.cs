using UnityEngine;

namespace Game.InteractionSystem
{
    [CreateAssetMenu(fileName = "Throw up ", menuName = "Object Behaviour/Throw up")]
    public class ThrowUpBehaviourSO : InteractionBehaviourSO
    {
        [SerializeField, Min(1)] protected float upForce = 10;
        public override void Execute(IInteractable interactable)
        {
            interactable.Rigidbody.AddForce(Vector3.up * upForce, ForceMode.Impulse);
        }
    }
}