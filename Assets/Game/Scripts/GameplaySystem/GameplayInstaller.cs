using System.ComponentModel;
using Zenject;

namespace Game.GameplaySystem
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<GameplayStateController>().AsSingle();
        }
    }
}