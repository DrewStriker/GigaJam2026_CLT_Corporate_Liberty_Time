using Zenject;

namespace Game.GameplaySystem
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<GameplayStateController>().AsSingle();
            Container.Bind<IWinConditionEvent>().To<WinConditionEvent>().AsSingle();
        }
    }
}