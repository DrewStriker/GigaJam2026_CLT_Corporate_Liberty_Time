using SceneLoadSystem;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;
using System;
using System.Threading.Tasks;

namespace Game.GameplaySystem
{
    [Serializable]
    public class GameplaySceneLoaderTest
    {
        [SerializeField] private AssetReference hudSceneReference;
        [SerializeField] private AssetReference gameOverReference;
        [Inject] private IGameplayState gameplayState;
        [Inject] private ISceneLoader sceneLoader;

        private void OnGameplayIntro()
        {
            gameplayState.SetState(StateType.Combat);
        }

        private async Task OnGameplayCombat()
        {
            await sceneLoader.LoadAsync(hudSceneReference);
        }

        private async Task OnGameplayEnd()
        {
            await sceneLoader.UnloadAsync(hudSceneReference);
            await sceneLoader.LoadAsync(gameOverReference);
        }
    }
}