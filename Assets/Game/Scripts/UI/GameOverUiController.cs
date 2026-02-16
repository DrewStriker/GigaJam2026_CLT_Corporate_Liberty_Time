using SceneLoadSystem;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game.UI
{
    public class GameOverUiController : MonoBehaviour
    {
        [SerializeField] private AssetReference gameplaySceneReference;
        [Inject] private ISceneLoader sceneLoader;


        public void ReloadGameplayScene()
        {
            sceneLoader.LoadAsync(gameplaySceneReference, LoadSceneMode.Single);
        }
    }
}