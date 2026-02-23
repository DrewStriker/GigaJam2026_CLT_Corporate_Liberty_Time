using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using SceneLoadSystem;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Zenject;

namespace Game.UI
{
    [Serializable]
    public class CarousselItem
    {
        public Sprite Icon;
        public string Description;
    }

    public class TutorialCaroussel : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TMP_Text text;

        [SerializeField] private CarousselItem[] items;
        private int currentIndex = 0;

        [SerializeField] private Button nextButton;
        [SerializeField] private Button skipButton;
        [Inject] private ISceneLoader sceneLoader;

        private void Start()
        {
            currentIndex = 0;
            UpdateItem();
            nextButton.onClick.AddListener(Next);
            if (PlayerPrefs.GetFloat("PassedTutorial") == 0)
            {
                skipButton.interactable = false;
                skipButton.transform.localScale = Vector3.zero;
            }

            skipButton.onClick.RemoveListener(LoadGameplayScene);
            skipButton.onClick.AddListener(LoadGameplayScene);
        }

        private void OnDestroy()
        {
            skipButton.onClick.RemoveListener(LoadGameplayScene);
            nextButton.onClick.RemoveListener(Next);
        }


        private async void UpdateItem()
        {
            nextButton.interactable = false;
            image.transform.DOScale(1.5f, 0.3f).SetEase(Ease.InOutQuad);
            text.DOFade(0, 0.3f).SetEase(Ease.InOutQuad);
            image.DOFade(0, 0.3f).SetEase(Ease.InOutQuad);
            await UniTask.Delay(300);
            image.sprite = items[currentIndex].Icon;
            text.SetText(items[currentIndex].Description);
            image.transform.DOScale(1f, 0.3f).SetEase(Ease.InOutQuad);
            text.DOFade(1, 0.3f).SetEase(Ease.InOutQuad);
            image.DOFade(1, 0.3f).SetEase(Ease.InOutQuad);
            nextButton.interactable = true;
        }

        public void Next()
        {
            if (currentIndex == items.Length - 1)
            {
                PlayerPrefs.SetFloat("PassedTutorial", 1);
                skipButton.interactable = true;
                skipButton.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);
            }

            currentIndex = (currentIndex + 1) % items.Length;
            UpdateItem();
        }

        public void LoadGameplayScene()
        {
            sceneLoader.LoadSceneAsync(Scenes.GameplayScene);
        }
    }
}