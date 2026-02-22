using System;
using DG.Tweening;
using Game.Core;
using Game.RankSystem;
using Game.TimeSystem;
using TMPro;
using UnityEngine;

namespace Game.UI
{
    public class UiHighScoreController : MonoBehaviour
    {
        public static string HighScoreKey = "HighScore";
        [SerializeField] private RankManager rankManager;
        [SerializeField] private TimerManager TimerManager;
        [SerializeField] private TMP_Text hgihScoreText;
        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.SetActiveEffect(false, 0);
        }

        private void OnEnable()
        {
            TimerManager.ExtraTimeExpired += OnExtraTimeExpired;
        }

        private void OnDisable()
        {
            TimerManager.ExtraTimeExpired -= OnExtraTimeExpired;
        }

        private void OnExtraTimeExpired()
        {
            _canvasGroup.SetActiveEffect(true, 1).SetDelay(1).SetUpdate(true);
            transform.GetChild(0).DOScale(1.5f, 1).SetDelay(1).SetLoops(-1).SetUpdate(true);
            SetHighScore((int)rankManager.CurrentScore);
        }

        public void SetHighScore(int highScore)
        {
            var currentHighScore = PlayerPrefs.GetInt(HighScoreKey);
            if (highScore > currentHighScore)
            {
                PlayerPrefs.SetInt(HighScoreKey, highScore);
                hgihScoreText.SetText(highScore.ToString());
            }
            else
            {
                hgihScoreText.SetText(currentHighScore.ToString());
            }
        }
    }
}