using Game.Core;
using SceneLoadSystem;
using UnityEngine;
using Zenject;

namespace Game.UI
{
    public class MenuSceneController : MonoBehaviour
    {
        [Inject] private ISceneLoader sceneLoader;


        public void QuitGame()
        {
            Application.Quit();
        }

        public void LoadTutorialScene()
        {
            sceneLoader.LoadSceneAsync(Scenes.TutorialScene);
        }

        public void LoadGameplayScene()
        {
            sceneLoader.LoadSceneAsync(Scenes.GameplayScene);
        }
    }
}