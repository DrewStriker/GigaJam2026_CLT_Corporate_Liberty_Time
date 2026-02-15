
using System;

namespace Game.Characters
{
    using Game.Input;
    public class PlayerStateMachine : StateMachine, IDisposable
    {
        private IPlayableCharacter Character;
        private PlayerStateFactory stateFactory;
        private PlayerInputController playerInputController;
        public PlayableCharacterMovementController PlayerMovementController { get; private set; }
        public PlayerStateMachine(IPlayableCharacter character)
        {
            this.Character = character;
            playerInputController = character.InputController;
            PlayerMovementController = character.MovementController;

            stateFactory = new PlayerStateFactory(this, character);
            CurrentState = stateFactory.IdleState;
            character.characterStats.OnHealthChanged += OnHealthChanged;
        }

  

        private bool IsValidState(BaseState callingState)
        {
            return callingState == CurrentState;
        }

        public void TryMovementState(BaseState callingState)
        {
            if (!IsValidState(callingState)) return;
            if (playerInputController.Movement.IsPressed() && PlayerMovementController.IsGrounded())
            {
                SwitchState(stateFactory.MovementState);
            }
        }

        public void TryIdleState(BaseState callingState)
        {
            if (!IsValidState(callingState)) return;
            if (!playerInputController.Movement.IsPressed() && PlayerMovementController.IsGrounded())
            {
                SwitchState(stateFactory.IdleState);
            }
        }

        public void TryJumpState(BaseState callingState)
        {
            if (!IsValidState(callingState)) return;
            if (playerInputController.Jump.WasPressedThisFrame() && PlayerMovementController.IsGrounded())
            {
                SwitchState(stateFactory.JumpState);
            }
        }

        public void TryAttackState(BaseState callingState)
        {
            if (!IsValidState(callingState)) return;
            if (playerInputController.Attack.WasPressedThisFrame())
            {
                SwitchState(stateFactory.AttackState);
            }
        }

        public void TryInteractState(BaseState callingState)
        {
            if (!IsValidState(callingState)) return;
            if (playerInputController.Interact.WasPressedThisFrame())
            {
                SwitchState(stateFactory.InteractState);
            }
        }

        private void SwitchState(BaseState state)
        {
            CurrentState.OnStateExit();
            CurrentState = state;
            CurrentState.OnStateEnter();
        }

        private void OnHealthChanged(float value)
        {
            if (value ==0) 
                SwitchState(stateFactory.DeathState);
            else 
                SwitchState(stateFactory.HurtState);
            
        }
        public void Dispose()
        {
            playerInputController?.Dispose();
            Character.characterStats.OnHealthChanged -= OnHealthChanged;
           
        }
    }
}

