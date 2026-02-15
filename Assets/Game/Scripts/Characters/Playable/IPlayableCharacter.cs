using Game.Input;
using UnityEngine;

namespace Game.Characters
{
    public interface IPlayableCharacter : ICharacter
    {
        public PlayerInputController InputController { get; }
        public PlayableCharacterMovementController MovementController { get; }
        public PlayerStateMachine StateMachine { get; }
        public Transform Transform { get; }
    }
}

