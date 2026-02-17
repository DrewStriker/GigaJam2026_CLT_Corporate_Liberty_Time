using System.Collections.Generic;
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

        private void Start()
        {
            Initialize();
            SubscriveEvents();
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
            if (stars.TryDequeue(out Image fillImage))
            {
                fillImage.enabled = true;
            }
        }

        private void UpdateScore(float score)
        {
            scoreText.text = score.ToString();
            rankProgressFill.fillAmount = rankManager.RankProgress;
        }

        private void OnDisable()
        {
            UnsubscriveEvents();
        }
    }
}
