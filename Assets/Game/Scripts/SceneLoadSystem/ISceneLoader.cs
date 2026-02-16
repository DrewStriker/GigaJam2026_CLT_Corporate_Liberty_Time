namespace SceneLoadSystem
{
    using UnityEngine.AddressableAssets;
    using UnityEngine.SceneManagement;
    using System.Threading.Tasks;

    public interface ISceneLoader
    {
        Task LoadAsync(AssetReference scene, LoadSceneMode mode = LoadSceneMode.Additive);
        Task UnloadAsync(AssetReference scene);
    }
}