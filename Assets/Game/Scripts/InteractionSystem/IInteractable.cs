using UnityEngine;

namespace Game.InteractionSystem
{
    public interface IInteractable
    {
        void Interact();
        Rigidbody Rigidbody { get; }
        Renderer Renderer { get; }
        Transform Transform { get; }
    }
}