using Zenject;
namespace Game.RankSystem
{
    public class RankInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<RankManager>().FromComponentInHierarchy().AsSingle();
        }
    }
}
