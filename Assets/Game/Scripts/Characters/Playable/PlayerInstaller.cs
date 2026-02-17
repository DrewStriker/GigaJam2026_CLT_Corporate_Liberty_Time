using Game.Input;
using Zenject;

namespace Game.Characters
{
    public class PlayerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerInputController>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerController>().FromComponentsInHierarchy().AsSingle();
        }
    }
}