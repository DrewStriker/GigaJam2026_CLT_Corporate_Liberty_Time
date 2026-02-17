using System;
using DG.Tweening;
using Game.Core;
using Game.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Game.InteractionSystem
{
    public class InteractableObject : MonoBehaviour, IInteractable
    {
        [SerializeField] private Collider collider;
        [Inject] private PlayerInputController playerInputController;
        protected new Renderer renderer;

        private Tween colorTween;
        private InputAction InteractInput;

        private void Awake()
        {
            renderer = GetComponentInChildren<Renderer>();
        }

        private void OnEnable()
        {
            InteractInput = playerInputController.Interact;
        }


        public virtual void Interact()
        {
            print("interecting");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(Tags.Player))
                StartEffect();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag(Tags.Player))
                StopEffect();
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag(Tags.Player))
                if (InteractInput.WasPressedThisFrame())
                    Interact();
        }


        private void StartEffect()
        {
            colorTween = renderer.DoColor(
                ShaderProperties.OverlayColor,
                new Color(1, 1, 1, 0.3f),
                0);
            // .SetLoops(-1, LoopType.Yoyo)
            // .SetEase(Ease.Linear);
        }

        private void StopEffect()
        {
            colorTween?.Kill();
            renderer.DoColor(ShaderProperties.OverlayColor,
                new Color(1, 1, 1, 0),
                0);
        }
    }
}