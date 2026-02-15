using DamageSystem;
using Game.Input;
using UnityEngine;
using Zenject;

namespace Game.Characters
{
    public interface IPlayableCharacter : ICharacter
    {
        [Inject]  public PlayerInputController InputController { get; }
       public PlayableCharacterMovementController MovementController { get; }
        public PlayerStateMachine StateMachine { get; }
        public Transform Transform { get; }
        public IDamager Damager { get; }
        public void UpdateBaseAnimation();
    }
}

