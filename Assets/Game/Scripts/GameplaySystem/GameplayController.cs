using System;
using Game.Characters;
using Game.GameplaySystem;
using SceneLoadSystem;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using Zenject;

namespace Game.Scripts.GameplaySystem
{
    public class GameplayController : MonoBehaviour
    {
        [SerializeField] private GameplaySceneLoaderTest test;
        [Inject] private IGameplayState gameplayState;
        [Inject] private IPlayableCharacter playableCharacter;
        [Inject] private ISceneLoader sceneLoader;

        private AsyncOperationHandle<SceneInstance> hudSceneHandle;
        private AsyncOperationHandle<SceneInstance> gameOverSceneHandle;


        private void Start()
        {
            test.Initialize(gameplayState, sceneLoader);
            gameplayState.StateChanged += OnStateChanged;
            playableCharacter.Death += OnPlayerDeath;
            gameplayState.SetState(StateType.Intro);
        }

        private void OnDestroy()
        {
            playableCharacter.Death -= OnPlayerDeath;
            gameplayState.StateChanged -= OnStateChanged;
            Addressables.UnloadSceneAsync(gameOverSceneHandle);

            if (gameOverSceneHandle.IsValid())
                Addressables.UnloadSceneAsync(gameOverSceneHandle);
        }

        private void OnStateChanged(StateType state)
        {
            switch (state)
            {
                case StateType.Intro:
                    OnGameplayIntro();
                    break;
                case StateType.Combat:
                    OnGameplayCombat();
                    break;
                case StateType.End:
                    OnGameplayEnd();
                    break;
            }
        }

        private void OnGameplayEnd()
        {
        }

        private void OnGameplayCombat()
        {
        }

        private void OnGameplayIntro()
        {
        }

        private void OnPlayerDeath()
        {
            gameplayState.SetState(StateType.End);
        }
    }
}