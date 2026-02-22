using System.Collections.Generic;
using DG.Tweening;
using Game.RankSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI
{
    public class UIScoreController : MonoBehaviour
    {
        [SerializeField] private Image starFill1;
        [SerializeField] private Image starFill2;
        [SerializeField] private Image starFill3;
        [SerializeField] private Image starFill4;
        [SerializeField] private Image starFill5;
        [SerializeField] private Image starFill6;
        [SerializeField] private Image rankProgressFill;
        [SerializeField] private TextMeshProUGUI scoreText;
        [Inject] private RankManager rankManager;
        private Queue<Image> stars;
        private Transform previousStarParent;

        private void Start()
        {
            Initialize();
            SubscriveEvents();
            previousStarParent = starFill1.transform.parent;
            previousStarParent.DOScale(1.5f, 0.5f).SetEase(Ease.OutBack);
        }

        private void Initialize()
        {
            DisableAllStars();
            stars = new Queue<Image>();
            stars.Enqueue(starFill1);
            stars.Enqueue(starFill2);
            stars.Enqueue(starFill3);
            stars.Enqueue(starFill4);
            stars.Enqueue(starFill5);
            stars.Enqueue(starFill6);
        }

        private void SubscriveEvents()
        {
            rankManager.OnScoreChanged += UpdateScore;
            rankManager.OnRankChanged += EnableNextStar;
        }

        private void UnsubscriveEvents()
        {
            rankManager.OnScoreChanged -= UpdateScore;
            rankManager.OnRankChanged -= EnableNextStar;
        }

        private void DisableAllStars()
        {
            starFill1.enabled = false;
            starFill2.enabled = false;
            starFill3.enabled = false;
            starFill4.enabled = false;
            starFill5.enabled = false;
            starFill6.enabled = false;
        }

        private void EnableNextStar()
        {
            previousStarParent.DOScale(1, 0.5f).SetEase(Ease.OutBack);

            if (stars.TryDequeue(out var fillImage))
            {
                fillImage.enabled = true;
                previousStarParent = fillImage.transform.parent;
            }

            previousStarParent.transform.DOScale(1.5f, 0.5f).SetEase(Ease.OutBack);
        }


        private void UpdateScore(float score)
        {
            scoreText.SetText(score.ToString("0"));
            rankProgressFill.fillAmount = rankManager.RankProgress;
        }

        private void OnDisable()
        {
            UnsubscriveEvents();
        }
    }
}