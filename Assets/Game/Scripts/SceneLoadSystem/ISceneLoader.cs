using UnityEngine;

namespace SceneLoadSystem
{
    using UnityEngine.AddressableAssets;
    using UnityEngine.SceneManagement;
    using System.Threading.Tasks;

    public static class Scenes
    {
        public const string GameplayScene = "GameplayScene";
    }

    public interface ISceneLoader
    {
        AsyncOperation LoadSceneAsync(string sceneName, LoadSceneMode mode = LoadSceneMode.Single);
        Task LoadAsync(AssetReference scene, LoadSceneMode mode = LoadSceneMode.Additive);
        Task UnloadAsync(AssetReference scene);
    }
}