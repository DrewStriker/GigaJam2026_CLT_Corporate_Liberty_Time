using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Input
{
    public enum InputType { InGame, UI }
   
    public class PlayerInputController : IMovementInfo
    {
        public InputAction Jump { get; private set; }
        public InputAction Movement { get; private set; }
        public InputAction Attack { get; private set; }
        public InputAction Interact { get; private set; }

        public Vector2 Direction { get; private set; }


        private PlayerInput playerInput;
        private Vector2 movementValue;

        public PlayerInputController()
        {
            playerInput = new PlayerInput();
            Initialize();
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
        }
        private void UnassignInputEvents()
        {
            Movement.performed -= OnMovementPerformed;
        }

        private void OnMovementPerformed(InputAction.CallbackContext context)
        {
            Direction = context.ReadValue<Vector2>();
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
            UnassignInputEvents();
        }
    }
}

