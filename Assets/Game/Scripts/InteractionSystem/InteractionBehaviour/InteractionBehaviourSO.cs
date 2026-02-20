using UnityEngine;

namespace Game.InteractionSystem
{
    public abstract class InteractionBehaviourSO : ScriptableObject
    {
        public abstract void Execute(IInteractable interactable);
    }
}