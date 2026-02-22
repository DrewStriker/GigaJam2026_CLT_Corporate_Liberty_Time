using Cysharp.Threading.Tasks;
using UnityEngine;

namespace SceneLoadSystem
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UnityEngine.AddressableAssets;
    using UnityEngine.ResourceManagement.AsyncOperations;
    using UnityEngine.ResourceManagement.ResourceProviders;
    using UnityEngine.SceneManagement;

    public sealed class AddressableSceneLoader : ISceneLoader
    {
        private readonly Dictionary<AssetReference, AsyncOperationHandle<SceneInstance>> _handles = new();

        public AsyncOperation LoadSceneAsync(string sceneName, LoadSceneMode mode = LoadSceneMode.Single)
        {
            return SceneManager.LoadSceneAsync(sceneName, mode);
        }

        public async Task LoadAsync(AssetReference scene, LoadSceneMode mode = LoadSceneMode.Additive)
        {
            if (_handles.ContainsKey(scene)) return;

            var handle = scene.LoadSceneAsync(mode);
            _handles.Add(scene, handle);

            await handle.ToUniTask();
        }

        public async Task UnloadAsync(AssetReference scene)
        {
            if (!_handles.TryGetValue(scene, out var handle)) return;

            await Addressables.UnloadSceneAsync(handle).Task;
            _handles.Remove(scene);
            // await handle.ToUniTask();
        }
    }
}