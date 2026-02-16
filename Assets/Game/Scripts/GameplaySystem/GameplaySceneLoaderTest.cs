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
        private IGameplayState gameplayState;
        private ISceneLoader sceneLoader;

        public void Initialize(IGameplayState gameplayState, ISceneLoader sceneLoader)
        {
            this.sceneLoader = sceneLoader;
            this.gameplayState = gameplayState;
            gameplayState.StateChanged += OnStateChanged;
        }

        public void Dispose()
        {
            gameplayState.StateChanged -= OnStateChanged;
        }

        private void OnStateChanged(StateType obj)
        {
            switch (obj)
            {
                // case StateType.Intro:
                //     OnGameplayIntro();
                //     break;
                case StateType.Combat:
                    OnGameplayCombat();
                    break;
                case StateType.End:
                    OnGameplayEnd();
                    break;
            }
        }

        private async Task OnGameplayCombat()
        {
            // await sceneLoader.LoadAsync(hudSceneReference);
        }

        private async Task OnGameplayEnd()
        {
            await sceneLoader.UnloadAsync(hudSceneReference);
            await sceneLoader.LoadAsync(gameOverReference);
        }
    }
}