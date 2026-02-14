
namespace Game.Characters
{
    using System;
    using Game.Input;
    public class PlayerStateMachine : StateMachine
    {
        private PlayerStateFactory stateFactory;
        private PlayerInputController playerInputController;
        public PlayableCharacterMovementController PlayerMovementController { get; private set; }
        public PlayerStateMachine(IPlayableCharacter character)
        {
            playerInputController = character.InputController;
            PlayerMovementController = character.MovementController;

            stateFactory = new PlayerStateFactory(this, character);
            CurrentState = stateFactory.IdleState;
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        public override void Update()
        {
            base.Update();
        }

        private void ExecuteIfValidState(BaseState callingState, Action action)
        {
            if (callingState != CurrentState) return;

            action?.Invoke();
        }

        public void TryMovementState(BaseState callingState)
        {
            ExecuteIfValidState(callingState, () =>
            {
                if (playerInputController.Movement.IsPressed())
                {
                    SwitchState(stateFactory.MovementState);
                }
            });
        }

        public void TryIdleState(BaseState callingState)
        {
            ExecuteIfValidState(callingState, () =>
            {
                if(!playerInputController.Movement.IsPressed())
                {
                    SwitchState(stateFactory.IdleState);
                }
            });
        }

        public void TryJumpState(BaseState callingState)
        {
            ExecuteIfValidState(callingState, () =>
            {
                if (playerInputController.Jump.WasPressedThisFrame())
                {
                    SwitchState(stateFactory.JumpState);
                }
            });
        }

        public void TryAttackState(BaseState callingState)
        {
            ExecuteIfValidState(callingState, () =>
            {
                if (playerInputController.Attack.WasPressedThisFrame())
                {
                    SwitchState(stateFactory.AttackState);
                }
            });
        }

        public void TryInteractState(BaseState callingState)
        {
            ExecuteIfValidState(callingState, () =>
            {
                if (playerInputController.Interact.WasPressedThisFrame())
                {
                    SwitchState(stateFactory.InteractState);
                }
            });
        }

        private void SwitchState(BaseState state)
        {
            CurrentState.OnStateExit();
            CurrentState = state;
            CurrentState.OnStateEnter();
        }
    }
}

