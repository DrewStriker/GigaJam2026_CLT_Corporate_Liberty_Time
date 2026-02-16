using SceneLoadSystem;
using Zenject;

namespace SceneLoadSystem
{
    public class SceneLoadInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ISceneLoader>()
                .To<AddressableSceneLoader>()
                .AsSingle();
        }
    }
}