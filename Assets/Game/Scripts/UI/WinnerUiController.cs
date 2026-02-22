using System;
using DG.Tweening;
using Game.Core;
using SceneLoadSystem;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game.UI
{
    public class WinnerUiController : MonoBehaviour
    {
        [SerializeField] private CanvasGroup finalMessage;

        // [SerializeField] private AssetReference gameplaySceneReference;
        [Inject] private ISceneLoader sceneLoader;


        public void ReloadGameplayScene()
        {
            sceneLoader.LoadSceneAsync(Scenes.GameplayScene);
        }

        private void Start()
        {
            finalMessage.SetActiveEffect(false, 0);
        }

        public void ShowFinalMessage()
        {
            finalMessage.SetActiveEffect(true, 0.3f);
            finalMessage.transform.localScale = Vector3.one / 2;
            finalMessage.transform.DOScale(1, 0.3f).SetEase(Ease.OutBack).SetUpdate(true);
        }
    }
}