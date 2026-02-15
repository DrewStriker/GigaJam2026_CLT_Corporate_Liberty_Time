using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Game.Input
{
    public enum InputType { InGame, UI }
   
    public class PlayerInputController : IMovementInfo, ITickable, IDisposable
    {
        public InputAction Jump { get; private set; }
        public InputAction Movement { get; private set; }
        public InputAction Attack { get; private set; }
        public InputAction Interact { get; private set; }

        public Vector2 Direction { get; private set; }


        private PlayerInput playerInput;
        private InputAction movementAction;

        public PlayerInputController()
        {
            playerInput = new PlayerInput();
            Initialize();
            movementAction = playerInput.FindAction("Movement");
        }

        private void Initialize()
        {
            Jump = playerInput.InGame.Jump;
            Movement = playerInput.InGame.Movement;
            Attack = playerInput.InGame.Attack;
            Interact = playerInput.InGame.Interact;

            AssignInputEvents();
            EnableInput(InputType.InGame);
        }

        private void AssignInputEvents()
        {
            Movement.performed += OnMovementPerformed;
            Movement.canceled += OnMovementPerformed;
        }
        private void UnassignInputEvents()
        {
            Movement.performed -= OnMovementPerformed;
            Movement.canceled -= OnMovementPerformed;
        }

        private void OnMovementPerformed(InputAction.CallbackContext context)
        {
            Direction = context.ReadValue<Vector2>();
            // Debug.Log(Mathf.Abs((Direction.magnitude)));
        }
        public void Tick()
        {
            Direction =movementAction.ReadValue<Vector2>();
        }

        public void EnableInput(InputType inputType)
        {
            switch (inputType)
            {
                case InputType.InGame:
                    playerInput.UI.Disable();
                    playerInput.InGame.Enable();
                    break;
                case InputType.UI:
                    playerInput.InGame.Disable();
                    playerInput.UI.Enable();
                    break;
            }
        }

        ~PlayerInputController()
        {
            Dispose(false);
        }


        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                playerInput?.Dispose();
                movementAction?.Dispose();
                Jump?.Dispose();
                Movement?.Dispose();
                Attack?.Dispose();
                Interact?.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

