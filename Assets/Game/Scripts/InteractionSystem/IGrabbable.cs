using UnityEngine;

namespace Game.InteractionSystem
{
    public interface IGrabbable
    {
        public Transform Holder { get; }
        void Grab(Transform holder);
        void Release();
        Transform Transform { get; }
    }
}