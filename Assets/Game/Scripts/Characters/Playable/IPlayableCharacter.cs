using DamageSystem;
using Game.CollectableSystem;
using Game.Core.SimplePool.SfxPool;
using Game.Input;
using Game.InteractionSystem;
using Game.ItemSystem;
using Game.WeaponSystem;
using UnityEngine;
using Zenject;

namespace Game.Characters
{
    public interface IPlayableCharacter : ICharacter, ITarget, ICollector<ItemType>, ICollector<WeaponType>
    {
        [Inject] public PlayerInputController InputController { get; }
        [Inject] public SfxPoolFacade SfxPoolFacade { get; }
        public PlayableCharacterMovementController MovementController { get; }
        public PlayerStateMachine StateMachine { get; }
        public IDamager Damager { get; }
        public void UpdateBaseAnimation();
        public ICollectable<WeaponType> WeaponEquipped { get; }
    }
}