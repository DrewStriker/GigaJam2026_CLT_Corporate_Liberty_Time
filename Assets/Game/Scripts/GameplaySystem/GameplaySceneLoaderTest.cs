using SceneLoadSystem;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;
using System;
using System.Threading.Tasks;
using Game.Core;

namespace Game.GameplaySystem
{
    [Serializable]
    public class GameplaySceneLoaderTest
    {
        // [SerializeField] private AssetReference hudSceneReference;
        // [SerializeField] private AssetReference gameOverReference;
        private IGameplayState gameplayState;
        private ISceneLoader sceneLoader;
        [SerializeField] private CanvasGroup gameOverCanvasGroup;

        [SerializeField] private CanvasGroup winCanvasGroup;

        public void Initialize(IGameplayState gameplayState, IWinConditionEvent winConditionEvent,
            ISceneLoader sceneLoader)
        {
            this.sceneLoader = sceneLoader;
            this.gameplayState = gameplayState;
            gameplayState.StateChanged += OnStateChanged;
            winConditionEvent.WinConditionMet += OnWinConditionMet;
            winCanvasGroup.SetActiveEffect(false, 0);
            gameOverCanvasGroup.SetActiveEffect(false, 0);
        }

        private void OnWinConditionMet(bool conditionMet)
        {
            if (conditionMet)
            {
                Debug.Log("You Won!");
                winCanvasGroup.SetActiveEffect(true);
            }
            else
            {
                gameOverCanvasGroup.SetActiveEffect(true);
                Debug.Log("You Lose!");
            }
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
            // await sceneLoader.UnloadAsync(hudSceneReference);
            // await sceneLoader.LoadAsync(gameOverReference);
        }
    }
}