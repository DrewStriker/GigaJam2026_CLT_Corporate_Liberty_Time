using Zenject;
using UnityEngine;
namespace Game.Characters
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerController _playerController;
        public override void InstallBindings()
        {
            Container.Bind<IPlayableCharacter>().FromInstance(_playerController).AsSingle();
        }
    }
}
