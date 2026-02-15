using System;
using Game.Input;

namespace Game.Characters
{
    public class PlayerStateMachine : StateMachine, IDisposable
    {
        private readonly IPlayableCharacter Character;
        private readonly PlayerInputController playerInputController;
        private readonly PlayerStateFactory stateFactory;

        public PlayerStateMachine(IPlayableCharacter character)
        {
            Character = character;
            playerInputController = character.InputController;
            PlayerMovementController = character.MovementController;

            stateFactory = new PlayerStateFactory(this, character);
            CurrentState = stateFactory.IdleState;
            character.characterStats.HealthChanged += HealthChanged;
        }

        public PlayableCharacterMovementController PlayerMovementController { get; }

        public void Dispose()
        {
            playerInputController?.Dispose();
            Character.characterStats.HealthChanged -= HealthChanged;
        }


        private bool IsValidState(BaseState callingState)
        {
            return callingState == CurrentState;
        }

        public void TryMovementState(BaseState callingState)
        {
            if (!IsValidState(callingState)) return;
            if (playerInputController.Movement.IsPressed() && PlayerMovementController.IsGrounded())
                SwitchState(stateFactory.MovementState);
        }

        public void TryIdleState(BaseState callingState)
        {
            if (!IsValidState(callingState)) return;
            if (!playerInputController.Movement.IsPressed() && PlayerMovementController.IsGrounded())
                SwitchState(stateFactory.IdleState);
        }

        public void TryJumpState(BaseState callingState)
        {
            if (!IsValidState(callingState)) return;
            if (playerInputController.Jump.WasPressedThisFrame() && PlayerMovementController.IsGrounded())
                SwitchState(stateFactory.JumpState);
        }

        public void TryAttackState(BaseState callingState)
        {
            if (!IsValidState(callingState)) return;
            if (playerInputController.Attack.WasPressedThisFrame()) SwitchState(stateFactory.AttackState);
        }

        public void TryInteractState(BaseState callingState)
        {
            if (!IsValidState(callingState)) return;
            if (playerInputController.Interact.WasPressedThisFrame()) SwitchState(stateFactory.InteractState);
        }

        private void SwitchState(BaseState state)
        {
            CurrentState.OnStateExit();
            CurrentState = state;
            CurrentState.OnStateEnter();
        }

        private void HealthChanged(int value)
        {
            if (value == 0)
                SwitchState(stateFactory.DeathState);
            else
                SwitchState(stateFactory.HurtState);
        }
    }
}