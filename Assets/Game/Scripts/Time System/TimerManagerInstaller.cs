using Zenject;

namespace Game.TimeSystem
{
    public class TimerManagerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ITimerManager>().FromComponentInHierarchy().AsSingle();
        }
    }
}
