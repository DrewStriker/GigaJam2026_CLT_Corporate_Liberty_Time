using DamageSystem;
using Game.CollectableSystem;
using Game.Input;
using Game.InteractionSystem;
using Game.WeaponSystem;
using UnityEngine;
using Zenject;
using Game.ItemSystem;

namespace Game.Characters
{
    public interface IPlayableCharacter : ICharacter, ITarget, ICollector<ItemType>, ICollector<WeaponType>
    {
        [Inject] public PlayerInputController InputController { get; }
        public PlayableCharacterMovementController MovementController { get; }
        public PlayerStateMachine StateMachine { get; }
        public IDamager Damager { get; }
        public void UpdateBaseAnimation();
    }
}