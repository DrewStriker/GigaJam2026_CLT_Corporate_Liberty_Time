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
        private AsyncOperationHandle<SceneInstance> loadHandle;
        [Inject] private IGameplayState gameplayState;
        [SerializeField] private AssetReference hudSceneReference;

        private void Start()
        {
            gameplayState.StateChanged += OnStateChanged;
            gameplayState.SetState(StateType.Intro);
        }

        private void OnDestroy()
        {
            gameplayState.StateChanged -= OnStateChanged;
            Addressables.UnloadSceneAsync(loadHandle);
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
                    break;
            }
        }


        private void OnGameplayIntro()
        {
            gameplayState.SetState(StateType.Combat);
        }

        private async void OnGameplayCombat()
        {
            loadHandle = hudSceneReference.LoadSceneAsync(LoadSceneMode.Additive);
        }
    }
}