using System;
using DG.Tweening;
using Game.Core;
using SceneLoadSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game
{
    [RequireComponent(typeof(CanvasGroup))]
    public class AutoStartupFadeOut : MonoBehaviour
    {
        private CanvasGroup canvasGroup;
        [Inject] private ISceneLoader sceneLoader;

        private void Awake()
        {
            // var duplicates = FindObjectsOfType<AutoStartupFadeOut>();
            //
            // if (duplicates.Length > 1)
            // {
            //     Destroy(gameObject);
            //     return;
            // }

            canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.interactable = false;
            canvasGroup.alpha = 1;
        }


        private void Start()
        {
            canvasGroup.SetActiveEffect(false, 0.5f);
            // SceneManager.activeSceneChanged += OnActiveSceneChanged;
            sceneLoader.OnSceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(string obj)
        {
            canvasGroup.SetActiveEffect(true, 0.5f);
        }

        private void OnDestroy()
        {
            sceneLoader.OnSceneLoaded -= OnSceneLoaded;

            // SceneManager.activeSceneChanged -= OnActiveSceneChanged;
        }

        private void OnActiveSceneChanged(Scene arg0, Scene arg1)
        {
            canvasGroup.alpha = 1;

            canvasGroup.SetActiveEffect(false, 0.5f).SetDelay(0.5f);
        }
    }
}