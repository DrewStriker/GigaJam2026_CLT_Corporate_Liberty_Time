using UnityEngine.SceneManagement;

namespace Game.Core
{
    public static class SceneLoader
    {
        public static void LoadMenuSceneAsync(LoadSceneMode mode = LoadSceneMode.Single)
        {
            SceneManager.LoadSceneAsync(0, mode);
        }

        public static void LoadGameplaySceneAsync(LoadSceneMode mode = LoadSceneMode.Single)
        {
            SceneManager.LoadSceneAsync(1, mode);
        }

        public static void LoadHudSceneAsync(LoadSceneMode mode = LoadSceneMode.Single)
        {
            SceneManager.LoadSceneAsync(2, mode);
        }
    }
}