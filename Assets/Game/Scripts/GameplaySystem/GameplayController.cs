using System.Threading.Tasks;
using Game.Characters;
using SceneLoadSystem;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game.Scripts.GameplaySystem
{
    public class GameplayController : MonoBehaviour
    {
        [SerializeField] private AssetReference hudSceneReference;
        [SerializeField] private AssetReference gameOverReference;
        [Inject] private IGameplayState gameplayState;
        [Inject] private IPlayableCharacter playableCharacter;
        [Inject] private ISceneLoader sceneLoader;

        private AsyncOperationHandle<SceneInstance> hudSceneHandle;
        private AsyncOperationHandle<SceneInstance> gameOverSceneHandle;


        private void Start()
        {
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
            print(state + "start");
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

        private void OnPlayerDeath()
        {
            gameplayState.SetState(StateType.End);
        }


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