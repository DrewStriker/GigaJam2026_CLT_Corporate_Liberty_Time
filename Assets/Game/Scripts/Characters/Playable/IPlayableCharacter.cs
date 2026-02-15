using Codice.Client.BaseCommands.Merge.Xml;
using DamageSystem;
using Game.CollectableSystem;
using Game.Input;
using Game.WeaponSystem;
using UnityEngine;
using Zenject;

namespace Game.Characters
{
    public interface IPlayableCharacter : ICharacter, ICollector<WeaponType>, ICollector<ItemType>
    {
        [Inject] public PlayerInputController InputController { get; }
        public PlayableCharacterMovementController MovementController { get; }
        public PlayerStateMachine StateMachine { get; }
        public Transform Transform { get; }
        public IDamager Damager { get; }
        public void UpdateBaseAnimation();
    }
}