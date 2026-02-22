using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.Core;
using Game.RankSystem;
using Game.TimeSystem;
using SceneLoadSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI
{
    public class WinnerUiController : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private CanvasGroup finalMessage;
        [SerializeField] private Image[] finalImageStars;
        [SerializeField] private RankManager RankManager;

        // [SerializeField] private AssetReference gameplaySceneReference;
        [Inject] private ISceneLoader sceneLoader;

        [Inject] private ITimerManager timerManager;

        public void ReloadGameplayScene()
        {
            sceneLoader.LoadSceneAsync(Scenes.GameplayScene);
        }

        private void OnEnable()
        {
            timerManager.ExtraTimeExpired += OnTimerEnd;
        }

        private void Start()
        {
            finalMessage.SetActiveEffect(false, 0);
        }


        private void OnDisable()
        {
            timerManager.ExtraTimeExpired -= OnTimerEnd;
        }

        private void OnTimerEnd()
        {
            float scoreValue = 0;
            DOTween.To(
                    () => scoreValue,
                    x => scoreValue = x,
                    RankManager.CurrentScore,
                    RankManager.CurrentRank * 0.5f)
                .SetEase(Ease.Linear).SetUpdate(true)
                .OnUpdate(() => { scoreText.SetText(scoreValue.ToString("0")); });
            SetUpStars();
        }

        public void ShowFinalMessage()
        {
            finalMessage.SetActiveEffect(true, 0.3f);
            finalMessage.transform.localScale = Vector3.one / 2;
            finalMessage.transform.DOScale(1, 0.3f).SetEase(Ease.OutBack).SetUpdate(true);
        }

        private async void SetUpStars()
        {
            var rank = RankManager.CurrentRank;
            finalImageStars.Select(x => x.enabled = false);
            for (var i = 0; i < rank; i++)
            {
                var image = finalImageStars[i];
                image.transform.localScale = Vector3.zero;
                image.gameObject.SetActive(true);
                image.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack).SetUpdate(true);
                image.transform.DORotate(Vector3.forward * 360, 0.5f, RotateMode.FastBeyond360).SetEase(Ease.OutBack)
                    .SetUpdate(true);
                await UniTask.Delay(500, true);
            }
        }
    }
}