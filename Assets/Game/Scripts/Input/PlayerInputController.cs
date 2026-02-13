using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Input
{
    public enum InputType { InGame, UI }
   
    public class PlayerInputController 
    {
        public InputAction Jump { get; private set; }
        public InputAction Movement { get; private set; }
        public InputAction Attack { get; private set; }
        public InputAction Interact { get; private set; }
        public Vector3 MovementDirection => GetMovementDirection();

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
            Movement.performed += context => movementValue = context.ReadValue<Vector2>();
        }
        private void UnassignInputEvents()
        {
            Movement.performed += context => movementValue = context.ReadValue<Vector2>();
        }

        private Vector3 GetMovementDirection()
        {
            return new Vector3(movementValue.x, 0, movementValue.y);
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

