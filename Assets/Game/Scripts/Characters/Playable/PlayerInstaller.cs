using Game.Input;
using Zenject;
using UnityEngine;
namespace Game.Characters
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerController _playerController;
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerInputController>(). AsSingle();
            Container.Bind<IPlayableCharacter>().FromInstance(_playerController).AsSingle();
        } 
    }
}
