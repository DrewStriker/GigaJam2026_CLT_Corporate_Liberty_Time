using DamageSystem;
using Game.CollectableSystem;
using Game.Input;
using Game.WeaponSystem;
using UnityEngine;
using Zenject;
using Game.ItemSystem;

namespace Game.Characters
{
    public interface IPlayableCharacter : ICharacter, ICollector<ItemType>, ICollector<WeaponType>
    {
        [Inject] public PlayerInputController InputController { get; }
        public PlayableCharacterMovementController MovementController { get; }
        public PlayerStateMachine StateMachine { get; }
        public Transform Transform { get; }
        public IDamager Damager { get; }
        public void UpdateBaseAnimation();
    }
}